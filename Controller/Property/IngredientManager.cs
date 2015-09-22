using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Final_Project;
using System.Windows.Forms;

namespace ISD_Project.Controller.Property
{
    class IngredientManager
    {
         string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        Ingredient Ing;

        public IngredientManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void saveIngredient(Ingredient Ing)
        {
            
            cmd.CommandText = "INSERT INTO ingredient(name, quantity, price, status)" +
                        " values(@name, @quantity, @price, @status)";
            cmd.Parameters.AddWithValue("@name", Ing.getName());
            cmd.Parameters.AddWithValue("@quantity", Ing.getQuantity());
            cmd.Parameters.AddWithValue("@price", Ing.getPrice());
            cmd.Parameters.AddWithValue("@status", Ing.getStatus());
            
        
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }


        public void updateIngredient(Ingredient Ing)
        {

            cmd.CommandText = " UPDATE ingredient SET name = @name, quantity = @quantity, " +
                              " price= @price , status = @status " +
                              " where id = @id ";
            
            cmd.Parameters.AddWithValue("@name", Ing.getName());
            cmd.Parameters.AddWithValue("@quantity", Ing.getQuantity());     
            cmd.Parameters.AddWithValue("@price", Ing.getPrice());
            cmd.Parameters.AddWithValue("@status", Ing.getStatus());
            cmd.Parameters.AddWithValue("@id", Ing.getId());
            
            cmd.ExecuteNonQuery();
            conn.Close();
       
        }




        public void deleteIngredient(String id)
        {

            cmd.CommandText = " DELETE from ingredient where id = @id ";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
           
        }







    }
}
