using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ClassManager
{
    public class MySqlDB
    {
        //static String HostName = "localhost";
        //static String DatabaseName = "class_manager";
        //static String UserName = "root";
        //static String UserPassword = "";

        //static String HostName = "sql12.freemysqlhosting.net";
        //static String DatabaseName = "sql12367924";
        //static String UserName = "sql12367924";
        //static String UserPassword = "LXArqD5xxD";
        ////https://www.freemysqlhosting.net/login/
        ////U_Name:   akbar@rmomail.com
        ////Pass:     A123789_MySql
        ////http://www.phpmyadmin.co/db_structure.php?db=sql12367924  


        //"Server=MYSQL5009.site4now.net;Database=db_a68612_akbartr;Uid=a68612_akbartr;Pwd=YOUR_DB_PASSWORD" 
        //https://smarterasp.net
        //U_Name    AkbarTree 
        //Pass      Ak1234Bar
        //email     akbar@treeheir.com

        static String HostName = "MYSQL5009.site4now.net";
        static String DatabaseName = "db_a68612_akbartr";
        static String UserName = "a68612_akbartr";
        static String UserPassword = "mySQL1234";



        public String LastError = "";
        private MySqlConnection Connection;
        private MySqlDataAdapter da;
        private MySqlCommandBuilder cb;
        private MySqlCommand cmd;


        String ConnectionString
        {
            get
            {
                String Output = "";
                Output += "server='" + HostName + "';";
                Output += "user id='" + UserName + "';";
                Output += "password='" + UserPassword + "';";
                Output += "database='" + DatabaseName + "';";
                Output += "pooling=false; charset = utf8;";
                //return "server='localhost';user id='root'; password='1234'; database='SmartCard'; pooling=false; charset = utf8;";
                return Output;
            }
        }

        public MySqlDB()
        {
            Connection = new MySqlConnection();
            da = new MySqlDataAdapter();
            cmd = new MySqlCommand();
            Connection.ConnectionString = ConnectionString;
            cmd.Connection = Connection;
            //da.SelectCommand = cmd;
            LastError = "";
            //conn.Open();
        }

        public Boolean CreateIfNotExists()
        {
            int Output = 0;
            LastError = "";
            String strSQL = "CREATE DATABASE  IF NOT EXISTS `" + DatabaseName + "`;";
            try
            {
                String strCon = "";
                strCon += "server='" + HostName + "';";
                strCon += "user id='" + UserName + "';";
                strCon += "password='" + UserPassword + "';";
                strCon += "pooling=false; charset = utf8;";
                Connection.ConnectionString = strCon;
                Connect();
                cmd = new MySqlCommand(strSQL, Connection);
                Output = cmd.ExecuteNonQuery();
                Disconnect();
                if (Output > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                try
                {
                    Disconnect();
                }
                catch { }
            }
            return (Output > 0);
        }

        public Boolean Connect()
        {
            Boolean Result = false;
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    Result = true;
                }
                else
                    Result = true;

            }
            catch (Exception ex)
            {
                this.LastError += "Error Connection Cannot Open" + ex.Message;
            }
            return (Result);
        }

        public Boolean Disconnect()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                if (Connection.State == ConnectionState.Closed)
                    return true;
                else
                {
                    this.LastError += ex.Message;
                    return false;
                }
            }
        }

        public DataTable ReadFromBank(string MySql)
        {
            DataTable dt = new DataTable();
            if (Connect())
            {
                try
                {
                    da = new MySqlDataAdapter(MySql, Connection);
                    cb = new MySqlCommandBuilder(da);
                    da.Fill(dt);
                }
                catch (MySqlException ex)
                {
                    LastError = "Error " + ex.Message;
                }
                Disconnect();
            }

            return dt;
        }

        public int RunQuery(String SQL)
        {
            int Output = 0;
            try
            {
                Connection.ConnectionString = ConnectionString;
                Connect();
                cmd = new MySqlCommand(SQL, Connection);
                cmd.ExecuteNonQuery();
                Output = 1;
            }
            catch (Exception e)
            {
                LastError = e.Message;
            }
            Disconnect();
            return Output;
        }

        public String Insert_ID(string Mysql)
        {
            DataTable dt = new DataTable();
            Mysql = Mysql + ";SELECT LAST_INSERT_ID();";
            string strID = "0";
            dt.Clear();
            try
            {
                Connect();
                cmd.CommandText = Mysql;
                strID = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                this.LastError += ex.Message;
            }
            Disconnect();
            return strID;
        }

        public Boolean CreateUsersTable()
        {
            var sql = "CREATE TABLE Users (";
            //sql += "id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,";
            sql += "UserName VARCHAR(15) CHARACTER SET utf8 COLLATE utf8_persian_ci  PRIMARY KEY,";
            sql += "PersonalCode INT(10) UNSIGNED ,";
            sql += "FirstName VARCHAR(30) CHARACTER SET utf8 COLLATE utf8_persian_ci  NOT NULL,";
            sql += "LastName VARCHAR(30) CHARACTER SET utf8 COLLATE utf8_persian_ci NOT NULL,";
            sql += "PhoneNumber VARCHAR(12) NOT NULL,";
            sql += "Email VARCHAR(50),";
            //sql += "BrithDate DATETIME DEFAULT CURRENT_TIMESTAMP,";
            sql += "BrithDate VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";
            sql += "Password VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";
            sql += "UserType VARCHAR(3))";
            //سه حرف برای نوع کاربر
            //ادمین AMD
            //استاد TCH
            //دانشجو STD
            sql += "CHARACTER SET utf8 COLLATE utf8_persian_ci;";//برای پشتیبانی از حروف فارسی
            try
            {
                if (RunQuery(sql) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            };
            return false;
        }

        public Boolean CreateExamsTable()
        {
            var sql = "CREATE TABLE Exams (";
            sql += "ExamId INT(6) UNSIGNED AUTO_INCREMENT ,";//ID اتوماتیک تولید میشود
            sql += "TeacherId VARCHAR(15),";//نام کاربری استاد ایجاد کننده آزمون
            sql += "ExamTitle VARCHAR(40)   NOT NULL,";//عنوان آزمون
            sql += "ExamDate DATETIME ,";//تاریخ و ساعت آزمون
            sql += "ExamLength INT(2) UNSIGNED ,";//طول آزمون به دقیقه
            sql += "ExamFile VARCHAR(150) ,";//نام فایل
            sql += " PRIMARY KEY (ExamId,TeacherId)";//بیش از یک کلید در جدول
            sql += ")";
            sql += "CHARACTER SET utf8 COLLATE utf8_persian_ci;";//برای پشتیبانی از حروف فارسی
            try
            {
                if (RunQuery(sql) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            };
            return false;
        }

        public Boolean CreateExamResultTable()
        {
            var sql = "CREATE TABLE ExamResults (";
            sql += "ExamId INT(6) UNSIGNED ,";//شناسه آزمون
            sql += "StudentID VARCHAR(15) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";//نام کاربری دانشجوی شرکت کننده در آزمون            
            sql += "ExamStart DATETIME ,";
            sql += "ResultFile VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";
            sql += "Score INT(1) UNSIGNED ,";//نمره دانشجو در آزمون
            sql += "PRIMARY KEY (ExamId,StudentID)";//بیش از یک کلید در جدول
            sql += ")";
            sql += "CHARACTER SET utf8 COLLATE utf8_persian_ci;";//برای پشتیبانی از حروف فارسی
            try
            {
                if (RunQuery(sql) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            };
            return false;
        }

        public Boolean CreateMessagesTable()
        {
            var sql = "CREATE TABLE Messages (";
            sql += "MessageId INT(6) UNSIGNED AUTO_INCREMENT  PRIMARY KEY,";//شناسه پیام
            sql += "MessageFrom VARCHAR(15) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";//فرستنده پیام
            sql += "MessageTo VARCHAR(15) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";//گیرنده پیام
            sql += "MessageBody VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_persian_ci ,";//متن پیام
            sql += "SendTime DATETIME ,";//زمان ارسال
            sql += "ReceiveTime DATETIME ";//زمان دریافت            
            sql += ")";
            sql += "CHARACTER SET utf8 COLLATE utf8_persian_ci;";//برای پشتیبانی از حروف فارسی
            try
            {
                if (RunQuery(sql) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

            };
            return false;
        }

    }
}