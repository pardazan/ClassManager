using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassManager
{
    public partial class MessengerPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)//بار اول که صفحه باز میشود اطلاعات کاربر واکشی میگردد اما در بارهای بعدی اطلاعات ویرایش شده توسط کاربر معتبر است و تکست باکس ها نباید تغییر کنند
            {
                Response.Redirect("Login.aspx");//کاربر اصلا وارد نشده است به صفحه لاگین هدایت میشود
            }
            String strParameter = Request.Params["un"];//در صورتی که صفحه با پارامتر باز شده باشد مشخصات کاربر فعال و قابل ذخیره میشود
            if (strParameter == null)//صفحه بدون پارامتر باز شده و بایستی دوباره از صفحه پیام رسان وارد شود
            {
                Response.Redirect("Messenger.aspx");
            }
            else
            {
                Session["MessageRec"] = strParameter;
                GetMessages();
                lblMessage.Attributes.Add("dir", "rtl");
                dgvMesseges.Attributes.Add("dir", "rtl");
            }

        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Messenger.aspx");
        }

        void GetMessages()
        {
            MySqlDB myDB = new MySqlDB();
            String strSQL = "SELECT * FROM Messages ";
            strSQL += "WHERE (MessageFrom ='" + Session["UserName"].ToString() + "'";
            strSQL += " AND MessageTo ='" + Session["MessageRec"].ToString() + "') OR (";
            strSQL += "MessageFrom ='" + Session["MessageRec"].ToString() + "'";
            strSQL += " AND MessageTo ='" + Session["UserName"].ToString() + "')";
            DataTable dtMessages = myDB.ReadFromBank(strSQL);
            for(int i=0;i<dtMessages.Rows.Count;i++)
            {
                if(dtMessages.Rows[i]["MessageFrom"].ToString() == Session["UserName"].ToString())
                {
                    dtMessages.Rows[i]["MessageBody"] = dtMessages.Rows[i]["MessageBody"].ToString() + "<<" + DateTime.Parse(dtMessages.Rows[i]["SendTime"].ToString()).ToString("MM/dd HH:mm ");

                }
                else
                {
                    dtMessages.Rows[i]["MessageBody"] = DateTime.Parse(dtMessages.Rows[i]["SendTime"].ToString()).ToString("MM/dd HH:mm ") + ">>" + dtMessages.Rows[i]["MessageBody"].ToString();
                }
            }
            if (dtMessages.Rows.Count > 0)
            {
                lblMessage.Text += "لیست پیام های ثبت شده شما و " + Session["MessageRec"].ToString();

                dgvMesseges.DataSource = dtMessages;
                dgvMesseges.DataBind(); //دقت کنم که بعد از بستن گرید به جدول باید آنرا باند کنم

            }
            else
            {
                lblMessage.Text += " شما هنوز هیچ پیامی ثبت نکرده اید";
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Message tmpMessage = new Message();
            tmpMessage.MessageFrom = Session["UserName"].ToString();
            tmpMessage.MessageTo = Session["MessageRec"].ToString();
            tmpMessage.SendTime = DateTime.Now;
            tmpMessage.MessageBody = txtMessageToSend.Text;
            if (tmpMessage.Insert())
            {
                lblMessage.Text = "ارسال شد";
                GetMessages();
                txtMessageToSend.Text = "";
            }
            else
                lblMessage.Text = "خطا در ارسال پیام";

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            GetMessages();
        }
    }
}