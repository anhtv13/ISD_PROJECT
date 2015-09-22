using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISD_Project.View;
using ISD_Project.View.Person;
using ISD_Project.View.Property;
using ISD_Project.View.Financial;
using ISD_Project.View.Report;

namespace ISD_Project.View
{
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword changepassword = new ChangePassword();
            changepassword.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffGUI stGUI = new StaffGUI();
            stGUI.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerGUI cGUI = new CustomerGUI();
            cGUI.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SupplierGUI suGUI = new SupplierGUI();
            suGUI.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"person.txt");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            IncomeGUI inc = new IncomeGUI();
            inc.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"income.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InfrastructureGUI infGUI = new InfrastructureGUI();
            infGUI.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IngredientGUI indGUI = new IngredientGUI();
            indGUI.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductGUI menGUI = new ProductGUI();
            menGUI.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"property.txt");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IngredientPayGUI IngPayGUI = new IngredientPayGUI();
            IngPayGUI.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SalaryGUI salGUI = new SalaryGUI();
            salGUI.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OtherFee othFee = new OtherFee();
            othFee.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ReportStaff repStaff = new ReportStaff();
            repStaff.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Chart chart = new Chart();
            chart.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ReportRevenue repRevenue = new ReportRevenue();
            repRevenue.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ReportCustomer repCustomer = new ReportCustomer();
            repCustomer.Show();
        }

        
    }
}
