using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrapshClassesDLL
{
   public class ClassValues
    {
        // Year Control
        public static int PublicYear { get; set; }
        // MainWindow Info
        public static int GroupNumbers { get; set; }
        // Person Self No ----
        public static int PersonNumbers { get; set; }
        // --------------------
        public static int NotPointNumbers { get; set; }
        public static int BigPoint20 { get; set; }
        public static int BigPoint10 = 0;
        public static int SmallPoint10 = 0;
        public static int HGPersonNumber = 0;
        // True And False
        public static bool TF = false;
        //See All Persons to Group Add
    //    public static bool FSTandSAP { get; set; }
        public static bool GroupAddorPointTable { get; set; }
        // Group Add Person
        public static ArrayList PersonsKeyName = new ArrayList();
        public static ArrayList PersonsKeyGroup = new ArrayList();
        public static ArrayList PersonsKeyNumber = new ArrayList();
        public static string Group { get; set; }
        public static string PName { get; set; }
        public static int SelectedNumber { get; set; }
        // Person Add
        public static bool Off { get; set; }
        // Person Name Change
        public static string PLastName { get; set; }
        //Person Point Add Select
        public static ArrayList PersonsKeyLastName = new ArrayList();
        // Point Delete Select
        public static ArrayList PersonsKeyPoint = new ArrayList();
        public static int Point { get; set; }
        // Point Table
        public static int SortNumber { get; set; }
        public static string TechValue1 { get; set; }
        public static string TechValue2 { get; set; }
        public static string TechValue3 { get; set; }
        // --Number
        public static int SelectedNumberFirst { get; set; }
        public static int SelectedNumberSecond { get; set; }
        public static int SelectedNumberThird { get; set; }
        // --Point
        public static int SelectedPointFirst { get; set; }
        public static int SelectedPointSecond { get; set; }
        public static int SelectedPointThird { get; set; }
        // --Name
        public static string SelectedNameFirst { get; set; }
        public static string SelectedNameSecond { get; set; }
        public static string SelectedNameThird { get; set; }
        // Self info
        public static ArrayList PersonsKeyWinner = new ArrayList();
        public static ArrayList KeySort = new ArrayList();
        public static ArrayList KeyYear = new ArrayList();

    }
}
