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

            int i = 0;
            string fileName = "../../../Categories/Categories.txt";
            ItemCat.Items.Clear();
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    string cat = sr.ReadLine();

                    //ItemCat.SelectedIndex = i;
                    ItemCat.Items.Add(cat);
                    ItemCat.Width = 100;
                    i++;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }

            ResultText.Visibility = Visibility.Collapsed;
            AddItemPane.Visibility = Visibility.Visible;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private bool containsSpace(string str)
        {
            str = str.Trim();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    return true;
                }
            }

            return false;
        }

        private bool containsNonLetters(string str)
        {
            str = str.Trim();
            for (int i = 0; i < str.Length; i++)
            {
                if ((!(str[i] > 64 && str[i] < 91)) && (!(str[i] > 97 && str[i] < 123)) && str[i] != '_')
                {
                    return true;
                }
            }

            return false;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string namecheck = ItemNameBox.Text;
            double pricecheck = 0;
            namecheck = namecheck.Trim();
            if (namecheck == "" || containsNonLetters(namecheck) || namecheck.Length > 10)
            {
                MessageBox.Show("The given name is incorrectly formatted." +
                    "\n\nNames require all alphabet characters or '_' character." +
                    "\n\nNames must be 10 characters or less.");
                return;
            }
            try
            {
                pricecheck = float.Parse(ItemPriceBox.Text);
            }
            catch(FormatException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message + "\n\nTry using actual numbers.");
                return;
            }

            if (pricecheck < 0)
            {
                MessageBox.Show("Please input a valid price.");
                return;
            }

            if (ItemCat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            //ItemPriceBox;
            //ItemCat;
            //ItemNameBox;

            AddItemPane.Visibility = Visibility.Collapsed;
            bool dup = false;
            string fileName = "../../../Prices/Prices.txt";
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] itemInfo = line.Split('\t');
                    String itemName = itemInfo[0];


                    if (itemName != "")
                    {

                        itemName += " - " + itemInfo[2];
                        if (itemName == (ItemNameBox.Text + " - " + ItemCat.Text))
                        {
                            ResultText.Text = ItemNameBox.Text + " already exists.";
                            ResultText.Visibility = Visibility.Visible;
                            dup = true;
                        }

                    }
                    
                }
                sr.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
            if (!dup)
            {
                product new_product = new product();
                new_product.name = ItemNameBox.Text;
                new_product.price = float.Parse(ItemPriceBox.Text);
                new_product.category = ItemCat.Text;
                server.Insert("Products", new employee(), new reciept(), new_product);
                using StreamWriter sw = new StreamWriter(fileName, append: true);

                sw.WriteLine(ItemNameBox.Text + '\t' + ItemPriceBox.Text + '\t' + ItemCat.Text);
                ResultText.Text = ItemNameBox.Text + " added to category " + ItemCat.Text;
                ResultText.Visibility = Visibility.Visible;
                
            }

            ItemNameBox.Text = "";
            ItemPriceBox.Text = "";
            ItemCat.SelectedIndex = 0;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemPane.Visibility = Visibility.Collapsed;
            ItemNameBox.Text = "";
            ItemPriceBox.Text = "";
            ItemCat.SelectedIndex = 0;

        }

        private void CancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteItemPane.Visibility = Visibility.Collapsed;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

            ResultText.Visibility = Visibility.Collapsed;
            DeleteStack.Children.Clear();


            string fileName = "../../../Prices/Prices.txt";
                try
                {
                    using StreamReader sr = new StreamReader(fileName);
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        String[] buttonInfo = line.Split('\t');
                        String buttonName = buttonInfo[0];
                   

                    if (buttonName != "" )
                        {
                            buttonName += " - " + buttonInfo[2];
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
            AddItemPane.Visibility = Visibility.Collapsed;
            MessageBoxResult result = MessageBox.Show("Delete " + ((Button)sender).Content + " ?", "Delete Item", MessageBoxButton.YesNoCancel);

            switch(result)
            {
                case MessageBoxResult.Yes:


                    ResultText.Text = "";
                    ResultText.Visibility = Visibility.Collapsed;
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
                            if (itemInfo[0] != "")
                            {
                                if (!((Button)sender).Content.Equals(itemInfo[0] + " - " + itemInfo[2]))
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
                        }
                        sr.Close();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Error reading items file\n" + ex.Message);

                    }

                    ResultText.Text += delete_product.name + " deleted from category " + delete_product.category;
                    ResultText.Visibility = Visibility.Visible;

                    server.Delete("Products", new employee(), new reciept(), delete_product);
                    updatedFile = updatedFile.TrimEnd('\n', '\r');
                    File.WriteAllText(filePath, String.Empty);

                    try
                    {
                        using StreamWriter sw = new StreamWriter(filePath);
                        if (updatedFile != "")
                            sw.WriteLine(updatedFile);
                        else
                            sw.Write(updatedFile);
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

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Visibility = Visibility.Collapsed;
            InvenStack.Children.Clear();


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
                        //buttonName += " - " + buttonInfo[2];
                        TextBox tb = new TextBox();

                        tb.Text = buttonName;
                        //tb.MinWidth = 100;
                        tb.SelectionOpacity = 0;
                        tb.Focusable = true;
                        tb.Cursor = Cursors.Arrow;
                        tb.IsReadOnly = true;
                        tb.Background = new SolidColorBrush(Colors.LightGray);
                        tb.GotFocus += new RoutedEventHandler(FocusButton1_Click);
                        tb.LostFocus += new RoutedEventHandler(LostFocusButton1);
                        
                        InvenStack.Children.Add(tb);


                    }

                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }

            InventorySetterGetter.Visibility = Visibility.Visible;
        }

        private void FocusButton1_Click(object sender, RoutedEventArgs e)
        {   
            TextBox T = e.OriginalSource as TextBox;

            T.Background = new SolidColorBrush(Colors.LightBlue);

        }

        private void LostFocusButton1(object sender, RoutedEventArgs e)
        {   

            TextBox T = e.OriginalSource as TextBox;

            T.Background = new SolidColorBrush(Colors.LightGray);

        }

        private void SetInventory_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox t in InvenStack.Children)
            {
                if (t.IsFocused)
                {
                    string i = InputBox.Text;
                    string query = "UPDATE Products SET Stock= " + InputBox.Text + " WHERE Name= '" + t.Text + "'";
                    server.update(query);
                }

            }

        }

        private void GetInventory_Click(object sender, RoutedEventArgs e)
        {

            foreach (TextBox t in InvenStack.Children)
            {
                if (t.IsFocused)
                {
                    List<string>[] temp = server.Select("Products");

                    int size = Int32.Parse(temp[4][0]);
                    for (int i = 0; i < size; i++)
                    {
                        if (temp[0][i] == t.Text)
                        {
                            ShowInv.Text = temp[3][i];
                        }
                        

                    }
               
                }

            }

        }

        private void ReturnInven_Click(object sender, RoutedEventArgs e)
        {
            InventorySetterGetter.Visibility = Visibility.Collapsed;
        }

        private void AddonButton_Click(object sender, RoutedEventArgs e)
        {



            int i = 0;
            string fileName = "../../../Categories/Categories.txt";
            AddonCat.Items.Clear();
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    string cat = sr.ReadLine();

                    //ItemCat.SelectedIndex = i;
                    AddonCat.Items.Add(cat);
                    AddonCat.Width = 100;
                    i++;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
            ResultText.Visibility = Visibility.Collapsed;
            AddAddonPane.Visibility = Visibility.Visible;

        }

        private void AddonConfirmButton_Click(object sender, RoutedEventArgs e)
        {

            string namecheck = AddonNameBox.Text;
            double pricecheck = 0;
            namecheck = namecheck.Trim();
            if (namecheck == "" || containsNonLetters(namecheck) || namecheck.Length > 10)
            {
                MessageBox.Show("The given name is incorrectly formatted.\n\nNames require all alphabet characters or '_' character.\n\nNames must be 10 characters or less.");
                return;
            }

            try
            {
                pricecheck = float.Parse(AddonPriceBox.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message + "\n\nTry using actual numbers.");
                return;
            }

            if (pricecheck < 0)
            {
                MessageBox.Show("Please input a valid price.");
                return;
            }

            if (AddonCat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }


            AddAddonPane.Visibility = Visibility.Collapsed;
            bool dup = false;
            //string fileName = "../../../Prices/AddonPrices.txt";
            string fileName = "../../../Prices/Prices.txt";
            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] AddonInfo = line.Split('\t');
                    String AddonName = AddonInfo[0];


                    if (AddonName != "")
                    {

                        AddonName += " - " + AddonInfo[2];
                        if (AddonName == (AddonNameBox.Text + " - " + AddonCat.Text))
                        {
                            ResultText.Text = AddonNameBox.Text + " already exists.";
                            ResultText.Visibility = Visibility.Visible;
                            dup = true;
                        }

                    }

                }
                sr.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading Addons file\n" + ex.Message);

            }
            if (!dup)
            {   
                product new_product = new product();
                new_product.name = '+' + AddonNameBox.Text;
                new_product.price = float.Parse(AddonPriceBox.Text);   
                new_product.category = AddonCat.Text;
                server.Insert("Products", new employee(), new reciept(), new_product);
                using StreamWriter sw = new StreamWriter(fileName, append: true);
                
                sw.WriteLine(AddonNameBox.Text + '\t' + AddonPriceBox.Text + '\t' + AddonCat.Text);
                ResultText.Text = AddonNameBox.Text + " added to category " + AddonCat.Text;
                ResultText.Visibility = Visibility.Visible;

            }

            AddonNameBox.Text = "";
            AddonPriceBox.Text = "";
            AddonCat.SelectedIndex = 0;
        }

        private void AddonCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddAddonPane.Visibility = Visibility.Collapsed;
            AddonNameBox.Text = "";
            AddonPriceBox.Text = "";
            AddonCat.SelectedIndex = 0;
        }

        private void AddCatButton_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Visibility = Visibility.Collapsed;
            AddCatPane.Visibility = Visibility.Visible;
        }

        private void CancelCatAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCatPane.Visibility = Visibility.Collapsed;
        }

        private void ConfirmCatAdd_Click(object sender, RoutedEventArgs e)
        {

            string namecheck = CatNameBox.Text;
            namecheck = namecheck.Trim();
            if (namecheck == "" || containsNonLetters(namecheck) || namecheck.Length > 10)
            {
                MessageBox.Show("The given name is incorrectly formatted." +
                    "\n\nNames require all alphabet characters or '_' character." +
                    "\n\nNames must be 10 characters or less.");
                return;
            }

            AddCatPane.Visibility = Visibility.Collapsed;
            bool dup = false;
            string fileName = "../../../Categories/Categories.txt";

            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    String catName = sr.ReadLine();
                    if (catName != "")
                    {
                        if(catName == (CatNameBox.Text))
                        {
                            ResultText.Text = CatNameBox.Text + " already exists.";
                            ResultText.Visibility = Visibility.Visible;
                            dup = true;
                        }
                    }
                }
                sr.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }

            if(!dup)
            {
                using StreamWriter sw = new StreamWriter(fileName, append: true);
                sw.WriteLine(CatNameBox.Text);
                ResultText.Text = CatNameBox.Text + " added as a new category.";
                ResultText.Visibility = Visibility.Visible;

            }

            CatNameBox.Text = "";
           
        }

        private void DeleteCat_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Visibility = Visibility.Collapsed;
            DeleteCatStack.Children.Clear();
            string fileName = "../../../Categories/Categories.txt";

            try
            {
                using StreamReader sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    String buttonName = sr.ReadLine();
                   


                    if (buttonName != "")
                    {
                       
                        Button NewButton = new Button();

                        NewButton.Content = buttonName;

                        NewButton.Click += new RoutedEventHandler(DeleteSelectedCat);

                        DeleteCatStack.Children.Add(NewButton);


                    }

                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
            DeleteCatPane.Visibility = Visibility.Visible;
           
            
        }

        public void DeleteSelectedCat(object sender, RoutedEventArgs e)
        {
            
            String itemsFileName = "../../../Prices/Prices.txt";
            List<String> allItems = new List<String>();
            bool hasItems = false;
            try
            {
                using StreamReader sr = new StreamReader(itemsFileName);
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] itemInfo = line.Split('\t');
                    String itemName = itemInfo[0];
                    String itemCat = itemInfo[2];

                    if(itemCat == ((Button)sender).Content.ToString())
                    {
                        allItems.Add(itemName);
                        hasItems = true;
                    }
                }
                sr.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }

            if (hasItems)
            {
                String msg = "The category " + ((Button)sender).Content + " has the following items in it. Delete the category and the items?\n\n";
                msg += string.Join(Environment.NewLine, allItems);
                MessageBoxResult deleteItemResult = MessageBox.Show(msg, "Continue", MessageBoxButton.YesNoCancel);
                switch (deleteItemResult)
                {
                    case MessageBoxResult.Yes:
                        String updatedItemFile = "";
                        try
                        {
                            using StreamReader sr = new StreamReader(itemsFileName);
                            product delete_product = new product();
                            while (!sr.EndOfStream)
                            {
                                String line = sr.ReadLine();
                                String[] itemInfo = line.Split('\t');

                                String itemCat = itemInfo[2];

                                if (!(itemCat == ((Button)sender).Content.ToString()))
                                {
                                    updatedItemFile += line + '\n';
                                }
                                else
                                {
                                    delete_product.name = itemInfo[0];
                                    delete_product.price = float.Parse(itemInfo[1]);
                                    delete_product.category = itemInfo[2];
                                    server.Delete("Products", new employee(), new reciept(), delete_product);
                                }

                            }

                            sr.Close();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }

                        try
                        {
                            using StreamWriter sw = new StreamWriter(itemsFileName);
                            sw.WriteLine(updatedItemFile);
                            sw.Close();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }

                        ResultText.Text = "";
                        ResultText.Visibility = Visibility.Collapsed;
                        String catFileName = "../../../Categories/Categories.txt";


                        String updatedCatFile = "";
                        //product delete_product = new product();

                        try
                        {
                            using StreamReader sr = new StreamReader(catFileName);
                            while (!sr.EndOfStream)
                            {
                                String cat = sr.ReadLine();
                                if (cat != "")
                                {
                                    if (!((Button)sender).Content.Equals(cat))
                                    {

                                        updatedCatFile += cat + '\n';
                                    }
                                    //else
                                    //{
                                    //    delete_product.name = itemInfo[0];
                                    //    delete_product.price = float.Parse(itemInfo[1]);
                                    //    delete_product.category = itemInfo[2];
                                    //}
                                }
                            }
                            sr.Close();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }

                        ResultText.Text += ((Button)sender).Content + " deleted.";
                        ResultText.Visibility = Visibility.Visible;

                        //server.Delete("Products", new employee(), new reciept(), delete_product);
                        updatedCatFile = updatedCatFile.TrimEnd('\n', '\r');

                        File.WriteAllText(catFileName, String.Empty);



                        try
                        {
                            using StreamWriter sw = new StreamWriter(catFileName);
                            sw.WriteLine(updatedCatFile);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }
                        DeleteCatStack.Children.Remove((Button)sender);

                        break;
                    case MessageBoxResult.No:

                        break;
                    case MessageBoxResult.Cancel:
                        DeleteCatPane.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Delete " + ((Button)sender).Content + " ?", "Delete Item", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        ResultText.Text = "";
                        ResultText.Visibility = Visibility.Collapsed;
                        String catFileName = "../../../Categories/Categories.txt";

                        String updatedCatFile = "";
                        //product delete_product = new product();

                        try
                        {
                            using StreamReader sr = new StreamReader(catFileName);
                            while (!sr.EndOfStream)
                            {
                                String cat = sr.ReadLine();
                                if (cat != "")
                                {
                                    if (!((Button)sender).Content.Equals(cat))
                                    {

                                        updatedCatFile += cat + '\n';
                                    }
                                    //else
                                    //{
                                    //    delete_product.name = itemInfo[0];
                                    //    delete_product.price = float.Parse(itemInfo[1]);
                                    //    delete_product.category = itemInfo[2];
                                    //}
                                }
                            }
                            //while(sr.Peek() == '\n')
                            //{
                            //    sr.Read();
                            //}
                            sr.Close();
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }

                        ResultText.Text += ((Button)sender).Content + " deleted.";
                        ResultText.Visibility = Visibility.Visible;

                        //server.Delete("Products", new employee(), new reciept(), delete_product);
                        updatedCatFile = updatedCatFile.Trim('\n', '\r');
                        

                        File.WriteAllText(catFileName, String.Empty);

                        try
                        {
                            using StreamWriter sw = new StreamWriter(catFileName);
                            if (updatedCatFile != "")
                                sw.WriteLine(updatedCatFile);
                            else
                                sw.Write(updatedCatFile);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Error reading items file\n" + ex.Message);

                        }
                        DeleteCatStack.Children.Remove((Button)sender);
                        // DeleteItemPane.Visibility = Visibility.Collapsed;

                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        DeleteCatPane.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        public void CancelCatDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteCatPane.Visibility = Visibility.Collapsed;
        }

        private void ChangeTax_Click(object sender, RoutedEventArgs e)
        {

            ResultText.Visibility = Visibility.Collapsed;
            ChangeTaxPane.Visibility = Visibility.Visible;
        }

        private void ConfirmTax_Click(object sender, RoutedEventArgs e)
        {
            String confFileName = "../../../Prices/Conf.txt";
            double newTax;

            try
            {
                newTax = Double.Parse(TaxBox.Text);
                newTax /= 100;

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message + "\n\nTry using actual numbers.");
                return;
            }

            try
            {
                using StreamWriter sw = new StreamWriter(confFileName);
                sw.WriteLine(newTax);


            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading items file\n" + ex.Message);

            }
            if(newTax < 0)
            {
                MessageBox.Show("Please input a valid tax rate.");
                return;
            }
            ResultText.Text += "Tax changed to " + newTax * 100 + "%";
            ResultText.Visibility = Visibility.Visible;
            ChangeTaxPane.Visibility = Visibility.Collapsed;
        }

        private void CancelTax_Click(object sender, RoutedEventArgs e)
        {
            ChangeTaxPane.Visibility = Visibility.Collapsed;
        }

        private void reciept_Click(object sender, RoutedEventArgs e)
        {
            List<string>[] temp = server.Select("Reciepts");
            string o = "";
            int size = Int32.Parse(temp[5][0]);

            for (int i = 0; i < size; i++)
            {
                o += "Content: " + encryption.Decrypt(temp[0][i], encryption.getKey()) + '\n' + "Item Amount: " + encryption.Decrypt(temp[1][i], encryption.getKey()) + '\n' + "Price: "
                    + encryption.Decrypt(temp[2][i], encryption.getKey()) + '\n' + "Tax Amount: " + encryption.Decrypt(temp[3][i], encryption.getKey()) + '\n' + "Full Price: " + encryption.Decrypt(temp[4][i], encryption.getKey()) + "\n\n\n";
            }
            MessageBox.Show(o);
        }
    }
}
