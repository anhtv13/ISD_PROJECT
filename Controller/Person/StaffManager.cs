using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Project.Model.Person;
using System.Windows.Forms;

namespace ISD_Project.Controller.Person
{
    class StaffManager
    {


        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        private Staff sta;

        public StaffManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        


        public void saveStaff(Staff sta)
        {
            cmd.CommandText = "INSERT INTO staff(name, address, email, phone, birthday, role)" +
                        " values(@name, @address, @email, @phone, @birthday, @role)";
                    cmd.Parameters.AddWithValue("@name", sta.getName());
                    cmd.Parameters.AddWithValue("@address", sta.getAddress());
                    cmd.Parameters.AddWithValue("@email", sta.getEmail());
                    cmd.Parameters.AddWithValue("@phone", sta.getPhone());
                    cmd.Parameters.AddWithValue("@birthday", sta.getBirthday());
                    cmd.Parameters.AddWithValue("@role", sta.getRoleId());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                        
        }


        public void updateStaff(Staff sta)
        {

            cmd.CommandText = " UPDATE staff SET name = @name, address=@address, "+
                              " email = @email, phone = @phone, birthday=@birthday, role = @role "+
                              " where id = @id ";
            cmd.Parameters.AddWithValue("@name", sta.getName());
            cmd.Parameters.AddWithValue("@address", sta.getAddress());
            cmd.Parameters.AddWithValue("@email", sta.getEmail());
            cmd.Parameters.AddWithValue("@phone", sta.getPhone());
            cmd.Parameters.AddWithValue("@birthday", sta.getBirthday());
            cmd.Parameters.AddWithValue("@role", sta.getRoleId());
            cmd.Parameters.AddWithValue("@id", sta.getId());
            cmd.ExecuteNonQuery();
            conn.Close();
          

        }


        public void deleteStaff(String id)
        {

            cmd.CommandText = " DELETE from staff where id = @id ";
            
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            

        }






    }
}
