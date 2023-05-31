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
using System.Data.SQLite;
using TrapshClassesDLL;

namespace Trapsh {
    /// <summary>
    /// Interaction logic for GroupAdd.xaml
    /// </summary>
    public partial class GroupAdd : Window {
        public GroupAdd() {
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

            private void GoupNameBtn_Click(object sender, RoutedEventArgs e) {
                try {

                if (string.IsNullOrEmpty(GroupNameTxt.Text)) {
                    MessageBox.Show("Lütfen boş yer bırakmayınız.", "Boş yer hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    DBWorksClass.TrueOrFalse(GroupNameTxt.Text);
                    MessageBoxResult RecordMessage = MessageBox.Show("\"" + GroupNameTxt.Text + "\" Adında bir grup oluşturulsun mu ?", "Grup Oluşturma Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (RecordMessage == MessageBoxResult.Yes && ClassValues.TF == false) {

                        DBWorksClass.CreateGroup(GroupNameTxt.Text);
                        GroupNames.Items.Clear();
                        DBWorksClass.TryShowListGroups(GroupNames);
                        GroupNameTxt.Text = "";

                    } else if (ClassValues.TF == true && RecordMessage != MessageBoxResult.No) {

                        MessageBox.Show("\"" + GroupNameTxt.Text + "\" Adında bir grubunuz zaten var.", "İsim benzerliği Hatası", MessageBoxButton.OK, MessageBoxImage.Stop);

                    } else {
                        ;
                    }

                }

                } catch (Exception Error) {
                    MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            try {
                ClassValues.Group = GroupNames.SelectedValue.ToString();
                ClassValues.GroupAddorPointTable = false;
                ClassValues.PublicYear = 0;
                ChangeWindowClass.SeeAlls();
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
                    e.Handled = true;
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}

