using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Final_Project;


namespace ISD_Project.Controller.Property
{
    class InfrastructureManager
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        Infrastructure Inf;

        public InfrastructureManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void saveInfrastructure(Infrastructure Inf)
        {
            
            cmd.CommandText = "INSERT INTO infrastructure(name, quantity, price, status)" +
                        " values(@name, @quantity,@price, @status)";
            cmd.Parameters.AddWithValue("@name", Inf.getName());
            cmd.Parameters.AddWithValue("@quantity", Inf.getQuantity());
            cmd.Parameters.AddWithValue("@price", Inf.getPrice());
            cmd.Parameters.AddWithValue("@status", Inf.getStatus());

            cmd.ExecuteNonQuery();
            conn.Close();
            
        }


        public void updateInfrastructure(Infrastructure Inf)
        {

            
            cmd.CommandText = " UPDATE infrastructure SET name = @name, quantity = @quantity, " +
                              " price = @price, status = @status " +
                              " where id = @id ";
            cmd.Parameters.AddWithValue("@id", Inf.getId());
            cmd.Parameters.AddWithValue("@name", Inf.getName());
            cmd.Parameters.AddWithValue("@quantity", Inf.getQuantity());
            cmd.Parameters.AddWithValue("@price", Inf.getPrice());
            cmd.Parameters.AddWithValue("@status", Inf.getStatus());

            
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }




        public void deleteInfrastructure(String id)
        {

            cmd.CommandText = " DELETE from infrastructure where id = @id ";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
           

        }









    }
}
