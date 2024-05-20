using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Pr_magazin
{
    /// <summary>
    /// Логика взаимодействия для korzina.xaml
    /// </summary>
    public partial class korzina : Window

    {
        //private int selectedProductId;
        private int currentUserId;
        private decimal totalPrice;
        private int currentOrderId;
        public korzina(int userId, int order_id)
        {
            InitializeComponent();
            currentUserId = userId;
            LoadCartItems();
            CalculateTotalPrice();
            currentOrderId = order_id;
        }
        private void CalculateTotalPrice()
        {
          
            using (magazinEntities14 db = new magazinEntities14())
            {
              
                decimal? total = db.orders
                    .Where(o => o.users_id == currentUserId)
                    .Sum(o => o.tovar.price);

                if (total.HasValue)
                {
                    totalPrice = total.Value;
                   
                    price.Text = $"Общая сумма: {totalPrice:C}";
                }
                else
                {
                   
                    price.Text = "Ваша корзина пуста.";
                }
            }
        }
        private void LoadCartItems()
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                var cartItems = db.orders
                    .Where(o => o.users_id == currentUserId)
                    .ToList();

                for_tovar.Children.Clear();

                double topPosition = 0;
                double leftPosition = 0;

                int productIndex = 0;

                foreach (var item in cartItems)
                {
                    products2 productControl = new products2(currentUserId);

                    
                    productControl.NameTextBlock.Text = item.tovar.name;
                    productControl.ColorTextBlock.Text = item.tovar.color;
                    productControl.SizeTextBlock.Text = item.tovar.size.ToString();
                    productControl.GenderTextBlock.Text = item.tovar.gender;
                    productControl.BrendTextBlock.Text = item.tovar.brend;
                    productControl.priceTextBlock.Text = item.tovar.price.ToString();
                   

                   
                    productControl.tovar.Source = new BitmapImage(new Uri(item.tovar.image_tovar));

                    double productLeftPosition = leftPosition + (productIndex % 2) * 360;

                    
                    Canvas.SetTop(productControl, topPosition);
                    Canvas.SetLeft(productControl, productLeftPosition);

                    
                    for_tovar.Children.Add(productControl);

                    productIndex++;

                    if (productIndex % 2 == 0)
                    {
                        
                        topPosition += 470;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool hasError = false;
            StringBuilder errorMessage = new StringBuilder();

            if (!Regex.IsMatch(AddressTextBox.Text, @"^[a-zA-Zа-яА-Я0-9/-].{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный адрес ");
                hasError = true;
            }

            if (!Regex.IsMatch(CityTextBox.Text, @"^[a-zA-Zа-яА-Я0-9/-].{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное название города");
                hasError = true;
            }

            if (!Regex.IsMatch(CountryTextBox.Text, @"^[a-zA-Zа-яА-Я0-9/-].{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное название страны");
                hasError = true;
            }

            if (hasError)
            {
                MessageBox.Show(errorMessage.ToString());
                return;
            }

            using (magazinEntities14 db = new magazinEntities14())
            {
                // Создаем запись в таблице adres_dostavki
                adres_dostavki newAddress = new adres_dostavki
                {
                    address = AddressTextBox.Text,
                    city = CityTextBox.Text,
                    country = CountryTextBox.Text,
                    users_id = currentUserId,
                    orders_id = currentOrderId
                };

                db.adres_dostavki.Add(newAddress);
                db.SaveChanges();

                int newAddressId = newAddress.id;

                // Создаем заказы в таблице orders
                var cartItems = db.orders
                    .Where(o => o.users_id == currentUserId)
                    .ToList();

                foreach (var item in cartItems)
                {
                    orders newOrder = new orders
                    {
                        users_id = currentUserId,
                        tovar_id = item.tovar_id,
                        order_id = newAddressId
                    };

                    db.orders.Add(newOrder);
                }

                db.SaveChanges();

                var ordersToDelete = db.orders.Where(o => o.users_id == currentUserId).ToList();
                foreach (var orderToDelete in ordersToDelete)
                {
                    db.orders.Remove(orderToDelete);
                }

                db.SaveChanges();
                for_tovar.Children.Clear();

                MessageBox.Show("Заказ успешно оформлен и корзина очищена");
            }
        }
   
    
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(currentUserId);
            mainWindow.Show();
            this.Close();
        }
    }
}
