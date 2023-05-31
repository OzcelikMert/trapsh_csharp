using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrapshClassesDLL;

namespace Trapsh {
    /// <summary>
    /// Interaction logic for PointTable.xaml
    /// </summary>
    public partial class PointTable : Window {
        public PointTable() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                ClassValues.SortNumber = 0;
                TryList();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Red;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Transparent;
        }

        private void Exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Close();
        }

        private void GroupNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                GoldPerson.Text = "Yok";
                IronPerson.Text = "Yok";
                BronzePerson.Text = "Yok";
                DBWorksClass.ShowYears(Years, GroupNames.SelectedValue.ToString());
                if (Years.Items.Count > 0) {
                    Years.SelectedIndex = 0;
                }
                TryListPersons();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

        }

        public void TryList() {

            DBWorksClass.TryShowListGroups(GroupNames);

        }

        int SelectedPersonNumber = 0;
        public void SelectedIndexName() {
            SelectedPersonNumber = PersonNames.SelectedIndex;
        }

        public void TryListPersons() {
            GoldPerson.Text = "Yok";
            IronPerson.Text = "Yok";
            BronzePerson.Text = "Yok";
            DBWorksClass.PTSortPersons(GroupNames.SelectedValue.ToString(), Convert.ToInt32(Years.SelectedValue));
            BestList();
        }

        public void BestList() {

            DBWorksClass.PT_FSTSort(GroupNames.SelectedValue.ToString(), Convert.ToInt32(Years.SelectedValue));
            GoldPerson.Text = ClassValues.TechValue1;
            IronPerson.Text = ClassValues.TechValue2;
            BronzePerson.Text = ClassValues.TechValue3;

        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            try {
                if (PersonNames.SelectedIndex == 3 && ClassValues.SortNumber != 0) {
                    ClassValues.Group = GroupNames.SelectedValue.ToString();
                    ClassValues.GroupAddorPointTable = true;
                    ClassValues.PublicYear = Convert.ToInt32(Years.SelectedValue);
                    ChangeWindowClass.SeeAlls();
                } else {
                    ;
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Years_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                ClassValues.PublicYear = Convert.ToInt32(Years.SelectedValue);
                TryListPersons();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
