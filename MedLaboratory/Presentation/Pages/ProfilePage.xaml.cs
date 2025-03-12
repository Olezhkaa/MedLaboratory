using MedLaboratory.DataBase;
using MedLaboratory.Presentation.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MedLaboratory.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        MedLaboratoryDBEntities db = new MedLaboratoryDBEntities();
        Employee _currentUser;
        MainWindow _mainWindow;
        public ProfilePage(Employee currentUser, MainWindow mainWindow)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _mainWindow = mainWindow;
            if(_currentUser != null)
            {
                string fullName = $"{_currentUser.LastName} {_currentUser.FirstName} {_currentUser.MiddleName}";
                string role = $"Роль: {_currentUser.Role.Title}";

                FullNameTextBlock.Text = fullName;
                RoleTextBlock.Text = role;

                if (LoadImage(_currentUser.Photo) != null) ProfileImage.Source = LoadImage(_currentUser.Photo);
            }
            
        }   

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void onClickNavigationWithButton(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            _mainWindow.Close();
        }
    }
}
