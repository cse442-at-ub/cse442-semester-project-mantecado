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

        private MySqlServer server = new MySqlServer();
        
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
            
            product new_product = new product();
            new_product.name = ItemNameBox.Text;
            new_product.price = float.Parse(ItemPriceBox.Text);
            new_product.category = ItemCat.Text;
            server.Insert("Products", new employee(), new reciept(), new_product);
            using StreamWriter sw = new StreamWriter("../../../Prices/Prices.txt", append: true);
         
            sw.WriteLine(ItemNameBox.Text + '\t' + ItemPriceBox.Text + '\t' + ItemCat.Text);
            ItemNameBox.Text = "";
            ItemPriceBox.Text = "";
            ItemCat.SelectedIndex = 0;



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemPane.Visibility = Visibility.Collapsed;
           

        }

        private void CancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteItemPane.Visibility = Visibility.Collapsed;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

           
            
            string fileName = "../../../Prices/Prices.txt";
                try
                {
                    using StreamReader sr = new StreamReader(fileName);
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        String[] buttonInfo = line.Split('\t');
                        String buttonName = buttonInfo[0];
                        if (buttonName != "")
                        {

                            Button NewButton = new Button();

                            NewButton.Content = buttonName;

                            NewButton.Click += new RoutedEventHandler(DeleteButton_Click);
                            DeleteStack.Children.Add(NewButton);
                        }

                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error reading items file\n" + ex.Message);

                }
                
            
            DeleteItemPane.Visibility = Visibility.Visible;

            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           
            MessageBoxResult result = MessageBox.Show("Delete " + ((Button)sender).Content + " ?", "Delete Item", MessageBoxButton.YesNoCancel);

            switch(result)
            {
                case MessageBoxResult.Yes:

                   

                    String filePath = "../../../Prices/Prices.txt";
                   
                   
                    String updatedFile = "";
                    product delete_product = new product();
                    
                    try
                    {
                        using StreamReader sr = new StreamReader(filePath);
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            String[] itemInfo = line.Split('\t');
                            if (!((Button)sender).Content.Equals(itemInfo[0]))
                            {
                                
                                updatedFile += line + '\n';
                            }
                            else
                            {
                                delete_product.name = itemInfo[0];
                                delete_product.price = float.Parse(itemInfo[1]);
                                delete_product.category = itemInfo[2];
                            }
                        }
                        sr.Close();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Error reading items file\n" + ex.Message);

                    }

                   
                    server.Delete("Products", new employee(), new reciept(), delete_product);
                    updatedFile = updatedFile.TrimEnd('\n', '\r');

                    File.WriteAllText(filePath, String.Empty);
                    

                  
                    try
                    {
                        using StreamWriter sw = new StreamWriter(filePath);
                        sw.WriteLine(updatedFile);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Error reading items file\n" + ex.Message);

                    }
                    DeleteStack.Children.Remove((Button)sender);
                   // DeleteItemPane.Visibility = Visibility.Collapsed;
                    break;

                case MessageBoxResult.No:
                    break;

                case MessageBoxResult.Cancel:
                    DeleteItemPane.Visibility = Visibility.Collapsed;
                    break;

            }

          

        }

       
    }
}
