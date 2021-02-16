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


namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for WindowUpdateCustomer.xaml
    /// </summary>
    public partial class WindowUpdateModel : Window
    {
        CarDealerContext dbContext;

        Model updateModel = new Model();

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
            //CarBrand.ItemsSource = brand.ToList();
            //BrandName.Text = selectedModel.Brand.Name;
            BrandName.Text = selectedModel.Brand.Name;
        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            //var tmp = BrandName.SelectedItem;

            char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };

            //------Brand-------//

            var model_brand = BrandName.SelectionBoxItem.ToString();
            string id_Brand = model_brand.Split(',')[0];
            int idBeforeTrim_Brand = Int32.Parse(id_Brand.Trim(charsToTrim));
            Brand tmp_brand = dbContext.Brands.Find(idBeforeTrim_Brand);
            updateModel.Brand = tmp_brand;

            dbContext.Update(updateModel);
            dbContext.SaveChanges();
            
            this.Close();
        }

    }
}
