<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamManager.aspx.cs" Inherits="ClassManager.ExamManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>مدیریت آزمونها و تکالیف</title>
    <link href="MyStyleSheet.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="CSS/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            //var buffer = txtExamDate.Text;
            //http://www.dynarch.com/jscal/      Calendar Documentation
            $("#<%=txtExamDate.ClientID %>").dynDateTime({
                showsTime: true,
                weekNumbers: false,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: true,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
            // txtExamDate.Text = buffer;
        });
    </script>
    <script>
        function Submit1_onclick() {

        }

        function File1_onclick() {

        }
    </script>

</head>

<body>
    <form id="MainForm" runat="server">
        <table align="center" cellpadding="2" class="registertable">
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtTeacherId" runat="server" class="myinput" placeholder="نام کاربری استاد" required="true"></asp:TextBox>
                </td>
                <td class="myColumn">نام کاربری استاد:
                </td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtExamId" runat="server" class="myinput" placeholder="شناسه آزمون (اتوماتیک)" required="true"></asp:TextBox>
                </td>
                <td class="myColumn">شناسه آزمون:</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtExamTitle" runat="server" class="myinput" placeholder="عنوان آزمون" required="true"></asp:TextBox>
                </td>
                <td class="myColumn">عنوان آزمون یا تکلیف:</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtExamDate" runat="server" class="myinput" Style="direction: ltr" placeholder="کلیک کنید" required="true"></asp:TextBox>
                </td>
                <td class="myColumn">تاریخ و ساعت:</td>
            </tr>
            <tr>
                <td class="myRow1">
                    <asp:TextBox ID="txtExamLength" runat="server" class="myinput" placeholder="مدت آزمون" required="true"></asp:TextBox>
                </td>
                <td class="myColumn">مدت آزمون(دقیقه):</td>
            </tr>
            <tr class="myRow1">
                <td class="myRow1" colspan="2">
                    <asp:Label ID="lblMessage" runat="server" class="myColumn" Text=""></asp:Label>
                    <br />
                    <asp:Button ID="btnRegister" runat="server" class="Button" Text="ثبت آزمون" OnClick="btnRegister_Click" Width="258px" />
                    <br />
                    <asp:TextBox ID="txtExamFile" runat="server" class="myinput" placeholder="فایل آزمون را آپلود کنید" required="true" ReadOnly="true"></asp:TextBox>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="56%" />
                    <br />
                    <asp:Button ID="btnUpload" runat="server" class="Button" Text="آپلود (بارگذاری) فایل آزمون" OnClick="btnUpload_Click" Height="46px" Width="92%" />
                    <br />
                    <asp:Button ID="btnSave" runat="server" class="Button" Text="ذخیره" OnClick="btnSave_Click" Visible="False" Width="258px" />
                    <br />
                    <asp:Button ID="btnAddStudents" runat="server" class="Button" Text="دانشجویان آزمون" Width="80%" Visible="false" OnClick="btnAddStudents_Click" />
                    <br />
                </td>
            </tr>
            <tr class="myRow1">
                <td class="myRow1" colspan="2">
                    <asp:Button ID="btnExit" runat="server" class="Button" Text="خروج" Visible="true" Width="258px" OnClick="btnExit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
