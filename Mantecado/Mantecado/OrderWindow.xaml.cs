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
    /// 
    public class item : System.Windows.UIElement
    {

        public item()
        {
            
        }

        public
        TextBox T;
        string ID;

    }

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

        int[] categories = new int[]{0, 1, 2, 3};

        private void Milkshake_button(object sender, RoutedEventArgs e)
        {

            item anItem = new item();           //instantiates an item and assigns it...
                                                //  we want to use some sort of wrapper class that can hold 
                                                //  extra information about our instantiated item


            anItem.T = new TextBox();           //instantiates a textbox and assigns it to our variable

            anItem.T.AcceptsReturn = true;      //allows newlines to be used in strings for formating

            anItem.T.AcceptsTab = true;         //allows tabs to be used in strings for formatting

            anItem.T.IsReadOnly = true;         //keep from writing to and deleting text in box

            anItem.T.Background = new SolidColorBrush(Colors.White);    //makes textbox background white

            anItem.T.BorderThickness = new Thickness(0);    //no border, looks more like one seamless order
            
            anItem.T.Cursor = Cursors.Arrow;    //default cursor is insert token, changed to always be arrow 

            anItem.T.Focusable = true;          //makes the text boxes focusable 

            anItem.T.SelectionOpacity = 0;      //keeps annoying selection field from appearing on mouse clicks of textbox


            //Event Handlers
            anItem.T.MouseLeave += new MouseEventHandler(tb_onMouseLeave);      //changes back to white when the mouse leaves region

            anItem.T.MouseEnter += new MouseEventHandler(tb_onMouseEnter);      //turns textbox blue when you mouse over

            anItem.T.GotFocus += new RoutedEventHandler(TextBoxOnFocus);        //turns blue with border when clicked

            anItem.T.LostFocus += new RoutedEventHandler(TextBoxLostFocus);     //change back to white



            
            //anItem.T.BringIntoView();       

            anItem.T.FontSize = 26;

            anItem.T.Text = "+Van. Shake\t";

            anItem.T.AppendText("$4.99");

            Stacky.Children.Add(anItem);
            

        }

        //turns textbox blue when you mouse over
        private void tb_onMouseEnter(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;    //connects to the textbox that called this handler

            T.Background = new SolidColorBrush(Colors.LightBlue);   //changes the color as you mouse over the boxes

        }

        //changes back to white when the mouse leaves region
        private void tb_onMouseLeave(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;

            if(!T.IsFocused)                                               //makes the background white only if it wasn't clicked on
                T.Background = new SolidColorBrush(Colors.White);

        }

        //turns blue with border when clicked
        private void TextBoxOnFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;

            T.Background = new SolidColorBrush(Colors.LightBlue);           //makes the textbox blue when clicked on

            T.BorderThickness = new Thickness(3);                           //gives it a border to emphasize selection
                
        }

        //proof of concept Context Button
        private void Strawberry_Button(object sender, RoutedEventArgs e)
        {              
            foreach (TextBox child in Stacky.Children)                      
            {
                if (child.IsFocused)
                {
                    child.AppendText("\n   +Strawberries\t");

                    child.AppendText("$.39");
                }
            }

        }

        //change back to white
        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {

            TextBox T = e.OriginalSource as TextBox;

            T.Background = new SolidColorBrush(Colors.White);           

            T.BorderThickness = new Thickness(0);

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
