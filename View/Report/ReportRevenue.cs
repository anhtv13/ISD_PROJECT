using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ISD_Project.View.Report
{
    public partial class ReportRevenue : Form
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;



        public ReportRevenue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int day, month, year;
                if (radioButtonDay.Checked)
                {
                    day = int.Parse(comboBoxDay.SelectedItem.ToString());
                    month = int.Parse(comboBoxMonth.SelectedItem.ToString());
                    year = int.Parse(comboBoxYear.SelectedItem.ToString());
                    displayIncome(day, month, year);
                }
                if (radioButtonMonth.Checked)
                {

                    month = int.Parse(comboBoxMonth.SelectedItem.ToString());
                    year = int.Parse(comboBoxYear.SelectedItem.ToString());
                    displayIncome(month, year);
                }
                if (radioButtonYear.Checked)
                {

                    year = int.Parse(comboBoxYear.SelectedItem.ToString());
                    displayIncome(year);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error range. Please check it again.\n");
            }


        }


        public void displayIncome(int day, int month, int year)
        {
            int income = 0;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.income where day(datetime) = " + day +
                " and month(datetime) = "+month +" and year(datetime) = "+year + " ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                income = income + int.Parse(reader.GetString("total"));              
            }
            conn.Close();
            labelIncome.Text = "Income: " + income;
            labelOutcome.Text = "Outcome: 0";
            labelRevenue.Text = "Revenue: " + income;
            
        }

        public void displayIncome(int month, int year)
        {
            int income = 0;
            int outcome = 0;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.income where month(datetime) = " + month + " and year(datetime) = " + year + " ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                income = income + int.Parse(reader.GetString("total"));
            }
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.outcome where month(time) = " + month + " and year(time) = " + year + " ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                outcome = int.Parse(reader.GetString("salary"))+int.Parse(reader.GetString("ingredientfee"))+int.Parse(reader.GetString("electricity"))+
                    int.Parse(reader.GetString("water"))+int.Parse(reader.GetString("internet"))+int.Parse(reader.GetString("otherfee"));
            }
            conn.Close();


            labelIncome.Text = "Income: " + income;
            labelOutcome.Text = "Outcome: "+outcome;
            labelRevenue.Text = "Revenue: " +(income-outcome);
            
        }


        public void displayIncome(int year)
        {
            int income = 0;
            int outcome = 0;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.income where  year(datetime) = " + year + " ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                income = income + int.Parse(reader.GetString("total"));
            }
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.outcome where year(time) = " + year + " ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                outcome = int.Parse(reader.GetString("salary")) + int.Parse(reader.GetString("ingredientfee")) + int.Parse(reader.GetString("electricity")) +
                    int.Parse(reader.GetString("water")) + int.Parse(reader.GetString("internet")) + int.Parse(reader.GetString("otherfee"));
            }
            conn.Close();



            labelIncome.Text = "Income: " + income;
            labelOutcome.Text = "Outcome: " + outcome;
            labelRevenue.Text = "Revenue: " + (income - outcome);
            
        }


    }
}
