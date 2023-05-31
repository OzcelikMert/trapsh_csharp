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
    /// Interaction logic for PersonDelete.xaml
    /// </summary>
    public partial class PersonDelete : Window {
        public PersonDelete() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                DBWorksClass.PDPersonShow(PersonNames);
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Red;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Red;
        }

        private void Exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Close();
        }

        private void PersonNameChangeBtn_Click(object sender, RoutedEventArgs e) {
            try {
                SelectedIndexName();
                if (FreeListError == true) {
                    MessageBox.Show("Listede isimi güncellenecek yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    ChangeWindowClass.PersonNameChangee();
                    DBWorksClass.PDPersonShow(PersonNames);
                    PersonNames.SelectedIndex = 0;
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void PND() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede silinecek yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                MessageBoxResult DeleteNameMessage = MessageBox.Show(" Listede \"" + ClassValues.PName + "\" adıyla bulunan bu üyeyi silmek istediğinize eminmisiniz ?", "Silme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteNameMessage == MessageBoxResult.Yes) {
                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    DBWorksClass.PersonDelete(ClassValues.SelectedNumber);
                    PersonNames.Items.Clear();
                    DBWorksClass.PDPersonShow(PersonNames);
                    PersonNames.SelectedIndex = 0;
                } else {
                    ;
                }
            }
        }

        private void PersonNameDeleteBtn_Click(object sender, RoutedEventArgs e) {
            try {
                PND();
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
                ClassValues.PName = PersonNames.SelectedValue.ToString();
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
                    PND();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}

