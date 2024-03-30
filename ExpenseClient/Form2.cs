using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseClient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today;
            ServiceReference1.Service1Client et = new ServiceReference1.Service1Client();
            DataSet ds = et.GetCategories();
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "CategoryName";
            comboBox1.DisplayMember = "CategoryName";
            label6.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            string name = textBox1.Text;
            decimal amount =  decimal.Parse(textBox2.Text);
            DateTime date = dateTimePicker1.Value;
            string Category = comboBox1.Text;

            ServiceReference1.Expense exp = new ServiceReference1.Expense();
            exp.ExpenseName = name;
            exp.Amount = amount;
            exp.ExpenseDate = date;
            exp.Category = Category;
            client.AddExpense(exp);
            label6.Text = "Expense Added";

            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.SelectedIndex = -1; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
