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

        public MainWindow(CarDealerContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetCustomers();
            GetBrand();
            GetModel();
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
            
        }
    }
}
