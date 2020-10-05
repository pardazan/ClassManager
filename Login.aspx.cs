using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //در صورتی که قبلا وارد شده است به صفحه مربوط هدایت میشود(استاد دانشجو و ادمین هر کدام به صفحه مربوطه
            if (Session["UserType"] != null && Session["UserName"] != null)//برای لاگ اوت کافی است یکی از این سشن ها را خالی کنیم
            {
                if (Session["UserType"].ToString() == "ADM")
                    Response.Redirect("AdminPanel.aspx");//در صورتی که کاربر مدیر است به پنل مدیر هدایت میشود
                if (Session["UserType"].ToString() == "TCH")
                    Response.Redirect("TeacherPanel.aspx");//در صورتی که کاربر استاد است به پنل استاد هدایت میشود
                if (Session["UserType"].ToString() == "STD")
                    Response.Redirect("StudentPanel.aspx");//در صورتی که کاربر دانشجو است به پنل دانشجو هدایت میشود
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length < 4 || txtPassword.Text.Length < 4)
            {
                lblMessage.Text = "نام کاربری و گذر واژه خود را وارد کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            Users CurrentUser = new Users(txtUserName.Text);
            if (CurrentUser.Password == txtPassword.Text)
            {
                lblMessage.Text = "شما با موفقیت وارد شدید";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Session["UserName"] = CurrentUser.UserName;
                Session["FirstName"] = CurrentUser.FirstName;
                Session["LastName"] = CurrentUser.LastName;
                Session["UserType"] = CurrentUser.UserType;// اینجا متغیر سشن را مقدار دهی کردم و در هر صفحه میتوانم چک کنم
                if (CurrentUser.UserType == "ADM")
                    Response.Redirect("AdminPanel.aspx");//در صورتی که کاربر مدیر است به پنل مدیر هدایت میشود
                if (CurrentUser.UserType == "TCH")
                    Response.Redirect("TeacherPanel.aspx");//در صورتی که کاربر استاد است به پنل استاد هدایت میشود
                if (CurrentUser.UserType == "STD")
                    Response.Redirect("StudentPanel.aspx");//در صورتی که کاربر دانشجو است به پنل دانشجو هدایت میشود
            }
            else
            {
                lblMessage.Text = "نام کاربری یا گذرواژه درست نیست";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnAbout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AboutMe.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserProfile.aspx");
        }
    }
}