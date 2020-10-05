using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ClassManager
{
    public class Message
    {
        public int MessageId = 0;
        public String MessageFrom = "";
        public String MessageTo = "";
        public String MessageBody = "";
        public DateTime SendTime = new DateTime();
        public DateTime ReceiveTime = new DateTime();

        public Message()
        {
        }
        public Message(int Message_Id)
        {
            MessageId = Message_Id;
            String strSQL = "Select * From  Messages Where MessageId = " + this.MessageId;
            MySqlDB myDB = new MySqlDB();            
            DataTable Output = myDB.ReadFromBank(strSQL);
            if (Output.Rows.Count > 0)
            {
                MessageId = int.Parse(Output.Rows[0]["MessageId"].ToString());
                MessageFrom = Output.Rows[0]["MessageFrom"].ToString();
                MessageTo = Output.Rows[0]["MessageTo"].ToString();
                MessageBody = Output.Rows[0]["MessageBody"].ToString();
                SendTime = DateTime.Parse(Output.Rows[0]["SendTime"].ToString());
                ReceiveTime = DateTime.Parse(Output.Rows[0]["ReceiveTime"].ToString());
            }
        }

        public Boolean Insert()
        {
            String strSQL = "INSERT INTO Messages (MessageFrom, MessageTo, SendTime, ReceiveTime, MessageBody)";
            strSQL += " VALUES ";
            strSQL += "('" + this.MessageFrom + "','" + this.MessageTo + "','" + 
                this.SendTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                this.ReceiveTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                this.MessageBody + "')";
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            MessageId = int.Parse(myDB.Insert_ID(strSQL));
            if (MessageId > 0)
                return true;
            else
                return false;
        }

        public Boolean Update()
        {
            String strSQL = "UPDATE Messages Set ";
            strSQL += "MessageFrom = '" + this.MessageFrom + "',";
            strSQL += "MessageTo = '" + this.MessageTo + "',";
            strSQL += "SendTime ='" + this.SendTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            strSQL += "ReceiveTime =" + this.ReceiveTime + ",";
            strSQL += "MessageBody = '" + this.MessageBody + "' ";
            strSQL += "WHERE MessageId = " + this.MessageId;
            MySqlDB myDB = new MySqlDB();
            int Output = myDB.RunQuery(strSQL);
            if (Output > 0)
                return true;
            else
                return false;
        }
    }


}