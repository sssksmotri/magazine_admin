using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для adminprofile.xaml
    /// </summary>
    public partial class adminprofile : Window
    {
        public ObservableCollection<tovar> userList = new ObservableCollection<tovar>();
        public int currentUserId;
        public magazinEntities14 db = new magazinEntities14();
        public Canvas tovarCanvas;
        public adminprofile(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            LoadProductData(currentUserId);
            //UpdateProfileImageFromDatabase(currentUserId);
            magazinEntities14 magazinEntities14 = new magazinEntities14();

            
        }
        public void LoadProductData(int userId)
        {
            try
            {
                
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
            catch (Exception ex)
            {
                // Ловим и выводим ошибку для каждого продукта, если что-то идет не так при создании productControl
                MessageBox.Show($"Ошибка при обработке продукта: {ex.Message}");
            }
        }


        //private void UpdateProfileImageFromDatabase(int userId)
        //{
        //    using (magazinEntities14 db = new magazinEntities14())
        //    {
        //        users user = db.users.FirstOrDefault(u => u.id == userId);
        //        if (user != null)
        //        {
        //            string imagePath = user.image_path;

        //            if (!string.IsNullOrEmpty(imagePath))
        //            {
        //                try
        //                {
        //                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
        //                    imageControl.Source = bitmap;
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
        //                }
        //            }
        //            else
        //            {
        //                // Выводите сообщение об отсутствии изображения
        //                MessageBox.Show("Отсутствует изображение профиля.");
        //            }
        //        }
        //    }
        //}
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            dobav_tovar Dobav_Tovar = new dobav_tovar(currentUserId);
            Dobav_Tovar.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            izmenenie_tov izmenenie_Tov = new izmenenie_tov(currentUserId);
            izmenenie_Tov.Show();
            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            delete_tovar delete_Tov = new delete_tovar(currentUserId);
            delete_Tov.Show();
            this.Close();
        }

        

        private void sortirovat(object sender, RoutedEventArgs e)
        {

        }

        private void sbros(object sender, RoutedEventArgs e)
        {

        }

        private void poisk(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            profile profile = new profile(currentUserId);
            profile.Show();
            this.Close();
        }
    }
}
