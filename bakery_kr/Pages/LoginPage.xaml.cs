using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        string AppPath = Directory.GetCurrentDirectory(); //Получает текущий рабочий каталог приложения
        public LoginPage()
        {
            InitializeComponent();
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\notvisib.png")); //Задается изображение "глаза" для просмотра пароля
        }
        private void ImgShowHide_PreviewMouseUp(object sender, MouseButtonEventArgs e) //Метод, который скрывает пароль, когда
        {
            HidePassword();
        }
        private void ImgShowHide_PreviewMouseDown(object sender, MouseButtonEventArgs e) //Метод, который показывает пароль, когда пкм нажато
        {
            ShowPassword();
        }
        private void ImgShowHide_MouseLeave(object sender, MouseEventArgs e) //Метод, который скрывает пароль, когда мышка покинула кнопку
        {
            HidePassword();
        }
        void ShowPassword() //Метод, который показывает пароль
        {
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\visib.png"));
            VisibTBoxPassword.Visibility = Visibility.Visible;
            PBoxPassword.Visibility = Visibility.Hidden;
            VisibTBoxPassword.Text = PBoxPassword.Password;
        }
        void HidePassword() //Метод, который скрывает пароль
        {
            ImgShowHide.Source = new BitmapImage(new Uri(AppPath + "\\notvisib.png"));
            VisibTBoxPassword.Visibility = Visibility.Hidden;
            PBoxPassword.Visibility = Visibility.Visible;
            PBoxPassword.Focus();
        }
        private void PBoxPassword_PasswordChanged(object sender, RoutedEventArgs e) //Метод, который делает видимой кнопку для просмотра пароля 
        {
            if (PBoxPassword.Password.Length > 0)
            {
                ImgShowHide.Visibility = Visibility.Visible;
            }
            else
            {
                ImgShowHide.Visibility = Visibility.Hidden;
            }
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e) //При нажатии на кнопку "Войти" происходит вход в систему
        {
            try
            {
                var UserLogin = App.Context.User.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password);
                var UserLogin1 = App.Context.User.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password != PBoxPassword.Password);
                if (UserLogin != null)
                {
                    App.CurrentUser = UserLogin;
                    NavigationService.Navigate(new PastryPage());
                }
                else if (UserLogin1 != null)
                {
                    var dialogResult = MessageBox.Show("Пароль введен неверно, попробуйте еще раз.", "Ошибка авторизации",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var dialogResult = MessageBox.Show("Пользователь с такими данными не найден. Зарегистрироваться?", "Ошибка авторизации",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new RegistrationPage());
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {
                        NavigationService.Navigate(new LoginPage());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void BtnReg_Click(object sender, RoutedEventArgs e) //При нажатии на кнопку "Зарегистрироваться" переходит перемещение на страницу регистрации
        {
            NavigationService.Navigate(new RegistrationPage());
        }

    }
}
