using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ISD_Project.Controller.Financial;

namespace ISD_Project.View.Financial
{
    public partial class IngredientPayGUI : Form
    {
        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;

        
        private double salary;
        private double ingredientfee;
        private int month;
        private String datetime;
        private OutcomeManager OutMan;


        public IngredientPayGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String a = comboBox1.SelectedValue.ToString();
                String b = textBox1.Text;
                int number;
                if (int.TryParse(b, out number) == true)
                {
                    int b1 = int.Parse(b);
                    if (b1 > 0)
                    {
                        double price = 0;
                        double money = 0;
                        double total = 0;
                        conn.Open();
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT * from ingredient where name = @name ";
                        cmd.Parameters.AddWithValue("@name", a);

                        //display name of product, quantity, money (price of product * quantity)
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            price = double.Parse(reader.GetString("price"));
                        }
                        money = int.Parse(b) * price;
                        dataGridView1.Rows.Add(a, b, price, money.ToString());

                        conn.Close();


                        int index = dataGridView1.Rows.Count;

                        for (int i = 0; i < index - 1; i++)
                        {
                            total = total + double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        }
                        label1.Text = total.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Quantity must be >0.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid quantity.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void IngredienPayGUI_Load(object sender, EventArgs e)
        {
            comboGetValue();
        }

        public void comboGetValue()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            MyDA.SelectCommand = new MySqlCommand("SELECT * FROM ingredient order by name ", conn);
            DataTable tb = new DataTable();
            MyDA.Fill(tb);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = tb;
            comboBox1.DataSource = bSource;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "name";
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveBill();
            printBill();
            MessageBox.Show("Save and print bill successfully.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int selectindex = dataGridView1.CurrentCell.RowIndex;
                if (selectindex >= 0)
                {
                    var row = dataGridView1.Rows[selectindex];
                    dataGridView1.Rows.Remove(row);


                    //calculate and print the total of the bill
                    double total = 0;
                    int index = dataGridView1.Rows.Count;
                    for (int i = 0; i < index - 1; i++)
                    {
                        total = total + double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    }
                    label1.Text = total.ToString();
                }
                else
                {
                    MessageBox.Show("Select an item to remove.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void saveBill()
        {
            try
            {

                ingredientfee = double.Parse(label1.Text);            
                DateTime now = DateTime.Now;
                month = now.Month;

                OutMan = new OutcomeManager();
                OutMan.saveIngredientFee(ingredientfee, month);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void printBill()
        {
            try
            {
                String title = "Now cafe";
                String address = "Số 83 - Tô Vĩnh Diện - quận Thanh Xuân - Hà Nội";
                String phone = "0918592616";
                String divide = "-----------------------------------------------------------------";
                String header = String.Format("{0,-25}{1,12}{2,13}{3,14}\n",
                                        "Name", "Quantity", "Price", "Money");
                String saleperson = "Tran Viet Anh";
                DateTime now = DateTime.Now;


                System.IO.StreamWriter file = new System.IO.StreamWriter(@"ingredientfee.doc");

                file.WriteLine(String.Format("{0,30}", title));
                file.WriteLine(String.Format("{0,25}", address));
                file.WriteLine(String.Format("{0,30}", phone));
                file.WriteLine(divide);

                //write customer
                





                file.WriteLine("Owner: " + saleperson);
                file.WriteLine("Ingredient Buying date: " + now);
                file.WriteLine(divide);
                file.WriteLine(header);
                int index = dataGridView1.Rows.Count;

                for (int i = 0; i < index - 1; i++)
                {
                    String content = String.Format("{0,-25}{1,12}{2,13}{3,14}",
                                    dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[2].Value.ToString(), dataGridView1.Rows[i].Cells[3].Value.ToString());
                    file.WriteLine(content);
                }
                file.WriteLine(divide);
                String payment = String.Format("{0,-25}{1,40}", "Total", label1.Text);
                file.WriteLine(payment);

                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
















    }
}
