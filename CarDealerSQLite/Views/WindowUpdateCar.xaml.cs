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
    public partial class WindowUpdateCar : Window
    {
        CarDealerContext dbContext;

        Car updateCar = new Car();
        
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

            char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
            var car_brand = CarBrand.SelectionBoxItem.ToString();
            string id_Brand = car_brand.Split(',')[0];
            int idBeforeTrim_Brand = Int32.Parse(id_Brand.Trim(charsToTrim));
            Brand tmp_brand = dbContext.Brands.Find(idBeforeTrim_Brand);
            updateCar.Brand = tmp_brand;

            var car_model = CarModel.SelectionBoxItem.ToString();
            string id_Model = car_model.Split(',')[0];
            int idBeforeTrim_Model = Int32.Parse(id_Model.Trim(charsToTrim));
            Model tmp_model = dbContext.Models.Find(idBeforeTrim_Model);
            updateCar.Model = tmp_model;

            var car_user = CarUser.SelectionBoxItem.ToString();
            string id_User = car_user.Split(',')[0];
            int idBeforeTrim_User = Int32.Parse(id_User.Trim(charsToTrim));
            Customer tmp_user = dbContext.Customers.Find(idBeforeTrim_User);
            updateCar.BookingUser= tmp_user;

            dbContext.Update(updateCar);
            dbContext.SaveChanges();

            MainWindow window = new MainWindow(dbContext);
            window.CarTab.IsSelected = true;
            window.Show();
            
            this.Close();
            
        }

    }
}
