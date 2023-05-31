using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Threading;

namespace TrapshClassesDLL {
   public class DBControlClass
    {

            static string DBN = AppDomain.CurrentDomain.BaseDirectory + "/Database/TrapshDB.db";
            static SQLiteConnection ConnectDB;

        private static void Create_DB() {

                SQLiteConnection.CreateFile(DBN);

        }

        public static void Create_DB_All() {

            string klasorAdi = "Database";
            string veriTabani = "TrapshDB.db";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/" + klasorAdi)) {

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/" + klasorAdi + "/" + veriTabani)) {

                Create_DB();

                }

                Create_Table();

            } else if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/" + klasorAdi)) {

                string klasorYeri = AppDomain.CurrentDomain.BaseDirectory;
                string klasorOlustur = klasorYeri + @"\" + klasorAdi;
                Directory.CreateDirectory(klasorOlustur);
                Create_DB();
                Create_Table();
            }

        }

        private static void Create_Table() {

            ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;");

            ConnectDB.Open();
            //Persons Table
            SQLiteCommand Table_1 = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Persons("
            +"'PersonNo'  INTEGER PRIMARY KEY AUTOINCREMENT,"
            +"'PersonName'    TEXT,"
            +"'PersonLastName'    TEXT,"
            +"'PersonGroup'   TEXT"
            +")", ConnectDB);
            Table_1.ExecuteNonQuery();
            //***************************
            //Group Table
            SQLiteCommand Table_2 = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Groups("
            + "'GroupNo'  INTEGER PRIMARY KEY AUTOINCREMENT,"
            + "'GroupName'    TEXT"
            + ")", ConnectDB);
            Table_2.ExecuteNonQuery();
            //***************************
            //Point Table
            SQLiteCommand Table_3 = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Points("
            + "'PersonNo'    INTEGER,"
            + "'PersonName'    TEXT,"
            + "'PersonLastName'    TEXT,"
            + "'PersonGroup'   TEXT,"
            + "'PersonPoint'  INTEGER,"
            + "'PersonFirstP'  INTEGER,"
            + "'PersonSecondP' INTEGER,"
            + "'PersonThirdP'  INTEGER,"
            + "'Year'   INTEGER,"
            + "'Sort'  INTEGER"
            + ")", ConnectDB);
            Table_3.ExecuteNonQuery();

            ConnectDB.Close();

        }

    }
}
