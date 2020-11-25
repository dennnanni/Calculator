<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calculator.Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <script type="text/javascript" src="JavaScript/JavaScript.js"></script>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form runat="server">
        <div >
                <asp:TextBox ID="txtEspressione" runat="server" Height="73px" Width="326px" Font-Size="21" ReadOnly="True"></asp:TextBox>
            <br />
                <asp:Button ID="btnMR" runat="server" Text="MR" Width="63" Height="30px" Enabled="false" OnClick="btnMR_Click"/>
                <asp:Button ID="btnMC" runat="server" Text="MC" Width="63" Height="30px" Enabled="false" OnClick="btnMC_Click"/>
                <asp:Button ID="btnMS" runat="server" Text="MS" Width="64px" Font-Size="" Height="30px" OnClick="btnMS_Click"/>
                <asp:Button ID="btnMemoriaPiu" runat="server" Text="M+" Width="63px" Font-Size="" Height="30px" OnClick="btnMemoryOperations_Click"/>
                <asp:Button ID="btnMemoriaMeno" runat="server" Text="M-" Width="63px" Font-Size="" Height="30px" OnClick="btnMemoryOperations_Click"/>
            <br />
                <asp:Button ID="btnPercentuale" runat="server" Text="%" Height="80px" Width="80px" Font-Size="30" OnClick="btnPercentuale_Click" />
                <asp:Button ID="btnCE" runat="server" Text="CE" Height="80px" Width="80px" Font-Size="30" OnClick="btnCE_Click"  />
                <asp:Button ID="btnC" runat="server" Text="C" Height="80px" Width="80px" Font-Size="30" OnClick="btnC_Click"  />
                <asp:Button ID="btnBack" runat="server" Text="←" Height="80" Width="80" Font-Size="30" OnClick="btnBack_Click"  />
            <br />
                <asp:Button ID="btnFratto" runat="server" Text="1/x" Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click"/>
                <asp:Button ID="btnQuadrato" runat="server" Text="x²" Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click"/>
                <asp:Button ID="btnRadice" runat="server" Text="√" Height="80px" Width="80px" Font-Size="30"  OnClick="Op_Click"/>
                <asp:Button ID="btnDiviso" runat="server" Text="/" Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click" />
            <br />
                <asp:Button ID="btn7" runat="server" Text="7" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btn8" runat="server" Text="8" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btn9" runat="server" Text="9" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btnPer" runat="server" Text="x"  Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click"/>
            <br />
                <asp:Button ID="btn4" runat="server" Text="4" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btn5" runat="server" Text="5" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btn6" runat="server" Text="6" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btnMeno" runat="server" Text="-" Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click"/>
            <br />
                <asp:Button ID="btn1" runat="server" Text="1" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btn2" runat="server" Text="2" Height="80px" Width="80px" Font-Size="30"  OnClick="Number_Click"/>
                <asp:Button ID="btn3" runat="server" Text="3" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btnPiu" runat="server" Text="+" Height="80px" Width="80px" Font-Size="30" OnClick="Op_Click"/>
            <br />
                <asp:Button ID="btnNegato" runat="server" Text="+/-" Height="80px" Width="80px" Font-Size="30" OnClick="btnNegato_Click"  />
                <asp:Button ID="btn0" runat="server" Text="0" Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click"/>
                <asp:Button ID="btnVirgola" runat="server" Text="," Height="80px" Width="80px" Font-Size="30" OnClick="Number_Click" />
                <asp:Button ID="btnUguale" runat="server" Text="=" Height="80px" Width="80px" Font-Size="30" OnClick="btnUguale_Click" />
            <br />
        </div>
    </form>
</body>
</html>
