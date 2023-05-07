using bakery_kr.Pages;
using System;
using System.Windows;

namespace bakery_kr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new LoginPage());
            //FrameMain.Navigate(new ShoppingCart());

        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack)
            {
                FrameMain.GoBack();
            }
        }
    }
}
