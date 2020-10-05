using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ClassManager
{
    public class Exam
    {
        public int ExamId = 0;
        public String TeacherId = "";
        public int ExamLength = 0;
        public String ExamTitle = "";
        public DateTime ExamDate = new DateTime();
        public String ExamFile = "";


        public Exam(int Exam_Id)
        {
            ExamId = Exam_Id;
            String strSQL = "Select * From  Exams Where ExamId = " + this.ExamId;
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            DataTable Output = myDB.ReadFromBank(strSQL);
            if (Output.Rows.Count > 0)
            {
                ExamId = int.Parse(Output.Rows[0]["ExamId"].ToString());
                TeacherId = Output.Rows[0]["TeacherId"].ToString();
                ExamTitle = Output.Rows[0]["ExamTitle"].ToString();
                ExamDate = DateTime.Parse( Output.Rows[0]["ExamDate"].ToString());
                ExamLength = int.Parse(Output.Rows[0]["ExamLength"].ToString());
                ExamFile = Output.Rows[0]["ExamFile"].ToString();
            }
        }
        

        public Boolean Insert()
        {
            String strSQL = "INSERT INTO Exams (TeacherId, ExamTitle, ExamDate, ExamLength, ExamFile)";
            strSQL += " VALUES ";
            strSQL += "('" + this.TeacherId + "','" + this.ExamTitle + "','" + this.ExamDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," + this.ExamLength + ",'" + this.ExamFile + "')";           
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            ExamId = int .Parse(myDB.Insert_ID(strSQL));
            if (ExamId > 0)
                return true;
            else
                return false;
        }

        public Boolean Update()
        {            
            String strSQL = "UPDATE Exams Set ";
            strSQL += "TeacherId = '" + this.TeacherId + "',";
            strSQL += "ExamTitle = '" + this.ExamTitle + "',";
            strSQL += "ExamDate ='" + this.ExamDate.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            strSQL += "ExamLength =" + this.ExamLength + ",";
            strSQL += "ExamFile = '" + this.ExamFile + "' ";            
            strSQL += "WHERE ExamId = " + this.ExamId ;
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            int Output = myDB.RunQuery(strSQL);
            if (Output > 0)
                return true;
            else
                return false;
        }
    }

}