using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassManager
{
    public partial class InitDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ایجاد بانک اطلاعات و دادن پیام مناسب
            String strMessage = "Initializing Database ...<BR>";
            MySqlDB myDB = new MySqlDB();
            if (myDB.CreateIfNotExists())
                strMessage += "Database Created....<BR>";
            else
            {
                if (myDB.LastError.Length > 0)
                    strMessage += "Error Creating Database " + myDB.LastError + ".<BR>";
                else
                    strMessage += "Database exits.<BR>";
            }
            
            //ایجاد جدول کاربران و دادن پیام مناسب
            if (myDB.CreateUsersTable())
                strMessage += "Users Table Created.<BR>";
            else
                strMessage += myDB.LastError + ".<BR>";

            //ایجاد کاربر مدیر در جدول کاربران
            Users testUser = new Users("Admin");           
            testUser.Password = "235689";
            testUser.FirstName = "مدیر";
            testUser.LastName = "سایت";
            testUser.PersonalCode = 100000;
            testUser.UserType = "ADM";
            if (testUser.Insert())
                strMessage += "Admin User Added.<BR>";
            else
                strMessage += "Error adding Admin User.<BR>";

            //ایجاد جدول امتحانات  و تکالیف و دادن پیام مناسب
            if (myDB.CreateExamsTable())
                strMessage += "Exams Table Created.<BR>";
            else
                strMessage += myDB.LastError + ".<BR>";

            //ایجاد جدول شرکت کنندگان در امتحانات  و تکالیف و دادن پیام مناسب
            if (myDB.CreateExamResultTable())
                strMessage += "ExamResults Table Created.<BR>";
            else
                strMessage += myDB.LastError + ".<BR>";

            //ایجاد جدول شرکت کنندگان در امتحانات  و تکالیف و دادن پیام مناسب
            if (myDB.CreateMessagesTable())
                strMessage += "MessagesTable Table Created.<BR>";
            else
                strMessage += myDB.LastError + ".<BR>";

            lblMessage.Text = strMessage;
        }
    }
}