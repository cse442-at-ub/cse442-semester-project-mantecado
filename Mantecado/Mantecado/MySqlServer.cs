using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Mantecado
{

    struct employee
    {
        public string name;
        public int age;
        public string id;
        public double pay_rate;
        public string sex;
        public string birthday;
    }

    struct reciept
    {
        public string order;
        public int item_amount;
        public double price;
        public double tax_amount;
        public double total_price;
    }

    struct product
    {
        public string name;
        public double price;
        public string category;
    }
    class MySqlServer
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MySqlServer()
        {
            server = "tethys.cse.buffalo.edu";
            database = "cse442_542_2020_spring_teamaa_db";
            uid = "felixdel";
            password = "50294909";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public bool Connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        public bool Disconnect()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Delete(string table, employee hire = new employee(), reciept order = new reciept(), product item = new product())
        {
            string query = "DELETE FROM " + table + " WHERE `Name` = ";
            if (table == "Products")
            {
                query += "'" + item.name + "'";
            }
            else
                return;

            
            if (this.Connect() == true)     // Open connection
            {

                MySqlCommand command = new MySqlCommand(query, connection);

                command.ExecuteNonQuery();

                this.Disconnect();      // Close connection
            }

        }

            public void Insert(string table, employee hire = new employee(), reciept order = new reciept(), product item = new product())
        {
            string query = "INSERT INTO " + table;

            // if it is an employee
            if (table == "employees")
            {
                query += "(name, age, id_num, pay, sex, birthday) VALUES('" + hire.name + "', " + hire.age + ", " + hire.id + ", " + hire.pay_rate + ", '" + hire.sex + "', " + hire.birthday + ")";
            }
            else if (table == "Reciepts")
            {
                query += "(contents, item_amount, price, tax_amount, full_price) VALUES('" + order.order + "', " + order.item_amount + ", " + order.price + ", " + order.tax_amount + ", " + order.total_price + ")";

            }
            else if (table == "Products")
            {
                query += "(Name, Price, Type) VALUES('" + item.name + "', " + item.price + ", '" + item.category + "')";
            }
            else
            {
                return;
            }
            //else it must be for the menu or something

            if (this.Connect() == true)     // Open connection
            {

                MySqlCommand command = new MySqlCommand(query, connection);

                command.ExecuteNonQuery();

                this.Disconnect();      // Close connection
            }
        }

        public List<string>[] Select(string table)
        {
            string query = "SELECT * FROM " + table;
            int count = 0;
            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();
            //Open connection
            if (this.Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (table == "employees")
                {
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["name"] + "");
                        list[1].Add(dataReader["age"] + "");
                        list[2].Add(dataReader["id_num"] + "");
                        list[3].Add(dataReader["pay"] + "");
                        list[4].Add(dataReader["sex"] + "");
                        list[5].Add(dataReader["birthday"] + "");
                        count++;
                    }
                    list[6].Add(Convert.ToString(count, 10));
                }
                else if (table == "Products")
                {
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["Name"] + "");
                        list[1].Add(dataReader["Price"] + "");
                        list[2].Add(dataReader["Type"] + "");
                        count++;
                    }
                    list[3].Add(Convert.ToString(count, 10));
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.Disconnect();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

    }
}
