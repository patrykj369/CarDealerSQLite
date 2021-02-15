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
    public partial class WindowUpdateBrand : Window
    {
        CarDealerContext dbContext;

        Brand updateBrand = new Brand();

        public WindowUpdateBrand(Brand selectedCar, CarDealerContext dbContext)
        {

            InitializeComponent();
            this.dbContext = dbContext;
            UpdateBrandGrid.DataContext = selectedCar;
            updateBrand = selectedCar;

        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            dbContext.Update(updateBrand);
            dbContext.SaveChanges();
            this.Close();
        }

    }
}
