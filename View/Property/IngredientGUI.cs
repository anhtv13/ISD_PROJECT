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
    public partial class IngredientGUI : Form
    {
        private String id;
        private String name;
        private double quantity;
        private double price;
        private String status;
        private String date;

        private Ingredient Ing;
        private IngredientManager IngMan;

        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;
        MySqlCommand cmd;

        public IngredientGUI()
        {
            InitializeComponent();
        }

        private void IngredientGUI_Load(object sender, EventArgs e)
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM ingredient order by id ", conn);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to add an ingredient?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    name = textBoxName.Text;
                    quantity = double.Parse(textBoxQuantity.Text);
                    price = double.Parse(textBoxPrice.Text);
                    status = textBoxStatus.Text;
                    Ing = new Ingredient(name, quantity, price, status);

                    if (Ing.repOK() && Ing.validatePrice(price))
                    {
                        IngMan = new IngredientManager();
                        IngMan.saveIngredient(Ing);

                        //write text file
                        //MessageBox.Show(cus.toString());
                        DateTime now = DateTime.Now;
                        date = now.ToString("yyyy-MM-dd");
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Add Ingredient{" + Ing.toString() + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Add Ingredient successfully.");

                    }
                    else
                    {
                        MessageBox.Show("Invalid Ingredient.");
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
                if (MessageBox.Show("Are you sure to update an ingredient?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        id = table1.Rows[selectedIndex].Cells[0].Value.ToString();
                        name = textBoxName.Text;
                        quantity = double.Parse(textBoxQuantity.Text);
                        price = double.Parse(textBoxPrice.Text);
                        status = textBoxStatus.Text;
                        Ing = new Ingredient(id, name, quantity, price, status);
                        if (Ing.repOK() && Ing.validatePrice(price))
                        {
                            IngMan = new IngredientManager();
                            IngMan.updateIngredient(Ing);

                            //write text file
                            DateTime now = DateTime.Now;
                            date = now.ToString("yyyy-MM-dd");
                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                            file.WriteLine("Update Ingredient{" + Ing.toString() + "}" + "    " + now);
                            file.Close();
                            tableGetData();
                            MessageBox.Show("Update Ingredient successfully.");

                        }
                        else
                        {
                            MessageBox.Show("Invalid Ingredient.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select an ingredient to update.");
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
                if (MessageBox.Show("Are you sure to add an ingredient?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = table1.CurrentCell.RowIndex;
                    if (selectedIndex >= 0)
                    {
                        id = table1.Rows[selectedIndex].Cells[0].Value.ToString();

                        IngMan = new IngredientManager();
                        IngMan.deleteIngredient(id);

                        //write text file
                        Ing = new Ingredient();
                        DateTime now = DateTime.Now;
                        date = now.ToString("yyyy-MM-dd");
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"property.txt", true);
                        file.WriteLine("Delete Ingredient{" + id + "}" + "    " + now);
                        file.Close();
                        tableGetData();
                        MessageBox.Show("Delete Ingredient successfully.");


                    }
                    else
                    {
                        MessageBox.Show("Select an ingredient to delete.");
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
            textBoxStatus.Text = "";
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
                MyDA.SelectCommand = new MySqlCommand("SELECT * FROM isd_project.Ingredient where name like '%" + pname + "%' order by id ", conn);

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
