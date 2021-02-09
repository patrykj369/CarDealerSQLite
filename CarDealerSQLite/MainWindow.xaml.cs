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

        private void AddItem(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();
            newCustomer = new Customer();
            name.Clear();
            surname.Clear();
            city.Clear();
            post.Clear();
            mail.Clear();
            phone.Clear();

        }

        //Customer selectedCustomer = new Customer();
        

        private void DeleteCustomer(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Warning;
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


        private void UpdateCustomer(object s, RoutedEventArgs e)
        {
            WindowUpdateCustomer updateWindow = new WindowUpdateCustomer();
            updateWindow.Show();
        }

    }
}
