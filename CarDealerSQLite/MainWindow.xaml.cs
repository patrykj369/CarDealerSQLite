using CarDealer.EntityFramework.Services;
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
using Microsoft.EntityFrameworkCore;
using CarDealer.EntityFramework.Models;

namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CarDealerContext dbContext;

        Customer newCustomer = new Customer();

        public MainWindow(CarDealerContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetCustomers();
            GetBrand();
            GetModel();
            GetCar();

            //----------------------------------------------------------------------//

            AddNewItemGrid.DataContext = newCustomer;
        }

        public MainWindow() { }

        private void GetCustomers()
        {
            Customer.ItemsSource = dbContext.Customers.ToList();
        }

        private void GetBrand()
        {
            Brand.ItemsSource = dbContext.Brands.ToList();
        }

        private void GetModel()
        {
            Model.ItemsSource = dbContext.Models
                .Select(model => new
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.BrandID,
                    Brand = model.Brand,
                }).ToList();
        }

        private void GetCar()
        {
            Car.ItemsSource = dbContext.Cars.ToList();
        }
        
        //-----------------------------Dodawanie Customera-------------------------------------------------------------//

        private void AddItem(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();

            string messageAdd = "Add new Customer: \n" + "ID: "+ newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " +newCustomer.Surname;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewItemGrid);
            newCustomer = new Customer();
            AddNewItemGrid.DataContext = newCustomer;
        }

        private void AddCar(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();

            string messageAdd = "Add new Customer: \n" + "ID: " + newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " + newCustomer.Surname;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewItemGrid);
            newCustomer = new Customer();
            AddNewItemGrid.DataContext = newCustomer;
        }

        private void AddModel(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();

            string messageAdd = "Add new Customer: \n" + "ID: " + newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " + newCustomer.Surname;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewItemGrid);
            newCustomer = new Customer();
            AddNewItemGrid.DataContext = newCustomer;
        }

        private void AddBrand(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();

            string messageAdd = "Add new Customer: \n" + "ID: " + newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " + newCustomer.Surname;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewItemGrid);
            newCustomer = new Customer();
            AddNewItemGrid.DataContext = newCustomer;
        }


        //----------------------------------------Czyszczenie textBoxow-------------------------------------------------//
        public void clearTextBox(Grid gridName)
        {
            foreach (Control txtBox in gridName.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                    ((TextBox)txtBox).Text = string.Empty;
                if (txtBox.GetType() == typeof(PasswordBox))
                    ((PasswordBox)txtBox).Password = string.Empty;
            }
        }

        //-----------------------------------UsuwanieCustomera---------------------------------------------------------//

        private void DeleteCustomer(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if(result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
                dbContext.Customers.Remove(customerToBeDelated);
                dbContext.SaveChanges();

                //wiadomosc wyswietlana na ekranie
                string messageBoxText = "Usunięto: \n" + "ID: " + customerToBeDelated.Id + "; Name: " + customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
                string caption = "Usuwanie";
                MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                MessageBoxImage iconDeleted = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                GetCustomers();
            }
            else
            {
                string messageInfo = "Usuwanie anulowane!";
                string captionInfo = "Usuwanie";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        //------------------------------------EdycjaCustomera----------------------------------------------------------//
        //zrob powiadomienie w popup//
        Customer selectedCustomer = new Customer();
        private void UpdateCustomer(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Czy na pewno chcesz edytować wybraną pozycję?";
            string captionQuestion = "Edycja";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if(result == MessageBoxResult.Yes)
            {
                selectedCustomer = (s as FrameworkElement).DataContext as Customer;
                WindowUpdateCustomer updateWindow = new WindowUpdateCustomer(selectedCustomer, this.dbContext);
                updateWindow.Show();
            }else
            {
                string messageEditionCancelled = "Edycja wycofana!";
                string captionEditionCancelled = "Edycja";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
                   
        }

    }
}
