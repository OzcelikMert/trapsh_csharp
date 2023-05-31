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
    /// Interaction logic for PointAdd.xaml
    /// </summary>
    public partial class PointAdd : Window {
        public PointAdd() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {

                NowYear.IsChecked = true;
                WriteYear.Text = DateTime.Now.Year.ToString();
                NamePersn.Text = ClassValues.PName + " " + ClassValues.PLastName; ;

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

        private void PointAddBtn_Click(object sender, RoutedEventArgs e) {
            try {
                YearSelect(Convert.ToBoolean(NowYear.IsChecked.Value));
                AddPo();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void AddPo() {
            if (string.IsNullOrEmpty(FirstPoint.Text) || string.IsNullOrEmpty(SecondPoint.Text) || string.IsNullOrEmpty(ThirdPoint.Text)) {
                MessageBox.Show("Lütfen gerekli yerleri doldurunuz.", "Boş Kutu Mesajı", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                byte FirstPnt = Convert.ToByte(FirstPoint.Text);
                byte SecondPnt = Convert.ToByte(SecondPoint.Text);
                byte ThirdPnt = Convert.ToByte(ThirdPoint.Text);
                int TotalPoint = FirstPnt + SecondPnt + ThirdPnt;
                MessageBoxResult Mesasageone = MessageBox.Show("\"" + NamePersn.Text + "\"" + " isimindeki üyeye :\r1. Puanı : " + FirstPnt.ToString() + "\r2. Puanı : " + SecondPnt.ToString() + "\r3. Puanı : " + ThirdPnt.ToString() + "\rToplam Puanı : " + TotalPoint.ToString() + "\rBu puanları vermek istediğinize eminmisiniz ?", "Puan Ekleme Mesajı", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (Mesasageone == MessageBoxResult.OK) {
                    DBWorksClass.AddPoint(ClassValues.SelectedNumber, ClassValues.PName, ClassValues.PLastName, ClassValues.Group, TotalPoint, FirstPnt, SecondPnt, ThirdPnt, _Date);
                    if (NowYear.IsChecked == true) {

                        Close();

                    } else {
                        ;
                    }

                } else {
                    ;
                }
            }
        }

        private void FirstPoint_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) {
                e.Handled = false;
            } else if (e.Key == Key.Tab) {
                e.Handled = false;
            } else if (e.Key == Key.Back) {
                e.Handled = false;
            } else {
                e.Handled = true;
            }
        }

        private void SecondPoint_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) {
                e.Handled = false;
            } else if (e.Key == Key.Tab) {
                e.Handled = false;
            } else if (e.Key == Key.Back) {
                e.Handled = false;
            } else {
                e.Handled = true;
            }
        }

        private void ThirdPoint_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) {
                e.Handled = false;
            } else if (e.Key == Key.Tab) {
                e.Handled = false;
            } else if (e.Key == Key.Back) {
                e.Handled = false;
            } else {
                e.Handled = true;
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    YearSelect(Convert.ToBoolean(NowYear.IsChecked.Value));
                    AddPo();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        short _Date { get; set; }
        public void YearSelect(bool Value) {

            if (Value == true) {

                _Date = Convert.ToInt16(DateTime.Now.Year);


            } else {

                _Date = Convert.ToInt16(WriteYear.Text);

            }

        }

        private void WriteYear_PreviewKeyDown(object sender, KeyEventArgs e) {

            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) {
                e.Handled = false;
            } else if (e.Key == Key.Tab) {
                e.Handled = false;
            } else if (e.Key == Key.Back) {
                e.Handled = false;
            } else {
                e.Handled = true;
            }

        }

        private void NowYear_Checked(object sender, RoutedEventArgs e) {

            WriteYear.IsEnabled = false;

        }

        private void ChangeYear_Checked(object sender, RoutedEventArgs e) {

            WriteYear.IsEnabled = true;

        }
    }
}
