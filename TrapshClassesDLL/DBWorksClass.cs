using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using Trapsh;

namespace TrapshClassesDLL {
   public class DBWorksClass {

        private static string DBN = AppDomain.CurrentDomain.BaseDirectory + "/Database/TrapshDB.db";
        private static SQLiteConnection ConnectDB;
        // Save User Sort
        public static void MainWindow_SaveDB(int Year) {

            int SortValues = 0;
            int Values = 0;
            int PointLane = -1;
            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand LookGroups = new SQLiteCommand("Select * from Groups", ConnectDB)) {
                    using (SQLiteDataReader ReadLookGroups = LookGroups.ExecuteReader()) {
                        while (ReadLookGroups.Read()) {
                        SortValues = 0;
                        Values = 0;
                        PointLane = -1;
                            using (SQLiteCommand Cd = new SQLiteCommand("Select * from Points where PersonGroup = '" + ReadLookGroups["GroupName"] + "' and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                                using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                                    while (DBread.Read()) {
                                        SortValues = Convert.ToInt32(DBread["PersonPoint"]);
                                        Values++;

                                        if (PointLane == SortValues) {
                                            Values -= 1;
                                        }

                                        using (SQLiteCommand UpdateSorts = new SQLiteCommand("Update Points set Sort = " + Values + " where PersonNo = " + DBread["PersonNo"] + " and Year = " + Year + " ", ConnectDB)) {
                                            UpdateSorts.ExecuteNonQuery();
                                        }
                                        PointLane = Convert.ToInt32(DBread["PersonPoint"]);
                                    }
                                }
                            }
                        }
                    }
                }

