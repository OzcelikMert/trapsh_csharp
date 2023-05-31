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
    /// Interaction logic for SaveUsersSort.xaml
    /// </summary>
    public partial class SaveUsersSort : Window {
        public SaveUsersSort() {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            try {
                if (YearSelect.Items.Count > 0) {

                    var Mesajcik = MessageBox.Show(YearSelect.SelectedValue.ToString() + " yılının sıralamasını kaydetmek istediğinize eminmisiniz ?\r(Aradan 1 yıl geçmediği sürece kaydedilen her sıralama eski sıralamayı yeniler.)", "Sıralamayı Kayıt Etme", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Mesajcik == MessageBoxResult.Yes) {
                        DBWorksClass.MainWindow_SaveDB(Convert.ToInt32(YearSelect.SelectedValue));
                        DBWorksClass.ShowYears_SUS(YearSelect);
                        YearSelect.SelectedIndex = 0;
                    } else {
                        ;
                    }
                } else {
                    MessageBox.Show("Yeterli Yıl Bulunmamaktadır!","Hata",MessageBoxButton.OK,MessageBoxImage.Error);
                }

            } catch (Exception a) {
                MessageBox.Show("Kayıt listesi alınırken bir hata meydana geldi. Hata Sebebi : " + a.ToString(), "HATA!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
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

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            DBWorksClass.ShowYears_SUS(YearSelect);
            YearSelect.SelectedIndex = 0;

        }
    }
}
