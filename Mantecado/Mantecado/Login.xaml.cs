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

namespace Mantecado
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            this.Close();
            orderWindow.Show();
            
        }

        private void Number1_Click(object sender, RoutedEventArgs e)
        {
           
            toTextBox(sender);
        }

        private void Number2_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number3_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);

        }

        private void Number4_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number5_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number6_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number7_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number8_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number9_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void Number0_Click(object sender, RoutedEventArgs e)
        {
            toTextBox(sender);
        }

        private void toTextBox(object sender)
        {
            int maxInput = 4;
            if (InputBox.Text.Length < maxInput)
                InputBox.Text += ((Button)sender).Content.ToString();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text.Length > 0)
                InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 1);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
