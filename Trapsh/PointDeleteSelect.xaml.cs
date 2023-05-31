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
    /// Interaction logic for PointDeleteSelect.xaml
    /// </summary>
    public partial class PointDeleteSelect : Window {
        public PointDeleteSelect() {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                DBWorksClass.ShowYears_PDS(YearsBox);
                YearsBox.SelectedIndex = 0;
                DBWorksClass.TryShowListGroups(GroupNames);
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void PointDeleteBtn_Click(object sender, RoutedEventArgs e) {
            try {
                DeleteP();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void DeleteP() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede puanı silinecek yeterli grup yada yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                ClassValues.PName = ClassValues.PersonsKeyName[SelectedPersonNumber].ToString();
                ClassValues.Point = Convert.ToInt32(ClassValues.PersonsKeyPoint[SelectedPersonNumber]);
                MessageBoxResult DeleteNameMessage = MessageBox.Show("Listede \"" + ClassValues.PName + "\" adıyla bulunan bu üyenin \"" + ClassValues.Point.ToString() + "\" puanını silmek istediğinize eminmisiniz ?", "Silme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteNameMessage == MessageBoxResult.Yes) {
                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    DBWorksClass.DeletePoint(ClassValues.SelectedNumber, Convert.ToInt32(YearsBox.SelectedItem));
                    PersonNames.Items.Clear();
                    TryListPersons();
                    PersonNames.SelectedIndex = 0;
                } else {
                    ;
                }
            }
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
            DBWorksClass.PDSTryListPersons(PersonNames, ClassValues.Group, Convert.ToInt32(YearsBox.SelectedValue));
            PersonNames.SelectedIndex = 0;

        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Delete) {
                    DeleteP();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
