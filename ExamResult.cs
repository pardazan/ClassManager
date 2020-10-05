using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ClassManager
{
    public class ExamResult
    {
        public int ExamId = 0;
        public String StudentID = "";
        public DateTime ExamStart = new DateTime();
        public String ResultFile = "";


        public ExamResult(int Exam_Id,String Student_Id)
        {
            ExamId = Exam_Id;
            StudentID = Student_Id;
            String strSQL = "Select * From  ExamResults Where ExamId = " + this.ExamId + " AND StudentID = '" + this.StudentID + "'";
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            DataTable Output = myDB.ReadFromBank(strSQL);
            if (Output.Rows.Count > 0)
            {
                ExamId = int.Parse(Output.Rows[0]["ExamId"].ToString());
                StudentID = Output.Rows[0]["StudentID"].ToString();
                try
                {
                    ExamStart = DateTime.Parse(Output.Rows[0]["ExamStart"].ToString());
                }
                catch { }
                 ResultFile = Output.Rows[0]["ResultFile"].ToString();
            }
        }


        public Boolean Insert()
        {
            String strSQL = "INSERT INTO ExamResults (ExamId, StudentID, ExamStart, ResultFile)";
            strSQL += " VALUES ";
            strSQL += "('" + this.StudentID + "','" + this.ExamStart.ToString("yyyy-MM-dd HH:mm:ss") + "','" + this.ResultFile + "')";
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            ExamId = int.Parse(myDB.Insert_ID(strSQL));
            if (ExamId > 0)
                return true;
            else
                return false;
        }

        public Boolean Update()
        {
            String strSQL = "UPDATE ExamResults Set ";           
            strSQL += "ExamStart ='" + this.ExamStart.ToString("yyyy-MM-dd HH:mm:ss") + "',";            
            strSQL += "ResultFile = '" + this.ResultFile + "' ";
            strSQL += "WHERE ExamId = " + this.ExamId + " AND StudentID = '" + this.StudentID + "'";
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