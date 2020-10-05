using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassManager
{
    public partial class UserProfile : System.Web.UI.Page
    {
        Users CurrentUser;//برای اینکه با رفرش شدن صفحه مقدار خالی نشود و کاربر جاری مشخص بماند در یک متغیر سشن ذخیره میشود
        protected void Page_Load(object sender, EventArgs e)
        {
            String strParameter = Request.Params["un"];//در صورتی که صفحه با پارامتر باز شده باشد مشخصات کاربر فعال و قابل ذخیره میشود
            if (strParameter == null)//صفحه بدون پارامتر باز شده و بایستی ثبت نام انجام گیرد
            {
                btnSave.Visible = false;
                btnRegister.Visible = true;
                this.Title = "ثبت نام کاربر جدید";
                txtUserType.Text = "STD";
                txtUserType.Enabled = false;
            }
            else
            {
                this.Title = "ویرایش پروفایل کاربر";
                if ((string)Session["UserType"] == null)
                    Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود                         
                
                if (Session["CurrentUser"] == null)//بار اول که صفحه باز میشود اطلاعات کاربر واکشی میگردد اما در بارهای بعدی اطلاعات ویرایش شده توسط کاربر معتبر است و تکست باکس ها نباید تغییر کنند
                {
                    txtUserName.Enabled = false;
                    if ((string)Session["UserType"] == "ADM")
                    {
                        txtUserType.Enabled = true;//امکان تغییر دسترسی کاربر فقط برای ادمین وجود دارد
                    }
                    else
                    {
                        strParameter = Session["UserName"].ToString();//اگر کاربر مدیر نباشد فقط میتواند اطلاعات خودش را ویرایش کند  
                        txtUserType.Enabled = false;//امکان تغییر دسترسی کاربر فقط برای ادمین وجود دارد                      
                    }
                    CurrentUser = new Users(strParameter);
                    txtUserName.Text = CurrentUser.UserName;
                    txtPersonalCode.Text = CurrentUser.PersonalCode.ToString();
                    txtFirstName.Text = CurrentUser.FirstName;
                    txtFamilyName.Text = CurrentUser.LastName;
                    txtPhoneNo.Text = CurrentUser.PhoneNumber;
                    txtEmail.Text = CurrentUser.Email;
                    txtBirthDay.Text = CurrentUser.BrithDate;
                    txtPassword.Text = CurrentUser.Password;
                    txtUserType.Text = CurrentUser.UserType;
                    Session["CurrentUser"] = CurrentUser;
                    btnSave.Visible = true;//
                    btnRegister.Visible = false;
                }
                else
                {
                    CurrentUser = (Users) Session["CurrentUser"];
                }
            }
           // btnReturn.Visible = true;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Users CurrentUser = new Users(txtUserName.Text);
            CurrentUser.PersonalCode = int.Parse(txtPersonalCode.Text);
            CurrentUser.FirstName = txtFirstName.Text;
            CurrentUser.LastName = txtFamilyName.Text;
            CurrentUser.PhoneNumber = txtPhoneNo.Text;
            CurrentUser.Email = txtEmail.Text;
            CurrentUser.BrithDate = txtBirthDay.Text;
            CurrentUser.Password = txtPassword.Text;
            //همه ثبت نام کنندگان به عنوان دانشجو ثبت میشوند و ادمین بعدا میتواند استادان را انتخاب کند
            CurrentUser.UserType = "STD";

            //چک کردن اینکه نام کاربری قبلا استفاده نشده باشد
            if (CurrentUser.UserExists())
            {
                lblMessage.Text = "ثبت نام انجام نشد نام کاربری مورد نظر شما قبلا استفاده شده نام کاربری دیگری انتخاب کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }


            if (CurrentUser.Insert())
            {
                lblMessage.Text = "ثبت نام انجام شد";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                btnRegister.Visible = false;
                btnLogin.Visible = true;
            }
            else
            {
                lblMessage.Text = "ثبت نام انجام نشد مشخصات را بازبینی کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           // CurrentUser = new Users(txtUserName.Text);
            CurrentUser.PersonalCode = int.Parse(txtPersonalCode.Text);
            CurrentUser.FirstName = txtFirstName.Text;
            CurrentUser.LastName = txtFamilyName.Text;
            CurrentUser.PhoneNumber = txtPhoneNo.Text;
            CurrentUser.Email = txtEmail.Text;
            CurrentUser.BrithDate = txtBirthDay.Text;
            CurrentUser.Password = txtPassword.Text;
            CurrentUser.UserType = txtUserType.Text;


            if (CurrentUser.Update())
            {
                lblMessage.Text = "پروفایل به روز شد";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                btnSave.Visible = false;
                btnLogin.Visible = true;
                Session["UserType"] = null;//برای اینکه در بار بعدی اطلاعات کاربر خوانده شود
            }
            else
            {
                lblMessage.Text = "به روز زسانی انجام نشد مشخصات را بازبینی کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["UserType"] = null;//پایان دادن به سشن کاربر و خروج از حساب
            Response.Redirect("Login.aspx");
        }
    
    }
}