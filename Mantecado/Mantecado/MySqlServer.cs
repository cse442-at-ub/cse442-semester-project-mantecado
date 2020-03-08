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
        public int id;
        public double pay_rate;
        public string sex;
        public string birthday;
    }

    class MySqlServer
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MySqlServer(){
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

        public void Insert(string data_base, employee hire = new employee())
        {
            string query = "INSERT INTO "+data_base;

            // if it is an employee
            //if(hire.name == null)
            query += "(name, age, id_num, pay, sex, birthday) VALUES('" + hire.name + "', " + hire.age + ", " + hire.id + ", " + hire.pay_rate+ ", '" + hire.sex + "', " + hire.birthday + ")" ;
            
            //else it must be for the menu or something

            if (this.Connect() == true)     // Open connection
            {

                MySqlCommand command = new MySqlCommand(query, connection);
        
                command.ExecuteNonQuery();

                this.Disconnect();      // Close connection
            }
        }

    }
}
