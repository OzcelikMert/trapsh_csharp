using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrapshClassesDLL;

namespace Trapsh {
    /// <summary>
    /// Interaction logic for LoaderWindow.xaml
    /// </summary>
    public partial class LoaderWindow : Window {

        public LoaderWindow() {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {

            await Task.Run(() =>
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();
                DBControlClass.Create_DB_All();
                watch.Stop();
                var elapsedMs = watch.Elapsed.Milliseconds;
                
                for (int i = 0; i <elapsedMs; i++) {

                LoaderBar.Dispatcher.Invoke(() => LoaderBar.Value = i, DispatcherPriority.Background);
                Thread.Sleep(20);

                }

            });

            this.Hide();
            ChangeWindowClass.MainWindows();

        }

    }
}
