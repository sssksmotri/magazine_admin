using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Pr_magazin
{
   
    public partial class podderzhka : Window
    {
        private int currentUserId;
        public podderzhka(int usersId)
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

                MessageBox.Show("Изображение успешно обновлено.");
                UpdateProfileImageFromDatabase(currentUserId);

            }
        }


        private void UpdateProfileImageFromDatabase(int userId)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                support_zhaloba support_Zhaloba = db.support_zhaloba.FirstOrDefault(z => z.id == userId);
                if (support_Zhaloba != null)
                {
                    string imagePath = support_Zhaloba.user_image;

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
                support_zhaloba support_Zhaloba = db.support_zhaloba.FirstOrDefault(z => z.id == userId);
                if (support_Zhaloba != null)
                {
                    
                    support_Zhaloba.user_image  = imagePath;

                   
                    db.SaveChanges();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            StringBuilder errorMessage = new StringBuilder();
            if (!Regex.IsMatch(users_email.Text, @"^(?=.*[a-zA-Z])[\p{L}0-9!#$%^&*()-_]+@[\p{L}0-9!#$%^&*()-_]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный адрес электронной почты.");
                hasError = true;
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
                support_zhaloba support_Zhaloba = new support_zhaloba()
                {
                    user_id = currentUserId,
                    message_user = users_message.Text,
                    email_users = users_email.Text
                };

               
                if (!string.IsNullOrEmpty(imageControl.Source?.ToString()))
                {
                    BitmapImage bitmap = (BitmapImage)imageControl.Source;
                    string imagePath = bitmap.UriSource?.LocalPath;
                    support_Zhaloba.user_image = imagePath;
                }

                db.support_zhaloba.Add(support_Zhaloba); 
                db.SaveChanges(); 

                MessageBox.Show("Спасибо, мы стараемся улучшить приложение");
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
