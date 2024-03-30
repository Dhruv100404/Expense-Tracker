using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseService
{
    [DataContract]
    public class Expense
    {
        [DataMember]
        public int ExpenseId { get; set; }
        [DataMember]
        public string ExpenseName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime ExpenseDate { get; set; }

        [DataMember]
        public string Category { get; set; }
    }
}
