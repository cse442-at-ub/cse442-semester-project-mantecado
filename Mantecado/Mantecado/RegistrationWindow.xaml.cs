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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        
        private readonly MySqlServer server = new MySqlServer();

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {

            if (server.Connect())
            {
                OrderWindow orderWindow = new OrderWindow();
                this.Close();
                orderWindow.Show();
            }
            else
            {
                MessageBox.Show("Error connecting to the server please try again.");
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] installs = new string[] { "Male", "Female", "Other" };
            gender_selection.Items.Add(installs);
        }
    }

}
