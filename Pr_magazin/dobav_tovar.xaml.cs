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
    /// Логика взаимодействия для dobav_tovar.xaml
    /// </summary>
    public partial class dobav_tovar : Window
    {
        public magazinEntities14 db = new magazinEntities14();
        private int currentUserId;
        private string imagePath;
        public tovar newProduct { get; set; } = new tovar();
        public dobav_tovar(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        

        public void button_image(object sender, RoutedEventArgs e)
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
                    
                    this.imagePath = imagePath; // Сохраняем путь к изображению
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Add_product(object sender, RoutedEventArgs e)
        {
            if (IsFieldsFilled())
            {
                create_tov();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ColorTextBox.Text) ||
                string.IsNullOrWhiteSpace(SizeTextBox.Text) ||
                string.IsNullOrWhiteSpace(GenderTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(BrendTextBox.Text) ||
                string.IsNullOrWhiteSpace(imagePath))
            {
                return false;
            }
            return true;
        }

        public void create_tov()
        {
            try
            {
                decimal? price = null;
                decimal priceValue;
                int? size = null;
                int sizeValue;
                tovar newProduct = new tovar
                {
                    name = NameTextBox.Text,
                    color = ColorTextBox.Text,
                    size = size,
                    gender = GenderTextBox.Text,
                    price = price,
                    brend = BrendTextBox.Text,
                    image_tovar = imagePath
                };

                db.tovar.Add(newProduct);
                db.SaveChanges();

                MessageBox.Show("Товар успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminprofile adminprofile = new adminprofile(currentUserId);

            adminprofile.Show();
            this.Close();
        }
    }
}
