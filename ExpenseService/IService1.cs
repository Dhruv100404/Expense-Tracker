using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ExpenseService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void AddExpense(Expense expense);

        [OperationContract]
        List<Expense> GetExpenses();

        //[OperationContract]
        //Expense GetExpense(int ExpenseId);

        [OperationContract]
        void UpdateExpense(Expense expense);

        [OperationContract]
        void DeleteExpense(int ExpenseId);

        [OperationContract]
        DataSet GetCategories();
    
    }
}
