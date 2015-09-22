using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ISD_Final_Project;
using ISD_Project.Controller.Property;

namespace ISD_Project.View.Property
{
    public partial class ProductGUI : Form
    {
        private String id;
        private String name;    
        private double quantity;
        private double price;
        private Product pro;
        private ProductManager proMan;

        private String date;

        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;


        public ProductGUI()
        {
            InitializeComponent();
        }

        private void ProductGUI_Load(object sender, EventArgs e)
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM product order by id ", conn);
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
                textBoxPrice.Text = table1.Rows[selectedIndex].Cells[2].Value.ToString();
                textBoxQuantity.Text = table1.Rows[selectedIndex].Cells[3].Value.ToString();
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
                if (MessageBox.Show("Are you sure to add a product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    quantity = double.Parse(textBoxQuantity.Text);
                    price = double.Parse(textBoxPrice.Text);
                    pro = new Product(name, quantity, price);

                    if (pro.repOK())
                    {
                        proMan = new ProductManager();
                        proMan.saveProduct(pro);

                        //write text file
                        //MessageBox.Show(cus.toString());
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Add Product{" + pro.toString() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add Product successfully.");

                    }
                    else
                    {
                        MessageBox.Show("Invalid Product.");
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
                if (MessageBox.Show("Are you sure to update a product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        name = textBoxName.Text;
                        quantity = double.Parse(textBoxQuantity.Text);
                        price = double.Parse(textBoxPrice.Text);
                        pro = new Product(name, quantity, price);
                        if (pro.repOK())
                        {
                            proMan = new ProductManager();
                            proMan.updateProduct(pro);

                            //write text file
                            DateTime now = DateTime.Now;
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                            file.WriteLine("Update Product{" + pro.toString() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update product successfully.");

                        }
                        else
                        {
                            MessageBox.Show("Invalid Product.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select a product to update.");
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
                if (MessageBox.Show("Are you sure to delete a product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        id = table1.Rows[selectedIndex].Cells[0].Value.ToString();

                        proMan = new ProductManager();
                        proMan.deleteProduct(id);

                        //write text file
                        pro = new Product();
                        DateTime now = DateTime.Now;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Delete Product{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete Product successfully.");


                    }
                    else
                    {
                        MessageBox.Show("Select a product to delete.");
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.product where name like '%" + pname + "%' order by id ", conn);

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
