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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

namespace CarDealerSQLite
{
    /// <summary>
    /// Głowne okno aplikacji, zawiera metody do dzialania w aplikacji
    /// </summary>
    public partial class MainWindow : Window
    {
        CarDealerContext _dbContext { get; set; }

        Customer newCustomer = new Customer();
        Brand newBrand = new Brand();
        Car newCar = new Car();
        Model newModel = new Model();


        /// <summary>
        /// Konstruktor jednoargumentowy
        /// </summary>
        public MainWindow(CarDealerContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            RefreashViews();



            //----------------------------------------------------------------------//

            AddNewItemGrid.DataContext = newCustomer;
            AddNewBrandGrid.DataContext = newBrand;
            AddNewModelGrid.DataContext = newModel;
            AddNewCarGrid.DataContext = newCar;
           
            
        }

        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public MainWindow() 
        {

        }

        /// <summary>
        /// Publiczna metoda zwracajaca obiekt typu Brand
        /// </summary>
        public Brand brandzik()
        {
            Brand brand = _dbContext.Brands.Find(1);
            return brand;
        }

        private void DisplayBrandList()
        {
            var brand = from b in _dbContext.Brands
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
            var models = from b in _dbContext.Models
                         select new
                        {
                            Id = b.Id,
                            Name = b.Name
                        };
            
            CarModel.ItemsSource = models.ToList();
        }

        private void DisplayBookingCustomersList()
        {
            var customers = from b in _dbContext.Customers
                            select new
                         {
                             Id = b.Id,
                             Name = b.Name + " "+ b.Surname + "; " + b.Email + "; " + b.PostNumberr + " " + b.City     
                         };

            CarUser.ItemsSource = customers.ToList();
        }

        private void GetCustomers()
        {
            
            Customer.ItemsSource = _dbContext.Customers.ToList();
        }

        private void GetBrand()
        {
            
            Brand.ItemsSource = _dbContext.Brands.ToList();
        }

        private void GetModel()
        {
            
            Model.ItemsSource = _dbContext.Models
                .Select(model => new
                {
                    Id = model.Id,
                    Name = model.Name,
                    Brand = model.Brand.Name
                }).ToList();
        }

