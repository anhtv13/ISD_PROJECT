using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISD_Project.Controller.Person;
using ISD_Project.Model.Person;
using MySql.Data.MySqlClient;

namespace ISD_Project.View.Person
{
    public partial class SupplierGUI : Form
    {

        private String name;

        private String address;
        private String email;
        private String phone;
        private String birthday;
        private String ingredient_id;

        private Supplier sup;
        private SupplierManager supMan;


        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;



        public SupplierGUI()
        {
            InitializeComponent();
        }

        private void SupplierGUI_Load(object sender, EventArgs e)
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM supplier order by id ", conn);
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



        

       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to add a supplier?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    address = textBoxAddress.Text;
                    email = textBoxEmail.Text;
                    phone = textBoxPhone.Text;
                    birthday = textBoxBirthday.Text;
                    ingredient_id = textBoxProductId.Text;

                    sup = new Supplier(name, address, email, phone, birthday, ingredient_id);
                    if (sup.repOK())
                    {
                        //MessageBox.Show(sup.toString());
                        supMan = new SupplierManager();
                        supMan.saveSupplier(sup);

                        //write text file
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Add supplier{" + sup.toStringInfo() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add supplier successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Supplier.");
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
                if (MessageBox.Show("Are you sure to update a supplier?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        ingredient_id = textBoxProductId.Text;

                        sup = new Supplier(id, name, address, email, phone, birthday, ingredient_id);
                        //MessageBox.Show(sta.toStringInfo());
                        if (sup.repOK())
                        {
                            ////MessageBox.Show(sta.toString());
                            supMan = new SupplierManager();
                            supMan.updateSupplier(sup);

                            //write text file
                            DateTime now = DateTime.Now;
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                            file.WriteLine("Update supplier{" + sup.toStringInfo() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update supplier successfully.");

                        }
                        else
                        {
                            MessageBox.Show("Invalid Supplier.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select a supplier to update.");
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
                if (MessageBox.Show("Are you sure to delete a supplier?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        String id = table1.Rows[selectedIndex].Cells[0].Value.ToString();
                        //MessageBox.Show(id);

                        supMan = new SupplierManager();
                        supMan.deleteSupplier(id);


                        //write text file
                        sup = new Supplier();
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"person.txt", true);
                        file.WriteLine("Delete supplier{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete supplier successfully.");
                    }
                    else
                        MessageBox.Show("Select a supplier to delete.");
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
            textBoxAddress.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxBirthday.Text = "";
            textBoxProductId.Text = "";
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
                textBoxProductId.Text = table1.Rows[selectedIndex].Cells[6].Value.ToString();
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.supplier where name like '%" + pname + "%' order by id ", conn);

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
