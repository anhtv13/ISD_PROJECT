using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ISD_Project.View.Financial
{
    public partial class SalaryGUI : Form
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        


        public SalaryGUI()
        {
            InitializeComponent();
        }

        private void Salary_Load(object sender, EventArgs e)
        {
            tableGetData();
            labelGetData();
        }


        public void labelGetData()
        {
            int index = table1.Rows.Count;
            double total=0;

            for (int i = 0; i < index - 1; i++)
            {
                total = total + double.Parse(table1.Rows[i].Cells[3].Value.ToString());
            }
            total = 2000 * total;
            label1.Text = total.ToString();
        }


        public void tableGetData()
        {
            try
            {
                //table1
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand("SELECT staff.id, staff.name, staffrole.role, staffrole.salarynumber"+
                " FROM isd_project.staff "+
                " JOIN isd_project.staffrole "+
                " ON staff.role = staffrole.id ", conn );
                DataTable tb = new DataTable();
                MyDA.Fill(tb);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = tb;
                table1.DataSource = bSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void table1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String staffName;
                double salary;
                int selectedIndex = table1.CurrentCell.RowIndex;
                //MessageBox.Show(table1.Rows[selectedIndex].Cells[0].Value.ToString());
                staffName = table1.Rows[selectedIndex].Cells[1].Value.ToString();
                salary=double.Parse( table1.Rows[selectedIndex].Cells[3].Value.ToString());
                salary = salary * 2000;
                MessageBox.Show(staffName + ": $" + salary.ToString()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }












    }
}
