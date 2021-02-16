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
using System.Data.Entity.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace CarDealerSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CarDealerContext dbContext;

        Customer newCustomer = new Customer();
        Brand newBrand = new Brand();
        Car newCar = new Car();
        Model newModel = new Model();

        public MainWindow(CarDealerContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetCustomers();
            GetBrand();
            GetModel();
            GetCar();
            DisplayBrandList();
            DisplayModelList();
            DisplayBookingCustomersList();

            //----------------------------------------------------------------------//

            AddNewItemGrid.DataContext = newCustomer;
            AddNewBrandGrid.DataContext = newBrand;
            AddNewModelGrid.DataContext = newModel;
            AddNewCarGrid.DataContext = newCar;
        }

        public MainWindow() { }

        private void DisplayBrandList()
        {
            var brand = from b in dbContext.Brands
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };

            BrandName.ItemsSource = brand.ToList();
            CarBrand.ItemsSource = brand.ToList();
        }

        private void DisplayModelList()
        {
            var models = from b in dbContext.Models
                        select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };
            
            CarModel.ItemsSource = models.ToList();
        }

        private void DisplayBookingCustomersList()
        {
            var customers = from b in dbContext.Customers
                         select new
                         {
                             Id = b.Id,
                             Name = b.Name + " "+ b.Surname + "; " + b.Email + "; " + b.PostNumberr + " " + b.City     
                         };

            CarUser.ItemsSource = customers.ToList();
        }

        private void GetCustomers()
        {
            
            Customer.ItemsSource = dbContext.Customers.ToList();
        }

        private void GetBrand()
        {
            
            Brand.ItemsSource = dbContext.Brands.ToList();
        }

        public void GetModel()
        {
            
            Model.ItemsSource = dbContext.Models
                .Select(model => new
                {
                    Id = model.Id,
                    Name = model.Name,
                    Brand = model.Brand.Name
                }).ToList();
        }

        private void GetCar()
        {
            
            Car.ItemsSource = dbContext.Cars
                .Select(car => new
                {
                    Id = car.Id,
                    Brand = car.Brand.Name,
                    BrandID = car.Brand.Id,
                    Model = car.Model.Name,
                    ModelID = car.Model.Id,
                    BookingUser = car.BookingUser.Name +" " + car.BookingUser.Surname,
                    BookingUserID = car.BookingUser.Id,
                    ProductionYear = car.ProductionYear,
                    Course = car.Course,
                    Capacity = car.Capacity,
                    RegistrationNumber = car.RegistrationNumber,
                    Price = car.Price,
                    Booking = car.Booking,
                    Image = car.Image
                }).ToList();
        }
        
        //-----------------------------Dodawanie Customera-------------------------------------------------------------//

        private void AddItem(object s, RoutedEventArgs e)
        {
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            GetCustomers();

            string messageAdd = "Add new Customer: \n" + "ID: "+ newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " +newCustomer.Surname;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewItemGrid);
            newCustomer = new Customer();

            AddNewItemGrid.DataContext = newCustomer;

            DisplayBrandList();
            DisplayBookingCustomersList();
        }

        private void AddCar(object s, RoutedEventArgs e)
        {
            char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };

            //------Brand-------//

            var model_brand = CarBrand.SelectionBoxItem.ToString();
            string id_Brand = model_brand.Split(',')[0];
            int idBeforeTrim_Brand = Int32.Parse(id_Brand.Trim(charsToTrim));
            Brand tmp_brand = dbContext.Brands.Find(idBeforeTrim_Brand);
            newCar.Brand = tmp_brand;

            //------------Model----------------//

            var model_Model = CarModel.SelectionBoxItem.ToString();
            string id_Model = model_Model.Split(',')[0];
            int idBeforeTrim_Model = Int32.Parse(id_Brand.Trim(charsToTrim));
            Model tmp_Model = dbContext.Models.Find(idBeforeTrim_Model);
            newCar.Model = tmp_Model;

            //------BookingUser--------------//

            var model_User = CarUser.SelectionBoxItem.ToString();
            string id_User = model_Model.Split(',')[0];
            int idBeforeTrim_User = Int32.Parse(id_User.Trim(charsToTrim));
            Customer tmp_User = dbContext.Customers.Find(idBeforeTrim_User);
            newCar.BookingUser = tmp_User;

            //-----------dodawanie nowego samochodu, zapisywanie zmian, reload widoku--------//

            dbContext.Cars.Add(newCar);
            dbContext.SaveChanges();
            GetCar();
            
            //-----------wyswietlanie komunikatow------------------//

            string messageAdd = "Add new Customer: \n" + "ID: " + newCar.Id + "; Model: " + newCar.Model.Name + "; Brand: " + newCar.Brand.Name;
            string captionAdd = "Add new Customer";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);

            //-------------czyszczenie formularza---------------//

            clearTextBox(AddNewCarGrid);
            newCar = new Car();
            AddNewCarGrid.DataContext = newCar;
      
        }

        private void AddModel(object s, RoutedEventArgs e)
        {

            var model = BrandName.SelectionBoxItem.ToString();
            char[] charsToTrim = { '{', 'I', 'd', '=', ' '};
            string id = model.Split(',')[0];
            int idBeforeTrim = Int32.Parse(id.Trim(charsToTrim));
            Brand tmp = dbContext.Brands.Find(idBeforeTrim);
            newModel.Brand = tmp;

            dbContext.Models.Add(newModel);
            dbContext.SaveChanges();
            GetModel();

            string messageAdd = "Add new Mode: \n" + "ID: " + newModel.Id + "; Name: " + newModel.Name;
            string captionAdd = "Add new Model";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewModelGrid);
            newModel = new Model();
            AddNewModelGrid.DataContext = newModel;

            DisplayBrandList();
            DisplayModelList();
        }

        private void AddBrand(object s, RoutedEventArgs e)
        {
            dbContext.Brands.Add(newBrand);
            dbContext.SaveChanges();
            GetBrand();
            
            string messageAdd = "Add new Brand: \n" + "ID: " + newBrand.Id + "; Name: " + newBrand.Name;
            string captionAdd = "Add new Brand";
            MessageBoxButton buttonAdd = MessageBoxButton.OK;
            MessageBoxImage iconAdd = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            clearTextBox(AddNewBrandGrid);
            newBrand = new Brand();
            AddNewBrandGrid.DataContext = newBrand;

            DisplayBrandList();
        }


        //----------------------------------------Czyszczenie textBoxow-------------------------------------------------//

        private void clearTextBox(Grid gridName)
        {
            foreach (Control txtBox in gridName.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                    ((TextBox)txtBox).Text = string.Empty;
                if (txtBox.GetType() == typeof(PasswordBox))
                    ((PasswordBox)txtBox).Password = string.Empty;
            }
        }

        //-----------------------------------UsuwanieCustomera---------------------------------------------------------//

        private void DeleteCustomer(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if(result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
                dbContext.Customers.Remove(customerToBeDelated);
                dbContext.SaveChanges();

                //wiadomosc wyswietlana na ekranie
                string messageBoxText = "Usunięto: \n" + "ID: " + customerToBeDelated.Id + "; Name: " + customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
                string caption = "Usuwanie";
                MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                MessageBoxImage iconDeleted = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                GetCustomers();
            }
            else
            {
                string messageInfo = "Usuwanie anulowane!";
                string captionInfo = "Usuwanie";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if (result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
                dbContext.Customers.Remove(customerToBeDelated);
                dbContext.SaveChanges();

                //wiadomosc wyswietlana na ekranie
                string messageBoxText = "Usunięto: \n" + "ID: " + customerToBeDelated.Id + "; Name: " + customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
                string caption = "Usuwanie";
                MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                MessageBoxImage iconDeleted = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                GetCustomers();
            }
            else
            {
                string messageInfo = "Usuwanie anulowane!";
                string captionInfo = "Usuwanie";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteBrand(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if (result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                var brandToBeDelated = (s as FrameworkElement).DataContext as Brand;
                dbContext.Brands.Remove(brandToBeDelated);
                dbContext.SaveChanges();

                //wiadomosc wyswietlana na ekranie
                string messageBoxText = "Usunięto: \n" + "ID: " + brandToBeDelated.Id + "; Name: " + brandToBeDelated.Name ;
                string caption = "Usuwanie";
                MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                MessageBoxImage iconDeleted = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                GetCustomers();
            }
            else
            {
                string messageInfo = "Usuwanie anulowane!";
                string captionInfo = "Usuwanie";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteModel(object s, RoutedEventArgs e)
        {

            string messageWarning = "Czy na pewno chcesz usunać ten obiekt? Operacja jest nieodwracalna!";
            string captionWarning = "Usuwanie";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if (result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
                dbContext.Customers.Remove(customerToBeDelated);
                dbContext.SaveChanges();

                //wiadomosc wyswietlana na ekranie
                string messageBoxText = "Usunięto: \n" + "ID: " + customerToBeDelated.Id + "; Name: " + customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
                string caption = "Usuwanie";
                MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                MessageBoxImage iconDeleted = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                GetCustomers();
            }
            else
            {
                string messageInfo = "Usuwanie anulowane!";
                string captionInfo = "Usuwanie";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }
        //------------------------------------EdycjaCustomera----------------------------------------------------------//

        //zrob powiadomienie w popup//
        //-------------------działa--------------------------
        Customer selectedCustomer = new Customer();
        private void UpdateCustomer(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Czy na pewno chcesz edytować wybraną pozycję?";
            string captionQuestion = "Edycja";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if(result == MessageBoxResult.Yes)
            {
                selectedCustomer = (s as FrameworkElement).DataContext as Customer;
                WindowUpdateCustomer updateWindow = new WindowUpdateCustomer(selectedCustomer, this.dbContext);
                updateWindow.Show();
            }else
            {
                string messageEditionCancelled = "Edycja wycofana!";
                string captionEditionCancelled = "Edycja";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
                   
        }

        //---------DO-ZROBIENIA----------------
        Car selectedCar = new Car();
        private void UpdateCar(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Czy na pewno chcesz edytować wybraną pozycję?";
            string captionQuestion = "Edycja";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if (result == MessageBoxResult.Yes)
            {
            var selectedCar2 = (s as FrameworkElement).DataContext.ToString();



                selectedCar = (s as FrameworkElement).DataContext as Car;
                WindowUpdateCar updateWindow = new WindowUpdateCar(selectedCar, this.dbContext);
                updateWindow.Show();
            }
            else
            {
                string messageEditionCancelled = "Edycja wycofana!";
                string captionEditionCancelled = "Edycja";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
        }

        //-------działa--------------
        Brand selectedBrand = new Brand();
        private void UpdateBrand(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Czy na pewno chcesz edytować wybraną pozycję?";
            string captionQuestion = "Edycja";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if (result == MessageBoxResult.Yes)
            {
                selectedBrand = (s as FrameworkElement).DataContext as Brand;
                WindowUpdateBrand updateWindow = new WindowUpdateBrand(selectedBrand, this.dbContext);
                updateWindow.Show();
            }
            else
            {
                string messageEditionCancelled = "Edycja wycofana!";
                string captionEditionCancelled = "Edycja";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }

        }

        Model selectedModel = new Model();
        
        private void UpdateModel(object s, RoutedEventArgs e)
        {
            //-----------------czysci-sledzenie-obiektu(pozwala-na-wielokrotna-edycje-tej-samej-pozycji)-----------------------
            dbContext.Entry(selectedModel).State = EntityState.Detached;
            string messageQuestion = "Czy na pewno chcesz edytować wybraną pozycję?";
            string captionQuestion = "Edycja";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);
                         

            if (result == MessageBoxResult.Yes)
            {
                string zmienna = ((s as FrameworkElement).DataContext).ToString();

                //----id------
                char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
                string tmp = zmienna.Split(',')[0];
                int idAfterTrim = Int32.Parse(tmp.Trim(charsToTrim));
                selectedModel.Id = idAfterTrim;

                //-------model-----
                string tmp2 = zmienna.Split(',')[1];
                int find = tmp2.IndexOf('=')+2;
                int all = tmp2.Length;
                int position = (all - find);
                string name = tmp2.Substring(find, position);
                selectedModel.Name = name;

                //-------marka------
                string tmp3 = zmienna.Split(',')[2];
                int find1 = tmp3.IndexOf('=') + 2;
                int all1 = tmp3.Length;
                int position1 = (all1 - find1)-2;
                string brand = tmp3.Substring(find1, position1);

                var query = dbContext.Brands;
                var query1 = query.Where(a => a.Name == brand).Single();
                            
                Brand fullBrand = dbContext.Brands.Find(query1.Id);
                selectedModel.Brand = fullBrand;
                
                WindowUpdateModel updateWindow = new WindowUpdateModel(selectedModel, this.dbContext);
                updateWindow.Show();
            }
            else
            {
                string messageEditionCancelled = "Edycja wycofana!";
                string captionEditionCancelled = "Edycja";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }

        }

        private void Reload(object s, RoutedEventArgs e)
        {
            InitializeComponent();
            GetCustomers();
            GetBrand();
            GetModel();
            GetCar();
            DisplayBrandList();
            DisplayModelList();
            DisplayBookingCustomersList();
            
        }
    }
}
