using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISD_Project.Model.Financial;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ISD_Project.Controller.Financial
{
    class IncomeManager
    {
       

        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;


        public IncomeManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }


        public void saveIncome(Income inc)
        {
            cmd.CommandText = "INSERT INTO income(customerid, total, datetime)" +
                        " values(@customerid, @total, @datetime)";
            cmd.Parameters.AddWithValue("@customerid", inc.getCustomerid());
            cmd.Parameters.AddWithValue("@total", inc.getTotal());
            cmd.Parameters.AddWithValue("@datetime", inc.getDatetime());
           
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }






    }
}
