using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ExpenseService
{
    
    public class Service1 : IService1
    {
        List<Expense> IService1.GetExpenses()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-UQ007ARG\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {   
                conn.Open();
                List<Expense> list = new List<Expense>();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Expense", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Expense {
                            ExpenseId = reader.GetInt32(0),
                            ExpenseName = reader.GetString(1),
                            Amount =  reader.GetDecimal(2),
                            ExpenseDate = reader.GetDateTime(3),
                            Category = reader.GetString(4),
                    
                    });

                }
            }
                

                return list;
            }
        }
        public void AddExpense(Expense expense)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-UQ007ARG\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Expense (ExpenseName, Amount, ExpenseDate, Category) VALUES (@ExpenseName, @Amount, @ExpenseDate, @Category)", conn);
                cmd.Parameters.AddWithValue("@ExpenseName", expense.ExpenseName);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@ExpenseDate", expense.ExpenseDate);
                cmd.Parameters.AddWithValue("@Category", expense.Category);
                cmd.ExecuteNonQuery();
            }

        }
    
        public void UpdateExpense(Expense expense)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-UQ007ARG\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Expense SET ExpenseName = @ExpenseName, Amount = @Amount, ExpenseDate = @ExpenseDate, Category = @Category WHERE ExpenseId = @ExpenseId", conn);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@ExpenseDate", expense.ExpenseDate);
                cmd.Parameters.AddWithValue("@ExpenseName", expense.ExpenseName);
                cmd.Parameters.AddWithValue("@Category", expense.Category);
                cmd.Parameters.AddWithValue("@ExpenseId", expense.ExpenseId);
                cmd.ExecuteNonQuery();
            }

        }

        public void DeleteExpense(int expenseId)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-UQ007ARG\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Expense WHERE ExpenseId = @ExpenseId", conn);
                cmd.Parameters.AddWithValue("@ExpenseId", expenseId);
                cmd.ExecuteNonQuery();
            }

        }

        DataSet IService1.GetCategories()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category", @"Data Source=LAPTOP-UQ007ARG\SQLEXPRESS01;Initial Catalog=ExpenseTracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
