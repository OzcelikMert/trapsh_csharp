using System;
using System.Collections;
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
    /// Interaction logic for SeeAllPersons.xaml
    /// </summary>
    public partial class SeeAllPersons : Window {
        public SeeAllPersons() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                ClassValues.PersonsKeyNumber.Clear();
                GroupName.Text = ClassValues.Group;
                // True = Point Table False = Group Add
                if (ClassValues.GroupAddorPointTable) {

                    YearsGrid.Visibility = Visibility.Visible;
                    DBWorksClass.ShowYears(YearsBox, ClassValues.Group);

                    if (ClassValues.PublicYear != 0 && YearsBox.Items.Count > 0) {

                        YearsBox.SelectedItem = ClassValues.PublicYear.ToString();

                    } else if (YearsBox.Items.Count > 0) {

                        YearsBox.SelectedIndex = 0;
                        ClassValues.PublicYear = YearsBox.SelectedIndex = 0;

                    }

                    YearValue.Text = "~ " + ClassValues.PublicYear + " ~";
                    DBWorksClass.PersonsShow_SortPoints(PersonNames, Convert.ToInt32(YearsBox.SelectedValue));

                } else {

                    YearsGrid.Visibility = Visibility.Hidden;
                    DBWorksClass.PersonsShow_NotPoints(PersonNames, ClassValues.Group);

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

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            try {

                ClassValues.PublicYear = Convert.ToInt32(YearsBox.SelectedValue);
                ClassValues.PersonNumbers = Convert.ToInt32(ClassValues.PersonsKeyNumber[PersonNames.SelectedIndex]);
                ChangeWindowClass.FirstSecondThirds();

            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void YearsBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                ClassValues.PersonsKeyNumber.Clear();
                DBWorksClass.PersonsShow_SortPoints(PersonNames, Convert.ToInt32(YearsBox.SelectedValue));
            } catch (Exception Error) {

                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();

            }
        }
    }

    public class AddCmboBoxItem {
        public AddCmboBoxItem(string sort, string text, string imagePath) {
            this.SortP = sort;
            this.TextYear = text;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(imagePath, UriKind.Relative);
            src.EndInit();
            this.ImageWin = src;
        }
        public  string SortP { get; set; }
        public string TextYear { get; set; }
        public BitmapImage ImageWin { get; set; }
    }

}
    