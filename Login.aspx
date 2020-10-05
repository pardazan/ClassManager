<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClassManager.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>    
    <title>ورود به سامانه</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>

<body>
    <form id="MainForm" runat="server">
        <table class="logintable">
            <tr>
                <td class="myRow1">
                    <h1 class="pagetitle">مشخصات خود را وارد کنید </h1>
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtUserName" runat="server" class="myinput" placeholder="نام کاربری" ></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtPassword" runat="server" class="myinput" placeholder="پسورد ورود به سامانه" TextMode="Password" ></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="myRow1">
                    <asp:Label ID="lblMessage" runat="server" class="myColumn" Text="..."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Button ID="btnLogin" runat="server" class="Button" Width="40%" Text="ورود" OnClick="btnLogin_Click" />
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Button ID="btnAbout" runat="server" class="Button" Width="40%" Text="درباره" OnClick="btnAbout_Click"/>
                    &nbsp 
                   <asp:Button ID="btnRegister" runat="server" class="Button" Width="40%" Text="ثبت نام" OnClick="btnRegister_Click"/>
                </td>
            </tr>           
        </table>
    </form>
</body>

</html>
