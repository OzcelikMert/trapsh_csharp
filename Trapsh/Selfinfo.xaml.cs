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
    /// Interaction logic for Selfinfo.xaml
    /// </summary>
    public partial class Selfinfo : Window {
        public Selfinfo() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
            
            FirstTool = true;
            YearsSee();
                if (ClassValues.PublicYear != 0 && Years.Items.Count > 0) {

                    int index = ClassValues.KeyYear.IndexOf(ClassValues.PublicYear.ToString());
                    Years.SelectedIndex = index;
                    CMBOXADDYEAR(ClassValues.PublicYear, true);
                    ImageSort();

                } else if (Years.Items.Count > 0) {

                    Years.SelectedIndex = 0;
                    CMBOXADDYEAR(Convert.ToInt32(ClassValues.KeyYear[0]), true);
                    ImageSort();

                } else {

                    CMBOXADDYEAR(0, false);

                }

            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.\n"+ "Hata Sebebi : " + Error.ToString(),"Hata!" , MessageBoxButton.OK, MessageBoxImage.Error);
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

        public class AddCmboBoxItem {
            public AddCmboBoxItem(string text, string imagePath) {
                this.TextYear = text;
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(imagePath, UriKind.Relative);
                src.EndInit();
                this.ImageWin = src;
            }
            public string TextYear { get; set; }
            public BitmapImage ImageWin { get; set; }
        }

        bool FirstTool = false;
        private void Years_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if (FirstTool == false) {
                    CMBOXADDYEAR(Convert.ToInt32(ClassValues.KeyYear[Years.SelectedIndex]), true);
                    ImageSort();
                } else {
                    FirstTool = false;
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public void YearsSee() {

            ClassValues.PersonsKeyWinner.Clear();
            ClassValues.KeySort.Clear();
            ClassValues.KeyYear.Clear();
            DBWorksClass.YearsInSort(Years, ClassValues.PersonNumbers);

        }

        public void CMBOXADDYEAR(int Year, bool Year_or_NotYear) {

            if (Year_or_NotYear) {

            DBWorksClass.YearsInValues(ClassValues.PersonNumbers, Year);
            PersonName.Text = ClassValues.PName;
            PersonLastName.Text = ClassValues.PLastName;
            GroupName.Text = ClassValues.Group;
            PointOne.Text = ClassValues.SmallPoint10.ToString();
            PointTwo.Text = ClassValues.BigPoint10.ToString();
            PointThree.Text = ClassValues.BigPoint20.ToString();
            TotalPoint.Text = ClassValues.Point.ToString();

            } else {

                DBWorksClass.Not_YearsInValues(ClassValues.PersonNumbers);
                PersonName.Text = ClassValues.PName;
                PersonLastName.Text = ClassValues.PLastName;
                GroupName.Text = ClassValues.Group;

            }


        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void small_MouseEnter(object sender, MouseEventArgs e) {
            small.Background = Brushes.Gray;
        }

        private void small_MouseLeave(object sender, MouseEventArgs e) {
            small.Background = Brushes.Transparent;
        }

        private void small_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        public void ImageSort() {

            if (Convert.ToInt32(ClassValues.PersonsKeyWinner[Years.SelectedIndex]) == 1) {
                WinnerSortText.Text = "";
                Winner.Source = (ImageSource)FindResource("Firstpng");
                Winner.Visibility = Visibility.Visible;
            } else if (Convert.ToInt32(ClassValues.PersonsKeyWinner[Years.SelectedIndex]) == 2) {
                WinnerSortText.Text = "";
                Winner.Source = (ImageSource)FindResource("Secondpng");
                Winner.Visibility = Visibility.Visible;
            } else if (Convert.ToInt32(ClassValues.PersonsKeyWinner[Years.SelectedIndex]) == 3) {
                WinnerSortText.Text = "";
                Winner.Source = (ImageSource)FindResource("Thirdpng");
                Winner.Visibility = Visibility.Visible;
            } else {
                WinnerSortText.Text = "#" + ClassValues.KeySort[Years.SelectedIndex].ToString();
                Winner.Visibility = Visibility.Hidden;
            }

        }
    }
}
