﻿using CarDealer.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                //optionsBuilder.UseSqlite("$Filename = {baziora.db}", x => x.SuppressForeignKeyEnforcement());
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
