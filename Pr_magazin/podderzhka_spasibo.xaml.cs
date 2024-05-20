using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Pr_magazin
{
    
    public partial class podderzhka_spasibo : Window
    {
        private int currentUserId;
        public podderzhka_spasibo(int usersId)
        {
            InitializeComponent();
            currentUserId = usersId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
              
                string imagePath = openFileDialog.FileName;

               
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                imageControl.Source = bitmap;

               
                SaveImagePathToDatabase(currentUserId, imagePath);

                MessageBox.Show("Изображение успешно добавлено.");
                UpdateProfileImageFromDatabase(currentUserId);

            }
        }


        private void UpdateProfileImageFromDatabase(int userId)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
               support_spasibo support_Spasibo = db.support_spasibo.FirstOrDefault(z => z.id == userId);
                if (support_Spasibo != null)
                {
                    string imagePath = support_Spasibo.user_image;

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                        imageControl.Source = bitmap;
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        private void SaveImagePathToDatabase(int userId, string imagePath)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                support_spasibo support_Spasibo = db.support_spasibo.FirstOrDefault(z => z.id == userId);
                if (support_Spasibo != null)
                {
                   
                    support_Spasibo.user_image = imagePath;

                    
                    db.SaveChanges();
                }
            }
        }

    

    private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            StringBuilder errorMessage = new StringBuilder();
            if (!Regex.IsMatch(users_email.Text, @"^(?!.*@.*@)(?!.*?\.\.)[\p{L}0-9!#$%^&*()-_]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                
                MessageBox.Show("Пожалуйста, введите корректный адрес электронной почты.");
                return;
            }

            if (!Regex.IsMatch(users_message.Text, @"^(?=.*[a-zA-Z])[\p{L}0-9!#$%^&*()-_]{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректно свой запрос.");
                hasError = true;
            }
            if (hasError)
            {
                MessageBox.Show(errorMessage.ToString());
                return;
            }
            using (magazinEntities14 db = new magazinEntities14())
            {
                support_spasibo support_Spasibo = new support_spasibo()
                {
                    user_id = currentUserId,
                    message_user = users_message.Text,
                    email_users = users_email.Text
                };

               
                if (!string.IsNullOrEmpty(imageControl.Source?.ToString()))
                {
                    BitmapImage bitmap = (BitmapImage)imageControl.Source;
                    string imagePath = bitmap.UriSource?.LocalPath;
                    support_Spasibo.user_image = imagePath;
                }
                MessageBox.Show("Спасибо, за вашу благодарность");
                db.support_spasibo.Add(support_Spasibo); 

                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(currentUserId);
            mainWindow.Show();
            this.Close();
        }
    }
}
