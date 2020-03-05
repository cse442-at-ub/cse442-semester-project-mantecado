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

namespace Mantecado
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }
        
        private void OrderMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        public void Milkshake_button(object sender, RoutedEventArgs e)
        {

            TextBox T = new TextBox();

            T.Text = "+Van. MilkShake       $#.##";

            T.Background = new SolidColorBrush(Colors.White);

            T.Height = 100;

            T.FontSize = 30;

            Stacky.Children.Add(T);

        }

        public void TraditionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Main_Traditional.IsVisible)
                Main_Traditional.Visibility = Visibility.Visible;

            if (Main_Milkshakes.IsVisible)
                Main_Milkshakes.Visibility = Visibility.Hidden;

            if (Main_Custard.IsVisible)
                Main_Custard.Visibility = Visibility.Hidden;

            if (Main_Non_Dairy.IsVisible)
                Main_Non_Dairy.Visibility = Visibility.Hidden;

        }

        private void CustardButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Main_Custard.IsVisible)
            {
                Main_Custard.Visibility = Visibility.Visible;
            }

            if (!Main_Custard.IsVisible)
                Main_Custard.Visibility = Visibility.Visible;

            if (Main_Milkshakes.IsVisible)
                Main_Milkshakes.Visibility = Visibility.Hidden;

            if (Main_Non_Dairy.IsVisible)
                Main_Non_Dairy.Visibility = Visibility.Hidden;

            if (Main_Traditional.IsVisible)
                Main_Traditional.Visibility = Visibility.Hidden;
               
        }

        private void MilksshakesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Main_Milkshakes.IsVisible)
                Main_Milkshakes.Visibility = Visibility.Visible;

            if (Main_Non_Dairy.IsVisible)
                Main_Non_Dairy.Visibility = Visibility.Hidden;

            if (Main_Custard.IsVisible)
                Main_Custard.Visibility = Visibility.Hidden;

            if (Main_Traditional.IsVisible)
                Main_Traditional.Visibility = Visibility.Hidden;

        }

        private void NonDairyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Main_Non_Dairy.IsVisible)
                Main_Non_Dairy.Visibility = Visibility.Visible;

            if (Main_Milkshakes.IsVisible)
                Main_Milkshakes.Visibility = Visibility.Hidden;

            if (Main_Custard.IsVisible)
                Main_Custard.Visibility = Visibility.Hidden;

            if (Main_Traditional.IsVisible)
                Main_Traditional.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
