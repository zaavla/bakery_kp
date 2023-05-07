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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для PastryPage.xaml
    /// </summary>
    public partial class PastryPage : Page
    {
        public PastryPage()
        {
            InitializeComponent();

            if (App.CurrentUser.Roled == 1) //Видимость кнопок при различных ролях пользователей (1 - admin, 2 - user, 3 - bakery)
            {
                BtnAddUpdatePastry.Visibility = Visibility.Visible;
                BtnAddUpdateUser.Visibility = Visibility.Visible;
                BtnAddOrder.Visibility = Visibility.Collapsed;
                BtnLookOrders.Visibility = Visibility.Visible;
            }
            else if(App.CurrentUser.Roled == 3)
            {
                BtnLookOrders.Visibility = Visibility.Visible;
                BtnAddOrder.Visibility = Visibility.Collapsed;
                BtnAddUpdateUser.Visibility = Visibility.Collapsed;
                BtnAddUpdatePastry.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAddUpdatePastry.Visibility = Visibility.Collapsed;
                BtnAddUpdateUser.Visibility = Visibility.Collapsed;
                BtnLookOrders.Visibility = Visibility.Collapsed;
                BtnAddOrder.Visibility = Visibility.Visible;
            }

            ComboSortBy.SelectedIndex = 0;
            UpdatePastrys();

            LViewPastrys.ItemsSource = App.Context.Pastrys.ToList();
        }
        private void UpdatePastrys() //Метод, обновляющий выпечку, находящуюся в БД
        {
            var shop = App.Context.Pastrys.ToList();

            if (ComboSortBy.SelectedIndex == 1) //Сортировка цены по возрастанию (index - 1)/убыванию (index - 2)
                shop = shop.OrderBy(p => p.Cost).ToList();
            if (ComboSortBy.SelectedIndex == 2)
                shop = shop.OrderByDescending(p => p.Cost).ToList();

            shop = shop.Where(p => p.Title.ToLower().
            Contains(TBoxSearch.Text.ToLower())).ToList(); //Поиск по названию выпечки

            LViewPastrys.ItemsSource = shop;
        }
        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e) //Сортировка выпечки по цене
        {
            UpdatePastrys();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) //Поиск выпечки по буквам
        {
            UpdatePastrys();
        }
        private void BtnAddUpdatePastry_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Добавить товар" перемещение на страницу добавления товара
        {
            NavigationService.Navigate(new AddEditPage());
        }
        private void BtnAddOrder_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Добавить заказ" перемещение на страницу оформления заказа
        {
            NavigationService.Navigate(new ShoppingCart());
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Удалить" происходит удаление товара из БД
        {
            var currentShop = (sender as Button).DataContext as Entities.Pastrys;

            if (MessageBox.Show($"Вы точно хотите удалить такой товар, как: " + $"{currentShop.Title}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Pastrys.Remove(currentShop);
                App.Context.SaveChanges();
                UpdatePastrys();
                NavigationService.Navigate(new PastryPage());
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Редактировать товар" перемещение на страницу редактирования
        {
            var currentPastrys = (sender as Button).DataContext as Entities.Pastrys;
            NavigationService.Navigate(new AddEditPage(currentPastrys));
        }

        private void BtnAddUpdateUser_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Добавить пользователя" перемещение на страницу регистрации для администраторов
        {
            NavigationService.Navigate(new RegForAdmin());
        }
        private void BtnLookOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Orders());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePastrys();
        }
    }
}
