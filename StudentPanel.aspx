<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPanel.aspx.cs" Inherits="ClassManager.StudentPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students Panel</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="pagetitle">
            <asp:Label ID="lblMessage" runat="server" Text="..."></asp:Label>
            <asp:DataGrid ID="dgvAllUsers" runat="server" class="userstable"></asp:DataGrid>
            <br />
            <asp:Button ID="btnProfile" runat="server" class="Button" Width="200px" Text="ویرایش پروفایل" OnClick="btnProfile_Click" />
               &nbsp
           <asp:Button ID="btnMessenger" runat="server" class="Button" Width="200px" Text="پیام رسان" OnClick="btnMessenger_Click"  />
           <br />
            <br />
            <asp:Button ID="btnExit" CssClass="Button" runat="server" Text="خروج" Width="200px" OnClick="btnExit_Click"/>            
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://shahreza.ac.ir/" Target="_blank">سایت دانشگاه</asp:HyperLink>
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://185.88.152.19/" Target="_blank">برگزاری کلاس</asp:HyperLink>
            <br />
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="javascript:history.go(-1)">بازگشت</asp:HyperLink>
            <br />
        </div>
    </form>
</body>
</html>
