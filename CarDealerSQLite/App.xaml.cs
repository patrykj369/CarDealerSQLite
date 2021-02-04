﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CarDealer.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<CarDealerContext>(option =>
            {
                option.UseSqlite("Data Source = CarDealerSQLite.db");
            });

            services.AddSingleton<MainWindow>();

            serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object s, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
