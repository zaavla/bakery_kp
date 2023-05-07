using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace bakery_kr.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Entities.Pastrys _currentPastrys = null; //Присвоение данных из таблицы Pastrys в переменную _currentPastrys
        private byte[] _mainImageData; //Массив байтов для хранения изображения
        public AddEditPage()
        {
            InitializeComponent();
        }
        public AddEditPage(Entities.Pastrys shop) //Сбор данных о выпечке из БД и их вставка в редактируемые поля
        {
            InitializeComponent();

            _currentPastrys = shop;
            Title = "Редактировать товар";
            TBoxTitle.Text = _currentPastrys.Title;
            TBoxCost.Text = $"{_currentPastrys.Cost:N2}";
            TBoxDescription.Text = _currentPastrys.Description;

            if (_currentPastrys.MainImage != null)
            {
                ImageService.Source = (ImageSource)new ImageSourceConverter().
                    ConvertFrom(_currentPastrys.MainImage);
            }
        }
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e) //Метод, срабатывающий при клике на кнопку, выбора изображения при нажатии на кнопку
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }
        private string CheckErrors() //Метод, в котором проверяются ошибки, которые можно допустить на данной странице
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxTitle.Text))
            {
                errorBuilder.AppendLine("Требуется обязательно ввести название выпечки.");
            }
            var serviceFromDB = App.Context.Pastrys.ToList().FirstOrDefault(p => p.Title.ToLower()
            == TBoxTitle.Text.ToLower());
            if (serviceFromDB != null && serviceFromDB != _currentPastrys)
            {
                errorBuilder.AppendLine("Такая выпечка уже существует в базе данных.");
            }
            decimal cost = 0;
            if (decimal.TryParse(TBoxCost.Text, out cost) == false || cost <= 0 || cost.ToString().Length >= 8)
            {
                errorBuilder.AppendLine("Стоимость выпечки должна быть записана положительным числом. Копейки в цене должны быть записаны после знака запятой.");
            }
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }
            return errorBuilder.ToString();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e) //Метод, который при нажатии на кнопку "Сохранить", сохраняет данные о товаре
        {
            try
            {
                var errorMessage = CheckErrors();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (_currentPastrys == null) //Если введенной выпечки нет в БД, то данные о ней добавляются туда
                    {
                        var shop = new Entities.Pastrys
                        {
                            Title = TBoxTitle.Text,
                            Cost = decimal.Parse(TBoxCost.Text),
                            Description = TBoxDescription.Text,
                            MainImage = _mainImageData
                        };
                        App.Context.Pastrys.Add(shop);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        _currentPastrys.Title = TBoxTitle.Text;
                        _currentPastrys.Cost = decimal.Parse(TBoxCost.Text);
                        _currentPastrys.Description = TBoxDescription.Text;
                        App.Context.SaveChanges();
                    }
                    NavigationService.GoBack();
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара в базу данных.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TBoxTitle_PreviewTextInput(object sender, TextCompositionEventArgs e) //Метод, запрещающий писать цифры в названии выпечки
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }
    }
}
