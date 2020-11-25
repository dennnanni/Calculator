using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculator
{
    /* TO DO:
     * provare ad usare js per pulsanti da tastiera
     * 
     * Note:
     * La combinazione 10+1=1/x==== funziona
     * 
     * DONE:
     * operazioni in successione
     * uguale in successione
     * aggiunta bottoni e allineamento
     * bottoni della memoria
     * gestione infiniti
     * verificare funzionamento uguali successione
     * verificare funzionamento operazioni successione
     */

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Se è il primo collegamento, crea i valori di sessione
            {
                Session["n1"] = null;
                Session["operatore"] = null;
                Session["n2"] = null;
                Session["result"] = null;
                Session["memory"] = null;
                Session["opBackup"] = null;
                Session["n2Backup"] = null;
            }
            
            // Se ci sono dei valori ancora non completati li visualizza
            if(Session["operatore"] == null)
            {
                if (Session["n1"] != null)
                    txtEspressione.Text = Session["n1"].ToString();
            }
            else
            {
                if (Session["n2"] != null)
                    txtEspressione.Text = Session["n2"].ToString();
            }
        }

        protected void Number_Click(object sender, EventArgs e)
        {
            string value = ((Button)sender).Text;
            string number = value;

            object n1 = Session["n1"], n2 = Session["n2"], op = Session["operatore"], result = Session["result"];

            // Se l'operatore non esiste ancora, significa che tutte le cifre selezionate vanno nel 
            // primo numero, se esiste vanno nel secondo
            if(op == null)
            {

                if (n1 == null || double.IsInfinity(double.Parse(n1.ToString())))
                {
                    n1 = "";
                    if(number == ",")
                        n1 = 0 + number.ToString();  // Concatena la cifra
                    else
                        n1 = n1.ToString() + number.ToString();  // Concatena la cifra

                    ErrorInterface(false);
                }
                else if (n1 != null) // Se il numero non è nullo, ci concateno la cifra
                {
                    n1 = n1.ToString() + number.ToString(); // Concatena la cifra
                }

                // Salva lo stato
                Session["n1"] = n1;

                //Visualizza
                txtEspressione.Text = n1.ToString();
            }
            else if (op != null)
            {
                // Se il secondo numero non esiste ancora, lo zero davanti è ininfluente
                if (n2 == null || double.IsInfinity(double.Parse(n2.ToString())))
                {
                    n2 = "";
                    n2 = n2.ToString() + number.ToString(); // Concatena la cifra
                }
                else if (n2 != null) // Se il numero non è nullo, ci concateno la cifra
                {
                    n2 = n2.ToString() + number.ToString(); // Concatena la cifra
                }

                // Salva lo stato
                Session["n2"] = n2;

                // Visualizza
                txtEspressione.Text = n2.ToString();
            }


        }

        protected void Op_Click(object sender, EventArgs e)
        {
            string op = ((Button)sender).Text;
            double result = 0, value = double.NaN;
            string name = "";
            object n1 = Session["n1"], n2 = Session["n2"], savedOp = Session["operatore"], savedResult = Session["result"];

            if (savedOp == null) 
            {
                if (n1 != null)
                {
                    value = double.Parse(n1.ToString());
                    name = "n1";
                }  
            }
            else
            {
                if(n2 != null)
                {
                    value = double.Parse(n2.ToString());
                    name = "n2";
                }
            }


            if (op == "√")
            {
                // Gestisce la radice pari di numeri negativi
                if(value < 0)
                {
                    ErrorInterface(true);
                    txtEspressione.Text = "Input non valido!";
                    Session["n1"] = null;
                    Session["operatore"] = null;
                    Session["n2"] = null;
                    return;
                }

                if(!double.IsNaN(value))
                    // Esegue l'operazione di radice sul numero inserito
                    result = Math.Sqrt(value);
            }
            else if(op == "x²")
            {
                if(!double.IsNaN(value))
                    // Esegue l'operazione di elevamento a potenza sul numero inserito
                    result = value * value;
            }
            else if(op == "1/x")
            {
                string message;
                result = Operations(1, value, "/", out message);

                if (double.IsNaN(result)) // Se l'operazione non è andata a buon fine
                {
                    txtEspressione.Text = message;
                    ErrorInterface(true);
                    Session["n1"] = null;
                    Session["n2"] = null;
                    Session["operatore"] = null;
                    return;
                }
            }
            else
            {
                // Se Il risultato è inizializzato e n1 no significa che l'operazione è successiva
                if (savedResult != null && n1 == null && n2 != null)
                {
                    string message;
                    n1 = savedResult;
                    result = Operations(double.Parse(n1.ToString()), double.Parse(n2.ToString()), savedOp.ToString(), out message);
                    
                    if (double.IsNaN(result))
                    {
                        txtEspressione.Text = message;
                        ErrorInterface(true);
                        Session["n1"] = null;
                        Session["n2"] = null;
                        Session["operatore"] = null;
                        return;
                    }

                    Session["n1"] = result;
                    Session["result"] = result;
                    Session["n2"] = null;
                    Session["operatore"] = op;
                    txtEspressione.Text = result.ToString();
                }
                else if(n1 != null && n2 != null) // In caso fosse la prima operazione
                {
                    string message;
                    result = Operations(double.Parse(n1.ToString()), double.Parse(n2.ToString()), savedOp.ToString(), out message);
              
                    if (double.IsNaN(result))
                    {
                        txtEspressione.Text = message;
                        ErrorInterface(true);
                        Session["n1"] = null;
                        Session["n2"] = null;
                        Session["operatore"] = null;
                        return;
                    }

                    Session["n1"] = result;
                    Session["result"] = result;
                    Session["n2"] = null;
                    Session["operatore"] = op;
                    txtEspressione.Text = result.ToString();
                }
                else // Se non ci sono operazioni da fare 
                {
                    Session["operatore"] = op;
                    txtEspressione.Text = "";
                    
                }

                return;
            }
            
            // Mostra il risultato
            txtEspressione.Text = result.ToString();
            Session[name] = result;
        }

        protected void btnUguale_Click(object sender, EventArgs e)
        {
            // L'operando di backup e il numero due di backup serve se ho gia fatto un operazione e continuo a 
            // premere uguale. L'operazione viene reiterata sul risultato.
            if(Session["operatore"] == null && Session["opBackup"] != null && Session["n2Backup"] != null)
            {
                double n1 = double.Parse(Session["n1"].ToString());
                double n2 = double.Parse(Session["n2Backup"].ToString());
                string op = Session["opBackup"].ToString();
                double result = Operations(n1, n2, op, out string message);

                txtEspressione.Text = result.ToString();
                Session["n1"] = result;
                Session["result"] = result;
            }

            if (Session["n1"] == null)
            {
                // Se il numero è il secondo ed è stato scelto il -, va restituito il corrispondente negativo
                if (Session["operatore"].ToString() == "-")
                    txtEspressione.Text = "-" + Session["n2"].ToString();
                else
                    txtEspressione.Text = Session["n2"].ToString();
            }
            else if (Session["n2"] == null)
            {
                txtEspressione.Text = Session["n1"].ToString();
            }
            else
            {
                double n1 = double.Parse(Session["n1"].ToString());
                double n2 = double.Parse(Session["n2"].ToString());
                string op = Session["operatore"].ToString();

                double result = Operations(n1, n2, op, out string message);

                if (double.IsNaN(result)) // Se c'è stato un problema
                {
                    txtEspressione.Text = message;
                    ErrorInterface(true);
                    Session["n1"] = null;
                    Session["n2"] = null;
                    Session["operatore"] = null;
                    return;
                }

                Session["n1"] = result;
                Session["result"] = result;
                Session["opBackup"] = op;
                Session["n2Backup"] = n2;
                Session["n2"] = null;
                Session["operatore"] = null;
                txtEspressione.Text = result.ToString();
            }
        }

        private double Operations(double n1, double n2, string op, out string message)
        {
            message = "ok";
            switch (op)
            {
                case "+": // Addizione
                    if (double.IsPositiveInfinity(n1) && double.IsNegativeInfinity(n2) ||
                        double.IsNegativeInfinity(n1) && double.IsPositiveInfinity(n2))
                    {
                        message = "Indeterminato";
                        return double.NaN;
                    }
                    return n1 + n2;

                case "-": // Sottrazione
                    if (double.IsPositiveInfinity(n1) && double.IsPositiveInfinity(n2) || 
                        double.IsNegativeInfinity(n1) && double.IsNegativeInfinity(n2))
                    {
                        message = "Indeterminato";
                        return double.NaN;
                    }
                    return n1 - n2;

                case "x": // Moltiplicazione
                    if (double.IsPositiveInfinity(n1) && double.IsNegativeInfinity(n2) ||
                        double.IsNegativeInfinity(n1) && double.IsPositiveInfinity(n2))
                    {
                        message = "Indeterminato";
                        return double.NaN;
                    }
                    return n1 * n2;

                case "/": // Divisione

                    if(double.IsInfinity(n1) && double.IsInfinity(n2))
                    {
                        message = "Indeterminato";
                        return double.NaN;
                    }

                    if (n2 != 0 && n1 != 0)
                    {
                        // Operazione di divisione
                        return n1 / n2;
                    }
                    else if (n2 == 0 && n1 == 0) // Indeterminato 
                    {
                        message = "Indeterminato";
                        return double.NaN;
                    }
                    else
                    {
                        message = "Divisione per zero";
                        return double.NaN; // Divisione per zero 
                    }

                default:
                    message = "errore";
                    return double.NaN;
            }
        }

        protected void btnCE_Click(object sender, EventArgs e)
        {
            object op = Session["operatore"];

            // Elimina il valore in base a quello che usando
            if(op == null)
            {
                Session["n1"] = null;
                Session["result"] = null;
            }
            else
            {
                Session["n2"] = null;
            }

            txtEspressione.Text = "";

            // Se i pulsanti degli operatori sono disabilitati allora devo riabilitarli
            if (!btnPiu.Enabled)
            {
                ErrorInterface(false);
            }
        }

        protected void btnC_Click(object sender, EventArgs e)
        {
            // Resetta tutti i valori 
            Session["n1"] = null;
            Session["n2"] = null;
            Session["operatore"] = null;
            Session["result"] = null;
            Session["opBackup"] = null;
            Session["n2Backup"] = null;
            txtEspressione.Text = "";
            
            // Se i pulsanti degli operatori sono disabilitati allora devo riabilitarli
            if (!btnPiu.Enabled)
            {
                ErrorInterface(false);
            }
        }

        protected void btnMR_Click(object sender, EventArgs e)
        {
            double memory = double.Parse(Session["memory"].ToString());

            if(Session["operatore"] == null)
            {
                Session["n1"] = memory;
                txtEspressione.Text = memory.ToString();
            }
            else
            {
                Session["n2"] = memory;
                txtEspressione.Text = memory.ToString();
            }
        }

        protected void btnMS_Click(object sender, EventArgs e)
        {
            double value = 0;

            if (txtEspressione.Text.Length != 0)
                value = double.Parse(txtEspressione.Text);

            Session["memory"] = value;

            btnMR.Enabled = true;
            btnMC.Enabled = true;
            btnMS.Enabled = false;
            
        }

        protected void btnMC_Click(object sender, EventArgs e)
        {
            Session["memory"] = null;

            btnMR.Enabled = false;
            btnMC.Enabled = false;
            btnMS.Enabled = true;
        }

        protected void btnMemoryOperations_Click(object sender, EventArgs e)
        {
            // Se non esiste una memoria salvata non continua
            if (Session["memory"] == null)
                return;

            // Ricava l'operazione scelta 
            string text = ((Button)sender).Text;
            
            // Il numero da sommare lo ricava direttamente dalla textbox
            double textboxValue = double.Parse(txtEspressione.Text);
            double mValue = double.Parse(Session["memory"].ToString());

            if (text.Contains("-"))
            {
                mValue = mValue - textboxValue;
            }
            else if (text.Contains("+"))
            {
                mValue = mValue - textboxValue;
            }

            Session["memory"] = mValue;
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            string value = "";
            
            // Decide su quale valore deve operare
            if (Session["operatore"] == null)
                value = "n1";
            else
                value = "n2";

            if(Session[value] != null && Session[value].ToString().Length > 0)
            {
                // Elimina l'ultimo carattere della stringa
                Session[value] = Session[value].ToString().Remove(Session[value].ToString().Length - 1);

                // Se sono ancora presenti delle cifre nel numero allora visualizza le rimanenti
                if (Session[value].ToString().Length > 0)
                    txtEspressione.Text = string.Format("{0:########,####}", Session[value].ToString());
                else // Se la stringa è vuota allora non mostra niente e reimposta i valori
                {
                    txtEspressione.Text = "";
                    Session[value] = null;
                    if (value == "n1")
                        Session["result"] = null;
                }
            }

            
        }

        protected void btnNegato_Click(object sender, EventArgs e)
        {
            string index = "";

            // Decide su quale valore deve operare
            if (Session["operatore"] == null)
                index = "n1";
            else
                index = "n2";

            if (Session[index] == null)
                return;

            // Ricava il valore attuale
            double value = double.Parse(Session[index].ToString());

            // Lo nega e lo reinserisce nella variabile, mostrandolo a video
            value = -value;
            Session[index] = value;
            txtEspressione.Text = Session[index].ToString();
        }

        protected void btnPercentuale_Click(object sender, EventArgs e)
        {
            object n1 = Session["n1"], n2 = Session["n2"], op = Session["operatore"];

            // Se si sta operando sul primo operando l'operazione di percentuale non è possibile
            if (n1 != null && n2 == null)
            {
                Session["result"] =  (double)(double.Parse(n1.ToString()) / 100);
                Session["n1"] = Session["result"];
                txtEspressione.Text = Session["result"].ToString();
                return;
            }

            if (op == null || n2 == null)
            {
                txtEspressione.Text = "";
                return;
            }
            

            double value1 = double.Parse(n1.ToString());
            double value2 = double.Parse(n2.ToString());

            if (op.ToString() == "+" || op.ToString() == "-")
                // Esegue l'operazione di percento
                Session["n2"] = value1 * value2 / 100;
            else
                Session["n2"] = value2 / 100;
            txtEspressione.Text = Session["n2"].ToString();
        }

        private void ErrorInterface(bool value)
        {
            value = !value;
            btnPer.Enabled = value;
            btnPiu.Enabled = value;
            btnRadice.Enabled = value;
            btnMeno.Enabled = value;
            btnDiviso.Enabled = value;
            btnUguale.Enabled = value;
            btnPercentuale.Enabled = value;
            btnQuadrato.Enabled = value;
            btnFratto.Enabled = value;
            btnNegato.Enabled = value;
        }
    }
}