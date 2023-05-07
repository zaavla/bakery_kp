using System.Linq;
using System.Windows;
using System.Windows.Controls;
using bakery_kr.Entities;
using System.Printing;
using System.Windows.Media;
using System;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        bakeryEntities dataEntities = new bakeryEntities(); 
        public Orders()
        {
            InitializeComponent();
        }
        private void Orders_Loaded(object sender, RoutedEventArgs e) //Метод, который при загрузке страницы обновляет все заказы из БД
        {
            var query =
            from Sales in dataEntities.Sales
            select new
            {
                Номер = Sales.Id_Sale, Логин = Sales.Login,
                Дата = Sales.Date_Sale, Адрес = Sales.Adress,
                Название = Sales.Pastrys.Title, 
                Количество = Sales.Quantity, Цена = Sales.Price, 
                Стоимость = Sales.TotalCost 
            }; //Запрос, чтобы выбрать столбцы из таблицы
            DataGrid.ItemsSource = query.ToList();
        }
        public static void Print(Visual elementToPrint, string description) //Печать таблицы со всеми заказами
        {
            using (var printServer = new LocalPrintServer()) 
            {
                var dialog = new PrintDialog();
                var qs = printServer.GetPrintQueues();
                dialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                dialog.PrintVisual(elementToPrint, description);
            }
        }
        private void BtnPrintOrders_Click(object sender, RoutedEventArgs e) //При нажатии кнопки "Печать списка заказов" происходит сохранения файла в pdf формате, для его печати
        {
            BtnPrintOrders.Visibility = Visibility.Hidden;
            Print(this, Convert.ToString(DataGrid));
        }
    }
}
