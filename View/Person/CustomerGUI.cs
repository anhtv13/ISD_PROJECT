using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ISD_Project.Model.Person;
using ISD_Project.Controller.Person;
using System.Globalization;

namespace ISD_Project.View.Person
{
    public partial class CustomerGUI : Form
    {
        

        private String name;
        
        private String address;
        private String email;
        private String phone;
        private String birthday;
        private String point;

        Customer cus;
        CustomerManager cusMan;


         string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        public CustomerGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to add a customer?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    address = textBoxAddress.Text;
                    email = textBoxEmail.Text;
                    phone = textBoxPhone.Text;
                    birthday = textBoxBirthday.Text;

                    cus = new Customer(name, address, email, phone, birthday, "0");
                    if (cus.repOK())
                    {
                        //MessageBox.Show(cus.toString());
                        cusMan = new CustomerManager();
                        cusMan.saveCustomer(cus);

                        //write text file
                        //MessageBox.Show(cus.toString());
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Add customer{" + cus.toStringInfo() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add customer successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Customer.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CustomerGUI_Load(object sender, EventArgs e)
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM customer order by id ", conn);
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

        private void button4_Click(object sender, EventArgs e)
        {
            tableGetData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxAddress.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxBirthday.Text = "";
        }

        private void table1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int selectedIndex = table1.CurrentCell.RowIndex;
                //MessageBox.Show(table1.Rows[selectedIndex].Cells[0].Value.ToString());
                textBoxName.Text = table1.Rows[selectedIndex].Cells[1].Value.ToString();
                textBoxAddress.Text = table1.Rows[selectedIndex].Cells[2].Value.ToString();
                textBoxEmail.Text = table1.Rows[selectedIndex].Cells[3].Value.ToString();
                textBoxPhone.Text = table1.Rows[selectedIndex].Cells[4].Value.ToString();

                DateTime dt = Convert.ToDateTime(table1.Rows[selectedIndex].Cells[5].Value.ToString());
                String dob = table1.Rows[selectedIndex].Cells[5].Value.ToString();
                textBoxBirthday.Text = dt.Year + "-" + dt.Month + "-" + dt.Day;
                labelPoint.Text =table1.Rows[selectedIndex].Cells[6].Value.ToString();
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
                if (MessageBox.Show("Are you sure to update a customer?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        String id = table1.Rows[selectedIndex].Cells[0].Value.ToString();
                        name = textBoxName.Text;
                        address = textBoxAddress.Text;
                        email = textBoxEmail.Text;
                        phone = textBoxPhone.Text;
                        birthday = textBoxBirthday.Text;
                        point = labelPoint.Text;

                        cus = new Customer(id, name, address, email, phone, birthday, point);
                        if (cus.repOK())
                        {
                            //MessageBox.Show("ID= " + cusID + "\n" + cus.toString());
                            cusMan = new CustomerManager();
                            cusMan.updateCustomer(cus);

                            //write text file
                            DateTime now = DateTime.Now;
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                            file.WriteLine("Update customer{" + cus.toStringInfo() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update customer successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Invalid Customer.");
                        }
                    }
                    else
                        MessageBox.Show("Select a customer to update.");
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
                if (MessageBox.Show("Are you sure to delete a customer?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        String id = table1.Rows[selectedIndex].Cells[0].Value.ToString();

                        cusMan = new CustomerManager();
                        cusMan.deleteCustomer(id);




                        //print log
                        cus = new Customer();
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Delete customer{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete Customer successfully.");
                    }

                    else
                        MessageBox.Show("Select a customer to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.customer where name like '%"+pname+"%' order by id ", conn);
              
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

       







    }
}
