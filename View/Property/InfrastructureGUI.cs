using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISD_Project.Controller.Property;
using ISD_Final_Project;
using MySql.Data.MySqlClient;

namespace ISD_Project.View.Property
{
    public partial class InfrastructureGUI : Form
    {
        private String id; 
        private String name;    
        private double quantity;
        private double price;
        private String status;
        private String date;

        private Infrastructure Inf;
        private InfrastructureManager InfMan;



        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        public InfrastructureGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to add an infrastructure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    quantity = double.Parse(textBoxQuantity.Text);
                    price = double.Parse(textBoxPrice.Text);
                    status = textBoxStatus.Text;
                    Inf = new Infrastructure(name, quantity, price, status);
                    if (Inf.repOK())
                    {
                        InfMan = new InfrastructureManager();
                        InfMan.saveInfrastructure(Inf);

                        //write text file
                        //MessageBox.Show(cus.toString());
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Add Infrastructure{" + Inf.toString() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add Infrastructure successfully.");

                    }
                    else
                    {
                        MessageBox.Show("Invalid Infrastructure.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to update an infrastructure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        id = table1.Rows[selectedIndex].Cells[0].Value.ToString();
                        name = textBoxName.Text;
                        quantity = double.Parse(textBoxQuantity.Text);
                        price = double.Parse(textBoxPrice.Text);
                        status = textBoxStatus.Text;
                        Inf = new Infrastructure(name, quantity, price, status);
                        Inf = new Infrastructure(id, name, quantity, price, status);
                        if (Inf.repOK())
                        {
                            InfMan = new InfrastructureManager();
                            InfMan.updateInfrastructure(Inf);

                            //write text file
                            DateTime now = DateTime.Now;
                            date = now.ToString("yyyy-MM-dd");
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                            file.WriteLine("Update Infrastructure{" + Inf.toString() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update Infrastructure successfully.");

                        }
                        else
                        {
                            MessageBox.Show("Invalid Infrastructure.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select an infrastructure to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete an infrastructure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        id = table1.Rows[selectedIndex].Cells[0].Value.ToString();

                        InfMan = new InfrastructureManager();
                        InfMan.deleteInfrastructure(id);

                        //write text file
                        //MessageBox.Show(cus.toString());
                        Inf = new Infrastructure();
                        DateTime now = DateTime.Now;
                        date = now.ToString("yyyy-MM-dd");
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Delete Infrastructure{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete Infrastructure successfully.");


                    }
                    else
                    {
                        MessageBox.Show("Select an infrastructure to delete.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableGetData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxQuantity.Text = "";
            textBoxPrice.Text = "";
            textBoxStatus.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //table1
                String pname = textBoxSearch.Text;
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.Infrastructure where name like '%" + pname + "%' order by id ", conn);

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

        private void InfrastructureGUI_Load(object sender, EventArgs e)
        {
            tableGetData();
        }

        public void tableGetData()
        {
            try
            {
                //table1
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM infrastructure order by id ", conn);
                DataTable tb = new DataTable();
                MyDA.Fill(tb);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = tb;
                table1.DataSource = bSource;
                conn.Close();
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
                int selectedIndex = table1.CurrentCell.RowIndex;
                //MessageBox.Show(table1.Rows[selectedIndex].Cells[0].Value.ToString());
                textBoxName.Text = table1.Rows[selectedIndex].Cells[1].Value.ToString();
                textBoxQuantity.Text = table1.Rows[selectedIndex].Cells[2].Value.ToString();
                textBoxPrice.Text = table1.Rows[selectedIndex].Cells[3].Value.ToString();
                textBoxStatus.Text = table1.Rows[selectedIndex].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }










    }
}