                ConnectDB.Close();
            }
        }

            public static void MainWindow_Info() {

            int NowYear = DateTime.Now.Year;
            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand FindNumber_1 = new SQLiteCommand("select count(*) from Groups", ConnectDB)) {
                    ClassValues.GroupNumbers = Convert.ToInt32(FindNumber_1.ExecuteScalar());
                }
                using (SQLiteCommand FindNumber_2 = new SQLiteCommand("select count(*) from Persons", ConnectDB)) {
                    ClassValues.PersonNumbers = Convert.ToInt32(FindNumber_2.ExecuteScalar());
                }
                using (SQLiteCommand FindNumber_3 = new SQLiteCommand("select count(*) from Points where Year = " + NowYear + " ", ConnectDB)) {
                       int PointSortValue = Convert.ToInt32(FindNumber_3.ExecuteScalar());
                    ClassValues.NotPointNumbers = ClassValues.PersonNumbers - PointSortValue;
                }
                using (SQLiteCommand FindNumber_4 = new SQLiteCommand("select count(*) from Points where PersonPoint >= 20 and Year = " + NowYear + "", ConnectDB)) {
                    ClassValues.BigPoint20 = Convert.ToInt32(FindNumber_4.ExecuteScalar());
                }
                using (SQLiteCommand FindNumber_5 = new SQLiteCommand("select count(*) from Points where PersonPoint >= 10 and PersonPoint < 20 and Year = " + NowYear + "", ConnectDB)) {
                    ClassValues.BigPoint10 = Convert.ToInt32(FindNumber_5.ExecuteScalar());
                }
                using (SQLiteCommand FindNumber_6 = new SQLiteCommand("select count(*) from Points where PersonPoint < 10 and Year = " + NowYear + "", ConnectDB)) {
                    ClassValues.SmallPoint10 = Convert.ToInt32(FindNumber_6.ExecuteScalar());
                }
                using (SQLiteCommand FindNumber_7 = new SQLiteCommand("select count(*) from Persons where Length(PersonGroup) = 0", ConnectDB)) {
                    ClassValues.HGPersonNumber = Convert.ToInt32(FindNumber_7.ExecuteScalar());
                }

                ConnectDB.Close();
            }
            MessageBox.Show("Kayıt edilen grup sayısı : " + ClassValues.GroupNumbers.ToString() + "\r\rKayıt edilen üye sayısı : " + ClassValues.PersonNumbers.ToString() + "\r\rGrubu olmayan üye sayısı : " + ClassValues.HGPersonNumber.ToString()
                    + "\r\rPuanı '20' ve üzerinde olan üye sayısı : " + ClassValues.BigPoint20.ToString() + "\r\rPuanı '20' ve '10' arasında olan üye sayısı : " + ClassValues.BigPoint10.ToString()
                    + "\r\rPuanı '10' dan düşük olan üye sayısı : " + ClassValues.SmallPoint10.ToString() + "\r\rPuanı daha girilmemiş olan üye sayısı : " + ClassValues.NotPointNumbers.ToString(), "Sayı Listesi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        class Employee : IComparable<Employee> { 
            public int Point { get; set; }
            public string Name { get; set; }

            public int CompareTo(Employee other) {
                // Alphabetic sort if salary is equal. [A to Z]
                if (this.Point == other.Point) {
                    return this.Name.CompareTo(other.Name);
                }
                // Default to salary sort. [High to low]
                return other.Point.CompareTo(this.Point);
            }

            public override string ToString() {
                // String representation.
                return this.Point.ToString() + "," + this.Name;
            }
        }

        public static void MainWindow_Info_2() {

            if (ClassValues.GroupNumbers > 1 && ClassValues.PersonNumbers > 1) {

                List<Employee> SortList = new List<Employee>();
                int NowYear = DateTime.Now.Year;
                int Total_Point = 0;
                string MessageBox_Message = "";

                using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                    ConnectDB.Open();

                    using (SQLiteCommand GroupNameCommand = new SQLiteCommand("select * from Groups", ConnectDB)) {
                        using (SQLiteDataReader Read_1 = GroupNameCommand.ExecuteReader()) {

                            while (Read_1.Read()) {

                                Total_Point = 0;

                                using (SQLiteCommand GroupTotalPointCommand = new SQLiteCommand("select * from Points where PersonGroup = '" + Read_1["GroupName"] + "' and Year = " + NowYear + "", ConnectDB)) {
                                    using (SQLiteDataReader Read_2 = GroupTotalPointCommand.ExecuteReader()) {

                                        while (Read_2.Read()) {

                                            Total_Point += Convert.ToInt32(Read_2["PersonPoint"]);

                                        }
                                    }
                                }

                                SortList.Add(new Employee() { Name = Read_1["GroupName"].ToString(), Point = Total_Point });
                            }
                        }
                    }

                    ConnectDB.Close();
                    SortList.Sort();
                    int Sort = 0;
                    for (int i = 0; i < SortList.Count; i++) {
                        Sort++;
                        MessageBox_Message += Sort.ToString() + ". Grup : " + SortList[i].Name.ToString() + ", Toplam Puanı : " + SortList[i].Point.ToString() + "\n\n";

                    }
                    MessageBox.Show(MessageBox_Message,"Grupların Sıralaması", MessageBoxButton.OK,MessageBoxImage.Information);
                }
            } else {
                MessageBox.Show("Grup sıralaması için yeterli grup yada paunı girilmiş yeterli üye yoktur!","Grupların Sıralaması", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


        public static void ShowYears_SUS(ComboBox CMBXName) {
            
            string OldYear = "-1";
            CMBXName.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand ShowYears = new SQLiteCommand("select Year from Points where Sort = 0 order by Year desc", ConnectDB)) {
                    using (SQLiteDataReader ShowYearsRead = ShowYears.ExecuteReader()) {
                        while (ShowYearsRead.Read()) {

                            if (OldYear != ShowYearsRead["Year"].ToString()) {

                                CMBXName.Items.Add(ShowYearsRead["Year"].ToString());

                            }
                            OldYear = ShowYearsRead["Year"].ToString();

                        }
                    }
                }

                ConnectDB.Close();
            }

        }
        //*****************************************************
        // Point delete select
        public static void ShowYears_PDS(ComboBox CMBXName) {

            string OldYear = "-1";
            CMBXName.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand ShowYears = new SQLiteCommand("select Year from Points order by Year desc", ConnectDB)) {
                    using (SQLiteDataReader ShowYearsRead = ShowYears.ExecuteReader()) {
                        while (ShowYearsRead.Read()) {

                            if (OldYear != ShowYearsRead["Year"].ToString()) {

                                CMBXName.Items.Add(ShowYearsRead["Year"].ToString());

                            }
                            OldYear = ShowYearsRead["Year"].ToString();

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void TryShowListGroups(ListBox ListBoxName) {

            ListBoxName.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Groups", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                        while (DBread.Read()) {
                            ListBoxName.Items.Add(DBread["GroupName"].ToString());
                        }

                    }
                }

                ConnectDB.Close();
            }

        }

        public static void TrueOrFalse(string GroupName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

            ClassValues.TF = false;

                using (SQLiteCommand CommandSearch = new SQLiteCommand("Select GroupName From Groups where GroupName = '" + GroupName + "' ", ConnectDB)) {
                    using (SQLiteDataReader TrueFalse = CommandSearch.ExecuteReader()) {
                        if (TrueFalse.Read()) {

                            ClassValues.TF = true;

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void CreateGroup(string GroupName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand AddCommand = new SQLiteCommand(null, ConnectDB)) {
                    AddCommand.CommandText = "INSERT INTO Groups (GroupName) VALUES (@GroupNamess)";
                    AddCommand.Parameters.AddWithValue("@GroupNamess", GroupName);
                    AddCommand.ExecuteNonQuery();
                }

                ConnectDB.Close();
            }

        }
        // See All Person Page , ADD Combo Box İtem
        public static void PersonsShow_NotPoints(ListBox PersonListBox, string Group) {

            PersonListBox.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Persons where PersonGroup = '" + Group + "' ", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                        while (DBread.Read()) {

                            PersonListBox.Items.Add(new AddCmboBoxItem("", DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString(), ""));
                            ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void PersonsShow_SortPoints(ListBox ListBoxName, int Years) {

                ListBoxName.Items.Clear();
                int SortValues = 0;
                int Values = 0;

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                    int PointLane = -1;
                    using (SQLiteCommand Cd = new SQLiteCommand("Select * from Points where PersonGroup = '" + ClassValues.Group + "' and Year = " + Years + " order by PersonPoint desc", ConnectDB)) {
                        using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                            while (DBread.Read()) {
                                SortValues = Convert.ToInt32(DBread["PersonPoint"]);
                                Values++;
                                if (PointLane == SortValues) {
                                    Values -= 1;
                                }

                                if (Values == 1) {
                                    ListBoxName.Items.Add(new AddCmboBoxItem("", DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString(), "Images/Sorts/First.png"));
                                } else if (Values == 2) {
                                    ListBoxName.Items.Add(new AddCmboBoxItem("", DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString(), "Images/Sorts/Second.png"));
                                } else if (Values == 3) {
                                    ListBoxName.Items.Add(new AddCmboBoxItem("", DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString(), "Images/Sorts/Third.png"));
                                } else {
                                    ListBoxName.Items.Add(new AddCmboBoxItem(Values.ToString() + ". ", DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString(), ""));
                                }
                                ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"].ToString());
                                PointLane = Convert.ToInt32(DBread["PersonPoint"]);
                            }
                        }
                    }
                ConnectDB.Close();
            }
        }
        //-------------------------------------------------------------------------
        public static void UpdateGroups(string OldGroupName, string NewGroupName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand AddCommandGroups = new SQLiteCommand(null, ConnectDB)) {
                    AddCommandGroups.CommandText = "Update Groups set GroupName = @GroupNamess where GroupName = '" + OldGroupName + "'";
                    AddCommandGroups.Parameters.AddWithValue("@GroupNamess", NewGroupName);
                    AddCommandGroups.ExecuteNonQuery();
                }
                using (SQLiteCommand CommandSearch = new SQLiteCommand("Select * From Persons where PersonGroup = '" + OldGroupName + "' ", ConnectDB)) {
                    using (SQLiteDataReader Look = CommandSearch.ExecuteReader()) {

                        if (Look.Read()) {

                            using (SQLiteCommand CommandUpdatePersons = new SQLiteCommand("Update Persons set PersonGroup = '" + NewGroupName + "' Where PersonGroup = '" + OldGroupName + "'  ", ConnectDB)) {
                                CommandUpdatePersons.ExecuteNonQuery();
                            }
                            using (SQLiteCommand CommandUpdatePoints = new SQLiteCommand("Update Points set PersonGroup = '" + NewGroupName + "' Where PersonGroup = '" + OldGroupName + "'  ", ConnectDB)) {
                                CommandUpdatePoints.ExecuteNonQuery();
                            }

                        }

                    }
                }

                ConnectDB.Close();
            }

        }

        public static void GroupsDelete(string GroupName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand DeleteCommand = new SQLiteCommand("Delete from Groups where GroupName = '" + GroupName + "'", ConnectDB)) {
                    DeleteCommand.ExecuteNonQuery();
                }

                using (SQLiteCommand CommandSearchPerson = new SQLiteCommand("Select * From Persons where PersonGroup = '" + GroupName + "' ", ConnectDB)) {
                    using (SQLiteDataReader Look = CommandSearchPerson.ExecuteReader()) {
                        while (Look.Read()) {

                            using (SQLiteCommand CommandUpdatePerson = new SQLiteCommand("Update Persons set PersonGroup = '' Where PersonGroup = '" + GroupName + "'  ", ConnectDB)) {
                                CommandUpdatePerson.ExecuteNonQuery();
                            }

                        }
                    }
                }

                using (SQLiteCommand CommandSearchPoints = new SQLiteCommand("Select * From Points where PersonGroup = '" + GroupName + "' ", ConnectDB)) {
                    using (SQLiteDataReader Look2 = CommandSearchPoints.ExecuteReader()) {
                        while (Look2.Read()) {

                            using (SQLiteCommand CommandUpdatePoints = new SQLiteCommand("Update Points set PersonGroup = '' Where PersonGroup = '" + GroupName + "'  ", ConnectDB)) {
                                CommandUpdatePoints.ExecuteNonQuery();
                            }
                        }
                    }
                }

                ConnectDB.Close();
            }

        }
        // Group Add Person and group delete person
        public static void GAPShowGroupAndPerson(ListBox PersonList, ListBox GroupList) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand CP = new SQLiteCommand("Select * from Persons where Length(PersonGroup) = 0 ", ConnectDB)) {
                    using (SQLiteDataReader DPread = CP.ExecuteReader()) {
                        while (DPread.Read()) {

                            PersonList.Items.Add(DPread["PersonName"].ToString() + " " + DPread["PersonLastName"].ToString());
                            ClassValues.PersonsKeyNumber.Add(DPread["PersonNo"]);
                            ClassValues.PersonsKeyName.Add(DPread["PersonName"].ToString() + " " + DPread["PersonLastName"].ToString());

                        }
                    }
                }

                using (SQLiteCommand CG = new SQLiteCommand("Select * from Groups", ConnectDB)) {
                    using (SQLiteDataReader DGread = CG.ExecuteReader()) {
                        while (DGread.Read()) {

                            GroupList.Items.Add(DGread["GroupName"].ToString());
                            ClassValues.PersonsKeyGroup.Add(DGread["GroupName"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void GAPUpdatePersonsGroup(int PersonNo, string Group) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand DeleteCommand = new SQLiteCommand("Update Persons set PersonGroup = '" + Group + "' Where PersonNo = " + PersonNo + "", ConnectDB)) {
                    DeleteCommand.ExecuteNonQuery();
                }
                using (SQLiteCommand DeleteCommand2 = new SQLiteCommand("Update Points set PersonGroup = '" + Group + "' Where PersonNo = " + PersonNo + "", ConnectDB)) {
                    DeleteCommand2.ExecuteNonQuery();
                }

                ConnectDB.Close();
            }

        }// ÖZÇELİK SOFTWARE - Copyright 2019

        public static void GDPShowGroup(ListBox GroupList) {

            GroupList.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand CPG = new SQLiteCommand("Select * from Groups", ConnectDB)) {
                    using (SQLiteDataReader DPGread = CPG.ExecuteReader()) {
                        while (DPGread.Read()) {

                            GroupList.Items.Add(DPGread["GroupName"].ToString());
                            ClassValues.PersonsKeyGroup.Add(DPGread["GroupName"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void GDPShowPerson(ListBox PersonList, string GroupName) {

            PersonList.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand CPG = new SQLiteCommand("Select * from Persons where PersonGroup = '"+GroupName+"'", ConnectDB)) {
                    using (SQLiteDataReader DPGread = CPG.ExecuteReader()) {
                        while (DPGread.Read()) {

                            PersonList.Items.Add(DPGread["PersonName"].ToString() + " " + DPGread["PersonLastName"].ToString());
                            ClassValues.PersonsKeyNumber.Add(DPGread["PersonNo"]);
                            ClassValues.PersonsKeyName.Add(DPGread["PersonName"].ToString() + " " + DPGread["PersonLastName"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }
        // Person Add
        public static void PersonsShow(ListBox PersonListBox) {

            PersonListBox.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Persons", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                        while (DBread.Read()) {

                            PersonListBox.Items.Add(DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static bool Off_2 { get; set; }
        // 1
        public static void PersonAddControl(string PersonName, string PersonLastName) {
            
            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand IsThereAnyC = new SQLiteCommand("select * from Persons where PersonName = '" + PersonName + "' and PersonLastName = '" + PersonLastName + "'  ", ConnectDB)) {
                    using (SQLiteDataReader Read = IsThereAnyC.ExecuteReader()) {
                        if (Read.Read()) {

                            Off_2 = true;
                            MessageBox.Show("\"" + PersonName + " " + PersonLastName + "\" isiminde kayıtli bir üye var !", "Kayıtlı Üye", MessageBoxButton.OK, MessageBoxImage.Error);

                        } else {

                            Off_2 = false;
                            ClassValues.PName = PersonName + " " + PersonLastName;

                        }
                    }
                }

                ConnectDB.Close();
            }
        }
        // 2
        public static void PersonAdd(bool Off, string PersonName, string PersonLastName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                if (Off == false) {

                    using (SQLiteCommand komt = new SQLiteCommand(null, ConnectDB)) {
                        komt.CommandText = "INSERT INTO Persons (PersonName,PersonLastName,PersonGroup) VALUES (@PersonNamee,@PersonLastNamee,@PersonGroupp)";
                        komt.Parameters.AddWithValue("@PersonNamee", PersonName);
                        komt.Parameters.AddWithValue("@PersonLastNamee", PersonLastName);
                        komt.Parameters.AddWithValue("@PersonGroupp", ClassValues.Group);
                        komt.ExecuteNonQuery();
                    }

                }

                ConnectDB.Close();
            }

        }
        // Person Delete
        public static void PDPersonShow(ListBox PersonList) {

            ClassValues.PersonsKeyNumber.Clear();
            PersonList.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Persons", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {

                        while (DBread.Read()) {
                            PersonList.Items.Add(DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString());
                            ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"]);
                        }

                    }
                }

                ConnectDB.Close();
            }

        }

        public static void PersonDelete(int PersonNo) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand DeletePersonCommand = new SQLiteCommand("Delete from Persons where PersonNo = " + PersonNo + " ", ConnectDB)) {
                    DeletePersonCommand.ExecuteNonQuery();
                }

                using (SQLiteCommand DeletePointC = new SQLiteCommand("Delete from Points where PersonNo = " + PersonNo + " ", ConnectDB)) {
                    DeletePointC.ExecuteNonQuery();
                }

                ConnectDB.Close();
            }

        }
        //Person Name Change
        public static bool FindCrash { get; set; }
        public static void SearchPerson(int PersonNo) {

            FindCrash = false;

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand SearchCommand = new SQLiteCommand("Select * from Persons where PersonNo = " + PersonNo + " ", ConnectDB)) {
                    using (SQLiteDataReader SCRead = SearchCommand.ExecuteReader()) {

                        if (SCRead.Read()) {

                            ClassValues.PName = SCRead["PersonName"].ToString();
                            ClassValues.PLastName = SCRead["PersonLastName"].ToString();

                        } else {
                            MessageBox.Show("Hata! Aranan üye bulunamadı.", "Veritabanı Sorunu", MessageBoxButton.OK, MessageBoxImage.Error);
                            FindCrash = true;
                        }

                    }
                }

                ConnectDB.Close();
            }

        }

        public static void TrueAndFalse_2(string Name, string LastName) {

            ClassValues.TF = false;

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand CommandSearch = new SQLiteCommand("Select PersonName,PersonLastName From Persons where PersonName = '" + Name + "' and PersonLastName = '" + LastName + "' ", ConnectDB)) {
                    using (SQLiteDataReader TrueFalse = CommandSearch.ExecuteReader()) {
                        if (TrueFalse.Read()) {
                            ClassValues.TF = true;
                        }
                    }
                }

                ConnectDB.Close();
            }
        }

        public static void ChangePersonName(int PersonNo ,string NewName, string NewLastName) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand UpdateCommand = new SQLiteCommand(null, ConnectDB)) {
                    UpdateCommand.CommandText = "Update Persons set PersonName = @PersonNamess , PersonLastName = @PersonLastNamess where PersonNo = " + PersonNo + " ";
                    UpdateCommand.Parameters.AddWithValue("@PersonNamess", NewName);
                    UpdateCommand.Parameters.AddWithValue("@PersonLastNamess", NewLastName);
                    UpdateCommand.ExecuteNonQuery();
                }
                using (SQLiteCommand UpdateCommand2 = new SQLiteCommand(null, ConnectDB)) {
                    UpdateCommand2.CommandText = "Update Points set PersonName = @PersonNamess , PersonLastName = @PersonLastNamess where PersonNo = " + PersonNo + " ";
                    UpdateCommand2.Parameters.AddWithValue("@PersonNamess", NewName);
                    UpdateCommand2.Parameters.AddWithValue("@PersonLastNamess", NewLastName);
                    UpdateCommand2.ExecuteNonQuery();
                }

                ConnectDB.Close();
            }

        }
        // Add Point
        public static void AddPoint(int PersonNo, string PersonName, 
            string PersonLastName, string PersonGroup, int PersonTotalPoint,
            int PersonFirstPoint, int PersonSecondPoint, int PersonThirdPoint, int Year) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand DatePoint = new SQLiteCommand("select * from Points where PersonNo = " + PersonNo + " and Year = " + Year + " ", ConnectDB)) {
                    using (SQLiteDataReader TrueAndFalse = DatePoint.ExecuteReader()) {
                        if (TrueAndFalse.Read()) {
                            MessageBoxResult QuestionMessage = MessageBox.Show("\"" + PersonName + " " + PersonLastName+"\" isimli kullanıcının "+Year.ToString()+" yılına ait \""+TrueAndFalse["PersonPoint"]+"\" puanlı kayıdı zaten bulunmakta!\n Puanı güncellemek istediğinize emin misiniz?","Kayıtlı Puan",MessageBoxButton.YesNo,MessageBoxImage.Information);
                            if (QuestionMessage == MessageBoxResult.Yes) {

                                using (SQLiteCommand UpdatePoint = new SQLiteCommand("Update Points set PersonPoint = " + PersonTotalPoint + " , PersonFirstP = " + PersonFirstPoint + " , PersonSecondP = " + PersonSecondPoint + " , PersonThirdP = " + PersonThirdPoint + " where PersonNo = " + PersonNo + " ", ConnectDB)) {
                                    UpdatePoint.ExecuteNonQuery();
                                }

                            } else {
                                ;
                            }

                        } else {

                            using (SQLiteCommand InsertPoint = new SQLiteCommand(null, ConnectDB)) {

                                InsertPoint.CommandText = "insert into Points values" +
                                    "(@PersonNo," +
                                    "@PersonName," +
                                    "@PersonLastName," +
                                    "@PersonGroup," +
                                    "@PersonTotalPoint," +
                                    "@PersonFirstPoint," +
                                    "@PersonSecondPoint," +
                                    "@PersonThirdPoint," +
                                    "@Year," +
                                    "@Sort)";
                                InsertPoint.Parameters.AddWithValue("@PersonNo", PersonNo);
                                InsertPoint.Parameters.AddWithValue("@PersonName", PersonName);
                                InsertPoint.Parameters.AddWithValue("@PersonLastName", PersonLastName);
                                InsertPoint.Parameters.AddWithValue("@PersonGroup", PersonGroup);
                                InsertPoint.Parameters.AddWithValue("@PersonTotalPoint", PersonTotalPoint);
                                InsertPoint.Parameters.AddWithValue("@PersonFirstPoint", PersonFirstPoint);
                                InsertPoint.Parameters.AddWithValue("@PersonSecondPoint", PersonSecondPoint);
                                InsertPoint.Parameters.AddWithValue("@PersonThirdPoint", PersonThirdPoint);
                                InsertPoint.Parameters.AddWithValue("@Year", Year);
                                InsertPoint.Parameters.AddWithValue("@Sort", 0);
                                InsertPoint.ExecuteNonQuery();

                            }

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void PointSelectWindowSort_User(ListBox ListBoxName, string GroupName) {

            ListBoxName.Items.Clear();
            ClassValues.PersonsKeyLastName.Clear();
            ClassValues.PersonsKeyName.Clear();
            ClassValues.PersonsKeyNumber.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();
                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Persons where PersonGroup = '" + GroupName + "' ", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                        while (DBread.Read()) {

                            using (SQLiteCommand Cd2 = new SQLiteCommand("select * from Points where PersonNo = " + Convert.ToInt32(DBread["PersonNo"]) + " and Year = " + DateTime.Now.Year + "", ConnectDB)) {
                                using (SQLiteDataReader DBread2 = Cd2.ExecuteReader()) {
                                    if (DBread2.Read()) {
                                        ;
                                    } else {

                                        ListBoxName.Items.Add(DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString());
                                        ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"]);
                                        ClassValues.PersonsKeyName.Add(DBread["PersonName"].ToString());
                                        ClassValues.PersonsKeyLastName.Add(DBread["PersonLastName"].ToString());

                                    }
                                }
                            }
                        }
                    }
                }

                ConnectDB.Close();
            }

        }
        //Delete Point
        public static void DeletePoint(int PersonNo,int Year) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand DatePoint = new SQLiteCommand("delete from Points  where PersonNo = " + PersonNo + " and Year = " + Year + " ", ConnectDB)) {
                    DatePoint.ExecuteNonQuery();
                }

                ConnectDB.Close();
            }

        }

        public static void PDSTryListPersons(ListBox ListBoxName,string Group, int Year) {

            ListBoxName.Items.Clear();
            ClassValues.PersonsKeyNumber.Clear();
            ClassValues.PersonsKeyName.Clear();
            ClassValues.PersonsKeyPoint.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Points where PersonGroup = '" + Group + "' and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {
                        while (DBread.Read()) {

                            ListBoxName.Items.Add(DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString() + " Puan : " + DBread["PersonPoint"].ToString());
                            ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"]);
                            ClassValues.PersonsKeyPoint.Add(DBread["PersonPoint"]);
                            ClassValues.PersonsKeyName.Add(DBread["PersonName"] + " " + DBread["PersonLastName"]);

                        }
                    }
                }

                ConnectDB.Close();
            }

        }
        // Point Table
        public static void ShowYears(ComboBox ComboBoxName, string GroupName) {

            string OldYear = "-1";
            ComboBoxName.Items.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Searching = new SQLiteCommand("Select * from Points where PersonGroup ='" + GroupName + "' order by Year desc", ConnectDB)) {
                    using (SQLiteDataReader ReadYear = Searching.ExecuteReader()) {

                        while (ReadYear.Read()) {
                            if (OldYear != ReadYear["Year"].ToString()) {

                                ComboBoxName.Items.Add(ReadYear["Year"].ToString());

                            }
                            OldYear = ReadYear["Year"].ToString();
                        }
                    }
                }

                ConnectDB.Close();
            }
        }

        public static void PTSortPersons(string GroupName, int Year) {

            ClassValues.PersonsKeyName.Clear();
            ClassValues.PersonsKeyNumber.Clear();
            ClassValues.PersonsKeyPoint.Clear();

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Cd = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and Year = " + Year + " order by PersonPoint desc ", ConnectDB)) {
                    using (SQLiteDataReader DBread = Cd.ExecuteReader()) {

                        while (DBread.Read()) {

                            ClassValues.PersonsKeyPoint.Add(DBread["PersonPoint"]);
                            ClassValues.PersonsKeyNumber.Add(DBread["PersonNo"]);
                            ClassValues.PersonsKeyName.Add(DBread["PersonName"].ToString() + " " + DBread["PersonLastName"].ToString());

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void PT_FSTSort(string GroupName, int Year) {

            int GroupAddedPersonCount = 0;
            int NumberFiftySeven = 0;

            ClassValues.TechValue1 = "Yok";
            ClassValues.TechValue3 = "Yok";
            ClassValues.TechValue2 = "Yok";

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand FindCount = new SQLiteCommand("Select count(*) from Points where PersonGroup = '" + GroupName + "' and Year = " + Year + "", ConnectDB)) {

                    GroupAddedPersonCount = Convert.ToInt32(FindCount.ExecuteScalar());
                        if (GroupAddedPersonCount == 1) {
                        ClassValues.SelectedNameFirst = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                        ClassValues.SelectedNumberFirst = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                        ClassValues.SelectedPointFirst = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                        ClassValues.TechValue1 = ClassValues.SelectedNameFirst;
                        ClassValues.SortNumber = 1;

                    } else if (GroupAddedPersonCount == 2) {
                        ClassValues.SelectedNameFirst = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                        ClassValues.SelectedNumberFirst = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                        ClassValues.SelectedPointFirst = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                        ClassValues.TechValue1 = ClassValues.SelectedNameFirst;
                        using (SQLiteCommand IdontNo = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and PersonPoint = " + ClassValues.SelectedPointFirst + " and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                            using (SQLiteDataReader Reads = IdontNo.ExecuteReader()) {
                                if (Reads.Read()) {
                                    NumberFiftySeven++;
                                    while (Reads.Read()) {
                                        ClassValues.TechValue1 += ", " + Reads["PersonName"].ToString() + " " + Reads["PersonLastName"].ToString();
                                        NumberFiftySeven++;
                                    }
                                } else {
                                    NumberFiftySeven++;
                                }
                            }
                        }
                        if (NumberFiftySeven == 2) {
                            ;
                        } else {
                            ClassValues.SelectedNameSecond = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                            ClassValues.SelectedNumberSecond = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                            ClassValues.SelectedPointSecond = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                            ClassValues.TechValue2 = ClassValues.SelectedNameSecond;
                            using (SQLiteCommand IdontNo2 = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and PersonPoint = " + ClassValues.SelectedPointSecond + " and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                                using (SQLiteDataReader Reads2 = IdontNo2.ExecuteReader()) {
                                    if (Reads2.Read()) {
                                        while (Reads2.Read()) {
                                            ClassValues.TechValue2 += ", " + Reads2["PersonName"].ToString() + " " + Reads2["PersonLastName"].ToString();
                                        }
                                    } else {
                                        ;
                                    }
                                }
                            }
                        }
                        ClassValues.SortNumber = 2;
                    } else if (GroupAddedPersonCount >= 3) {
                        ClassValues.SelectedNameFirst = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                        ClassValues.SelectedNumberFirst = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                        ClassValues.SelectedPointFirst = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                        ClassValues.TechValue1 = ClassValues.SelectedNameFirst;
                        using (SQLiteCommand IdontNo = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and PersonPoint = " + ClassValues.SelectedPointFirst + " and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                            using (SQLiteDataReader Reads = IdontNo.ExecuteReader()) {
                                if (Reads.Read()) {
                                    NumberFiftySeven++;
                                    while (Reads.Read()) {
                                        ClassValues.TechValue1 += ", " + Reads["PersonName"].ToString() + " " + Reads["PersonLastName"].ToString();
                                        NumberFiftySeven++;
                                    }
                                } else {
                                    NumberFiftySeven++;
                                }
                            }
                        }
                        if (GroupAddedPersonCount == NumberFiftySeven) {
                            ;
                        } else {
                            ClassValues.SelectedNameSecond = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                            ClassValues.SelectedNumberSecond = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                            ClassValues.SelectedPointSecond = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                            ClassValues.TechValue2 = ClassValues.SelectedNameSecond;
                            using (SQLiteCommand IdontNo2 = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and PersonPoint = " + ClassValues.SelectedPointSecond + " and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                                using (SQLiteDataReader Reads2 = IdontNo2.ExecuteReader()) {
                                    if (Reads2.Read()) {
                                        NumberFiftySeven++;
                                        while (Reads2.Read()) {
                                            ClassValues.TechValue2 += ", " + Reads2["PersonName"].ToString() + " " + Reads2["PersonLastName"].ToString();
                                            NumberFiftySeven++;
                                        }
                                    } else {
                                        NumberFiftySeven++;
                                    }
                                }
                            }
                            if (GroupAddedPersonCount == NumberFiftySeven) {
                                ;
                            } else {
                                ClassValues.SelectedNameThird = ClassValues.PersonsKeyName[NumberFiftySeven].ToString();
                                ClassValues.SelectedNumberThird = Convert.ToInt32(ClassValues.PersonsKeyNumber[NumberFiftySeven]);
                                ClassValues.SelectedPointThird = Convert.ToInt32(ClassValues.PersonsKeyPoint[NumberFiftySeven]);
                                ClassValues.TechValue3 = ClassValues.SelectedNameThird;
                                using (SQLiteCommand IdontNo3 = new SQLiteCommand("Select * from Points where PersonGroup = '" + GroupName + "' and PersonPoint = " + ClassValues.SelectedPointThird + " and Year = " + Year + " order by PersonPoint desc", ConnectDB)) {
                                    using (SQLiteDataReader Reads3 = IdontNo3.ExecuteReader()) {
                                        if (Reads3.Read()) {
                                            while (Reads3.Read()) {
                                                ClassValues.TechValue3 += ", " + Reads3["PersonName"].ToString() + " " + Reads3["PersonLastName"].ToString();
                                            }
                                        } else {
                                            ;
                                        }
                                    }
                                }
                            }
                        }
                        ClassValues.SortNumber = 3;
                    } else {
                        ClassValues.TechValue1 = "Yok";
                        ClassValues.TechValue2 = "Yok";
                        ClassValues.TechValue3 = "Yok";
                        ClassValues.SortNumber = 0;
                    }
                }

                ConnectDB.Close();
            }
        }
        // Self Info
        public static void YearsInSort(ComboBox ComboBoxName, int PersonNo) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand SearchYears = new SQLiteCommand("select * from Points where PersonNo = " + PersonNo + " order by Year desc ", ConnectDB)) {
                    using (SQLiteDataReader ReadDate = SearchYears.ExecuteReader()) {
                        while (ReadDate.Read()) {
                            if (Convert.ToInt32(ReadDate["Sort"]) == 1) {
                                ComboBoxName.Items.Add(new Selfinfo.AddCmboBoxItem(ReadDate["Year"].ToString(), "Images/Sorts/First.png"));
                                ClassValues.PersonsKeyWinner.Add(1);
                            } else if (Convert.ToInt32(ReadDate["Sort"]) == 2) {
                                ComboBoxName.Items.Add(new Selfinfo.AddCmboBoxItem(ReadDate["Year"].ToString(), "Images/Sorts/Second.png"));
                                ClassValues.PersonsKeyWinner.Add(2);
                            } else if (Convert.ToInt32(ReadDate["Sort"]) == 3) {
                                ComboBoxName.Items.Add(new Selfinfo.AddCmboBoxItem(ReadDate["Year"].ToString(), "Images/Sorts/Third.png"));
                                ClassValues.PersonsKeyWinner.Add(3);
                            } else {
                                ComboBoxName.Items.Add(new Selfinfo.AddCmboBoxItem(ReadDate["Year"].ToString(), ""));
                                ClassValues.KeySort.Add(Convert.ToInt32(ReadDate["Sort"]));
                                ClassValues.PersonsKeyWinner.Add(0);
                            }
                                ClassValues.KeyYear.Add(ReadDate["Year"].ToString());
                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void YearsInValues(int PersonNo, int Year) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Command = new SQLiteCommand("select * from Points where PersonNo = " + PersonNo + " and Year = " + Year + "", ConnectDB)) {
                    using (SQLiteDataReader ReadBy = Command.ExecuteReader()) {
                        if (ReadBy.Read()) {

                            ClassValues.PName = ReadBy["PersonName"].ToString();
                            ClassValues.PLastName = ReadBy["PersonLastName"].ToString();
                            ClassValues.Group = ReadBy["PersonGroup"].ToString();
                            ClassValues.SmallPoint10 = Convert.ToInt32(ReadBy["PersonFirstP"]);
                            ClassValues.BigPoint10 = Convert.ToInt32(ReadBy["PersonSecondP"]);
                            ClassValues.BigPoint20 = Convert.ToInt32(ReadBy["PersonThirdP"]);
                            ClassValues.Point = Convert.ToInt32(ReadBy["PersonPoint"]);

                        }
                    }
                }

                ConnectDB.Close();
            }

        }

        public static void Not_YearsInValues(int PersonNo) {

            using (ConnectDB = new SQLiteConnection("Data Source = " + DBN + "; Version = 3;")) {

                ConnectDB.Open();

                using (SQLiteCommand Command = new SQLiteCommand("select * from Persons where PersonNo = " + PersonNo + " ", ConnectDB)) {
                    using (SQLiteDataReader ReadBy = Command.ExecuteReader()) {
                        if (ReadBy.Read()) {

                            ClassValues.PName = ReadBy["PersonName"].ToString();
                            ClassValues.PLastName = ReadBy["PersonLastName"].ToString();
                            ClassValues.Group = ReadBy["PersonGroup"].ToString();

                        }
                    }
                }

                ConnectDB.Close();
            }

        }
//----------------------------------------------
    }
}
