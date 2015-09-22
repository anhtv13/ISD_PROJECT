using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ISD_Project.Model.Financial;

namespace ISD_Project.Controller.Financial
{
    class OutcomeManager
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;


        public OutcomeManager()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void saveSalary(double salary, int month)
        {
            cmd.CommandText = "update isd_project.outcome set salary = @salary where month(time)= @month";
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@month", month);
          

            cmd.ExecuteNonQuery();
            conn.Close();

        }



        public void saveIngredientFee(double fee, int month)
        {
            cmd.CommandText = "update isd_project.outcome set ingredientfee = @ingredientfee where month(time)= @month";
            cmd.Parameters.AddWithValue("@ingredientfee", fee);
            cmd.Parameters.AddWithValue("@month", month);


            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void savefee(Outcome outcome, int month)
        {
            cmd.CommandText = "update isd_project.outcome set electricity = @electricity, " +
                                                            " water = @water, "+
                                                            " internet = @internet, "+
                                                            " otherfee = @otherfee " +
                                                            " where month(time)= @month";
            cmd.Parameters.AddWithValue("@electricity", outcome.getElectricity());
            cmd.Parameters.AddWithValue("@water", outcome.getWater());
            cmd.Parameters.AddWithValue("@internet", outcome.getInternet());
            cmd.Parameters.AddWithValue("@otherfee", outcome.getOtherFee());
            cmd.Parameters.AddWithValue("@month", month);


            cmd.ExecuteNonQuery();
            conn.Close();

        }
















    }
}
