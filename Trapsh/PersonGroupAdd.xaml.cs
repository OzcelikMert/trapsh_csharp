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
    /// Interaction logic for PersonGroupAdd.xaml
    /// </summary>
    public partial class PersonGroupAdd : Window {
        public PersonGroupAdd() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                ClassValues.Off = true;
                NameText.Text = ClassValues.PName;
                DBWorksClass.TryShowListGroups(GroupNames);
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
            ClassValues.Off = true;
            Close();
        }

        public void PSG() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede kayıdı alınacak üyenin girebileceği yeterli grup yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                ClassValues.Off = true;
            } else {
                MessageBoxResult DeleteNameMessage = MessageBox.Show("\"" + NameText.Text + "\" adlı kayıt edilecek üyeyi \"" + ClassValues.Group + "\"  adı ile bulunan bu gruba eklemek istediğinize eminmisiniz ?", "Gruba Katılma Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteNameMessage == MessageBoxResult.Yes) {
                    ClassValues.Off = false;
                    Close();
                } else {
                    ;
                }
            }
        }

        private void PersonSlctGroupBtn_Click(object sender, RoutedEventArgs e) {
            try {
                PSG();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        bool FreeListError = false;
        public void SelectedIndexName() {
            FreeListError = false;
            if (GroupNames.Items.Count > 0) {
                ClassValues.Group = GroupNames.SelectedValue.ToString();
            } else {
                FreeListError = true;
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    PSG();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
