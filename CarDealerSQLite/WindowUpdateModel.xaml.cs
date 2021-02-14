﻿using CarDealer.EntityFramework.Models;
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
    public partial class WindowUpdateModel : Window
    {
        CarDealerContext dbContext;

        Model updateModel = new Model();

        public WindowUpdateModel(Model selectedCar, CarDealerContext dbContext)
        {

            InitializeComponent();
            this.dbContext = dbContext;
            UpdateModelGrid.DataContext = selectedCar;
            updateModel = selectedCar;

        }

        private void UpdateItem(object s, RoutedEventArgs a)
        {
            dbContext.Update(updateModel);
            dbContext.SaveChanges();
            this.Close();
        }

    }
}
