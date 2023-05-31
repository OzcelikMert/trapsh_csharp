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
    /// Interaction logic for PersonNameChange.xaml
    /// </summary>
    public partial class PersonNameChange : Window {
        public PersonNameChange() {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {

                DBWorksClass.SearchPerson(ClassValues.SelectedNumber);
                if (DBWorksClass.FindCrash) {
                    Close();
                } else {
                    NameTxtbox.Text = ClassValues.PName;
                    LastNameTxtbox.Text = ClassValues.PLastName;
                }
               
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

        public void NB() {
            if (string.IsNullOrEmpty(NameTxtbox.Text) || string.IsNullOrEmpty(LastNameTxtbox.Text) || (string.IsNullOrEmpty(NameTxtbox.Text) && string.IsNullOrEmpty(LastNameTxtbox.Text))) {
                MessageBox.Show("Lütfen boş yer bırakmayınız.", "Boş yer hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                DBWorksClass.TrueAndFalse_2(NameTxtbox.Text, LastNameTxtbox.Text);
                MessageBoxResult RecordMessage = MessageBox.Show("\"" + ClassValues.PName + " " + ClassValues.PLastName + "\" isimi ve soyisimi bulunan bu üye " + "\"" + NameTxtbox.Text + " " + LastNameTxtbox.Text + "\" olacak şekilde yeni bir isim ve soyisim olarak değiştirilsin mi ?", "İsim ve Soyisim Değiştirme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (RecordMessage == MessageBoxResult.Yes && ClassValues.TF == false) {

                    DBWorksClass.ChangePersonName(ClassValues.SelectedNumber, NameTxtbox.Text, LastNameTxtbox.Text);
                    Close();

                } else if (ClassValues.TF == true && RecordMessage != MessageBoxResult.No) {
                    MessageBox.Show("\"" + NameTxtbox.Text + " " + LastNameTxtbox.Text + "\" Adında bir üye zaten var.", "İsim benzerliği Hatası", MessageBoxButton.OK, MessageBoxImage.Stop);
                } else {
                    ;
                }

            }
        }

        private void NameBtn_Click(object sender, RoutedEventArgs e) {
            try {
                NB();
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
                    NB();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
