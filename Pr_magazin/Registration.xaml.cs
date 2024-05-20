using System;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;

namespace Pr_magazin
{
    
    public partial class Registration : Window
    {
        
        public Registration()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            avtorizasia avtorizasia = new avtorizasia();
            avtorizasia.Show();
            this.Close();
        }
        private int currentUserId;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            StringBuilder errorMessage = new StringBuilder();

            if (!Regex.IsMatch(UsernameTextBox.Text, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное имя (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(UsersurnameTextBox.Text, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректную фамилию (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(numberTextBox.Text, @"^\d{8}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный номер телефона (ровно 8 цифр).");
                hasError = true;
            }

            DateTime minDate = new DateTime(1950, 1, 1);
            DateTime maxDate = new DateTime(2022 , 12, 31);

            if (!DateTime.TryParseExact(UserDate_of_birthdayTextBox.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректную дату рождения (дд.мм.гггг).");
                hasError = true;
            }
            else if (parsedDate < minDate || parsedDate > maxDate)
            {
                errorMessage.AppendLine($"Пожалуйста, введите дату рождения корректно");
                hasError = true;
            }
            string gender = UsergenderTextBox.Text.ToLower();
            if (gender != "мужской" && gender != "женский")
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный пол (допустимые значения: мужской, женский).");
                hasError = true;
            }

            if (!Regex.IsMatch(UserloginTextBox.Text, @"^[a-zA-Z0-9_]{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный логин (только буквы, цифры и _).");
                hasError = true;
            }

            if (!Regex.IsMatch(emailTextBox.Text, @"^(?=.*[a-zA-Z])[\p{L}0-9!#$%^&*()-_]+@[\p{L}0-9!#$%^&*()-_]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный адрес электронной почты.");
                hasError = true;
            }

            if (!Regex.IsMatch(PasswordBox.Text, @"^(?=.*[a-zA-Z])(?=.*[0-9!()-_]).{8,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный пароль (минимум 8 символов, хотя бы 1 буква, цифра или символ !()-_).");
                hasError = true;
            }

            if (hasError)
            {
                MessageBox.Show(errorMessage.ToString());
                return;
            }
            using (magazinEntities14 db = new magazinEntities14())
            {
                users user = new users()
                {
                    name = UsernameTextBox.Text,
                    surname = UsersurnameTextBox.Text,
                    number = int.Parse(numberTextBox.Text),
                    date_of_birthday = UserDate_of_birthdayTextBox.Text,
                    gender = UsergenderTextBox.Text,
                    login = UserloginTextBox.Text,
                    email = emailTextBox.Text,
                    password = PasswordBox.Text, 
                };
                MessageBox.Show("Вы успешно зарегестировались в системе");
                currentUserId = user.id;
                db.users.Add(user);
                db.SaveChanges();
                avtorizasia avtorizasia = new avtorizasia();
                avtorizasia.Show();
                this.Close();
            }
        }
    }
}
