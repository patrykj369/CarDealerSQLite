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
    public partial class WindowUpdateCustomer : Window
    {
        CarDealerContext dbContext;

        Customer updateCustomer = new Customer();

        public WindowUpdateCustomer(Customer selectedCustomer, CarDealerContext dbContext)
        {
            
            InitializeComponent();
            this.dbContext = dbContext;
            UpdateCustomerGrid.DataContext = selectedCustomer;
            updateCustomer = selectedCustomer;

        }
        
        private void UpdateItem(object s, RoutedEventArgs a)
        {
            dbContext.Update(updateCustomer);
            dbContext.SaveChanges();
            this.Close();
        }    
       
    }
}
