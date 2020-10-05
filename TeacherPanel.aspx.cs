using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class TeacherPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserType"] == null)
                Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود
            if ((string)Session["UserType"] == "TCH")
            {
                //کاربر وارد شده و استاد است
                lblMessage.Text ="استاد گرامی " + Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + " خوش آمدید ";
                MySqlDB myDB = new MySqlDB();
                DataTable dtUsers = myDB.ReadFromBank("Select * From  Exams WHERE TeacherId ='" + Session["UserName"].ToString() + "'");
                if (dtUsers.Rows.Count > 0)
                {
                    lblMessage.Text += "<BR> لیست آزمون های ثبت شده شما <BR> برای دیدن اطلاعات هر آزمون روی شناسه آن آزمون کلیک کنید";
                    for (int i = 0; i < dtUsers.Rows.Count; i++)//ویریش اطلاعات خام جدول و تبدیل عنوان آزمون  به هایپرلینک برای هدایت به صفحه ویرایش اطلاعات
                    {
                        dtUsers.Rows[i]["ExamTitle"] = "<a href=\"ExamManager.aspx?ex=" + dtUsers.Rows[i]["ExamId"].ToString() + "\">" + dtUsers.Rows[i]["ExamTitle"].ToString() + "</a>";
                    }
                    dgvAllUsers.DataSource = dtUsers;
                    dgvAllUsers.DataBind(); //دقت کنم که بعد از بستن گرید به جدول باید آنرا باند کنم
                }
                else
                {
                    lblMessage.Text += "<BR> شما هیچ آزمونی ثبت نکرده اید";
                }
            }
            else
            {
                //کاربر وارد شده ولی استاد نیست
                lblMessage.Text = Session["UserName"].ToString() + " شما اجازه استفاده از این صفحه را ندارید ";
            }

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ExamManager.aspx?ex=0");
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