        private void GetCar()
        {
            
            Car.ItemsSource = _dbContext.Cars
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
                    Price = car.Price
                }).ToList();
        }
        
        //-----------------------------Dodawanie Customera-------------------------------------------------------------//

        private void AddItem(object s, RoutedEventArgs e)
        {
            if(newCustomer.Name != null && newCustomer.Surname != null && newCustomer.PhoneNumber != null && newCustomer.PostNumberr != null && newCustomer.City !=null && newCustomer.Email != null && newCustomer.PhoneNumber.Length == 9 && newCustomer.PostNumberr.Length == 6 && newCustomer.PostNumberr.Contains('-') && newCustomer.Email.Contains('@') && newCustomer.Email.Contains('.'))
            {
                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();
                
                string messageAdd = "Add new Customer: \n" + "ID: " + newCustomer.Id + "; Name: " + newCustomer.Name + "; Surname: " + newCustomer.Surname;
                string captionAdd = "Add new Customer";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
                clearTextBox(AddNewItemGrid);
                newCustomer = new Customer();
                RefreashViews();
                AddNewItemGrid.DataContext = newCustomer;
            }
            else
            {
                string messageAdd = "The required data has not been completed!";
                string captionAdd = "Incorrect data!";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            }
            
            
        }

        private void AddCar(object s, RoutedEventArgs e)
        {
            char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };

            //------Brand-------//

            var model_brand = CarBrand.SelectionBoxItem.ToString();
            string id_Brand = model_brand.Split(',')[0];
            int idBeforeTrim_Brand = Int32.Parse(id_Brand.Trim(charsToTrim));
            Brand tmp_brand = _dbContext.Brands.Find(idBeforeTrim_Brand);
            newCar.Brand = tmp_brand;

            //------------Model----------------//

            var model_Model = CarModel.SelectionBoxItem.ToString();
            string id_Model = model_Model.Split(',')[0];
            int idBeforeTrim_Model = Int32.Parse(id_Model.Trim(charsToTrim));
            Model tmp_Model = _dbContext.Models.Find(idBeforeTrim_Model);
            newCar.Model = tmp_Model;

            //------BookingUser--------------//

            var model_User = CarUser.SelectionBoxItem.ToString();
            string id_User = model_Model.Split(',')[0];
            int idBeforeTrim_User = Int32.Parse(id_User.Trim(charsToTrim));
            Customer tmp_User = _dbContext.Customers.Find(idBeforeTrim_User);
            newCar.BookingUser = tmp_User;

            if (newCar.Model.Name != null && newCar.Brand.Name != null && newCar.BookingUser.Name != null && newCar.Capacity != null && newCar.Course != null && newCar.Price != null && newCar.ProductionYear != null && newCar.RegistrationNumber != null && newCar.ProductionYear.Length == 4)
            {
                //-----------dodawanie nowego samochodu, zapisywanie zmian, reload widoku--------//

                _dbContext.Cars.Add(newCar);
                _dbContext.SaveChanges();
                RefreashViews();

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
            else
            {
                string messageAdd = "The required data has not been completed!";
                string captionAdd = "Incorrect data!";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            }

        }

        private void AddModel(object s, RoutedEventArgs e)
        {
            
            
                var model = BrandName.SelectionBoxItem.ToString();
                char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
                string id = model.Split(',')[0];
                int idBeforeTrim = Int32.Parse(id.Trim(charsToTrim));
                Brand tmp = _dbContext.Brands.Find(idBeforeTrim);
                newModel.Brand = tmp;

            if (newModel.Name != null && newModel.Brand != null)
            {
                _dbContext.Models.Add(newModel).Reload();
                _dbContext.SaveChanges();
                RefreashViews();

                string messageAdd = "Add new Mode: \n" + "ID: " + newModel.Id + "; Name: " + newModel.Name;
                string captionAdd = "Add new Model";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
                clearTextBox(AddNewModelGrid);
                newModel = new Model();
                AddNewModelGrid.DataContext = newModel;
                RefreashViews();
            }
            else
            {
                string messageAdd = "The required data has not been completed!";
                string captionAdd = "Incorrect data!";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            }
        }

        private void AddBrand(object s, RoutedEventArgs e)
        {
            if(newBrand.Name != null)
            {
                _dbContext.Brands.AsNoTracking();
                _dbContext.Brands.Add(newBrand);
                _dbContext.SaveChanges();
                GetBrand();

                string messageAdd = "Add new Brand: \n" + "ID: " + newBrand.Id + "; Name: " + newBrand.Name;
                string captionAdd = "Add new Brand";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
                clearTextBox(AddNewBrandGrid);
                newBrand = new Brand();
                AddNewBrandGrid.DataContext = newBrand;

                RefreashViews();

            }
            else
            {
                string messageAdd = "The required data has not been completed!";
                string captionAdd = "Incorrect data!";
                MessageBoxButton buttonAdd = MessageBoxButton.OK;
                MessageBoxImage iconAdd = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(messageAdd, captionAdd, buttonAdd, iconAdd);
            }
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

            string messageWarning = "Are you sure you want to delete this item? The operation is irreversible!";
            string captionWarning = "Removal";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    //Usuwanie z bazy
                    var customerToBeDelated = (s as FrameworkElement).DataContext as Customer;
                    _dbContext.Customers.Remove(customerToBeDelated);
                    _dbContext.SaveChanges();

                    //wiadomosc wyswietlana na ekranie
                    string messageBoxText = "Removed: \n" + "ID: " + customerToBeDelated.Id + "; Name: " + customerToBeDelated.Name + "; Surname: " + customerToBeDelated.Surname;
                    string caption = "Removal";
                    MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                    MessageBoxImage iconDeleted = MessageBoxImage.Information;
                    MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                    RefreashViews();
                }
                catch (Exception)
                {
                    string messageDeleteCancelled = "This item cannot be deleted, please try restarting the application!";
                    string captionDeleteCancelled = "Removal";
                    MessageBoxButton buttonDeleteCancelled = MessageBoxButton.OK;
                    MessageBoxImage iconDeleteCancelled = MessageBoxImage.Warning;
                    MessageBoxResult resultDeleteCancelled = MessageBox.Show(messageDeleteCancelled, captionDeleteCancelled, buttonDeleteCancelled, iconDeleteCancelled);
                }
            }
            else
            {
                string messageInfo = "Delete canceled!";
                string captionInfo = "Removal";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteCar(object s, RoutedEventArgs e)
        {

            string messageWarning = "Are you sure you want to delete this item? The operation is irreversible!";
            string captionWarning = "Removal";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var selectedCar2 = (s as FrameworkElement).DataContext.ToString();
                    var carToBeDelated = new Car();

                    //----id------
                    char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
                    string tmp = selectedCar2.Split(',')[0];
                    int idAfterTrim = Int32.Parse(tmp.Trim(charsToTrim));
                    carToBeDelated.Id = idAfterTrim;

                    //----BrandId------
                    char[] charsToTrim1 = { '{', 'B', 'r', 'a', 'n', 'd', 'I', 'D', '=', ' ' };
                    string tmp1 = selectedCar2.Split(',')[2];
                    int idAfterTrim1 = Int32.Parse(tmp1.Trim(charsToTrim1));
                    carToBeDelated.BrandID = idAfterTrim1;
                    carToBeDelated.Brand = _dbContext.Brands.Find(idAfterTrim1);

                    //----ModelId------
                    char[] charsToTrim2 = { '{', 'M', 'o', 'd', 'e', 'l', 'I', 'D', '=', ' ' };
                    string tmp2 = selectedCar2.Split(',')[4];
                    int idAfterTrim2 = Int32.Parse(tmp2.Trim(charsToTrim2));
                    carToBeDelated.ModelID = idAfterTrim2;
                    carToBeDelated.Model = _dbContext.Models.Find(idAfterTrim2);

                    //----BookingUserId------
                    char[] charsToTrim3 = { '{', 'B', 'o', 'k', 'i', 'n', 'g', 'U', 's', 'e', 'r', 'I', 'D', '=', ' ' };
                    string tmp3 = selectedCar2.Split(',')[6];
                    int idAfterTrim3 = Int32.Parse(tmp3.Trim(charsToTrim3));
                    carToBeDelated.BookingUserID = idAfterTrim3;
                    carToBeDelated.BookingUser = _dbContext.Customers.Find(idAfterTrim3);

                    //------DATA-DO-ZROBIENIA------
                    char[] charsToTrim8 = { '{', 'P', 'r', 'o', 'd', 'u', 't', 'i', 'n', 'Y', 'e', 'a', 'c', '=', ' ' };
                    string tmp8 = selectedCar2.Split(',')[7];
                    string productionYear = tmp8.Trim(charsToTrim8);
                    carToBeDelated.ProductionYear = productionYear;

                    //----Course------
                    char[] charsToTrim4 = { '{', 'C', 'o', 'u', 'r', 's', 'e', '=', ' ' };
                    string tmp4 = selectedCar2.Split(',')[8];
                    string course = tmp4.Trim(charsToTrim4);
                    carToBeDelated.Course = course;

                    //----Capacity------
                    char[] charsToTrim5 = { '{', 'C', 'a', 'p', 'c', 'i', 't', 'y', '=', ' ' };
                    string tmp5 = selectedCar2.Split(',')[9];
                    string capacity = tmp5.Trim(charsToTrim5);
                    carToBeDelated.Capacity = capacity;

                    //----Registration------
                    char[] charsToTrim6 = { '{', 'R', 'e', 'g', 'i', 's', 't', 'r', 'a', 't', 'o', 'n', 'N', 'u', 'm', 'b', '=', ' ' };
                    string tmp6 = selectedCar2.Split(',')[10];
                    string registration = tmp6.Trim(charsToTrim6);
                    carToBeDelated.RegistrationNumber = registration;

                    //----Price------
                    char[] charsToTrim7 = { '{', 'P', 'r', 'i', 'c', 'e', '=', ' ', '}' };
                    string tmp7 = selectedCar2.Split(',')[11];
                    string price = tmp7.Trim(charsToTrim7);
                    carToBeDelated.Price = price;

                    //Usuwanie z bazy
                    _dbContext.Cars.Remove(carToBeDelated);
                    _dbContext.SaveChanges();

                    //wiadomosc wyswietlana na ekranie
                    string messageBoxText = "Removed: \n" + "ID: " + carToBeDelated.Id + "; Brand: " + carToBeDelated.Brand.Name + "; Model: " + carToBeDelated.Model.Name;
                    string caption = "Removal";
                    MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                    MessageBoxImage iconDeleted = MessageBoxImage.Information;
                    MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                    RefreashViews();
                }
                catch (Exception)
                {
                    string messageDeleteCancelled = "This item cannot be deleted, please try restarting the application!";
                    string captionDeleteCancelled = "Removal";
                    MessageBoxButton buttonDeleteCancelled = MessageBoxButton.OK;
                    MessageBoxImage iconDeleteCancelled = MessageBoxImage.Warning;
                    MessageBoxResult resultDeleteCancelled = MessageBox.Show(messageDeleteCancelled, captionDeleteCancelled, buttonDeleteCancelled, iconDeleteCancelled);
                }
            }
            else
            {
                string messageInfo = "Delete canceled!";
                string captionInfo = "Removal";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteBrand(object s, RoutedEventArgs e)
        {

            string messageWarning = "Are you sure you want to delete this item? The operation is irreversible!";
            string captionWarning = "Removal";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);


            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    //Usuwanie z bazy
                    var brandToBeDelated = (s as FrameworkElement).DataContext as Brand;
                    _dbContext.Brands.Remove(brandToBeDelated);
                    _dbContext.SaveChanges();

                    //wiadomosc wyswietlana na ekranie
                    string messageBoxText = "Removed: \n" + "ID: " + brandToBeDelated.Id + "; Name: " + brandToBeDelated.Name;
                    string caption = "Removal";
                    MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                    MessageBoxImage iconDeleted = MessageBoxImage.Information;
                    MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                    RefreashViews();
                }
                catch (Exception)
                {
                    string messageDeleteCancelled = "This item cannot be deleted, please try restarting the application!";
                    string captionDeleteCancelled = "Removal";
                    MessageBoxButton buttonDeleteCancelled = MessageBoxButton.OK;
                    MessageBoxImage iconDeleteCancelled = MessageBoxImage.Warning;
                    MessageBoxResult resultDeleteCancelled = MessageBox.Show(messageDeleteCancelled, captionDeleteCancelled, buttonDeleteCancelled, iconDeleteCancelled);
                }
            }
            else
            {
                string messageInfo = "Delete canceled!";
                string captionInfo = "Removal";
                MessageBoxButton buttonInfo = MessageBoxButton.OK;
                MessageBoxImage iconInfo = MessageBoxImage.Warning;
                MessageBox.Show(messageInfo, captionInfo, buttonInfo, iconInfo);

            }
        }

        private void DeleteModel(object s, RoutedEventArgs e)
        {

            string messageWarning = "Are you sure you want to delete this item? The operation is irreversible!";
            string captionWarning = "Removal";
            MessageBoxButton buttonWarning = MessageBoxButton.YesNo;
            MessageBoxImage iconWarning = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageWarning, captionWarning, buttonWarning, iconWarning);

            
            Model modelToBeDelated = new Model();
            _dbContext.Entry(modelToBeDelated).State = EntityState.Detached;

            if (result == MessageBoxResult.Yes)
            {
                //Usuwanie z bazy
                //var modelToBeDelated = (s as FrameworkElement).DataContext as Model;

                string zmienna = ((s as FrameworkElement).DataContext).ToString();

                //----id------
                char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
                string tmp = zmienna.Split(',')[0];
                int idAfterTrim = Int32.Parse(tmp.Trim(charsToTrim));
                modelToBeDelated.Id = idAfterTrim;

                //-------model-----
                string tmp2 = zmienna.Split(',')[1];
                int find = tmp2.IndexOf('=') + 2;
                int all = tmp2.Length;
                int position = (all - find);
                string name = tmp2.Substring(find, position);
                modelToBeDelated.Name = name;

                //-------marka------
                string tmp3 = zmienna.Split(',')[2];
                int find1 = tmp3.IndexOf('=') + 2;
                int all1 = tmp3.Length;
                int position1 = (all1 - find1) - 2;
                string brand = tmp3.Substring(find1, position1);

                var query = _dbContext.Brands.AsNoTracking();

                var query1 = query.Where(a => a.Name == brand).Single();

                Brand fullBrand = _dbContext.Brands.Find(query1.Id);
                modelToBeDelated.Brand = fullBrand;

                try
                {
                    _dbContext.Models.Remove(modelToBeDelated);
                    _dbContext.SaveChanges();

                    //wiadomosc wyswietlana na ekranie
                    string messageBoxText = "Removed: \n" + "ID: " + modelToBeDelated.Id + "; Name: " + modelToBeDelated.Name; ;
                    string caption = "Removal";
                    MessageBoxButton buttonDeleted = MessageBoxButton.OK;
                    MessageBoxImage iconDeleted = MessageBoxImage.Information;
                    MessageBox.Show(messageBoxText, caption, buttonDeleted, iconDeleted);
                    RefreashViews();
                }
                catch (Exception)
                {
                    string messageDeleteCancelled = "This item cannot be deleted, please try restarting the application!";
                    string captionDeleteCancelled = "Removal";
                    MessageBoxButton buttonDeleteCancelled = MessageBoxButton.OK;
                    MessageBoxImage iconDeleteCancelled = MessageBoxImage.Warning;
                    MessageBoxResult resultDeleteCancelled = MessageBox.Show(messageDeleteCancelled, captionDeleteCancelled, buttonDeleteCancelled, iconDeleteCancelled);
                }
            }
            else
            {
                string messageInfo = "Delete canceled!";
                string captionInfo = "Removal";
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
            string messageQuestion = "Are you sure you want to edit the selected item?";
            string captionQuestion = "Edition";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if(result == MessageBoxResult.Yes)
            {
                selectedCustomer = (s as FrameworkElement).DataContext as Customer;
                WindowUpdateCustomer updateWindow = new WindowUpdateCustomer(selectedCustomer, this._dbContext);
                updateWindow.Show();
                this.Close();
            }
            else
            {
                string messageEditionCancelled = "Edition cancelled!";
                string captionEditionCancelled = "Edition";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
                   
        }

        //---------DO-ZROBIENIA----------------
        Car selectedCar = new Car();
        private void UpdateCar(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Are you sure you want to edit the selected item?";
            string captionQuestion = "Edition";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if (result == MessageBoxResult.Yes)
            {
                var selectedCar2 = (s as FrameworkElement).DataContext.ToString();

                //----id------
                char[] charsToTrim = { '{', 'I', 'd', '=', ' ' };
                string tmp = selectedCar2.Split(',')[0];
                int idAfterTrim = Int32.Parse(tmp.Trim(charsToTrim));
                selectedCar.Id = idAfterTrim;

                //----BrandId------
                char[] charsToTrim1 = { '{', 'B', 'r', 'a', 'n', 'd', 'I', 'D', '=', ' ' };
                string tmp1 = selectedCar2.Split(',')[2];
                int idAfterTrim1 = Int32.Parse(tmp1.Trim(charsToTrim1));
                selectedCar.BrandID = idAfterTrim1;
                selectedCar.Brand = _dbContext.Brands.Find(idAfterTrim1);

                //----ModelId------
                char[] charsToTrim2 = { '{', 'M', 'o', 'd', 'e', 'l', 'I', 'D', '=', ' ' };
                string tmp2 = selectedCar2.Split(',')[4];
                int idAfterTrim2 = Int32.Parse(tmp2.Trim(charsToTrim2));
                selectedCar.ModelID = idAfterTrim2;
                selectedCar.Model = _dbContext.Models.Find(idAfterTrim2);

                //----BookingUserId------
                char[] charsToTrim3 = { '{', 'B', 'o', 'k', 'i', 'n', 'g', 'U', 's', 'e', 'r', 'I', 'D', '=', ' ' };
                string tmp3 = selectedCar2.Split(',')[6];
                int idAfterTrim3 = Int32.Parse(tmp3.Trim(charsToTrim3));
                selectedCar.BookingUserID = idAfterTrim3;
                selectedCar.BookingUser = _dbContext.Customers.Find(idAfterTrim3);

                //------DATA-DO-ZROBIENIA------
                char[] charsToTrim8 = { '{', 'P', 'r', 'o', 'd', 'u', 't', 'i', 'n', 'Y', 'e', 'a', 'c', '=', ' ' };
                string tmp8 = selectedCar2.Split(',')[7];
                string productionYear = tmp8.Trim(charsToTrim8);
                selectedCar.ProductionYear = productionYear;

                //----Course------
                char[] charsToTrim4 = { '{', 'C', 'o', 'u', 'r', 's', 'e', '=', ' ' };
                string tmp4 = selectedCar2.Split(',')[8];
                string course = tmp4.Trim(charsToTrim4);
                selectedCar.Course = course;

                //----Capacity------
                char[] charsToTrim5 = { '{', 'C', 'a', 'p', 'c', 'i', 't', 'y', '=', ' ' };
                string tmp5 = selectedCar2.Split(',')[9];
                string capacity = tmp5.Trim(charsToTrim5);
                selectedCar.Capacity = capacity;

                //----Registration------
                char[] charsToTrim6 = { '{', 'R', 'e', 'g', 'i', 's', 't', 'r', 'a', 't', 'o', 'n', 'N', 'u', 'm', 'b', '=', ' ' };
                string tmp6 = selectedCar2.Split(',')[10];
                string registration = tmp6.Trim(charsToTrim6);
                selectedCar.RegistrationNumber = registration;

                //----Price------
                char[] charsToTrim7 = { '{', 'P', 'r', 'i', 'c', 'e', '=', ' ', '}' };
                string tmp7 = selectedCar2.Split(',')[11];
                string price = tmp7.Trim(charsToTrim7);
                selectedCar.Price = price;

                WindowUpdateCar updateWindow = new WindowUpdateCar(selectedCar, this._dbContext);
                updateWindow.Show();
                this.Close();
            }
            else
            {
                string messageEditionCancelled = "Edition cancelled!";
                string captionEditionCancelled = "Edition";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }
        }

        //-------działa--------------
        Brand selectedBrand = new Brand();
        private void UpdateBrand(object s, RoutedEventArgs e)
        {
            string messageQuestion = "Are you sure you want to edit the selected item?";
            string captionQuestion = "Edition";
            MessageBoxButton buttonQuestion = MessageBoxButton.YesNo;
            MessageBoxImage iconQuestion = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageQuestion, captionQuestion, buttonQuestion, iconQuestion);


            if (result == MessageBoxResult.Yes)
            {
                selectedBrand = (s as FrameworkElement).DataContext as Brand;
                WindowUpdateBrand updateWindow = new WindowUpdateBrand(selectedBrand, this._dbContext);
                updateWindow.Show();
                this.Close();
            }
            else
            {
                string messageEditionCancelled = "Edition cancelled!";
                string captionEditionCancelled = "Edition";
                MessageBoxButton buttonEditionCancelled = MessageBoxButton.OK;
                MessageBoxImage iconEditionCancelled = MessageBoxImage.Warning;
                MessageBoxResult resultEditionCancelled = MessageBox.Show(messageEditionCancelled, captionEditionCancelled, buttonEditionCancelled, iconEditionCancelled);
            }

        }

        Model selectedModel = new Model();
        
        private void UpdateModel(object s, RoutedEventArgs e)
        {
            //-----------------czysci-sledzenie-obiektu(pozwala-na-wielokrotna-edycje-tej-samej-pozycji)-----------------------
            _dbContext.Entry(selectedModel).State = EntityState.Detached;
            string messageQuestion = "Are you sure you want to edit the selected item?";
            string captionQuestion = "Edition";
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

                var query = _dbContext.Brands;
                var query1 = query.Where(a => a.Name == brand).Single();
                            
                Brand fullBrand = _dbContext.Brands.Find(query1.Id);
                selectedModel.Brand = fullBrand;

                
                WindowUpdateModel updateWindow = new WindowUpdateModel(selectedModel, this._dbContext);
                updateWindow.Show();
                this.Close();
            }
            else
            {
                string messageEditionCancelled = "Edition cancelled!";
                string captionEditionCancelled = "Edition";
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

            //System.Windows.Application.Current.Shutdown();

        }

        /// <summary>
        /// Publiczna metoda do odswiezania
        /// </summary>
        public void RefreashViews()
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
