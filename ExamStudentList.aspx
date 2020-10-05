<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamStudentList.aspx.cs" Inherits="ClassManager.ExamStudentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MyStyleSheet.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <table align="center" cellpadding="2" class="registertable" >
            <tr>
                <td class="myRow1">
                    <asp:Button ID="btnRegisterAll" runat="server" class="Button" Text="ذخیره"  Width="258px" OnClick="btnRegisterAll_Click" />
                    <br />              
                    <asp:Button ID="btnExit" runat="server" class="Button" Text="خروج" Visible="true" Width="258px" OnClick="btnExit_Click" />         
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="lblMessage" runat="server" Text="..."></asp:Label>            
         <asp:DataGrid ID="dgvAllStudents" runat="server" class="userstable" AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="نمره">
                                <ItemTemplate>
                                    <asp:TextBox ID="Score" runat="server" Enabled="true" Text ="0" MaxLength="3"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="پاسخنامه">
                                <ItemTemplate>
                                    <asp:HyperLink ID="ResultFile" runat="server">مشاهده</asp:HyperLink>                                   
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="UserName" HeaderText="نام کاربری" ReadOnly="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FirstName" HeaderText="نام" ReadOnly="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LastName" HeaderText="نام خانوادگی" ReadOnly="True"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="انتخاب">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Selected" runat="server" Enabled="true" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
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
