using MedLaboratory.DataBase;
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

namespace MedLaboratory.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для HistoryLoginPage.xaml
    /// </summary>
    public partial class HistoryLoginPage : Page
    {

        MedLaboratoryDBEntities db = new MedLaboratoryDBEntities();
        public HistoryLoginPage()
        {
            InitializeComponent();
            HistoryDataGrid.ItemsSource = db.History_Login.ToList();
            FiltrComboBox.Items.Add("Нет");
            FiltrComboBox.Items.Add("Успешно");
            FiltrComboBox.Items.Add("Не успешно");
            FiltrComboBox.SelectedIndex = 0;
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateHistory_Logins();
        }

        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateHistory_Logins();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateHistory_Logins();
        }

        private void UpRadionButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateHistory_Logins();
        }

        private void DownRadionButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateHistory_Logins();
        }

        private void UpdateHistory_Logins()
        {
            List<History_Login> currentHistoryLogin = db.History_Login.ToList() ;
            


            if(FiltrComboBox != null && FiltrComboBox.SelectedIndex > 0)
            {
                string selectedResult = FiltrComboBox.SelectedValue.ToString();
                
                currentHistoryLogin = currentHistoryLogin.Where(x => x.Result.Trim() == selectedResult.Trim()).ToList();
            }

            switch(SortComboBox.SelectedIndex)
            {
                case 1:
                    {
                        if (UpRadionButton.IsChecked.Value)
                            currentHistoryLogin = currentHistoryLogin.OrderBy(x => x.Login_DateTime).ToList();
                        if (DownRadionButton.IsChecked.Value)
                            currentHistoryLogin = currentHistoryLogin.OrderByDescending(x => x.Login_DateTime).ToList();
                        break;
                    }
            }

            if(SearchComboBox != null && SearchTextBox != null)
            {

                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            currentHistoryLogin = currentHistoryLogin.Where(x => x.Employee.Login.ToLower().Trim().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            currentHistoryLogin = currentHistoryLogin.Where(x => x.Employee.LastName.ToLower().Trim().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            currentHistoryLogin = currentHistoryLogin.Where(x => x.Employee.FirstName.ToLower().Trim().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    

                }
                
            }
            

            if(HistoryDataGrid != null) HistoryDataGrid.ItemsSource = currentHistoryLogin.ToList();

            if (currentHistoryLogin.Count == 0 && SearchTextBox != null && SearchTextBox.Text.Trim() != "")
            {
                MessageBox.Show("По вашему запросу ничего не найдено");
                SearchTextBox.Text = "";
            }
        }
    }
}
