using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class ExamManager : System.Web.UI.Page
    {
        Exam CurrentExam;//برای اینکه با رفرش شدن صفحه مقدار خالی نشود و آزمون جاری مشخص بماند
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserType"] == null)
                Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود                         
            if ((string)Session["UserType"] != "TCH")
                Response.Redirect("Login.aspx");//کاربر وارد شده اما استاد نیست به صفحه لاگین هدایت میشود 

            String strParameter = Request.Params["ex"];//در صورتی که صفحه با پارامتر باز شده باشد مشخصات آزمون ویرایش میشود
            if (strParameter == null)
            {
                Response.Redirect("/Login.aspx");//صفحه بدون پارامتر باز شده و بایستی به صفحه لاگین هدایت شود
            }
            else
            {
                if (!Page.IsPostBack)
                    Session["CurrentExam"] = null; //برای اینکه در اولین ورود اطلاعات حتمن خوانده شود
                int ExamId = -1;//شناسه آزمون  با مقدار منفی برای چک کردن شناسه وارد شده بعنوان پارامتر
                try
                {
                    ExamId = int.Parse(strParameter);//اگر با موفقیت انجام شود پارامتر وارده عدد است و بعنوان شناسه آزمون استفاده میشود
                }
                catch { }
                //اینجا اگر شناسه آزمون مثبت باشد یعنی استاد میخواهد آزمونی را ویرایش کند
                // اگر شناسه آزمون صفر باشد یعنی استاد میخواهد آزمون جدید ایجاد کند
                //اگر منفی باشد عدد پارامتر معتبر نبوده و به صفحه لاگین هدایت میشود
                if (ExamId < 0)
                {
                    Response.Redirect("/Login.aspx");//صفحه با پارامتر غلط باز شده و بایستی به صفحه لاگین هدایت شود
                }
                txtExamId.Enabled = false;
                txtTeacherId.Enabled = false;
                if (ExamId == 0)
                {
                    if (Session["CurrentExam"] == null)
                    {
                        CurrentExam = new Exam(0);
                        CurrentExam.TeacherId = Session["UserName"].ToString();
                        Session["CurrentExam"] = CurrentExam;
                    }
                    else
                    {
                        CurrentExam = (Exam)Session["CurrentExam"];
                    }
                    
                    txtExamFile.Visible = false;
                    btnUpload.Visible = false;//اینها را مخفی میکنم تا بعد از ثبت آزمون نشان دهم و استاد بتواند فایل را بفرستد
                    FileUpload1.Visible = false;
                    btnRegister.Visible =true;
                    btnSave.Visible = false;
                    txtTeacherId.Text = (string)Session["UserName"];
                }

                if (ExamId > 0)
                {
                    btnAddStudents.Visible = true;

                    if (Session["CurrentExam"] == null)//بار اول که صفحه باز میشود اطلاعات آزمون واکشی میگردد اما در بارهای بعدی اطلاعات ویرایش شده توسط کاربر معتبر است و تکست باکس ها نباید تغییر کنند
                    {
                        CurrentExam = new Exam(ExamId);
                        txtExamId.Text = CurrentExam.ExamId.ToString();
                        txtTeacherId.Text = CurrentExam.TeacherId;
                        txtExamTitle.Text = CurrentExam.ExamTitle.ToString();
                        txtExamDate.Text = CurrentExam.ExamDate.ToString();
                        txtExamLength.Text = CurrentExam.ExamLength.ToString();
                        txtExamFile.Text = CurrentExam.ExamFile;
                        Session["CurrentExam"] = CurrentExam;
                        btnSave.Visible = true;//
                        btnRegister.Visible = false;
                    }
                    else
                    {
                        CurrentExam = (Exam)Session["CurrentExam"];
                    }
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentExam.TeacherId = txtTeacherId.Text;
                CurrentExam.ExamTitle = txtExamTitle.Text;
                CurrentExam.ExamDate = DateTime.Parse(Request.Form[txtExamDate.UniqueID]);
                // CurrentExam.ExamDate = DateTime.Parse(txtExamDate.Text);
                CurrentExam.ExamLength = int.Parse(txtExamLength.Text);
                CurrentExam.ExamFile = txtExamFile.Text;               

                if (CurrentExam.Insert())
                {
                    lblMessage.Text = "ثبت آزمون انجام شد فایل پرسشنامه را آپلود کنید";
                    txtExamFile.Visible = true;
                    btnUpload.Visible = true;
                    FileUpload1.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    btnRegister.Visible = false;
                    txtExamId.Text = CurrentExam.ExamId.ToString();
                    Session["CurrentExam"] = CurrentExam;//تغییرات در سشن اعمال شود
                    btnAddStudents.Visible = true;
                    Response.Redirect("~/ExamManager.aspx?ex=" + CurrentExam.ExamId.ToString());
                }
                else
                {
                    lblMessage.Text = "ثبت آزمون انجام نشد مشخصات را بازبینی کنید";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch
            {
                lblMessage.Text = "ثبت آزمون انجام نشد مشخصات را بازبینی کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CurrentExam.TeacherId = txtTeacherId.Text;
            CurrentExam.ExamTitle = txtExamTitle.Text;
            CurrentExam.ExamDate = DateTime.Parse(txtExamDate.Text);
            CurrentExam.ExamLength = int.Parse(txtExamLength.Text);
            CurrentExam.ExamFile = txtExamFile.Text;
            txtExamFile.Visible = true;
            btnUpload.Visible = true;
            FileUpload1.Visible = true;

            if (CurrentExam.Update())
            {
                lblMessage.Text = "اطلاعات آزمون به روز شد.";
                lblMessage.ForeColor = System.Drawing.Color.Green;                
                Session["CurrentExam"] = null;//برای اینکه در بار بعدی اطلاعات آزمون خوانده شود              
                btnAddStudents.Visible = true;
                btnSave.Visible = false;
                btnRegister.Visible = false;
            }
            else
            {
                lblMessage.Text = "به روز زسانی انجام نشد مشخصات را بازبینی کنید";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        /*
        private void Submit1_ServerClick(object sender, System.EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = CurrentExam.ExamId.ToString();
                
                string SaveLocation = Server.MapPath("Uploaded") + "\\" + fn;
                Session["UplodedFile"] = SaveLocation;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    lblMessage.Text = "فایل با موفقيت ارسال شد";
                    txtExamFile.Text = System.IO.Path.GetFileName(File1.PostedFile.FileName);                                        
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "آپلود موفقیت آمیز نبود";
                    //Response.Write("Error: " + ex.Message);
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to return a generic error message. 
                }
            }
            else
            {
                Response.Write("لطفا ابتدا فایلی را برای آپلود انتخاب کنید");
            }
        }
*/
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {
                String FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);//از اسم فایل اصلی استفاده کردم تا نوع آن مشخص باشد
                FileName = CurrentExam.ExamId.ToString() + FileName;//شناسه آزمون را پشت نام فایل گذاشتم تا اگر اسم فایل دو آزمون  یکی بود اشتباه نشود
                String FolderName = Server.MapPath("Uploaded") + "\\" + CurrentExam.TeacherId;

                if (!System.IO.Directory.Exists(FolderName))
                {
                    System.IO.Directory.CreateDirectory(FolderName);
                }
                // Session["UplodedFile"] = SaveLocation;
                try
                {
                    FileUpload1.PostedFile.SaveAs(FolderName + "\\" + FileName);
                    lblMessage.Text = "فایل با موفقيت ارسال شد";
                    txtExamFile.Text = FileName;                    
                    CurrentExam.ExamFile = txtExamFile.Text;
                    CurrentExam.Update();//ذخیره نام فایل در آزمون                    
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "آپلود موفقیت آمیز نبود";
                    //Response.Write("Error: " + ex.Message);
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to return a generic error message. 
                }
            }
            else
            {
                lblMessage.Text = "لطفا ابتدا فایلی را برای آپلود انتخاب کنید";
            }
        }

        protected void btnAddStudents_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ExamStudentList.aspx?ex=" + CurrentExam.ExamId.ToString());
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeacherPanel.aspx");
        }
    }
}