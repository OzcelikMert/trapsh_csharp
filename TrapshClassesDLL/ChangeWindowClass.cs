using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trapsh;

namespace TrapshClassesDLL {
   public class ChangeWindowClass {

        static public void GroupAdds() {
            GroupAdd GA = new GroupAdd();
            GA.ShowDialog();
        }

        static public void GroupChangeAndDeletes() {
            GroupChangeAndDelete GCD = new GroupChangeAndDelete();
            GCD.ShowDialog();
        }

        static public void GroupChange() {
            ChangeGroupName GC = new ChangeGroupName();
            GC.ShowDialog();
        }

        static public void PersonAdds() {
            PersonAdd PA = new PersonAdd();
            PA.ShowDialog();
        }

        static public void PersonGroupAdds() {
            PersonGroupAdd PGA = new PersonGroupAdd();
            PGA.ShowDialog();
        }

        static public void PersonDeletee() {
            PersonDelete PD = new PersonDelete();
            PD.ShowDialog();
        }

        static public void PersonNameChangee() {
            PersonNameChange PNC = new PersonNameChange();
            PNC.ShowDialog();
        }

        static public void DeletePersonGroups() {
            GroupDeletePerson DPG = new GroupDeletePerson();
            DPG.ShowDialog();
        }

        static public void AddPersonGroups() {
            GroupAddPerson APG = new GroupAddPerson();
            APG.ShowDialog();
        }

        static public void AddAndDeletePoints() {
            PointAddSelect ADP = new PointAddSelect();
            ADP.ShowDialog();
        }

        static public void AddPoints() {
            PointAdd AP = new PointAdd();
            AP.ShowDialog();
        }

        static public void DeletesPoints() {
            PointDeleteSelect DP = new PointDeleteSelect();
            DP.ShowDialog();
        }

        static public void PointTables() {
            PointTable PT = new PointTable();
            PT.ShowDialog();
        }

        static public void FirstSecondThirds() {
            Selfinfo FST = new Selfinfo();
            FST.Show();
        }


        static public void SeeAlls() {
            SeeAllPersons SAP = new SeeAllPersons();
            SAP.ShowDialog();
        }

        static public void SaveUsersSorts() {

            SaveUsersSort SUS = new SaveUsersSort();
            SUS.ShowDialog();

        }
        static public void MainWindows() {

            MainWindow MW = new MainWindow();
            MW.Show();

        }

    }
}