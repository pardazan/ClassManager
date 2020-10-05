using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class Messenger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserType"] == null)
                Response.Redirect("Login.aspx");//کاربر وارد نشده است به صفحه لاگین هدایت میشو         
                lblMessage.Text = Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + " خوش آمدید ";
                lblMessage.Text += "<BR> لیست کاربران ثبت شده <BR> برای ارسال پیام به هر کاربر روی نام کاربری وی کلیک کنید";
                MySqlDB myDB = new MySqlDB();
                DataTable dtUsers = myDB.ReadFromBank("Select * From  Users");
                if (dtUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUsers.Rows.Count; i++)//ویریش اطلاعات خام جدول و تبدیل نام گاربری به هایپرلینک برای هدایت به صفحه ویرایش اطلاعات
                    {
                        dtUsers.Rows[i]["UserName"] = "<a href=\"MessengerPanel.aspx?un=" + dtUsers.Rows[i]["UserName"].ToString() + "\">" + dtUsers.Rows[i]["UserName"].ToString() + "</a>";
                    }
                    dgvAllUsers.DataSource = dtUsers;
                    dgvAllUsers.DataBind(); //دقت کنم که بعد از بستن گرید به جدول باید آنرا باند کنم
                }
        }



        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}