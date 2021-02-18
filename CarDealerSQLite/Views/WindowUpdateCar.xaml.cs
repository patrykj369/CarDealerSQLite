using CarDealer.EntityFramework.Models;
using CarDealer.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for WindowUpdateCustomer.xaml
    /// </summary>
    public partial class WindowUpdateCar : Window
    {
        CarDealerContext dbContext;

        Car updateCar = new Car();

        public WindowUpdateCar(Car selectedCar, CarDealerContext dbContext)
        {

            InitializeComponent();
            this.dbContext = dbContext;
            UpdateCarGrid.DataContext = selectedCar;
            updateCar = selectedCar;

           
            var brand = from b in dbContext.Brands
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };

            CarBrand.ItemsSource = brand.ToList();
            CarBrand.Text = selectedCar.Brand.Name;

            var model = from b in dbContext.Models
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };

            CarModel.ItemsSource = model.ToList();
            CarModel.Text = selectedCar.Model.Name;

            var user = from b in dbContext.Customers
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name + " " + b.Surname
                        };

            CarUser.ItemsSource = user.ToList();
            CarUser.Text = selectedCar.BookingUser.Name + " " + selectedCar.BookingUser.Surname;

        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            dbContext.Update(updateCar);
            dbContext.SaveChanges();
            this.Close();
        }

    }
}
