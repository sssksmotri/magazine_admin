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
using System.Windows.Shapes;

namespace Pr_magazin
{
    /// <summary>
    /// Логика взаимодействия для delete_tovar.xaml
    /// </summary>
    public partial class delete_tovar : Window
    {
        public magazinEntities14 db = new magazinEntities14();
        private int currentUserId;
        public delete_tovar(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProductIdTextBox.Text, out int productId))
            {
                delete_tov(productId); 
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID товара.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void delete_tov(int productId)
        {
            if (productId <= 0)
            {
                MessageBox.Show("ID товара должен быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
            var productToDelete = db.tovar.FirstOrDefault(p => p.id == productId);

            if (productToDelete != null)
            {
                try
                {
                    
                    db.tovar.Remove(productToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Товар успешно удален из базы данных.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Товар с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
