using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Project.Model.Person;
using System.Windows.Forms;

namespace ISD_Project.Controller.Person
{
    class CustomerManager
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        Customer cus;

        public CustomerManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void saveCustomer(Customer cus)
        {
            cmd.CommandText = "INSERT INTO customer(name, address, email, phone, birthday, point)" +
                        " values(@name, @address, @email, @phone, @birthday, @point)";
                    cmd.Parameters.AddWithValue("@name", cus.getName());
                    cmd.Parameters.AddWithValue("@address", cus.getAddress());
                    cmd.Parameters.AddWithValue("@email", cus.getEmail());
                    cmd.Parameters.AddWithValue("@phone", cus.getPhone());
                    cmd.Parameters.AddWithValue("@birthday", cus.getBirthday());
                    cmd.Parameters.AddWithValue("@point", cus.getPoint());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                      
        }


        public void updateCustomer(Customer cus)
        {

            cmd.CommandText = " UPDATE customer SET name = @name, address=@address, "+
                              " email = @email, phone = @phone, birthday=@birthday "+
                              " where id = @id ";
            cmd.Parameters.AddWithValue("@name", cus.getName());
            cmd.Parameters.AddWithValue("@address", cus.getAddress());
            cmd.Parameters.AddWithValue("@email", cus.getEmail());
            cmd.Parameters.AddWithValue("@phone", cus.getPhone());
            cmd.Parameters.AddWithValue("@birthday", cus.getBirthday());
            cmd.Parameters.AddWithValue("@id", cus.getId());
            cmd.ExecuteNonQuery();
            conn.Close();
            

        }




        public void updateCustomerPoint(String id, double newPoint)
        {
            double currentPoint = 0;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from customer where id = "+id+" ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                currentPoint = double.Parse(reader.GetString("point"));
            }
            conn.Close();


            currentPoint = currentPoint + newPoint;
            conn.Open();
            cmd.CommandText = " UPDATE customer SET point = @point where id=" + id + " ";
            cmd.Parameters.AddWithValue("@point", currentPoint);
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }


        public void deleteCustomer(String id)
        {

            cmd.CommandText = " DELETE from customer where id = @id ";
            
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            

        }








    }
}
