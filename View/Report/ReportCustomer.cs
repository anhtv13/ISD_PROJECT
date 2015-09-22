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
    public partial class ReportCustomer : Form
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;

        public ReportCustomer()
        {
            InitializeComponent();
        }

        private void ReportCustomer_Load(object sender, EventArgs e)
        {
            label1.Text = labelGetData();
        }

        public String labelGetData()
        {
            int customerNumber = 0;
            DateTime now = DateTime.Now;
            

            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from isd_project.income where month(datetime) = " + now.Month + " ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customerNumber++;
            }
            conn.Close();



            String s = "There are " + customerNumber + " arrivals to our Cafe Shop this month.";
               
            return s;
        }







    }
}
