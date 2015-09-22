using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISD_Project.Model.Person;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ISD_Project.Controller.Person
{
    class SupplierManager
    {
    
       string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;


        private Supplier sup;

        public SupplierManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }




        public void saveSupplier(Supplier sup)
        {
            cmd.CommandText = "INSERT INTO supplier(name, address, email, phone, birthday, ingredient_id)" +
                        " values(@name, @address, @email, @phone, @birthday, @ingredient_id)";
            cmd.Parameters.AddWithValue("@name", sup.getName());
            cmd.Parameters.AddWithValue("@address", sup.getAddress());
            cmd.Parameters.AddWithValue("@email", sup.getEmail());
            cmd.Parameters.AddWithValue("@phone", sup.getPhone());
            cmd.Parameters.AddWithValue("@birthday", sup.getBirthday());
            cmd.Parameters.AddWithValue("@ingredient_id", sup.getIngredientid());
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }


        public void updateSupplier(Supplier sup)
        {

            cmd.CommandText = " UPDATE supplier SET name = @name, address=@address, " +
                              " email = @email, phone = @phone, birthday=@birthday, ingredient_id = @ingredient_id " +
                              " where id = @id ";
            cmd.Parameters.AddWithValue("@name", sup.getName());
            cmd.Parameters.AddWithValue("@address", sup.getAddress());
            cmd.Parameters.AddWithValue("@email", sup.getEmail());
            cmd.Parameters.AddWithValue("@phone", sup.getPhone());
            cmd.Parameters.AddWithValue("@birthday", sup.getBirthday());
            cmd.Parameters.AddWithValue("@ingredient_id", sup.getIngredientid());
            cmd.Parameters.AddWithValue("@id", sup.getId());
            cmd.ExecuteNonQuery();
            conn.Close();
           
        }


        public void deleteSupplier(String id)
        {

            cmd.CommandText = " DELETE from supplier where id = @id ";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
           

        }




    }
}
