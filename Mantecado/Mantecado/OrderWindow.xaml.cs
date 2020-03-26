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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
       
        Order o = new Order();
        
        public OrderWindow()
        {
            InitializeComponent();
           
        }
        
        private void OrderMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void newItem(object sender)
        {
            Item NewItem = new Item();

            NewItem.itemName = ((Button)sender).Content.ToString();
            try
            {
                using StreamReader sr = new StreamReader("../../../Prices/Prices.txt");
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                   
                    String[] itemInfo = line.Split('\t');
                    String itemName = itemInfo[0];
                    String itemPrice = itemInfo[1];

                    if(NewItem.itemName.Equals(itemName))
                    {
                        NewItem.itemPrice = Convert.ToDouble(itemPrice);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading employee file\n" + ex.Message);

            }


            NewItem.T = new TextBox();

            NewItem.T.IsReadOnly = true;

            NewItem.T.Background = new SolidColorBrush(Colors.White);

            NewItem.T.BorderThickness = new Thickness(0);

            NewItem.T.Cursor = Cursors.Arrow;

            NewItem.T.Focusable = true;

            NewItem.T.SelectionOpacity = 0;
            
            NewItem.T.MouseLeave += new MouseEventHandler(tb_onMouseLeave);
            NewItem.T.MouseEnter += new MouseEventHandler(tb_onMouseEnter);
            NewItem.T.GotFocus += new RoutedEventHandler(TextBoxOnFocus);
            NewItem.T.LostFocus += new RoutedEventHandler(TextBoxLostFocus);

            NewItem.T.Text = String.Format("{0, -20} {1,5} ", NewItem.itemName, ("\t$" + NewItem.itemPrice));

            o.AddItem(NewItem);

            Subtotal.Content = "Subtotal: $" + o.GetSubtotal();
            Taxes.Content = "Tax: $" + o.GetTax();
            Total.Content = "Total: $" + o.GetTotalPrice();
          

            NewItem.T.Background = new SolidColorBrush(Colors.White);

            NewItem.T.FontSize = 30;
            
            Stacky.Children.Add(NewItem.T);
        }

        public void Milkshake_button(object sender, RoutedEventArgs e)
        {
            newItem(sender);
        }
        public void Tradition_button(object sender, RoutedEventArgs e)
        {
            newItem(sender);
        }

        public void NonDairy_button(object sender, RoutedEventArgs e)
        {
            newItem(sender);
        }

        public void Custard_button(object sender, RoutedEventArgs e)
        {
            newItem(sender);
        }
        

        public void Mod1_Click(object sender, RoutedEventArgs e)
        {
            AddOns newAddon = new AddOns();
           
            newAddon.addonName = ((Button)sender).Content.ToString();

            newAddon.addonPrice = 0.39;
            
           

            foreach (Item i in o.OrderItems)
            {
                
                
                if (i.T.IsFocused == true)
                {
                    i.T.AppendText("\n   +" + newAddon.addonName + "\t$" + newAddon.addonPrice);
                    o.AddAddon(i, newAddon);
                    Subtotal.Content = "Subtotal: $" + o.GetTotalPrice();
                }
                
            }

           
            

           
           

        }

        private void tb_onMouseEnter(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;    //connects to the textbox that called this handler

            T.Background = new SolidColorBrush(Colors.LightBlue);   //changes the color as you mouse over the boxes

        }

        private void TextBoxOnFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;
            

            T.Background = new SolidColorBrush(Colors.LightBlue);           //makes the textbox blue when clicked on

            T.BorderThickness = new Thickness(3);                           //gives it a border to emphasize selection

            Mod1.Visibility = Visibility.Visible;
            Mod1.Content = "Strawberries";
            Mod1.Focusable = false;

            Mod2.Visibility = Visibility.Visible;
            Mod2.Content = "Oreos";
            Mod2.Focusable = false;

            Mod3.Visibility = Visibility.Visible;
            Mod3.Content = "Chocolate Syrup";
            Mod3.Focusable = false;

            Delete.Visibility = Visibility.Visible;
            Delete.Focusable = false;
        

            



        }

        
        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;
            
            T.Background = new SolidColorBrush(Colors.White);

            T.BorderThickness = new Thickness(0);
           
           

        }

        private void tb_onMouseLeave(object sender, RoutedEventArgs e)
        {
            TextBox T = e.OriginalSource as TextBox;

            if (!T.IsFocused)
                T.Background = new SolidColorBrush(Colors.White);
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

        private void SendStay_Click(object sender, RoutedEventArgs e)
        {
            using StreamWriter outputOrder = new StreamWriter("../../../Orders/order.txt");
            outputOrder.WriteLine(o.ToString());
            
        }

        private void SendQuit_Click(object sender, RoutedEventArgs e)
        {
            using StreamWriter outputOrder = new StreamWriter("../../../Orders/order.txt");
            outputOrder.WriteLine(o.ToString());
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
        

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Item i in o.OrderItems)
            {
                if (i.T.IsFocused == true)
                {
                    o.RemoveItem(i);
                    Stacky.Children.Remove(i.T);
                    break;
                    
                }

            }
            //Subtotal.Content = "Subtotal: $" + o.GetSubtotal();
            //Taxes.Content = "Tax: $" + o.GetTax();
            //Total.Content = "Total: $" + o.GetTotalPrice();


        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
