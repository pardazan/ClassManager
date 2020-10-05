using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassManager
{
    public partial class DoExam : System.Web.UI.Page
    {
        Exam CurrentExam;//آزمون جاری
        ExamResult CurrentResult;//نتیجه جاری

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["UserType"] == null)
                Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود                         
            if ((string)Session["UserType"] != "STD")
                Response.Redirect("Login.aspx");//کاربر وارد شده اما دانشجو نیست به صفحه لاگین هدایت میشود 

            String strParameter = Request.Params["ex"];//در صورتی که صفحه با پارامتر باز شده باشد مشخصات آزمون ویرایش میشود
            if (strParameter == null)
            {
                Response.Redirect("/Login.aspx");//صفحه بدون پارامتر باز شده و بایستی به صفحه لاگین هدایت شود
            }
            else
            {
                int ExamId = -1;//شناسه آزمون  با مقدار منفی برای چک کردن شناسه وارد شده بعنوان پارامتر
                try
                {
                    ExamId = int.Parse(strParameter);//اگر با موفقیت انجام شود پارامتر وارده عدد است و بعنوان شناسه آزمون استفاده میشود
                }
                catch { }
                if (ExamId <= 0)
                {
                    Response.Redirect("/Login.aspx");//صفحه با پارامتر غلط باز شده و بایستی به صفحه لاگین هدایت شود
                }
                txtTeacherId.Enabled = false;

                if (ExamId > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["CurrentExam"] = null; //برای اینکه در اولین ورود اطلاعات حتمن خوانده شود
                        Session["CurrentResult"] = null;
                    }

                    if (Session["CurrentExam"] == null)//بار اول که صفحه باز میشود اطلاعات آزمون واکشی میگردد اما در بارهای بعدی اطلاعات ویرایش شده توسط کاربر معتبر است و تکست باکس ها نباید تغییر کنند
                    {
                        CurrentExam = new Exam(ExamId);
                        txtTeacherId.Text = CurrentExam.TeacherId;
                        txtExamTitle.Text = CurrentExam.ExamTitle.ToString();
                        txtExamDate.Text = CurrentExam.ExamDate.ToString();
                        txtExamLength.Text = CurrentExam.ExamLength.ToString();
                        Session["CurrentExam"] = CurrentExam;
                        CurrentResult = new ExamResult(ExamId,Session["UserName"].ToString());                        
                        txtResultFile.Text = CurrentResult.ResultFile;
                        if(CurrentResult.ExamStart >new DateTime())//ببینیم زمان شروع برای دانشجو ثبت شده یا نه
                        {
                            txtResultstart.Text = CurrentResult.ExamStart.ToString();
                        }                       
                        Session["CurrentResult"] = CurrentResult;
                    }
                    else
                    {
                        CurrentExam = (Exam)Session["CurrentExam"];
                        CurrentResult = (ExamResult)Session["CurrentResult"];
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {
                if (CurrentExam.ExamDate > DateTime.Now)
                {
                    //آزمون هنوز شروع نشده است
                    lblMessage.Text = "دانشجوی گرامی زمان آغاز آزمون یا دریافت تکلیف هنوز نرسیده است";
                    return;//عملیات آپلود لغو میشود
                }
                if (CurrentExam.ExamDate.AddMinutes(CurrentExam.ExamLength) < DateTime.Now)
                {
                    //آزمون تمام شده است
                    lblMessage.Text = "دانشجوی گرامی زمان آزمون یا دریافت تکلیف پایان یافته است";
                    return;//عملیات دانلود لغو میشود
                }
                String FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileName = CurrentExam.ExamId.ToString() + FileName; //شناسه آزمون را در ابتدای نام فایل اضافه کردم که مشخص باشد هر پاسخ مربوط به کدام آزمون است
                String FolderName = Server.MapPath("Uploaded") + "\\" + Session["UserName"].ToString();//فولدر مخصوص این دانشجو که پاسخ نامه های او در آن ذخیره میشود
                if (!System.IO.Directory.Exists(FolderName))
                {
                    System.IO.Directory.CreateDirectory(FolderName);
                }
                // Session["UplodedFile"] = SaveLocation;
                try
                {
                    FileUpload1.PostedFile.SaveAs(FolderName + "\\" + FileName);
                    lblMessage.Text = "فایل پاسخ نامه با موفقيت ارسال شد";
                    txtResultFile.Text = FileName;
                    CurrentResult.ResultFile = FileName;
                    CurrentResult.Update();
                    btnUpload.Visible = false;
                    FileUpload1.Visible = false;
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
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (CurrentExam.ExamDate > DateTime.Now)
            {
                //آزمون هنوز شروع نشده است
                lblMessage.Text = "دانشجوی گرامی زمان آغاز آزمون یا دریافت تکلیف هنوز نرسیده است";
                return;//عملیات آپلود لغو میشود
            }
            if(CurrentExam.ExamDate.AddMinutes(CurrentExam.ExamLength) < DateTime.Now)
            {
                //آزمون تمام شده است
                lblMessage.Text = "دانشجوی گرامی زمان آزمون یا دریافت تکلیف پایان یافته است";
                return;//عملیات دانلود لغو میشود
            }

            lblMessage.Text = "دانشجوی گرامی از زمانی که فایل آزمون را دانلود میکنید آزمون شما آغاز شده و در مهلت مقرر میتوانید پاسخنامه خود را آپلود کنید لازم نیست آنلاین بمانید";
            lblMessage.Text += "<A href = \"" + "/Uploaded/" + CurrentExam.TeacherId + "/" + CurrentExam.ExamFile + "\">دانلود پرسشنامه </>";
            btnDownload.Visible = false;
            if (CurrentResult.ExamStart == new DateTime())
            {
                CurrentResult.ExamStart = DateTime.Now;//اینجا دانشجو فایل پرسشنامه آزمون را دانلود کرده و زمان او ثبت میشود
                CurrentResult.Update();                
                Session["CurrentResult"] = CurrentResult;
            }
            else
            {
                //دانشجو قبلا فایل را دانلود کرده و زمان او ثبت شده است
            }
            if (CurrentResult.ExamStart > new DateTime())
                txtResultstart.Text = CurrentResult.ExamStart.ToString();
            else
                txtResultstart.Text = "";
            // Response.Redirect("~/Uploaded/" + CurrentExam.TeacherId+"/"  + CurrentExam.ExamFile);

        }

    }
}