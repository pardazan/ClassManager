<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoExam.aspx.cs" Inherits="ClassManager.DoExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Do Exam</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <table align="center" cellpadding="2" class="registertable">
            <tr>
                <td class="myRow1">
                    <asp:Label ID="txtTeacherId" runat="server" class="myinput" placeholder="نام کاربری استاد" required="true"></asp:Label>
                </td>
                <td class="myColumn">نام استاد:
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="txtExamTitle" runat="server" class="myinput" placeholder="عنوان آزمون" required="true"></asp:Label>
                </td>
                <td class="myColumn">عنوان آزمون یا تکلیف:</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="txtExamDate" runat="server" class="myinput" Style="direction: ltr" placeholder="کلیک کنید" required="true"></asp:Label>
                </td>
                <td class="myColumn">تاریخ و ساعت:</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:Label ID="txtExamLength" runat="server" class="myinput" placeholder="مدت آزمون" required="true"></asp:Label>
                </td>
                <td class="myColumn">مدت آزمون(دقیقه):</td>
            </tr>
            <tr>
                <td class="myRow1">                    
                    <asp:Button ID="btnDownload" runat="server" class="Button" Text="شروع آزمون" OnClick="btnDownload_Click" Height="46px" Width="192px" />
                </td>
                <td class="myColumn">&nbsp;پرسشنامه آزمون یا تکلیف:</td>
            </tr>
            <tr class="myRow1">
                <td class="myRow1" colspan="2">
                    <asp:Label ID="lblMessage" runat="server" class="myColumn" Text=""></asp:Label>
                    <br />
                    </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtResultstart" runat="server" class="myinput" placeholder="هنوز پرسشنامه دانلود نشده" required="true" ReadOnly="true"></asp:TextBox>                    
                </td>
                <td class="myColumn">آغاز آزمون توسط دانشجو :</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtResultFile" runat="server" class="myinput" placeholder="فایل پاسخنامه خود را آپلود کنید" required="true" ReadOnly="true"></asp:TextBox>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" />
                    <br />
                    <asp:Button ID="btnUpload" runat="server" class="Button" Text="آپلود (بارگذاری)" OnClick="btnUpload_Click" Height="46px" Width="192px" />

                </td>
                <td class="myColumn">پاسخنامه :</td>
            </tr>
            
            <tr>
                <td class="myRow1" colspan="2">
                    <asp:Button ID="btnExit" runat="server" class="Button" Text="خروج" Visible="true" Width="258px" OnClick="btnExit_Click" />

                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
