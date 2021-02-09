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
            var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
            dbContext.Customers.Remove(customerToBeDelated);
            string messageBoxText = "Deleted succesfully: \n" + "ID: " + customerToBeDelated.Id +"; Name: "+ customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
            string caption = "Deleted succesfully!";
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(messageBoxText, caption, button, icon);
            dbContext.SaveChanges();
            GetCustomers();
        }
    }
}
