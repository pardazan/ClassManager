using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class StudentPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserType"] == null)
                Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود
            if ((string)Session["UserType"] == "STD")
            {
                //کاربر وارد شده و دانشجو است
                lblMessage.Text = "دانشجوی گرامی " + Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + " خوش آمدید ";
                MySqlDB myDB = new MySqlDB();
                String strSQL = "SELECT Exams.ExamId, Exams.ExamTitle, Exams.TeacherId, Exams.ExamDate,ExamResults.ResultFile ,ExamResults.Score as نمره ";
                strSQL += "FROM Exams ";
                strSQL += "INNER JOIN ExamResults ON ExamResults.ExamId = Exams.ExamId ";
                strSQL += "WHERE ExamResults.StudentID ='" + Session["UserName"].ToString() + "'";
                DataTable dtExams = myDB.ReadFromBank(strSQL);
                if (dtExams.Rows.Count > 0)
                {
                    lblMessage.Text += "<BR> لیست آزمون ها یا تکالیف ثبت شده شما <BR> برای دیدن اطلاعات هر آزمون روی شناسه آن آزمون کلیک کنید";
                    for (int i = 0; i < dtExams.Rows.Count; i++)//ویریش اطلاعات خام جدول و تبدیل عنوان آزمون  به هایپرلینک برای هدایت به صفحه ویرایش اطلاعات
                    {
                        dtExams.Rows[i]["ExamTitle"] = "<a href=\"DoExam.aspx?ex=" + dtExams.Rows[i]["ExamId"].ToString() + "\">" + dtExams.Rows[i]["ExamTitle"].ToString() + "</a>";
                    }
                    dgvAllUsers.DataSource = dtExams;
                    dgvAllUsers.DataBind(); //دقت کنم که بعد از بستن گرید به جدول باید آنرا باند کنم
                }
                else
                {
                    lblMessage.Text += "<BR> شما در حال حاضر هیچ آزمون یا تکلیفی ندارید";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");//کاربر دانشجو نیست به صفحه لاگین هدایت میشود
            }
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserProfile.aspx?un=" + Session["UserName"].ToString());
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session["UserType"] = null;//پایان دادن به سشن کاربر و خروج از حساب
            Response.Redirect("Login.aspx");
        }
        protected void btnMessenger_Click(object sender, EventArgs e)
        {
            Response.Redirect("Messenger.aspx");
        }
    }
}