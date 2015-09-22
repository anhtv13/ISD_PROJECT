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
    public partial class ReportStaff : Form
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        


        public ReportStaff()
        {
            InitializeComponent();
        }

        private void ReportStaff_Load(object sender, EventArgs e)
        {
            label1.Text =labelGetData();
        }

        public String labelGetData()
        {
            int staffNumber = 0;
            int managerNumber = 0;
            int batistaNumber = 0;
            int cashierNumber = 0;
            int guardNumber = 0;

            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from staff";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                staffNumber++;
            }          
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from staff where role=1 ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                managerNumber++;
            }
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from staff where role=2 ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                batistaNumber++;
            }
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from staff where role=3 ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cashierNumber++;
            }
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from staff where role=4 ";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                guardNumber++;
            }
            conn.Close();


            String s = "There are " + staffNumber + " staves."+
                "\nIncluding: "+
                "\n+" + managerNumber + " managers"+
                "\n+" + batistaNumber + " batistas"+
                "\n+" + cashierNumber + " carshiers"+
                "\n+" + guardNumber + " guards";
            return s;
        }

        


    }
}
