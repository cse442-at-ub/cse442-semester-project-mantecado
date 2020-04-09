using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Mantecado
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItemPane.Visibility = Visibility.Visible;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemPane.Visibility = Visibility.Collapsed;
            using StreamWriter sr = new StreamWriter("../../../Prices/Prices.txt", append: true);
         
            sr.WriteLine(ItemNameBox.Text + '\t' + ItemPriceBox.Text + '\t' + ItemCat.Text);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemPane.Visibility = Visibility.Collapsed;

        }
    }
}
