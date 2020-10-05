<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="ClassManager.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <title>پروفایل کاربر</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />

    </head>

    <body>
        <form id="MainForm" runat="server">
            <table  align ="center" cellpadding="2" class="registertable">
                <tr>
                    <td class="myRow1">
                        <asp:TextBox ID="txtUserName" runat="server" class="myinput" placeholder="نام کاربری یکتا انتخاب کنید" required = "true"></asp:TextBox>
                    </td>
                    <td class="myColumn">نام کاربری:
                    </td>
                </tr>
                <tr>
                    <td class="myRow1">
                        <asp:TextBox ID="txtPersonalCode" runat="server" class="myinput" placeholder="کد پرسنلی یا شماره دانشجویی" required = "true"></asp:TextBox>                                               
                    <td class="myColumn">کد پرسنلی:</td>
                </tr>
                <tr>
                    <td class="myRow1"> 
                        <asp:TextBox ID="txtFirstName" runat="server" class="myinput" placeholder="نام شما" required = "true"></asp:TextBox>
                    </td>  
                    <td class="myColumn">نام:</td>
                </tr>
                <tr>
                    <td class="myRow1"> 
                          <asp:TextBox ID="txtFamilyName" runat="server" class="myinput" placeholder="نام خانوادگی شما" required = "true"></asp:TextBox>                     
                    </td>
                    <td class="myColumn">نام خانوادگی:</td>
                </tr>
                <tr>
                    <td class="myRow1">    
                         <asp:TextBox ID="txtPhoneNo" runat="server" class="myinput" placeholder="شماره تماس با شما" required = "true"></asp:TextBox>                     
                   </td>
                    <td class="myColumn">شماره تماس:</td>
                </tr>
                <tr>
                    <td class="myRow1">  
                        <asp:TextBox ID="txtEmail" runat="server" class="myinput" placeholder="پست الکترونیک شما" required = "true"></asp:TextBox>                  
                    </td>
                    <td class="myColumn">ایمیل:</td>
                </tr>
                <tr>
                    <td class="myRow1"> 
                        <asp:TextBox ID="txtBirthDay" runat="server" class="myinput" placeholder="تاریخ تولد شما" required = "true"></asp:TextBox>                  
                     </td>
                    <td class="myColumn">تاریخ تولد:</td>
                </tr>
                <tr>
                    <td class="myRow1">
                        <asp:TextBox ID="txtPassword" runat="server" class="myinput" placeholder="پسورد ورود به سامانه" required = "true"></asp:TextBox>                  
                    </td>
                    <td class="myColumn">گذر واژه:</td>
                </tr>
                <tr>
                    <td class="myRow1">
                        <asp:TextBox ID="txtUserType" runat="server" class="myinput" placeholder="(ادمینADM)(دانشجو STD)(استادTCH)" required = "true" MaxLength="3" Enabled="False">STD</asp:TextBox>                  
                    </td>
                    <td class="myColumn">نوع کاربری</td>
                </tr>
                <tr>
                    <td class="myColumn" colspan="2">
                        <asp:Label ID="lblMessage" runat="server" class="myColumn"  Text="..."></asp:Label>
                        
                         <br />
                        <asp:Button ID="btnRegister" runat="server" class="Button" Text="ثبت نام" OnClick="btnRegister_Click" Width="200px" />
                        <asp:Button ID="btnSave" runat="server" class="Button" Text="ذخیره" OnClick="btnSave_Click" Visible="False"  Width="200px"/>
                        <br />
                        <asp:Button ID="btnLogin" runat="server" class="Button" Text="ورود" OnClick="btnLogin_Click" Visible="False"  Width="200px"/>
                       <br />
                         <br />
                         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:history.go(-1)">بازگشت</asp:HyperLink>       
                    </td>
                </tr>
                </table>
       </form>
    </body>
</html>
