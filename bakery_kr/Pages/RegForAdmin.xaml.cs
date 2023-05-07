using bakery_kr.Entities;
using ClassLibrary;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegForAdmin.xaml
    /// </summary>
    public partial class RegForAdmin : Page
    {
        string AppPath = Directory.GetCurrentDirectory();
        private Entities.User _currentUser = null; //Получение данных пользователей из бд в переменную 
        public RegForAdmin()
        {
            InitializeComponent();
            ComboTypeRole.ItemsSource = App.Context.Role.Select(c => c.Role_name).ToList();
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\notvisib.png"));
        }

        private void BtnRLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckErrorsPass().Length != 0)
                {
                    MessageBox.Show(CheckErrorsPass());
                    return;
                }
                else
                {
                    int idRol;
                    switch (ComboTypeRole.Text) //Администраторы могут регистрировать аккаунты любой роли
                    {
                        case "Администратор":
                            idRol = 1;
                            break;
                        case "Пользователь":
                            idRol = 2;
                            break;
                        case "Пекарь":
                            idRol = 3;
                            break;
                    }
                    var idRole = App.Context.Role.Where(c => c.Role_name == ComboTypeRole.Text).Select(c => c.Id).FirstOrDefault();

                    var service = new User
                    {
                        Login = TBoxRLogin.Text,
                        Password = PBoxRPassword.Password.ToString(),
                        Roled = idRole,
                    };
                    App.Context.User.Add(service);
                    App.Context.SaveChanges();
                    MessageBox.Show("Пользователь добавлен");
                    NavigationService.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления нового аккаунта.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ImgShowHide_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword();
        }

        private void ImgShowHide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPassword();
        }
        private void ImgShowHide_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePassword();
        }

        void ShowPassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\visib.png"));
            VisibTBoxRPassword.Visibility = Visibility.Visible;
            PBoxRPassword.Visibility = Visibility.Hidden;
            VisibTBoxRPassword.Text = PBoxRPassword.Password;
        }
        void HidePassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\notvisib.png"));
            VisibTBoxRPassword.Visibility = Visibility.Hidden;
            PBoxRPassword.Visibility = Visibility.Visible;
            PBoxRPassword.Focus();
        }

        private string CheckErrorsPass() //Проверка условий для пароля
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxRLogin.Text))
            {
                errorBuilder.AppendLine("Поле логин требуется обязательно заполнить.");
            }
            if (TBoxRLogin.Text.Length < 5)
            {
                errorBuilder.AppendLine("Логин должен иметь длину от 5 символов.");
            }
            if (TBoxRLogin.Text.Length > 18)
            {
                errorBuilder.AppendLine("Логин должен иметь длину до 18 символов.");
            }
            if (PBoxRPassword.Password.Length < 8)
            {
                errorBuilder.AppendLine("Пароль должен иметь длину от 8 символов.");
            }
            if (PBoxRPassword.Password.Length > 18)
            {
                errorBuilder.AppendLine("Пароль должен иметь длину до 18 символов.");
            }
            if (String.IsNullOrEmpty(ComboTypeRole.Text))
            {
                errorBuilder.AppendLine("Выбор роли аккаунта обязателен.");
            }
            var userFromDB = App.Context.User.ToList().FirstOrDefault(p => p.Login == TBoxRLogin.Text);
            if (_currentUser != userFromDB && userFromDB != null)
            {
                errorBuilder.AppendLine("Такой пользователь уже есть в базе данных");
            }
            if (_currentUser == null && (ReliabilityLAdmin.Content == "Уровень надежности пароля: Ненадежный." || ReliabilityLAdmin.Content == "Уровень надежности пароля: Простой."))
            {
                errorBuilder.AppendLine("Уровень защиты пароля должен быть выше.");
            }
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }
            return errorBuilder.ToString();
        }

        private void PBoxRPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordCheck = ReliabilityPass.Check_Password(PBoxRPassword.Password);
            ReliabilityLAdmin.Content = passwordCheck;

            if (PBoxRPassword.Password.Length > 0)
            {
                ImgShowHide.Visibility = Visibility.Visible;
            }
            else
            {
                ImgShowHide.Visibility = Visibility.Hidden;
            }

            if (PBoxRPassword.Password != PBoxDbRPassword.Password)
            {
                BtnRLogin.IsEnabled = false;
                return;
            }
            else BtnRLogin.IsEnabled = true;
        }

        private void PBoxDbRPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PBoxDbRPassword.Password != PBoxRPassword.Password)
            {
                BtnRLogin.IsEnabled = false;
                return;
            }
            else if (PBoxRPassword.Password.Length > 5)
            {
                BtnRLogin.IsEnabled = true;
            }
        }
    }
}
