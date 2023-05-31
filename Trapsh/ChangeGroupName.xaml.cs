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
    /// Interaction logic for ChangeGroupName.xaml
    /// </summary>
    public partial class ChangeGroupName : Window {
        public ChangeGroupName() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            NameTextGroup.Text = ClassValues.Group;
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

        public void GNC() {
            if (string.IsNullOrEmpty(GroupNameTxt.Text)) {
                MessageBox.Show("Lütfen boş yer bırakmayınız.", "Boş yer hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                DBWorksClass.TrueOrFalse(GroupNameTxt.Text);
                MessageBoxResult RecordMessage = MessageBox.Show("\"" + NameTextGroup.Text + "\" adı olan grup " + "\"" + GroupNameTxt.Text + "\" adında bir grup olarak değiştirilsin mi ?", "Grup Adı Değiştirme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (RecordMessage == MessageBoxResult.Yes && ClassValues.TF == false) {

                    DBWorksClass.UpdateGroups(ClassValues.Group, GroupNameTxt.Text);
                    Close();
                } else if (ClassValues.TF == true && RecordMessage != MessageBoxResult.No) {
                    MessageBox.Show("\"" + GroupNameTxt.Text + "\" Adında bir grubunuz zaten var.", "İsim benzerliği Hatası", MessageBoxButton.OK, MessageBoxImage.Stop);
                } else {
                    ;
                }

            }
        }

        private void GoupNameBtn_Click(object sender, RoutedEventArgs e) {
            try {
                GNC();
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
                    GNC();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

    }
}
