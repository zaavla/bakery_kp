using bakery_kr.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Collections.Generic;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Page
    {
       
        public ShoppingCart()
        {
            InitializeComponent();
            ComboTypePastry.ItemsSource = App.Context.Pastrys.Select(c => c.Title).ToList();
            TBoxSLogin.Text = App.CurrentUser.Login;
            DatePicker.DisplayDateStart = DateTime.Now;
            DatePicker.DisplayDateEnd = DateTime.Now.AddMonths(2);
        }
        
        private string CheckErrors()
        {
            int result = 1000;
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxAdress.Text))
            {
                errorBuilder.AppendLine("Требуется ввести адрес доставки.");
            }
            if (DatePicker.SelectedDate == null)
            {
                errorBuilder.AppendLine("Требуется ввести дату доставки.");
            }
            if (ComboTypePastry == null)
            {
                errorBuilder.AppendLine("Требуется выбрать выпечку.");
            }
            if (string.IsNullOrWhiteSpace(TBoxQuantity.Text) || !Int32.TryParse(TBoxQuantity.Text, out result) 
                || System.Text.RegularExpressions.Regex.IsMatch(TBoxQuantity.Text, "[^0-9]") || result <= 0)
            {
                errorBuilder.AppendLine("Количество выпечки должно быть записано целым, положительным числом.");
            }
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }
            return errorBuilder.ToString();
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    var idPastry = App.Context.Pastrys.Where(c => c.Title == ComboTypePastry.Text).Select(c => c.ID).FirstOrDefault();

                    var service = new Sales
                    {
                        Login = TBoxSLogin.Text,
                        Date_Sale = DatePicker.SelectedDate.Value.Date,
                        Adress = TBoxAdress.Text,
                        Id_Pastry = idPastry,
                        Quantity = Convert.ToInt32(TBoxQuantity.Text),
                        Price = Convert.ToDecimal(TBoxPrice.Text),
                        TotalCost = Convert.ToDecimal(TBoxTotalCost.Text),
                    };
                    App.Context.Sales.Add(service);
                    App.Context.SaveChanges();

                    var dialogResult = MessageBox.Show("Заказ отправлен в обработку.", "Заказ успешно отправлен",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    if(dialogResult == MessageBoxResult.OK)
                    {
                        TBoxQuantity.Text = null; TBoxTotalCost.Text = null; TBoxPrice.Text = null;
                        DatePicker.SelectedDate = null;
                    }
                }
                catch(Exception ex)
                {
                MessageBox.Show("Ошибка создания заказа.", ex.Message);
            }
        }
        }
        
        private void UpdatePastry() //Подставление цены, в зависимости от выбранной выпечки
        {
            try
            {
                var CBT = App.Context.Pastrys.Where(c => ComboTypePastry.SelectedItem == c.Title).Select(c => c.Cost).FirstOrDefault();
                TBoxPrice.Text = $"{CBT:N2}";
            }
            catch(System.NullReferenceException ex)
            {
                MessageBox.Show("Требуется выбрать вид выпечки.", ex.Message);
            }
            
        }
        private void ComboTypePastry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TBoxQuantity.Text = null;
            TBoxTotalCost.Text = null;
            UpdatePastry();
            
        }
        private void UpdateTotalCost() //Расчет цены, в зависимости от количества
        {
            try
            {
                int result = 1000;
                if (string.IsNullOrWhiteSpace(TBoxQuantity.Text) || !Int32.TryParse(TBoxQuantity.Text, out result)
                    || string.IsNullOrWhiteSpace(TBoxPrice.Text))
                {
                    return;
                }
                else
                {
                    decimal TotalCostOrder = Convert.ToDecimal(TBoxPrice.Text) * Convert.ToDecimal(TBoxQuantity.Text);
                    TBoxTotalCost.Text = TotalCostOrder.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка вычисления итоговой стоимости.", ex.Message);
            }
        }
        private void TBoxQuantity_TextChanged(object sender, TextChangedEventArgs e) //Обновление цены при вводе количества товара
        {
            if (TBoxQuantity.Text != null || Convert.ToInt32(TBoxQuantity.Text) > 0)
            {
                UpdatePastry();
                UpdateTotalCost();
            }
        }

        private void TBoxPrice_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdatePastry();
            UpdateTotalCost();
        }
    }
}
