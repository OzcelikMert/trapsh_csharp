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
    /// Interaction logic for GroupChangeAndDelete.xaml
    /// </summary>
    public partial class GroupChangeAndDelete : Window {
        public GroupChangeAndDelete() {
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

            private void Exit_MouseEnter(object sender, MouseEventArgs e) {
                Exit.Background = Brushes.Red;
            }

            private void Exit_MouseLeave(object sender, MouseEventArgs e) {
                Exit.Background = Brushes.Transparent;
            }

            private void Exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
                Close();
            }

            private void GroupNameChangeBtn_Click(object sender, RoutedEventArgs e) {
                try {
                    SelectedIndexName();
                    if (FreeListError == true) {
                        MessageBox.Show("Listede isimi güncellenecek yeterli grup yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                    } else {
                        ChangeWindowClass.GroupChange();
                        GroupNames.Items.Clear();
                        DBWorksClass.TryShowListGroups(GroupNames);
                        GroupNames.SelectedIndex = 0;
                    }
                } catch (Exception Error) {
                    MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }

            public void GND() {
                SelectedIndexName();
                if (FreeListError == true) {
                    MessageBox.Show("Listede silinecek yeterli grup yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    MessageBoxResult DeleteNameMessage = MessageBox.Show(" Listede \"" + ClassValues.Group + "\" adıyla bulunan bu grubu silmek istediğinize eminmisiniz ?", "Silme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (DeleteNameMessage == MessageBoxResult.Yes) {

                    DBWorksClass.GroupsDelete(ClassValues.Group);
                    GroupNames.Items.Clear();
                        DBWorksClass.TryShowListGroups(GroupNames);
                        GroupNames.SelectedIndex = 0;
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

        bool FreeListError = false;
        public void SelectedIndexName() {
            FreeListError = false;
            if (GroupNames.Items.Count > 0) {
                ClassValues.Group = GroupNames.SelectedItem.ToString();
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
    }
}
