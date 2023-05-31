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
    /// Interaction logic for GroupDeletePerson.xaml
    /// </summary>
    public partial class GroupDeletePerson : Window {
        public GroupDeletePerson() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {

                ClassValues.PersonsKeyGroup.Clear();
                DBWorksClass.GDPShowGroup(GroupNames);

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

        public void GND() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede gruptan çıkarılacak yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                ClassValues.PName = ClassValues.PersonsKeyName[SelectedPersonNumber].ToString();
                ClassValues.Group = ClassValues.PersonsKeyGroup[SelectedPersonNumber].ToString();
                MessageBoxResult DeleteNameMessage = MessageBox.Show(" Listede \"" + ClassValues.PName + "\" adıyla bulunan bu üyeyi \"" + ClassValues.Group + "\" grubundan çıkartmak istediğinize eminmisiniz ?", "Çıkartma Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteNameMessage == MessageBoxResult.Yes) {
                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    DBWorksClass.GAPUpdatePersonsGroup(ClassValues.SelectedNumber, "");
                    ClassValues.PersonsKeyNumber.Clear();
                    ClassValues.PersonsKeyName.Clear();
                    DBWorksClass.GDPShowPerson(PersonNames, GroupNames.SelectedValue.ToString());
                    PersonNames.SelectedIndex = 0;

                } else {
                    ;
                }
            }
        }

        private void GroupNameDeleteBtn_Click(object sender, RoutedEventArgs e) {
            try {
                GND();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        int SelectedPersonNumber = 0;
        bool FreeListError = false;
        public void SelectedIndexName() {
            FreeListError = false;
            if (PersonNames.Items.Count > 0) {
                SelectedPersonNumber = PersonNames.SelectedIndex;
            } else {
                FreeListError = true;
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Delete) {
                    GND();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void GroupNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                ClassValues.PersonsKeyNumber.Clear();
                ClassValues.PersonsKeyName.Clear();
                DBWorksClass.GDPShowPerson(PersonNames, GroupNames.SelectedValue.ToString());
                PersonNames.SelectedIndex = 0;
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}



