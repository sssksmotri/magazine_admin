using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Pr_magazin
{
    public partial class profile : Window
    {
        private int currentUserId;
        private ObservableCollection<users> userDataList = new ObservableCollection<users>();

        public profile(int userId)
        {

            InitializeComponent();
            currentUserId = userId;
            UpdateProfileImageFromDatabase(currentUserId);
            LoadUserData(currentUserId);
        }
        private void LoadUserData(int userId)
        {
            using (magazinEntities14 db = new magazinEntities14())
            {
                users user = db.users.FirstOrDefault(u => u.id == userId);
                if (user != null)
                {
                    name1.Text = user.name;
                    surname1.Text = user.surname;
                    number1.Text = user.number.ToString(); 
                    Date_of_birthday.Text = user.date_of_birthday;
                    gender1.Text = user.gender;
                    privet_user.Text= user.name;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            StringBuilder errorMessage = new StringBuilder();

            if (!Regex.IsMatch(name1.Text, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное имя (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(surname1.Text, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректную фамилию (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(number1.Text, @"^\d{8}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный номер телефона (ровно 8 цифр).");
                hasError = true;
            }
            DateTime minDate = new DateTime(1950, 1, 1);
            DateTime maxDate = new DateTime(2023, 12, 31);

            if (!DateTime.TryParseExact(Date_of_birthday.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректную дату рождения (дд.мм.гггг).");
                hasError = true;
            }
            else if (parsedDate < minDate || parsedDate > maxDate)
            {
                errorMessage.AppendLine($"Пожалуйста, введите дату рождения корректно");
                hasError = true;
            }

            string gender = gender1.Text.ToLower();
            if (gender != "мужской" && gender != "женский")
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный пол (допустимые значения: мужской, женский).");
                hasError = true;
            }

            if (!hasError)
            {
                UpdateUserData(currentUserId, name1.Text, surname1.Text, number1.Text, Date_of_birthday.Text, gender1.Text);
                MessageBox.Show("Данные успешно обновлены.");
            }
            else
            {
                MessageBox.Show(errorMessage.ToString(), "Ошибка ввода");
            }
        }

       
        private void UpdateUserData(int userId, string newName, string newSurname, string newNumber, string newDateOfBirth, string newGender)
        {
            using ( magazinEntities14 db = new magazinEntities14())
            {
                users updatedUser = db.users.FirstOrDefault(u => u.id == userId);
                if (updatedUser != null)
                {
                    
                    updatedUser.name = newName;
                    updatedUser.surname = newSurname;
                    updatedUser.number = int.Parse(newNumber); 
                    updatedUser.date_of_birthday = newDateOfBirth;
                    updatedUser.gender = newGender;

                    
                    db.SaveChanges();
                }
            }
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(currentUserId);

            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
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
                users user = db.users.FirstOrDefault(u => u.id == userId);
                if (user != null)
                {
                    string imagePath = user.image_path;

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
                users updatedUser = db.users.FirstOrDefault(u => u.id == userId);
                if (updatedUser != null)
                {
                    
                    updatedUser.image_path = imagePath;

                    
                    db.SaveChanges();
                }
            }
        }
    }
}
