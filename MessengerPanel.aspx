<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessengerPanel.aspx.cs" Inherits="ClassManager.MessengerPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>پیام رسان</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" cellpadding="2" class="registertable">
            <tr>
                <td class="myRow1">
                    <asp:Button ID="btnExit" runat="server" class="Button" Text="خروج" Visible="true" Width="258px" OnClick="btnExit_Click" />
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="lblMessage" runat="server" Text="..."></asp:Label>
                    <br />
                    <asp:Button ID="btnRefresh" runat="server" class="Button" Text="بازخوانی" Visible="true" Width="47%" OnClick="btnRefresh_Click" />
                    <asp:DataGrid ID="dgvMesseges" runat="server" class="userstable" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="MessageBody" HeaderText="پیام ها" ReadOnly="True"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="#E3EAEB" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtMessageToSend" class="myinput" runat="server" Enabled="true" Text="" placeholder="پیام خود را حداکثر 245 حرف اینجا بنویسید" Width="80%" MaxLength="255" TextMode="MultiLine" />
                    <asp:Button ID="btnSend" runat="server" class="Button" Text=">>" Visible="true" Width="9%" OnClick="btnSend_Click" />

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
