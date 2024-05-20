using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pr_magazin
{
    
    /// <summary>
    /// Логика взаимодействия для izmenenie_tov.xaml
    /// </summary>
    public partial class izmenenie_tov : Window
    {
        private int currentUserId;
        public magazinEntities14 db = new magazinEntities14();
        public tovar selectedProduct;
        public izmenenie_tov(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }


        public void save_tov()
        {
            if (selectedProduct != null)
            {
                try
                {
                    
                    selectedProduct.name = ProductNameTextBox.Text;
                    selectedProduct.color = ProductColorTextBox.Text;
                    selectedProduct.size = ProductSizeTextBox.Text.ToString().Length;               
                    selectedProduct.gender = ProductGenderTextBox.Text;
                    selectedProduct.price = ProductPriceTextBox.Text.ToString().Length;

                    selectedProduct.brend = BrendTextBox.Text;

                    
                    db.SaveChanges();
                    MessageBox.Show("Изменения успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Сначала найдите товар по его ID.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void Isfailed()
        {
            if (selectedProduct != null)
            {
                try
                {
                    // Проверяем, что все обязательные поля заполнены
                    if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                        string.IsNullOrWhiteSpace(ProductColorTextBox.Text) ||
                        string.IsNullOrWhiteSpace(ProductSizeTextBox.Text) ||
                        string.IsNullOrWhiteSpace(ProductGenderTextBox.Text) ||
                        string.IsNullOrWhiteSpace(ProductPriceTextBox.Text) ||
                        string.IsNullOrWhiteSpace(BrendTextBox.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Обновляем данные товара в базе данных
                    selectedProduct.name = ProductNameTextBox.Text;
                    selectedProduct.color = ProductColorTextBox.Text;
                    selectedProduct.size = ProductSizeTextBox.Text.ToString().Length;
                    selectedProduct.gender = ProductGenderTextBox.Text;
                    selectedProduct.price = ProductPriceTextBox.Text.ToString().Length;
                    selectedProduct.brend = BrendTextBox.Text;

                    // Сохраняем изменения
                    db.SaveChanges();
                    MessageBox.Show("Изменения успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Сначала найдите товар по его ID.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void save(object sender, RoutedEventArgs e)

        {
            save_tov();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            adminprofile adminprofile = new adminprofile(currentUserId);

            adminprofile.Show();
            this.Close();
        }
        public void SearchProductById(int productId)
        {
            try
            {
                
                selectedProduct = db.tovar.FirstOrDefault(p => p.id == productId);

                if (selectedProduct != null)
                {
                    
                    ProductDataPanel.Visibility = Visibility.Visible;
                    ProductNameTextBox.Text = selectedProduct.name;
                    ProductColorTextBox.Text = selectedProduct.color;
                    ProductSizeTextBox.Text = selectedProduct.size.ToString();
                    ProductGenderTextBox.Text = selectedProduct.gender;
                    ProductPriceTextBox.Text = selectedProduct.price.ToString();
                    BrendTextBox.Text = selectedProduct.brend;
                    if (!string.IsNullOrEmpty(selectedProduct.image_tovar))
                        ProductImage.Source = LoadImage(selectedProduct.image_tovar); 
                }
                else
                {
                    
                    MessageBox.Show("Товар с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void search(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProductIdTextBox.Text, out int productId))
            {
                SearchProductById(productId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID товара.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем полный путь к выбранному изображению
                string imagePath = openFileDialog.FileName;

                try
                {
                    // Обновляем изображение товара
                    selectedProduct.image_tovar = imagePath;
                    ProductImage.Source = LoadImage(imagePath); // Загрузка нового изображения товара
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private BitmapImage LoadImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            return bitmap;
        }
    }
}

    

