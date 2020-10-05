using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class ExamStudentList : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            String strParameter = Request.Params["ex"];//در صورتی که صفحه با پارامتر باز شده باشد مشخصات آزمون ویرایش میشود
           // strParameter = "1";//پس از مرحله آزمایشی پاک شود
            int ExamId = -1;//شناسه آزمون  با مقدار منفی برای چک کردن شناسه وارد شده بعنوان پارامتر

            if (strParameter == null)
            {
                Response.Redirect("/Login.aspx");//صفحه بدون پارامتر باز شده و بایستی به صفحه لاگین هدایت شود
            }
            else
            {
                 try
                {
                    ExamId = int.Parse(strParameter);//اگر با موفقیت انجام شود پارامتر وارده عدد است و بعنوان شناسه آزمون استفاده میشود
                }
                catch { }

            }
            Session["ExamId"] = ExamId;//برای اینکه در این سشن در دسترس باشد
            
            if (!Page.IsPostBack)
            {
                MySqlDB db = new MySqlDB();
                DataTable dtStudents = db.ReadFromBank("Select UserName, FirstName, LastName FROM Users WHERE UserType ='STD'");
                if (dtStudents.Rows.Count > 0)
                {
                   // dtStudents.Columns.Add("Selected", typeof(Boolean));//افزودن فیلد قابل انتخاب برای دانشجو
                    lblMessage.Text += "<BR> لیست آزمون های ثبت شده شما <BR> برای دیدن اطلاعات هر آزمون روی شناسه آن آزمون کلیک کنید";                  
                    dgvAllStudents.DataSource = dtStudents;
                    dgvAllStudents.DataBind(); //دقت کنم که بعد از بستن گرید به جدول باید آنرا باند کنم
                    for (int i = 0; i < dtStudents.Rows.Count; i++)//ویریش اطلاعات خام جدول و تبدیل پاسخنامه آزمون  به هایپرلینک برای دانلود توسط استاد
                    {
                        DataTable dtExamSt = db.ReadFromBank("Select StudentId, ExamId, Score, ResultFile FROM ExamResults WHERE StudentId ='" + dtStudents.Rows[i]["UserName"].ToString() + "' AND ExamID=" + ExamId.ToString());
                        if (dtExamSt.Rows.Count > 0)
                        {
                            ((CheckBox)dgvAllStudents.Items[i].Cells[5].FindControl("Selected")).Checked = true;
                            ((TextBox) dgvAllStudents.Items[i].Cells[0].FindControl("Score")).Text=dtExamSt.Rows[0]["Score"].ToString();                                                        
                            ((HyperLink)dgvAllStudents.Items[i].Cells[1].FindControl("ResultFile")).NavigateUrl = "/Uploaded/" + dtStudents.Rows[i]["UserName"].ToString() +"/" + dtExamSt.Rows[0]["ResultFile"].ToString();
                        }
                    }
                }
                else
                {
                    lblMessage.Text += "<BR> هیچ دانشجویی در بانک اطلاعات ثبت نشده است";
                }
            }
        }
        protected void btnRegisterAll_Click(object sender, EventArgs e)
        {
            MySqlDB db = new MySqlDB();           
            String strSQL = "DELETE FROM ExamResults WHERE ExamId = " + Session["ExamId"].ToString();//پاک کردن اطلاعات موجود برای حذف انتخاب نشده ها
             db.RunQuery(strSQL);        
            for (int i = 0; i < dgvAllStudents.Items.Count; i++)
            {
                String StudentID = dgvAllStudents.Items[i].Cells[2].Text;
                int StudentScore = 0;
                try
                {
                    StudentScore = int.Parse(((TextBox)dgvAllStudents.Items[i].Cells[0].FindControl("Score")).Text);//اگر نمره ثبت نشده خطا نگیریم
                }
                catch { }
                if (((CheckBox)dgvAllStudents.Items[i].Cells[5].FindControl("Selected")).Checked)
                {
                    strSQL = "INSERT INTO ExamResults (ExamId, StudentId, Score)";
                    strSQL += " VALUES ";
                    strSQL += "(" + Session["ExamId"].ToString() + ",'" + StudentID + "',"  + StudentScore.ToString() +  ")";
                    db.RunQuery(strSQL);
                }
            
            }
            btnRegisterAll.Visible = false;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeacherPanel.aspx");          
        }
    }
}