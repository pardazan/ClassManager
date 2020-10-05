using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ClassManager
{
    public class Users
    {
        public String UserName = "";
        public int PersonalCode = 0;
        public String FirstName = "";
        public String LastName = "";
        public String PhoneNumber = "";
        public String Email = "";
        public String BrithDate = "";
        public String Password = "";
        public String UserType = "";

        public Users(String User_Name)
        {
            UserName = User_Name;
            String strSQL = "Select * From  Users Where UserName ='" + this.UserName + "'";
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            DataTable Output = myDB.ReadFromBank(strSQL);
            if (Output.Rows.Count > 0)
            {
                PersonalCode = int.Parse(Output.Rows[0]["PersonalCode"].ToString());
                FirstName = Output.Rows[0]["FirstName"].ToString();
                LastName = Output.Rows[0]["LastName"].ToString();
                PhoneNumber = Output.Rows[0]["PhoneNumber"].ToString();
                Email = Output.Rows[0]["Email"].ToString();
                BrithDate = Output.Rows[0]["BrithDate"].ToString();
                Password = Output.Rows[0]["Password"].ToString();
                UserType = Output.Rows[0]["UserType"].ToString();
            }

        }

        public Boolean UserExists()
        {
            String strSQL = "Select * From  Users Where UserName ='" + this.UserName + "'";
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            DataTable Output = myDB.ReadFromBank(strSQL);
            if (Output.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public Boolean Insert()
        {
            //نام کاربری و پسورد باید بیش از 4 حرف داشته و شماره دانشجویی نیز عدد مخالف صفر باشد           
            if (UserName.Length < 4 || PersonalCode == 0 || Password.Length < 5)
                return false;
            String strSQL = "INSERT INTO Users (UserName, PersonalCode, FirstName, LastName, PhoneNumber";
            strSQL += ", Email, BrithDate, Password, UserType) VALUES ";
            strSQL += "('" + this.UserName + "'," + this.PersonalCode + ",'" + this.FirstName + "','" + this.LastName + "','" + this.PhoneNumber + "','";
            strSQL += this.Email + "','" + this.BrithDate + "','" + this.Password + "','" + this.UserType + "')"; //(for sql code "'"+"'" to define string variable: '" + firstName +"'  )
            MySqlDB myDB = new MySqlDB();
            // myDB.Connect();
            int Output = myDB.RunQuery(strSQL);
            if (Output > 0)
                return true;
            else
                return false;
        }

        public Boolean Update()
        {
            //نام کاربری و پسورد باید بیش از 4 حرف داشته و شماره دانشجویی نیز عدد مخالف صفر باشد           
            if (UserName.Length < 4 || PersonalCode == 0 || Password.Length < 5)
                return false;
            String strSQL = "UPDATE Users Set ";           
            strSQL += "PersonalCode =" + this.PersonalCode + ",";
            strSQL += "FirstName = '" + this.FirstName + "',";
            strSQL += "LastName ='" + this.LastName + "',";
            strSQL += "Email = '" + this.Email + "',";
            strSQL += "BrithDate = '" + this.BrithDate + "',";
            strSQL += "PhoneNumber = '" + this.PhoneNumber + "',";
            strSQL += "UserType = '" + this.UserType + "',";
            strSQL += "Password ='" + this.Password + "'";
            strSQL += "WHERE UserName = '" + this.UserName + "'";                
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