using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pr_magazin
{
   
    public partial class MainWindow : Window
    {
        public ObservableCollection<tovar> userList = new ObservableCollection<tovar>();
        private int currentUserId;
        public magazinEntities14 context;
        public MainWindow(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            UpdateProfileImageFromDatabase(currentUserId);
            LoadProductData(currentUserId);

        }
        public void LoadProductData(int userId)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                var productsList = db.tovar.ToList();

                tovar.Children.Clear();

                double topPosition = 245;
                double leftPosition = 80;

                int productIndex = 0;

                foreach (var product in productsList)
                {
                    products productControl = new products(userId);

                    productControl.NameTextBlock.Text = product.name;
                    productControl.ColorTextBlock.Text = product.color;
                    productControl.SizeTextBlock.Text = product.size.ToString();
                    productControl.GenderTextBlock.Text = product.gender;
                    productControl.BrendTextBlock.Text = product.brend;
                    productControl.priceTextBlock.Text = product.price.ToString();
                    productControl.tovar.Source = new BitmapImage(new Uri(product.image_tovar));
                    Canvas.SetTop(productControl, topPosition);
                    Canvas.SetLeft(productControl, leftPosition);

                    tovar.Children.Add(productControl);

                    productIndex++;

                    if (productIndex % 3 == 0)
                    {
                        topPosition += 460;
                        leftPosition = 80;
                    }
                    else
                    {
                        leftPosition += 360;
                    }
                }
            }
        }


        private void UpdateProfileImageFromDatabase(int userId)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                users user = db.users.FirstOrDefault(u => u.id == userId);
                if (user != null)
                {
                    string imagePath = user.image_path;

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        try
                        {
                            BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                            imageControl.Source = bitmap;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Выводите сообщение об отсутствии изображения
                        MessageBox.Show("Отсутствует изображение профиля.");
                    }
                }
            }
        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            korzina korzinaWindow = new korzina(currentUserId, currentUserId);
            korzinaWindow.Show();
            this.Close();
        }

      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            profile profile = new profile(currentUserId);
            profile.Show();
            this.Close();
        }

       

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(currentUserId);

            mainWindow.Show();
            this.Close();
        }


       

       


        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчики: Саидов Н.Н., Садыков А.М.,Нечаев М.Д. гр.4338 ");
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Close();
        }

        




        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            using (magazinEntities14     db = new magazinEntities14())
            {
                var selectedBrand = (BrandComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var selectedGender = (genderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var selectedSize = (SizeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var selectedColor = (ColorComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
              

                var filteredProducts = db.tovar
                    .Where(p =>
                        (selectedBrand == null || p.brend == selectedBrand) &&
                        (selectedSize == null || p.size.ToString() == selectedSize) &&
                        (selectedColor == null || p.color == selectedColor) &&
                        (selectedGender == null || p.gender == selectedGender))
                    .ToList();

                DisplayFilteredProducts(filteredProducts);
            }
        }
                private void DisplayFilteredProducts(List<tovar> filteredProducts)
        {
            tovar.Children.Clear();

            double topPosition = 245;
            double leftPosition = 80;

            int productIndex = 0;

            foreach (var product in filteredProducts)
            {
                products productControl = new products(currentUserId);

                productControl.NameTextBlock.Text = product.name;
                productControl.ColorTextBlock.Text = product.color;
                productControl.SizeTextBlock.Text = product.size.ToString();
                productControl.GenderTextBlock.Text = product.gender;
                productControl.BrendTextBlock.Text = product.brend;
                productControl.priceTextBlock.Text = product.price.ToString();

                productControl.tovar.Source = new BitmapImage(new Uri(product.image_tovar));

                Canvas.SetTop(productControl, topPosition);
                Canvas.SetLeft(productControl, leftPosition);

                tovar.Children.Add(productControl);

                productIndex++;

                if (productIndex % 3 == 0)
                {
                    topPosition += 460;
                    leftPosition = 80;
                }
                else
                {
                    leftPosition += 360;
                }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                using (magazinEntities14 db = new magazinEntities14())
                {
                    var searchResults = db.tovar
                        .Where(p =>
                            p.name.Contains(searchText) ||
                            p.color.Contains(searchText) ||
                            p.gender.Contains(searchText) ||
                            p.brend.Contains(searchText) ||
                            p.price.ToString().Contains(searchText))
                        .ToList();

                    DisplayFilteredProducts(searchResults);
                }
            }
            else
            {
                
                LoadProductData(currentUserId);
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            BrandComboBox.SelectedItem = null;
            SizeComboBox.SelectedItem = null;
            ColorComboBox.SelectedItem = null;


            LoadProductData(currentUserId);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            podderzhka podderzhka = new podderzhka(currentUserId);
            podderzhka.Show();
            this.Close();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            podderzhka_spasibo podderzhka_Spasibo = new podderzhka_spasibo(currentUserId);
            podderzhka_Spasibo.Show();
            this.Close();
        }

        
    }

}

    

