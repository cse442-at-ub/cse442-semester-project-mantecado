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


        bool CheckForInjection(string text)
        {
            if (text.IndexOf("SELECT", 0, text.Length) != -1 || text.IndexOf("INSERT", 0, text.Length) != -1 || text.IndexOf("DELETE", 0, text.Length) != -1 || text.IndexOf("UPDATE", 0, text.Length) != -1|| text.IndexOf("*", 0, text.Length) != -1)
            {
                return false;
            }
            if(text.IndexOf("*", 0, text.Length) != -1)
            {
                return false;
            }
            return true;
        }


        int ValidData()
        {
            if(id_box.Text.Length > 4 || id_box.Text.Length == 0)
            {
                return 1;
            }

            if(name_box.Text.Length > 20 || gender_box.Text.Length > 20 || birthday_box.Text.Length > 20)
            {
                return 2;
            }

            if(!CheckForInjection(name_box.Text) || !CheckForInjection(gender_box.Text) || !CheckForInjection(birthday_box.Text))
            {
                return 3;
            }

            int i = 0;
            decimal j = 0;

            if(!int.TryParse(age_box.Text, out i) || !int.TryParse(id_box.Text, out i) || !decimal.TryParse(pay_box.Text, out j))
            {
                return 4;
            }

            return 0;
        }

        private employee GetData()
        {
            employee new_user = new employee();
            new_user.name = encryption.Encrypt(name_box.Text, encryption.getKey());
            new_user.age = encryption.Encrypt(age_box.Text, encryption.getKey());
            new_user.id = encryption.Encrypt(id_box.Text, encryption.getKey());
            new_user.pay_rate = encryption.Encrypt(pay_box.Text, encryption.getKey());
            new_user.sex = encryption.Encrypt(gender_box.Text, encryption.getKey());
            new_user.birthday = encryption.Encrypt(birthday_box.Text, encryption.getKey());

            return new_user;
        }
        private void Submit_Click_1(object sender, RoutedEventArgs e)
        {
            int validation = ValidData();
            if (validation == 0)
            {
                employee data = GetData();

                //List<string>[] temp = server.Select("employees");
                server.Insert("employees",data);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();

                MessageBox.Show("New Employee: " + encryption.Decrypt(data.name,encryption.getKey()) + " has been added to data base.\nUser ID: " + encryption.Decrypt(data.id, encryption.getKey())+"\nPay Rate: " + encryption.Decrypt(data.pay_rate, encryption.getKey()) + "\n");
            }
            else
            {
                switch (validation)
                {
                    case 1:
                        MessageBox.Show("ID number is of not of size 4 please try again. ");
                        break;
                    case 2:
                        MessageBox.Show("Name, Gender or Birthday field exceed length of 20 please try again.");
                        break;
                    case 3:
                        MessageBox.Show("Please don't try and hack my server. :(");
                        break;
                    case 4:
                        MessageBox.Show("Age, ID Number, or Pay are supposed to be numbers please try again.");
                        break;
                    default:
                        MessageBox.Show("Some error occured!");
                        break;
                }
            }
        }
    }
}
