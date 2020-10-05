<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="ClassManager.AdminPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Panel</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="pagetitle">
            <asp:Label ID="lblMessage" runat="server" Text="..."></asp:Label>
            <asp:DataGrid ID="dgvAllUsers" runat="server" class="userstable"></asp:DataGrid>
            <asp:Button ID="btnMessenger" runat="server" class="Button" Width="200px" Text="پیام رسان" OnClick="btnMessenger_Click" />
            <br /><br />
            <asp:Button ID="btnExit" CssClass="Button" runat="server" Text="خروج" Width="200px" OnClick="btnExit_Click" />
        </div>
    </form>
</body>
</html>
