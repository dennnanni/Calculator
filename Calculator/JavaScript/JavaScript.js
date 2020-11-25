// Listener degli eventi
window.addEventListener('keydown', doKeyDown, true);

function doKeyDown(evt) {
    var pressedBtn;
    var shiftPressed = !!window.event.shiftKey;

    // Gestione dei vari codici da tastiera, sia del tastierino numerico che della tastiera
    switch (evt.keyCode) {

        case 96:
        case 48:
            pressedBtn = document.getElementById("btn0");
            break;

        case 97:
        case 49:
            pressedBtn = document.getElementById("btn1");
            break;

        case 98:
        case 50:
            pressedBtn = document.getElementById("btn2");
            break;

        case 99:
        case 51:
            pressedBtn = document.getElementById("btn3");
            break;

        case 100:
        case 52:
            pressedBtn = document.getElementById("btn4");
            break;

        case 53:
            if (shiftPressed) {
                pressedBtn = document.getElementById("btnPercentuale");
            }
            else
                pressedBtn = document.getElementById("btn5");

            break;

        case 101:
            pressedBtn = document.getElementById("btn5");
            break;

        case 102:
        case 54:
            pressedBtn = document.getElementById("btn6");
            break;

        case 103:
        case 55:
            pressedBtn = document.getElementById("btn7");
            break;

        case 104:
        case 56:
            pressedBtn = document.getElementById("btn8");
            break;

        case 105:
        case 57:
            pressedBtn = document.getElementById("btn9");
            break;

        case 106:
            pressedBtn = document.getElementById("btnPer");
            break;

        case 107:
            pressedBtn = document.getElementById("btnPiu");
            break;

        case 109:
            pressedBtn = document.getElementById("btnMeno");
            break;

        case 110:
            pressedBtn = document.getElementById("btnVirgola");
            break;

        case 111:
            pressedBtn = document.getElementById("btnDiviso");
            break;

        case 13: 
            pressedBtn = document.getElementById("btnUguale");
            break;

        case 8: // Su Firefox viene intercettato dai tasti di navigazione tra le pagine
            pressedBtn = document.getElementById("btnBack");
            break;

        default:
            return;

    }

    // Simulazione del click
    pressedBtn.click();
}