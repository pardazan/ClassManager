<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messenger.aspx.cs" Inherits="ClassManager.Messenger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>پیام رسان</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" cellpadding="2" class="registertable" >
            <tr>
                <td class="myRow1">
                    <asp:Button ID="btnExit" runat="server" class="Button" Text="خروج" Visible="true" Width="258px" OnClick="btnExit_Click" />         
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="lblMessage" runat="server" Text="..."></asp:Label>            
         <asp:DataGrid ID="dgvAllUsers" runat="server" class="userstable" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>                            
                            <asp:BoundColumn DataField="UserName" HeaderText="نام کاربری" ReadOnly="True"></asp:BoundColumn>                          
                            <asp:BoundColumn DataField="LastName" HeaderText="نام خانوادگی" ReadOnly="True"></asp:BoundColumn>
                              <asp:BoundColumn DataField="FirstName" HeaderText="نام" ReadOnly="True"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>                          
            </tr>            
        </table>
    </form>
</body>
</html>
