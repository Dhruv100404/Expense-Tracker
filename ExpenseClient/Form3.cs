using ExpenseClient.ServiceReference1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpenseClient
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Displaydata()
        {
            dataGridView1.Rows.Clear();
            ServiceReference1.Service1Client cl = new ServiceReference1.Service1Client();

            List<Expense> ds = cl.GetExpenses().ToList();
            foreach (var courierItem in ds)
            {
                dataGridView1.Rows.Add(
                    courierItem.ExpenseId,
                    courierItem.ExpenseName,
                    courierItem.Amount,
                    courierItem.ExpenseDate,
                    courierItem.Category
                );
            }

            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ServiceReference1.Service1Client cl = new ServiceReference1.Service1Client();
            if (e.ColumnIndex == 5)
            {
           
                Expense exp = new Expense();
                exp.ExpenseId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                exp.ExpenseName = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                exp.Amount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                exp.ExpenseDate = (DateTime)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                exp.Category = (string)dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                cl.UpdateExpense(exp);
                label1.Text = "Expenses Updated";
            }

            if(e.ColumnIndex == 6)
            {
                int expenseid = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                cl.DeleteExpense(expenseid);
                label1.Text = "Expenses Deleted";
                Displaydata();
                

            }

        }


        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text= "";
            ServiceReference1.Service1Client et = new ServiceReference1.Service1Client();
            DataSet ds = et.GetCategories();
            ArrayList arrayList = new ArrayList();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                arrayList.Add(dr["CategoryName"].ToString());
            }
            Category.Items.AddRange(arrayList.ToArray());
            Displaydata();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
