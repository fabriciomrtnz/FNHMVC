using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace FNHMVC.Model
{

    public class Expense
    {
        public virtual int ExpenseId { get; set; }
        public virtual string TransactionDesc { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Amount { get; set; }
        public virtual Category Category { get; set; }
    }
}
