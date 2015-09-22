using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISD_Project.Model.Person;
using ISD_Project.Controller.Person;
using MySql.Data.MySqlClient;

namespace ISD_Project.View.Person
{
    public partial class StaffGUI : Form
    {
        private String name;

        private String address;
        private String email;
        private String phone;
        private String birthday;
        private int role;

        private Staff sta;
        private StaffManager staMan;

        
         string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
       



        public StaffGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to add a staff?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    address = textBoxAddress.Text;
                    email = textBoxEmail.Text;
                    phone = textBoxPhone.Text;
                    birthday = textBoxBirthday.Text;
                    role = int.Parse(comboBox1.SelectedItem.ToString());

                    sta = new Staff(name, address, email, phone, birthday, role);
                    if (sta.repOK())
                    {
                        //MessageBox.Show(sta.toString());
                        staMan = new StaffManager();
                        staMan.saveStaff(sta);

                        //write text file
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Add staff{" + sta.toStringInfo() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add staff successfully");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Staff.");
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
                if (MessageBox.Show("Are you sure to update a staff?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        role = int.Parse(comboBox1.SelectedItem.ToString());

                        sta = new Staff(id, name, address, email, phone, birthday, role);
                        //MessageBox.Show(sta.toStringInfo());
                        if (sta.repOK())
                        {
                            ////MessageBox.Show(sta.toString());
                            staMan = new StaffManager();
                            staMan.updateStaff(sta);

                            //write text file
                            DateTime now = DateTime.Now;
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                            file.WriteLine("Update staff{" + sta.toStringInfo() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update staff successfully.");

                        }
                        else                    
                            MessageBox.Show("Invalid Staff.");                     
                    }
                    else                    
                        MessageBox.Show("Select a staff to update.");                    
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
                if (MessageBox.Show("Are you sure to delete a staff?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        String id = table1.Rows[selectedIndex].Cells[0].Value.ToString();
                        //MessageBox.Show(id);

                        staMan = new StaffManager();
                        staMan.deleteStaff(id);


                        //write text file
                        sta = new Staff();
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Delete staff{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete staff successfully.");
                    }
                    else
                        MessageBox.Show("Select a staff to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void StaffGUI_Load(object sender, EventArgs e)
        {
            tableGetData();
        }


        public void tableGetData(){
            try
            {
                //table1
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM staff order by id", conn);
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
                textBoxAddress.Text = table1.Rows[selectedIndex].Cells[2].Value.ToString();
                textBoxEmail.Text = table1.Rows[selectedIndex].Cells[3].Value.ToString();
                textBoxPhone.Text = table1.Rows[selectedIndex].Cells[4].Value.ToString();

                DateTime dt = Convert.ToDateTime(table1.Rows[selectedIndex].Cells[5].Value.ToString());
                String dob = table1.Rows[selectedIndex].Cells[5].Value.ToString();
                textBoxBirthday.Text = dt.Year + "-" + dt.Month + "-" + dt.Day;
                comboBox1.SelectedItem = table1.Rows[selectedIndex].Cells[6].Value;
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
            comboBox1.SelectedValue="";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxAddress.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxBirthday.Text = "";
            comboBox1.SelectedItem = "";
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.staff where name like '%" + pname + "%' order by id ", conn);

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
