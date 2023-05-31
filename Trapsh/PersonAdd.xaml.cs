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
    /// Interaction logic for PersonAdd.xaml
    /// </summary>
    public partial class PersonAdd : Window {
        public PersonAdd() {
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
                DBWorksClass.PersonsShow(PersonNames);
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void AddPe() {
            if ((string.IsNullOrEmpty(PersonNameTxt.Text) || string.IsNullOrEmpty(PersonLastNameTxt.Text)) || (string.IsNullOrEmpty(PersonNameTxt.Text) && string.IsNullOrEmpty(PersonLastNameTxt.Text)) || (PersonNameTxt.Foreground == Brushes.DarkSlateGray || PersonLastNameTxt.Foreground == Brushes.DarkSlateGray)) {
                MessageBox.Show("Boş Yer Bıkma ! ", "Boş Bırakma Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {

                DBWorksClass.PersonAddControl(PersonNameTxt.Text, PersonLastNameTxt.Text);
                if (!DBWorksClass.Off_2) {

                ChangeWindowClass.PersonGroupAdds();
                DBWorksClass.PersonAdd(ClassValues.Off, PersonNameTxt.Text, PersonLastNameTxt.Text);

                }
                DBWorksClass.PersonsShow(PersonNames);
                PersonNameTxt.Foreground = Brushes.DarkSlateGray;
                PersonLastNameTxt.Foreground = Brushes.DarkSlateGray;
                PersonNameTxt.Text = "İsim";
                PersonLastNameTxt.Text = "Soyisim";
                PersonNameTxt.Focus();

            }
        }

        private void PersonNameBtn_Click(object sender, RoutedEventArgs e) {
            try {
                AddPe();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void PersonNameTxt_KeyDown(object sender, KeyEventArgs e) {
            if (PersonNameTxt.Foreground == Brushes.DarkSlateGray) {

                PersonNameTxt.Text = "";
                PersonNameTxt.Foreground = Brushes.Black;

            } else {
                ;
            }
        }

        private void PersonNameTxt_MouseLeave(object sender, MouseEventArgs e) {
            if (string.IsNullOrEmpty(PersonNameTxt.Text)) {

                PersonNameTxt.Foreground = Brushes.DarkSlateGray;
                PersonNameTxt.Text = "İsim";

            } else {
                ;
            }
        }

        private void PersonNameTxt_MouseEnter(object sender, MouseEventArgs e) {
            if (PersonNameTxt.Foreground == Brushes.DarkSlateGray) {

                PersonNameTxt.Text = "";
                PersonNameTxt.Foreground = Brushes.Black;

            } else {
                ;
            }
        }

        private void PersonLastNameTxt_KeyDown(object sender, KeyEventArgs e) {
            if (PersonLastNameTxt.Foreground == Brushes.DarkSlateGray) {

                PersonLastNameTxt.Text = "";
                PersonLastNameTxt.Foreground = Brushes.Black;

            } else {
                ;
            }
        }

        private void PersonLastNameTxt_MouseLeave(object sender, MouseEventArgs e) {
            if (string.IsNullOrEmpty(PersonLastNameTxt.Text)) {

                PersonLastNameTxt.Foreground = Brushes.DarkSlateGray;
                PersonLastNameTxt.Text = "Soyisim";

            } else {
                ;
            }
        }

        private void PersonLastNameTxt_MouseEnter(object sender, MouseEventArgs e) {
            if (PersonLastNameTxt.Foreground == Brushes.DarkSlateGray) {

                PersonLastNameTxt.Text = "";
                PersonLastNameTxt.Foreground = Brushes.Black;

            } else {
                ;
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    AddPe();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}

