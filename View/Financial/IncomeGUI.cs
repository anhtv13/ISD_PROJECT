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
using ISD_Project.Model.Financial;
using ISD_Project.Controller.Person;
using ISD_Final_Project;
using ISD_Project.Controller.Property;

namespace ISD_Project.View.Person
{
    public partial class IncomeGUI : Form
    {
        private String customerid;
        private double total;
        private String datetime;
        private Income inc;
        private IncomeManager incMan;
        private Product Pro;
        private ProductManager ProMan;


        string cs = @"server=localhost;
                    userid=root;
                    password=root;
                    database=isd_project";

        MySqlConnection conn;


        public IncomeGUI()
        {
            InitializeComponent();
        }

        

        private void Income_Load(object sender, EventArgs e)
        {
            comboGetValue();
        }


        public void comboGetValue()
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            MyDA.SelectCommand = new MySqlCommand("SELECT * FROM product order by name ", conn);
            DataTable tb = new DataTable();
            MyDA.Fill(tb);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = tb;
            comboBox2.DataSource = bSource;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "name";
            conn.Close();




            conn = new MySqlConnection(cs);
            conn.Open();
            MyDA = new MySqlDataAdapter();
            MyDA.SelectCommand = new MySqlCommand("SELECT * FROM customer order by name", conn);
            tb = new DataTable();
            MyDA.Fill(tb);
            bSource = new BindingSource();
            bSource.DataSource = tb;
            
            comboBox1.DataSource = bSource;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "name";    
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
            saveBill();//save bill to database
            
            //update product quantity after order
            int index = dataGridView1.Rows.Count;
            for (int i = 0; i < index - 1; i++)
            {
                String name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                double quantity = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                updateProductQuantity(name, quantity);
                //MessageBox.Show(name+" "+ quantity);
            }

            //update customer point
            updateCustomerPoint();
            //print log file income.txt
            printLog();
            //print bill bill.doc
            printBill();
            MessageBox.Show("Pay and bill successfully.");
        }


        public void updateProductQuantity(String name, double quantity)
        {
            try
            {
                ProMan = new ProductManager();
                ProMan.updateProductQuantity(name, quantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void updateCustomerPoint()
        {
            try
            {
                CustomerManager cusMan = new CustomerManager();
                int convertId = int.Parse(label2.Text);
                if (convertId > 0)
                {
                    cusMan.updateCustomerPoint(label2.Text, int.Parse(label1.Text));
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
               
                customerid = label2.Text;
                

                total = double.Parse(label1.Text);
                DateTime now = DateTime.Now;
                datetime = now.ToString("yyyy-MM-dd HH':'mm':'ss");

                inc = new Income(customerid, total, datetime);
                incMan = new IncomeManager();
                incMan.saveIncome(inc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void printLog()
        {
            try
            {
                //write text file
                //MessageBox.Show(cus.toString());
                DateTime now = DateTime.Now;
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"income.txt", true);
                file.WriteLine(inc.toString());
                file.Close();
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


                System.IO.StreamWriter file = new System.IO.StreamWriter(@"bill.doc");

                file.WriteLine(String.Format("{0,30}", title));
                file.WriteLine(String.Format("{0,25}", address));
                file.WriteLine(String.Format("{0,30}", phone));
                file.WriteLine(divide);

                //write customer
                file.WriteLine("Customer: " + comboBox1.SelectedValue.ToString());





                file.WriteLine("Sale Person: " + saleperson);
                file.WriteLine("Order date: " + now);
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




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String a = comboBox2.SelectedValue.ToString();
                String b = textBox2.Text;
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
                        cmd.CommandText = "SELECT * from product where name = @name ";
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

        

     
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * from customer where name = @name ";
            cmd.Parameters.AddWithValue("@name", comboBox1.SelectedValue.ToString());

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                label2.Text = reader.GetString("id");
            }
            conn.Close();
        }
















    }
}
