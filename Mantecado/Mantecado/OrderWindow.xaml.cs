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
        //Item cur = new Item();
        Item cur = new Item();
        static int num = 0;
        static int numAddons = 0;
        // bool orderPresent = false;

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

            //orderPresent = true;

            Item newShake = new Item();
            
            newShake.itemName = ((Button)sender).Content.ToString();
            num++;

            newShake.itemPrice = 4.99;

            newShake.T = new TextBox();

            newShake.T.IsReadOnly = true;

            newShake.T.Background = new SolidColorBrush(Colors.White);

            newShake.T.BorderThickness = new Thickness(0);

            newShake.T.Cursor = Cursors.Arrow;

            newShake.T.Focusable = true;

            newShake.T.SelectionOpacity = 0;

            newShake.T.MouseLeave += new MouseEventHandler(tb_onMouseLeave);
            newShake.T.MouseEnter += new MouseEventHandler(tb_onMouseEnter);
            newShake.T.GotFocus += new RoutedEventHandler(TextBoxOnFocus);
            newShake.T.LostFocus += new RoutedEventHandler(TextBoxLostFocus);

            // T.Text = "+Vanilla MilkShake       $#.##";
            newShake.T.Text = String.Format("{0, -20} {1,5} ", newShake.itemName, ("\t$" + newShake.itemPrice));
           
            //T.Text.PadRight(5);
            o.AddItem(newShake);
            
            Subtotal.Content = "Subtotal: $" + o.GetTotalPrice();

            newShake.T.Background = new SolidColorBrush(Colors.White);

            //T.Height = 100;

            newShake.T.FontSize = 30;
            
            Stacky.Children.Add(newShake.T);
            

        }

        public void Mod1_Click(object sender, RoutedEventArgs e)
        {
            AddOns newAddon = new AddOns();
           
            newAddon.addonName = ((Button)sender).Content.ToString();

            newAddon.addonPrice = 0.39;
            //MessageBox.Show(cur.itemName);
            //Item it = sender as Item;
            //MessageBox.Show(cur.itemName);
            foreach(Item i in o.OrderItems)
            {
                /*
                if(cur.itemName.Equals(i.itemName))
                {
                    //cur = i;
                }
                */
            }

            foreach (Item i in o.OrderItems)
            {
                //o.OrderItems.
                
                if (i.T.IsFocused == true)
                {
                    i.T.AppendText("\n   +" + newAddon.addonName + "\t$" + newAddon.addonPrice);
                    o.AddAddon(i, newAddon);
                    Subtotal.Content = "Subtotal: $" + o.GetTotalPrice();
                }
                
            }

            cur.ItemAddons.Add(newAddon);
            //TextBox currentText = new TextBox();
            

            //currentText.Text = String.Format("{0, -20} {1,5} ", cur.itemName, ("\t$" + cur.itemPrice));
            //int index = Stacky.Children.IndexOf(currentText);
            //MessageBox.Show("i " + index);
            ////int index = Stacky.Children.
            //TextBox T = new TextBox();
            //T.IsReadOnly = true;
            //T.Background = new SolidColorBrush(Colors.White); 
            //T.BorderThickness = new Thickness(0);
            //T.Cursor = Cursors.Arrow;
            //T.Focusable = true;
            //T.SelectionOpacity = 0;
            //T.Text = String.Format("{0, -20} {1,5} ", newAddon.addonName, ("\t$" + newAddon.addonPrice));
            //Stacky.Children.Insert(index + 1, T);
            //Stacky.Children.
            //numAddons++;
            //MessageBox.Show(o.OrderItems.IndexOf(cur).ToString());

           
           

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

            Mod3.Visibility = Visibility.Visible;
            Mod3.Content = "Chocolate Syrup";

            



        }

        
        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;
            //e.Handled = true;
            //MessageBox.Show("Lost focus");
            T.Background = new SolidColorBrush(Colors.White);

            T.BorderThickness = new Thickness(0);
            //MessageBox.Show(T.Text);
            /*foreach(Item i in o.OrderItems)
            {
                if()
            }*/
            //Mod1.Visibility = Visibility.Hidden;
            //Mod2.Visibility = Visibility.Hidden;
            //Mod3.Visibility = Visibility.Hidden;
            
            cur.itemName = T.Text.Split(' ')[0];
            //MessageBox.Show(cur.itemName);
           // MessageBox.Show(cur.itemName);
            

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

        private void OMB_14_Click(object sender, RoutedEventArgs e)
        {



        }

        private void OMB_16_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
