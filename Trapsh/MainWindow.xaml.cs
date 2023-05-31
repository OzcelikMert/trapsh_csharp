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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using TrapshClassesDLL;
using System.Diagnostics;

namespace Trapsh {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Hide_MouseEnter(object sender, MouseEventArgs e) {
            Hide.Background = Brushes.Gray;
        }

        private void Hide_MouseLeave(object sender, MouseEventArgs e) {
            Hide.Background = Brushes.Transparent;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Red;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e) {
            Exit.Background = Brushes.Transparent;
        }

        private void Exit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            MessageBoxResult ExitMessage = MessageBox.Show("Çıkmak istediğinize eminmisiniz ?", "Çıkış Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ExitMessage == MessageBoxResult.Yes) {
                Application.Current.Shutdown();
            } else {
                ;
            }
        }

        private void Hide_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void GroupAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.GroupAdds();
        }

        private void GroupDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.GroupChangeAndDeletes();
        }

        private void PersonAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.PersonAdds();
        }

        private void PersonDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.PersonDeletee();
        }

        private void GroupAddP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.AddPersonGroups();
        }

        private void GroupDeleteP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.DeletePersonGroups();
        }

        private void PointAddDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.AddAndDeletePoints();
        }

        private void PointDeletes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.DeletesPoints();
        }

        private void PointShow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.PointTables();
        }

        private void SaveG_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ChangeWindowClass.SaveUsersSorts();
        }

        private void Help_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            try {
                Process.Start("Open Help.bat");
                Process.Start("https://www.ozceliksoftware.com/");
            } catch (Exception Error) {
                MessageBox.Show("Gerekli bilgiler alınırken bir hata meydana geldi. Hata Sebebi : " + Error.ToString(), "HATA!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GroupAdd_MouseEnter(object sender, MouseEventArgs e) {
            GroupAdd.Background = Brushes.LightGray;
        }

        private void GroupAdd_MouseLeave(object sender, MouseEventArgs e) {
            GroupAdd.Background = Brushes.Transparent;
        }

        private void GroupDelete_MouseEnter(object sender, MouseEventArgs e) {
            GroupDelete.Background = Brushes.LightGray;
        }

        private void GroupDelete_MouseLeave(object sender, MouseEventArgs e) {
            GroupDelete.Background = Brushes.Transparent;
        }

        private void GroupAddP_MouseEnter(object sender, MouseEventArgs e) {
            GroupAddP.Background = Brushes.LightGray;
        }

        private void GroupAddP_MouseLeave(object sender, MouseEventArgs e) {
            GroupAddP.Background = Brushes.Transparent;
        }

        private void PersonAdd_MouseEnter(object sender, MouseEventArgs e) {
            PersonAdd.Background = Brushes.LightGray;
        }

        private void PersonAdd_MouseLeave(object sender, MouseEventArgs e) {
            PersonAdd.Background = Brushes.Transparent;
        }

        private void PersonDelete_MouseEnter(object sender, MouseEventArgs e) {
            PersonDelete.Background = Brushes.LightGray;
        }

        private void PersonDelete_MouseLeave(object sender, MouseEventArgs e) {
            PersonDelete.Background = Brushes.Transparent;
        }

        private void GroupDeleteP_MouseEnter(object sender, MouseEventArgs e) {
            GroupDeleteP.Background = Brushes.LightGray;
        }

        private void GroupDeleteP_MouseLeave(object sender, MouseEventArgs e) {
            GroupDeleteP.Background = Brushes.Transparent;
        }

        private void PointAddDelete_MouseEnter(object sender, MouseEventArgs e) {
            PointAddDelete.Background = Brushes.LightGray;
        }

        private void PointAddDelete_MouseLeave(object sender, MouseEventArgs e) {
            PointAddDelete.Background = Brushes.Transparent;
        }

        private void PointDeletes_MouseEnter(object sender, MouseEventArgs e) {
            PointDeletes.Background = Brushes.LightGray;
        }

        private void PointDeletes_MouseLeave(object sender, MouseEventArgs e) {
            PointDeletes.Background = Brushes.Transparent;
        }

        private void PointShow_MouseEnter(object sender, MouseEventArgs e) {
            PointShow.Background = Brushes.LightGray;
        }

        private void PointShow_MouseLeave(object sender, MouseEventArgs e) {
            PointShow.Background = Brushes.Transparent;
        }

        private void ToppGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();

        }

        private void Quest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            try {
                // Users Info
                DBWorksClass.MainWindow_Info();
                // Groups Sort Info
                DBWorksClass.MainWindow_Info_2();
            } catch (Exception a) {

                MessageBox.Show("Gerekli bilgiler alınırken bir hata meydana geldi. Hata Sebebi : " + a.ToString(), "HATA!!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
