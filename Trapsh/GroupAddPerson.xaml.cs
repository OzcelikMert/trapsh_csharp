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
    /// Interaction logic for GroupAddPerson.xaml
    /// </summary>
    public partial class GroupAddPerson : Window {
        public GroupAddPerson() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                ClassValues.PersonsKeyGroup.Clear();
                ClassValues.PersonsKeyNumber.Clear();
                ClassValues.PersonsKeyName.Clear();
                PersonNames.Items.Clear();
                GroupNames.Items.Clear();
                DBWorksClass.GAPShowGroupAndPerson(PersonNames,GroupNames);
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

        public void GPA() {
            SelectedIndexName();
            if (FreeListError == true) {
                MessageBox.Show("Listede eklenecek yeterli grup yada yeterli üye yoktur.", "Sayı Yetersizliği Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                ClassValues.Group = ClassValues.PersonsKeyGroup[SelectedGroupNumber].ToString();
                ClassValues.PName = ClassValues.PersonsKeyName[SelectedPersonNumber].ToString();
                MessageBoxResult DeleteNameMessage = MessageBox.Show(" Listede \"" + ClassValues.PName + "\" adıyla bulunan bu üyeyi \"" + ClassValues.Group + "\" grubuna Eklemek istediğinize eminmisiniz ?", "Ekleme Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (DeleteNameMessage == MessageBoxResult.Yes) {
                    ClassValues.SelectedNumber = Convert.ToInt32(ClassValues.PersonsKeyNumber[SelectedPersonNumber]);
                    ClassValues.PersonsKeyGroup.Clear();
                    ClassValues.PersonsKeyNumber.Clear();
                    ClassValues.PersonsKeyName.Clear();
                    PersonNames.Items.Clear();
                    GroupNames.Items.Clear();
                    DBWorksClass.GAPUpdatePersonsGroup(ClassValues.SelectedNumber, ClassValues.Group);
                    DBWorksClass.GAPShowGroupAndPerson(PersonNames, GroupNames);
                    PersonNames.SelectedIndex = 0;
                    GroupNames.SelectedIndex = 0;
                } else {
                    ;
                }
            }
        }

        private void GroupNameAddBtn_Click(object sender, RoutedEventArgs e) {
            try {
            GPA();
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        int SelectedPersonNumber = 0;
        int SelectedGroupNumber = 0;
        bool FreeListError = false;
        public void SelectedIndexName() {
            FreeListError = false;
            if (PersonNames.Items.Count > 0 && GroupNames.Items.Count > 0) {
                SelectedPersonNumber = PersonNames.SelectedIndex;
                SelectedGroupNumber = GroupNames.SelectedIndex;
            } else {
                FreeListError = true;
            }
        }

        private void IconGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {
                if (e.Key == Key.Enter) {
                    GPA();
                }
            } catch (Exception Error) {
                MessageBox.Show("Hata oluştu,lütfen desteğe bildiriniz.Hata Sebebi : " + Error.ToString(), "Hata!!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
