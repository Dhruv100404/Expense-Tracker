using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseClient
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            chart1.Titles.Add("Expense Summary");
            chart2.Titles.Add("Expense Summary");
        }

        private void Summary(DateTime date1,DateTime date2)
        {
            string connectionString = "Data Source=LAPTOP-UQ007ARG\\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SUM(Amount) AS Total, Category FROM Expense WHERE ExpenseDate BETWEEN @StartDate and @EndDate GROUP BY Category;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", date1);
                    command.Parameters.AddWithValue("@EndDate", date2);

                    chart2.Series["s2"].Points.Clear();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            int totalAmount = Convert.ToInt32(reader["Total"]);

                            chart2.Series["s2"].Points.AddXY(category, totalAmount);
                        }
                    }
                }
            }
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            dateTimePicker2.Value = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Today;
            chart1.Size = new Size(300, 300);
            chart1.Series["s1"].IsValueShownAsLabel = true;
            chart3.Hide();
            string connectionString = "Data Source=LAPTOP-UQ007ARG\\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SUM(Amount) AS Total, Category FROM Expense GROUP BY Category;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            int totalAmount = Convert.ToInt32(reader["Total"]);

                            chart1.Series["s1"].Points.AddXY(category, totalAmount);
                        }
                    }
                }
            }

            ServiceReference1.Service1Client et = new ServiceReference1.Service1Client();
            DataSet ds = et.GetCategories();
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "CategoryName";
            comboBox1.DisplayMember = "CategoryName";

            chart2.Size = new Size(300, 300);
            chart2.Series["s2"].IsValueShownAsLabel = true;

            Summary(dateTimePicker1.Value.AddDays(-1), dateTimePicker2.Value); 

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            
            Summary(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            
            chart3.Size = new Size(300, 300);
            chart3.Series["s3"].IsValueShownAsLabel = true;
            chart3.Show();
            string connectionString = "Data Source=LAPTOP-UQ007ARG\\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string cname = comboBox1.Text;
                string query = "SELECT SUM(Amount) AS Total, ExpenseName FROM Expense WHERE Category = @name GROUP BY ExpenseName;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", cname);
                    

                    chart3.Series["s3"].Points.Clear();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string expense = reader["ExpenseName"].ToString();
                            int totalAmount = Convert.ToInt32(reader["Total"]);

                            chart3.Series["s3"].Points.AddXY(expense, totalAmount);
                        }
                    }
                }
            }
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }
    }
}
