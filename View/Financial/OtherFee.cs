using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ISD_Project.Model.Financial;
using ISD_Project.Controller.Financial;

namespace ISD_Project.View.Financial
{
    public partial class OtherFee : Form
    {
        double electricity;
        double water;
        double internet;
        double otherfee;
        private Outcome outcome;
        private OutcomeManager outMan;

        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;


        public OtherFee()
        {
            InitializeComponent();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    electricity = double.Parse(textBoxElectricity.Text);
                    water = double.Parse(textBoxWater.Text);
                    internet = double.Parse(textBoxInternet.Text);
                    otherfee = double.Parse(textBoxOthers.Text);
                    DateTime now = DateTime.Now;


                    outcome = new Outcome(electricity, water, internet, otherfee);
                    if (outcome.repOK() == true)
                    {
                        outMan = new OutcomeManager();
                        outMan.savefee(outcome, now.Month);
                        tableGetData();
                        MessageBox.Show("Save successfully.");
                    }
                    else
                        MessageBox.Show("Wrong input.");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxElectricity.Text = "";
            textBoxWater.Text = "";
            textBoxInternet.Text = "";
            textBoxOthers.Text = "";
        }

        private void OtherFee_Load(object sender, EventArgs e)
        {
            tableGetData();
            labelGetData();
        }

        public void labelGetData()
        {
            int index = table1.Rows.Count;
            double salary;
            double ingredientfee;
            double total = 0;
           


            salary = double.Parse(table1.CurrentRow.Cells[2].Value.ToString());
            ingredientfee = double.Parse(table1.CurrentRow.Cells[3].Value.ToString());
            electricity =  double.Parse(table1.CurrentRow.Cells[4].Value.ToString());
            water = double.Parse(table1.CurrentRow.Cells[5].Value.ToString());
            internet = double.Parse(table1.CurrentRow.Cells[6].Value.ToString());
            otherfee = double.Parse(table1.CurrentRow.Cells[7].Value.ToString());
           // MessageBox.Show(salary +" "+ingredientfee +" "+ water +" "+ internet +" "+ otherfee);
            total = salary + ingredientfee +electricity+ water + internet + otherfee;

            
            label4.Text = total.ToString();
        }

        public void tableGetData()
        {
            try
            {
                //table1
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.outcome ;", conn);
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
            int selectedIndex = table1.CurrentCell.RowIndex;
            //MessageBox.Show(table1.Rows[selectedIndex].Cells[0].Value.ToString());
            labelStaffSalary.Text = table1.CurrentRow.Cells[2].Value.ToString();
            labelIngredientFee.Text = table1.CurrentRow.Cells[3].Value.ToString();
            textBoxElectricity.Text = table1.Rows[selectedIndex].Cells[4].Value.ToString();
            textBoxWater.Text = table1.Rows[selectedIndex].Cells[5].Value.ToString();
            textBoxInternet.Text = table1.Rows[selectedIndex].Cells[6].Value.ToString();
            textBoxOthers.Text = table1.Rows[selectedIndex].Cells[7].Value.ToString();
        }








    }
}
