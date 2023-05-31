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
    /// Interaction logic for PointAddSelect.xaml
    /// </summary>
    public partial class PointAddSelect : Window {
        public PointAddSelect() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                DBWorksClass.TryShowListGroups(GroupNames);
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void AADP() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede puan eklenecek yeterli grup yada yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                if (PersonNames.SelectedIndex >= 0) {

                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    ClassValues.PName = ClassValues.PersonsKeyName[SelectedPersonNumber].ToString();
                    ClassValues.PLastName = ClassValues.PersonsKeyLastName[SelectedPersonNumber].ToString();
                    ChangeWindowClass.AddPoints();
                    PersonNames.Items.Clear();
                    TryListPersons();
                } else {
                    MessageBox.Show("Lütfen listeden bir üye seçin.", "Seçilmemiş Üye Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PointAddBtn_Click(object sender, RoutedEventArgs e) {
            try {
                AADP();
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

        int SelectedPersonNumber = 0;
        int SelectedGroupNumber = 0;
        bool FreeListError = false;
        public void SelectedIndexName() {
            FreeListError = false;
            if (PersonNames.Items.Count > 0 && GroupNames.Items.Count > 0) {
                SelectedPersonNumber = PersonNames.SelectedIndex;
                SelectedGroupNumber = GroupNames.SelectedIndex;
            } else {
                FreeListError = true;
            }
        }

        public void TryListPersons() {

            ClassValues.Group = GroupNames.SelectedValue.ToString();
            DBWorksClass.PointSelectWindowSort_User(PersonNames, ClassValues.Group);

        }

        private void GroupNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                TryListPersons();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void PersonNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                SelectedIndexName();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    AADP();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}

