using MedLaboratory.DataBase;
using MedLaboratory.Presentation.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MedLaboratory.Presentation.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        MedLaboratoryDBEntities db = new MedLaboratoryDBEntities();
        Employee _currentUser;
        public MainWindow(Employee currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            MainFrame.NavigationService.Navigate(new Presentation.Pages.ProfilePage(_currentUser, this));

            LoadUI(_currentUser.Role);

            if(_currentUser.ID_Role == 1)
            {
                TimerTextBlock.Visibility = Visibility.Visible;
                _timer = new DispatcherTimer();
                _time = TimeSpan.FromSeconds(1);
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
                _timer.Start();
            }
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerTextBlock.Text = _time.ToString("c");
            _time = _time.Add(TimeSpan.FromSeconds(+1));
            if (_time == TimeSpan.FromSeconds(41)) MessageBox.Show("Осталось 20 сек");
            if (_time == TimeSpan.FromSeconds(61))
            {
                _timer.Stop();
                var loginWindow = new LoginWindow(1800);
                loginWindow.Show();
                this.Close();
            }
        }

        private void LoadUI(Role role)
        {
            if(role.ID == 1)
            {
                HistoryLoginButton.Visibility = Visibility.Collapsed;
                ReportsInsuranceButton.Visibility = Visibility.Collapsed;
                SuppliesButton.Visibility = Visibility.Collapsed;
            }
            else if (role.ID == 2)
            {
                BiomaterialButton.Visibility = Visibility.Collapsed;
                AnalizatorButton.Visibility = Visibility.Collapsed;
                HistoryLoginButton.Visibility = Visibility.Collapsed;
                SuppliesButton.Visibility = Visibility.Collapsed;
            }
            else if (role.ID == 3)
            {
                BiomaterialButton.Visibility = Visibility.Collapsed;
                AnalizatorButton.Visibility = Visibility.Collapsed;
                ReportsInsuranceButton.Visibility = Visibility.Collapsed;
            }
        }

        private void onClickNavigationWithButton(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(ReportsButton)) { MainFrame.NavigationService.Navigate(new ReportsPage()); }
            if (sender.Equals(BiomaterialButton)) { MainFrame.NavigationService.Navigate(new ReportsPage()); }
            if (sender.Equals(AnalizatorButton)) { MainFrame.NavigationService.Navigate(new ReportsPage()); }
            if (sender.Equals(HistoryLoginButton)) { MainFrame.NavigationService.Navigate(new HistoryLoginPage()); }
            if (sender.Equals(SuppliesButton)) { MainFrame.NavigationService.Navigate(new ReportsPage()); }
            if (sender.Equals(ReportsInsuranceButton)) { MainFrame.NavigationService.Navigate(new ReportsPage()); }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack) BackButton.Visibility = Visibility.Visible;
            else BackButton.Visibility = Visibility.Hidden;
        }

        private void ImageIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Presentation.Pages.ProfilePage(_currentUser, this));

        }
    }
}
