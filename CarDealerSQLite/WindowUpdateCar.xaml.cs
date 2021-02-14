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

        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            dbContext.Update(updateCar);
            dbContext.SaveChanges();
            this.Close();
        }

    }
}
