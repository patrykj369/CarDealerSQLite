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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for WindowUpdateCustomer.xaml
    /// </summary>
    public partial class WindowUpdateCustomer : Window
    {
        CarDealerContext dbContext;

        Customer updateCustomer = new Customer();

        //zablkowoanie mozliwosci zamkniecia okna krzyzykiem
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
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
