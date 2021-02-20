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
    /// Publiczna klasa WindowUpdateModel
    /// </summary>
    public partial class WindowUpdateModel : Window
    {
        CarDealerContext dbContext;

        Model updateModel = new Model();

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

        /// <summary>
        /// Konstruktor klasy WindowUpdateModel
        /// </summary>
        public WindowUpdateModel(Model selectedModel, CarDealerContext dbContext)
        {
            InitializeComponent();
            this.dbContext = dbContext;
            UpdateModelGrid.DataContext = selectedModel;
            updateModel = selectedModel;
            

            var brand = from b in dbContext.Brands
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };

            BrandName.ItemsSource = brand.ToList();
            BrandName.Text = selectedModel.Brand.Name;
           
        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            
            dbContext.Entry(updateModel).State = EntityState.Detached;
            char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };

            //------Brand-------//
            
            var model_brand = BrandName.SelectionBoxItem.ToString();
            string id_Brand = model_brand.Split(',')[0];
            int idBeforeTrim_Brand = Int32.Parse(id_Brand.Trim(charsToTrim));
            Brand tmp_brand = dbContext.Brands.Find(idBeforeTrim_Brand);
            updateModel.Brand = tmp_brand;

            try
            {
                dbContext.Update(updateModel);
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                string messageEditionCancelled = "This item cannot be edited, please try restarting the application!";
                string captionEditionCancelled = "Edition";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
            finally
            {
                MainWindow window = new MainWindow(dbContext);
                window.ModelTab.IsSelected = true;
                window.Show();
                this.Close();
            }
            
            
        }

        
    }
}
