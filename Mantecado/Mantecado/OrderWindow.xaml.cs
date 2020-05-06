﻿using System;
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
using System.Windows.Media.TextFormatting;
using System.Security.Cryptography;
using MySqlX.XDevAPI.Relational;
using Renci.SshNet.Messages;

namespace Mantecado
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        Order o = new Order();
        private readonly MySqlServer server = new MySqlServer();

        public OrderWindow()
        {
            InitializeComponent();
            AddCatButtons();
            updatePrice();

        }

        private void OrderMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void AddCatButtons()
        {
            int colNum = 0;
            int numCats = 0;
            try
            {
                using StreamReader sr = new StreamReader("../../../Categories/Categories.txt");
                while(!sr.EndOfStream)
                {
                    numCats++;
                    String catInfo = sr.ReadLine();
                    
                    Button NewButton = new Button();
                    NewButton.Style = (Style)FindResource("RoundButtonTemplate");
                    NewButton.Name = catInfo;
                    NewButton.Content = catInfo;
                    NewButton.Click += new RoutedEventHandler(CategoryHandler);
                    //NewButton.Background = new SolidColorBrush(Colors.LightGreen);
                    NewButton.Margin = new Thickness(10);
                    switch(numCats)
                    {
                        
                        case 0:
                            NewButton.Background = new SolidColorBrush(Colors.LightGreen);
                            break;
                        case 1:
                            NewButton.Background = new SolidColorBrush(Colors.LightPink);
                            break;
                        case 2:
                            NewButton.Background = new SolidColorBrush(Colors.MediumPurple);
                            break;
                        case 3:
                            NewButton.Background = new SolidColorBrush(Colors.PeachPuff);
                            break;
                        case 4:
                            NewButton.Background = new SolidColorBrush(Colors.LightBlue);
                            break;
                        case 5:
                            NewButton.Background = new SolidColorBrush(Colors.Salmon);
                            break;
                        case 6:
                            NewButton.Background = new SolidColorBrush(Colors.MediumSeaGreen);
                            break;
                        case 7:
                            NewButton.Background = new SolidColorBrush(Colors.DeepPink);
                            break;
                        case 8:
                            NewButton.Background = new SolidColorBrush(Colors.Aquamarine);
                            break;
                        default:
                            NewButton.Background = new SolidColorBrush(Colors.RosyBrown);
                            break;

                    }
                    
                    if(numCats > 4)
                    {
                        ColumnDefinition colDef = new ColumnDefinition();
                        MainCategoriesWindow.ColumnDefinitions.Add(colDef);
                    }
                        
                    NewButton.FontSize = 30;
                    Grid.SetRow(NewButton, 0);
                    Grid.SetColumn(NewButton, colNum);

                    Grid catPage = new Grid();
                    
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    ColumnDefinition colDef2 = new ColumnDefinition();
                    ColumnDefinition colDef3 = new ColumnDefinition();
                    ColumnDefinition colDef4 = new ColumnDefinition();

                    RowDefinition rowDef1 = new RowDefinition();
                    RowDefinition rowDef2 = new RowDefinition();
                    RowDefinition rowDef3 = new RowDefinition();
                    RowDefinition rowDef4 = new RowDefinition();

                    catPage.ColumnDefinitions.Add(colDef1);
                    catPage.ColumnDefinitions.Add(colDef2);
                    catPage.ColumnDefinitions.Add(colDef3);
                    catPage.ColumnDefinitions.Add(colDef4);


                    catPage.RowDefinitions.Add(rowDef1);
                    catPage.RowDefinitions.Add(rowDef2);
                    catPage.RowDefinitions.Add(rowDef3);
                    catPage.RowDefinitions.Add(rowDef4);

                    catPage.Name = NewButton.Name;
                    catPage.Visibility = Visibility.Collapsed;

                    buttonSheet.Children.Add(catPage);
                    
                    MainCategoriesWindow.Children.Add(NewButton);
                    colNum++;

                }
            }
            catch(IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
        }

        private void AddButtons(object sender)
        {
            int rowNum = 0;
            int colNum = 0;
            try
            {
                using StreamReader sr = new StreamReader("../../../Prices/Prices.txt");
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] buttonInfo = line.Split('\t');
                    String buttonName = buttonInfo[0];
                    String buttonCat = buttonInfo[2];

                    if (((Button)sender).Name.Equals(buttonCat))
                    {
                        
                        if (buttonCat.Equals(((Button)sender).Name))
                        {
                            Button NewButton = new Button();
                            NewButton.Content = buttonName;
                            NewButton.Click += new RoutedEventHandler(ButtonHandler);
                            NewButton.Background = ((Button)sender).Background;
                            NewButton.FontSize = 30;

                            Grid.SetRow(NewButton, rowNum);
                            Grid.SetColumn(NewButton, colNum);
                            
                            foreach (Grid g in buttonSheet.Children)
                            {
                                if (((Button)sender).Name == g.Name)
                                {
                                    
                                    g.Children.Add(NewButton);
                                }
                            }
                           
                            
                            rowNum++;
                            if (rowNum == 4)
                            {
                                colNum++;
                                rowNum = 0;
                            }
                        }
                        

                    }

                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
        }
        private void AddOnButtons(object sender)
        {
            int rowNum = 0;
            int colNum = 0;
            try
            {
                using StreamReader sr = new StreamReader("../../../Prices/AddonPrices.txt");
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] buttonInfo = line.Split('\t');
                    String buttonName = buttonInfo[0];
                    String buttonCat = buttonInfo[2];

                    

                    if (((Button)sender).Content.ToString().Equals(buttonCat))
                    {

                        Button NewButton = new Button();
                        NewButton.Content = buttonName;
                        NewButton.Click += new RoutedEventHandler(Mod1_Click);
                        NewButton.Background = new SolidColorBrush(Colors.LightGreen);
                        NewButton.FontSize = 30;



                        Grid.SetRow(NewButton, rowNum);
                        Grid.SetColumn(NewButton, colNum);
                        ItemContextButtons.Children.Add(NewButton);
                        colNum++;
                        if (colNum == 3)
                        {
                            rowNum++;
                            colNum = 0;
                        }

                        if (buttonCat.Equals("Traditional"))
                        {
                            foreach (Button B in ItemContextButtons.Children)
                            {
                                B.Background = new SolidColorBrush(Colors.LightGreen);
                            }
                        }
                        if (buttonCat.Equals("Custard"))
                        {
                            foreach (Button B in ItemContextButtons.Children)
                            {
                                B.Background = new SolidColorBrush(Colors.LightPink);
                            }
                        }
                        if (buttonCat.Equals("Milkshakes"))
                        {
                            foreach (Button B in ItemContextButtons.Children)
                            {
                                B.Background = new SolidColorBrush(Colors.MediumPurple);
                            }
                        }
                        if (buttonCat.Equals("Vegan"))
                        {
                            foreach (Button B in ItemContextButtons.Children)
                            {
                                B.Background = new SolidColorBrush(Colors.PeachPuff);
                            }
                        }

                        {
                            
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
        }

        private void updatePrice()
        {
            File.Delete("../../../Prices/Prices.txt");
            List<string>[] temp = server.Select("Products");
            using StreamWriter sr = new StreamWriter("../../../Prices/Prices.txt");
            int size = Int32.Parse(temp[4][0]);

            for (int i = 0; i < size; i++)
            {
                sr.WriteLine(temp[0][i] + '\t' + temp[1][i] + '\t' + temp[2][i]);
            }
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
                    String itemCategory = itemInfo[2];


                    if (NewItem.itemName.Equals(itemName))
                    {
                        NewItem.itemPrice = Convert.ToDouble(itemPrice);
                    }

                    NewItem.itemCategory = itemCategory;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading employee file\n" + ex.Message);

            }

            TextBox T = createFirstBox();

            T.Text = String.Format("{0, -10} {1,5} ", NewItem.itemName, ("\t$" + (NewItem.itemPrice).ToString("0.00")));

            o.AddItem(NewItem);

            Subtotal.Content = "Subtotal: $" + o.GetTotalPrice();

            NewItem.B = new Border();

            StackPanel S = new StackPanel();



            S.Children.Add(T);

            S.Focusable = true;

            S.MouseLeave += new MouseEventHandler(sp_onMouseLeave);
            S.MouseEnter += new MouseEventHandler(sp_onMouseEnter);
            S.LostFocus += new RoutedEventHandler(StackPanelLostFocus);

            NewItem.B.Child = S;

            Subtotal.Content = "Subtotal: $" + o.GetSubtotal();
            Taxes.Content = "Tax: $" + o.GetTax();
            Total.Content = "Total: $" + o.GetTotalPrice();


            Stacky.Children.Add(NewItem.B);

        }

        TextBox createBox(string content)
        {
            TextBox T = new TextBox();
            T.IsReadOnly = true;
            T.Background = new SolidColorBrush(Colors.White);
            T.BorderThickness = new Thickness(0);
            T.Cursor = Cursors.Arrow;
            T.Focusable = true;
            T.SelectionOpacity = 0;
            T.MouseLeave += new MouseEventHandler(tb_onMouseLeave);
            T.MouseEnter += new MouseEventHandler(tb_onMouseEnter);
            T.GotFocus += new RoutedEventHandler(TextBoxOnFocus);
            T.LostFocus += new RoutedEventHandler(TextBoxLostFocus);
            T.Text = content;
            T.FontSize = 30;

            return T;
        }

        TextBox createFirstBox()
        {
            TextBox T = new TextBox();
            T.IsReadOnly = true;
            T.Background = new SolidColorBrush(Colors.White);
            T.BorderThickness = new Thickness(0);
            T.Cursor = Cursors.Arrow;
            T.Focusable = true;
            T.SelectionOpacity = 0;
            T.FontSize = 30;
            T.GotFocus += new RoutedEventHandler(StackPanelOnFocus);
            return T;
        }

        public void ButtonHandler(object sender, RoutedEventArgs e)
        {
            newItem(sender);
        }
        
        
        public void Mod1_Click(object sender, RoutedEventArgs e)
        {

            AddOns newAddon = new AddOns();
            newAddon.addonName = ((Button)sender).Content.ToString();

            newAddon.addonPrice = 0.39;


            StackPanel S;

            foreach (Item i in o.OrderItems)
            {
                if (i.B.Child.IsFocused)
                {
                    S = i.B.Child as StackPanel;
                    TextBox T = createBox(String.Format("{0, -10} {1,5} ", "  +" + newAddon.addonName, ("\t$" + newAddon.addonPrice)));
                    o.AddAddon(i, newAddon);
                    S.Children.Add(T);
                }

            }
            Subtotal.Content = "Subtotal: $" + o.GetSubtotal();
            Taxes.Content = "Tax: $" + o.GetTax();
            Total.Content = "Total: $" + o.GetTotalPrice();
        }
        private void tb_onMouseEnter(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;    //connects to the textbox that called this handler

            T.Background = new SolidColorBrush(Colors.LightBlue);   //changes the color as you mouse over the boxes

        }

        private void sp_onMouseEnter(object sender, RoutedEventArgs e)
        {

            StackPanel S = e.OriginalSource as StackPanel;

            foreach (Item i in o.OrderItems)
            {
                if (i.B.Child.IsMouseOver)
                {
                    if (!S.IsFocused)
                        i.B.BorderThickness = new Thickness(1);
                }
            }
        }

        private void TextBoxOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox T = e.OriginalSource as TextBox;
            T.Background = new SolidColorBrush(Colors.LightBlue);           //makes the textbox blue when clicked on
            T.BorderThickness = new Thickness(2);                           //gives it a border to emphasize selection
        }

        private void StackPanelOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox T = e.OriginalSource as TextBox;

            foreach (Item i in o.OrderItems)
            {
                StackPanel S = i.B.Child as StackPanel;

                foreach (TextBox t in S.Children)
                {
                    if (t == T)
                    {
                        S.Focus();
                        i.B.BorderThickness = new Thickness(4);
                    }
                }
            }

            foreach(Button Mod in ItemContextButtons.Children)
            {
                Mod.Visibility = Visibility.Visible;
                Mod.Focusable = false;

            }

            Delete.Visibility = Visibility.Visible;
            Delete.Focusable = false;
        }



        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;

            T.Background = new SolidColorBrush(Colors.White);

            T.BorderThickness = new Thickness(0);

        }

        private void StackPanelLostFocus(object sender, RoutedEventArgs e)
        {
            StackPanel S = e.OriginalSource as StackPanel;

            foreach (Item i in o.OrderItems)
            {
                if (i.B.Child == S)
                {
                    if (i.B.Child.IsMouseOver)
                    {
                        i.B.BorderThickness = new Thickness(1);
                    }
                    else
                    {
                        i.B.BorderThickness = new Thickness(0);
                    }

                }
            }

            foreach(Button Mod in ItemContextButtons.Children)
            {
                Mod.Visibility = Visibility.Hidden;
            }
        }

        private void tb_onMouseLeave(object sender, RoutedEventArgs e)
        {
            TextBox T = e.OriginalSource as TextBox;

            if (!T.IsFocused)
                T.Background = new SolidColorBrush(Colors.White);
        }

        private void sp_onMouseLeave(object sender, RoutedEventArgs e)
        {

            StackPanel S = e.OriginalSource as StackPanel;

            foreach (Item i in o.OrderItems)
            {
                if (!i.B.BorderThickness.Equals(0))
                {
                    if (!i.B.Child.IsFocused)
                        i.B.BorderThickness = new Thickness(0);
                }
            }

        }

        public void CategoryHandler(object sender, RoutedEventArgs e)
        {
            foreach(Grid g in buttonSheet.Children)
            {
                if (((Button)sender).Name == g.Name)
                {
                   
                    g.Visibility = Visibility.Visible;
                }
                else
                    g.Visibility = Visibility.Collapsed;

            }

            AddButtons(sender);
            AddOnButtons(sender);
        }
        
        
        private reciept GetData()
        {
            reciept data = new reciept();
            data.order = o.ToString();
            char[] delim = { '\n', '\t' };
            string[] words = data.order.Split(delim);
            string temp = "";
            foreach (var word in words)
            {
                temp += word + ' ';
            }
            data.order = temp;
            data.item_amount = o.GetItemAmount();
            data.price = Double.Parse(o.GetSubtotal());
            data.tax_amount = Double.Parse(o.GetTax());
            data.total_price = Double.Parse(o.GetTotalPrice());
            return data;
        }

        public void UpdateInventory()
        {

            List<Item> UniqueItems = new List<Item>();
            
            foreach (Item oi in o.OrderItems)
            {
                bool found = false;

                if (UniqueItems.Count == 0)
                {
                    UniqueItems.Add(oi);
                }
                foreach(Item ui in UniqueItems)
                {
                    if (ui.itemName == oi.itemName)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    UniqueItems.Add(oi);
                }

            }

            foreach (Item ui in UniqueItems)
            {
                List<string>[] temp = server.Select("Products");

                int size = Int32.Parse(temp[4][0]);
                int stock = 0;
                int itemCount = 0;
                for (int i = 0; i < size; i++)
                {
                    if(temp[0][i] == ui.itemName)
                    {
                        stock = Int32.Parse(temp[3][i]);
                    }
                }

                foreach(Item oi in o.OrderItems)
                {
                    if (ui.itemName == oi.itemName)
                    {
                        itemCount++;
                    }
                }

                if (itemCount > stock)
                {
                    MessageBox.Show("Not enough " + ui.itemName + " in stock.");
                    
                }
                else
                {

                    string query = "UPDATE Products SET Stock= " + (stock - itemCount) + " WHERE Name= '" + ui.itemName + "'";
                    server.update(query);

                }
           

            }

        }


        private void SendStay_Click(object sender, RoutedEventArgs e)
        {


            UpdateInventory();
            reciept data = GetData();
            server.Insert("Reciepts", new employee(), data);
            using StreamWriter outputOrder = new StreamWriter("../../../Orders/order.txt");
            outputOrder.WriteLine(o.ToString());

        }

        private void SendQuit_Click(object sender, RoutedEventArgs e)
        {
            reciept data = GetData();
            server.Insert("Reciepts", new employee(), data);
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
                if (i.B.Child.IsFocused)
                {
                    o.RemoveItem(i);
                    Stacky.Children.Remove(i.B);
                    break;
                }
            }

            TextBox T = e.OriginalSource as TextBox;

            foreach (Item i in o.OrderItems)
            {
                StackPanel S = i.B.Child as StackPanel;

                foreach (TextBox t in S.Children)
                {
                    if (t.IsFocused)
                    {
                        string[] addonName = t.Text.Split('+', '\t');
                        addonName[1] = addonName[1].Trim();
                        o.RemoveAddon(i, addonName[1]);
                        S.Children.Remove(t);
                        break;
                    }
                }
            }

            Subtotal.Content = "Subtotal: $" + o.GetSubtotal();
            Taxes.Content = "Tax: $" + o.GetTax();
            Total.Content = "Total: $" + o.GetTotalPrice();

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void CatButton_MouseLeave(object sender, MouseEventArgs e)
        {

            Button bu = sender as Button;

            if ((string)bu.Content == "Traditional")
                bu.Background = new SolidColorBrush(Colors.LightGreen);

            if ((string)bu.Content == "Custard")
                bu.Background = new SolidColorBrush(Colors.LightPink);

            if ((string)bu.Content == "Milkshakes")
                bu.Background = new SolidColorBrush(Colors.MediumPurple);

            if ((string)bu.Content == "Vegan")
                bu.Background = new SolidColorBrush(Colors.PeachPuff);
        }

            private void CatButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button bu = sender as Button;

            bu.Background = new SolidColorBrush(Colors.LightBlue);

        }
    }
}
