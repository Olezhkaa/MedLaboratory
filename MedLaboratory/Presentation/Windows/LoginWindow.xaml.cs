using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MedLaboratory.DataBase;

namespace MedLaboratory.Presentation.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MedLaboratoryDBEntities db = new MedLaboratoryDBEntities();
        Random random = new Random();
        string stringCaptcha;
        int counterNotCompleted = 0;
        bool blockVoidTen = false;
        bool blockVoidMin = false;
        private System.Timers.Timer timer = new System.Timers.Timer(10000);
        private System.Timers.Timer timer2 = new System.Timers.Timer(180000);



        public LoginWindow()
        {
            InitializeComponent();
            CaptchaStackPanel.Visibility = Visibility.Collapsed;
            CaptchaTextBlock.Visibility = Visibility.Collapsed;
            CaptchaCheck.Visibility = Visibility.Collapsed;
            UpdateCaptcha();
            
        }

        public LoginWindow(int time)
        {
            InitializeComponent();
            CaptchaStackPanel.Visibility = Visibility.Collapsed;
            CaptchaTextBlock.Visibility = Visibility.Collapsed;
            CaptchaCheck.Visibility = Visibility.Collapsed;
            UpdateCaptcha();

            MessageBox.Show("Перерыв 30 мин!");
            timer2.Start();
            blockVoidMin = true;
            timer.Elapsed += OnTimerElapsedMin;
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = db.Employee.FirstOrDefault(p => p.Login == LoginTextBox.Text && p.Password == PasswordBox.Password);
            if (blockVoidTen || blockVoidMin)
            {
                MessageBox.Show("ЖДИ!");
            }
            else if (LoginTextBox.Text.Equals("") || PasswordBox.Password.Equals(""))
            {
                MessageBox.Show("Заполните все поля");
            }
            else if (currentUser == null)
            {
                SetHistoryLogin(false);
                MessageBox.Show("Данные не верны. Попробуйте еще раз...");
                counterNotCompleted++;
                CaptchaStackPanel.Visibility = Visibility.Visible;
                CaptchaTextBlock.Visibility = Visibility.Visible;
                CaptchaCheck.Visibility = Visibility.Visible;
                if (counterNotCompleted > 1)
                {
                    MessageBox.Show("Ожидание 10 сек");
                    blockVoidTen = true;
                    timer.Elapsed += OnTimerElapsedSec;
                    timer.Start();
                }
            }
            else if (counterNotCompleted>=1 && CaptchaCheck.Text != stringCaptcha)
            {
                MessageBox.Show("Ошибка при вводе символов!");
                SetHistoryLogin(false);
            }
            else
            {
                SetHistoryLogin(true);
                var mainWindow = new MainWindow(currentUser);
                mainWindow.Show();
                this.Close();
            }
            
            UpdateCaptcha();
            CaptchaCheck.Clear();
        }

        private void SetHistoryLogin(bool result)
        {
            if (result)
            {
                DateTime dateTime = DateTime.Now;
                History_Login history_Login = new History_Login();
                Employee employee = db.Employee.FirstOrDefault(p => p.Login == LoginTextBox.Text);
                history_Login.ID_Employee = employee.ID;
                history_Login.Login_DateTime = dateTime;
                history_Login.Result = "Успешно";
                employee.Last_login = dateTime;
                db.History_Login.Add(history_Login);
                db.SaveChanges();
                
            }
            else
            {
                Employee employee = db.Employee.FirstOrDefault(p => p.Login == LoginTextBox.Text);

                if (employee != null)
                {
                    DateTime dateTime = DateTime.Now;
                    History_Login history_Login = new History_Login();
                    history_Login.ID_Employee = employee.ID;
                    history_Login.Login_DateTime = dateTime;
                    history_Login.Result = "Не успешно";
                    db.History_Login.Add(history_Login);
                    db.SaveChanges();
                }
                
            }
        }

        private void UpdateCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha();
        }

        private void UpdateCaptcha()
        {
            stringCaptcha = "";
            SymbolsPanel.Children.Clear();
            NoiseConvas.Children.Clear();

            GenerationNoise(random.Next(10, 15));
            GenerationSymbols(4);
        }
        private void GenerationNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)random.Next(50, 150), (byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
                border.Height = random.Next(2, 10);
                border.Width = random.Next(20, 100);
                border.RenderTransform = new RotateTransform(random.Next(0, 90));

                NoiseConvas.Children.Add(border);
                Canvas.SetLeft(border, random.Next(0, 200));
                Canvas.SetTop(border, random.Next(0, 70));

                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)random.Next(50, 150), (byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256)));
                ellipse.Height = ellipse.Width = random.Next(5, 50);
                ellipse.RenderTransform = new RotateTransform(random.Next(0, 90));

                NoiseConvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, random.Next(0, 200));
                Canvas.SetTop(ellipse, random.Next(0, 70));
            }
        }
        private void GenerationSymbols(int count)
        {
            string alphabet = "QEWRQWREFWER123212";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol;
                lbl.FontSize = random.Next(20, 50);
                lbl.RenderTransform = new RotateTransform(random.Next(-45, 45));
                lbl.Margin = new Thickness(10, 10, 10, 10);

                stringCaptcha += symbol;
                SymbolsPanel.Children.Add(lbl);
            }
        }

        private void OnTimerElapsedSec(object sender, ElapsedEventArgs e)
        {
            
            MessageBox.Show("Вы можете снова пробовать");
            blockVoidTen = false;
            timer.Stop();
        }
        private void OnTimerElapsedMin(object sender, ElapsedEventArgs e)
        {

            MessageBox.Show("Вы можете снова пробовать");
            blockVoidMin = false;
            timer.Stop();
        }
    }
}
