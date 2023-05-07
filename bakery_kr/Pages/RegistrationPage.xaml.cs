using bakery_kr.Entities;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ClassLibrary;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        string AppPath = Directory.GetCurrentDirectory();
        private Entities.User _currentUser = null; //Получение данных пользователей из бд в переменную 
        public RegistrationPage()
        {
            InitializeComponent();
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\notvisib.png")); //Доступ к картинке для просмотра пароля
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
                    var service = new User
                    {
                        Login = TBoxRLogin.Text,
                        Password = PBoxRPassword.Password.ToString(),
                        Roled = 2,
                    };
                    App.Context.User.Add(service);
                    App.Context.SaveChanges();
                    MessageBox.Show("Пользователь добавлен.");
                    TBoxRLogin.Text = null; PBoxDbRPassword.Password = null;
                    PBoxRPassword.Password = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void PBoxRPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordCheck = ReliabilityPass.Check_Password(PBoxRPassword.Password);
            ReliabilityL.Content = passwordCheck; //Присваивание надписи надежности пароля

            if (PBoxRPassword.Password.Length > 0)
            {
                ImgShowHide.Visibility = Visibility.Visible; //Если пароль длиннее 1 символа - разрешить возможность смотреть пароль
            }
            else
            {
                ImgShowHide.Visibility = Visibility.Hidden;
            }

            if (PBoxRPassword.Password != PBoxDbRPassword.Password) //Если пароли одинаковы, то кнопка "Зарегистрироваться" активна
            { 
                BtnRLogin.IsEnabled = false;
                return;
            }
            else BtnRLogin.IsEnabled = true;
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
            var userFromDB = App.Context.User.ToList().FirstOrDefault(p => p.Login == TBoxRLogin.Text);
            if (_currentUser != userFromDB && userFromDB != null)
            {
                errorBuilder.AppendLine("Такой пользователь уже есть в базе данных");
            }

            if (_currentUser == null && (ReliabilityL.Content == "Уровень надежности пароля: Ненадежный." || ReliabilityL.Content == "Уровень надежности пароля: Простой."))
            {
                errorBuilder.AppendLine("Уровень защиты пароля должен быть выше.");
            }
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }
            return errorBuilder.ToString();
        }

        private void PBoxDbRPassword_PasswordChanged(object sender, RoutedEventArgs e) 
        {
            if (PBoxDbRPassword.Password != PBoxRPassword.Password) //Если пароли одинаковы, то кнопка "Зарегистрироваться" активна
            {
                BtnRLogin.IsEnabled = false;
                return;
            }
            else if (PBoxRPassword.Password.Length>5) //Пароль должен быть длиннее 5 символов, иначе кнопка не работает
            {
                BtnRLogin.IsEnabled = true;
            }
        }

    }
}
