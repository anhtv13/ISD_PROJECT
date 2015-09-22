using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Final_Project;

namespace ISD_Project.Controller.Property
{
    class ProductManager
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        Product Pro;

        public ProductManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void saveProduct(Product Pro)
        {
            cmd.CommandText = "INSERT INTO product(name, quantity, price)" +
                        " values(@name, @quantity, @price)";
            cmd.Parameters.AddWithValue("@name", Pro.getName());
            cmd.Parameters.AddWithValue("@quantity", Pro.getQuantity());
            cmd.Parameters.AddWithValue("@price", Pro.getPrice());
           
            
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }


        public void updateProduct(Product Pro)
        {

            cmd.CommandText = " UPDATE product SET name = @name, quantity = @quantity, " +
                              " price = @price " +
                              " where id = @id ";
            cmd.Parameters.AddWithValue("@name", Pro.getName());
            cmd.Parameters.AddWithValue("@quantity", Pro.getQuantity());
            cmd.Parameters.AddWithValue("@price", Pro.getPrice());
            cmd.Parameters.AddWithValue("@id", Pro.getId());
            
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }




        public void deleteProduct(String id)
        {

            cmd.CommandText = " DELETE from product where id = @id ";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }




        public void updateProductQuantity(String name, double useQuantity)
        {
            double currentQuantity = 0;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from product where name = '" + name + "' ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                currentQuantity = double.Parse(reader.GetString("quantity"));
            }
            conn.Close();


            currentQuantity = currentQuantity - useQuantity;
            conn.Open();
            cmd.CommandText = " UPDATE product SET quantity = @quantity where name ='" + name + "' ";
            cmd.Parameters.AddWithValue("@quantity", currentQuantity);
            cmd.ExecuteNonQuery();
            conn.Close();

        }









    }
}
