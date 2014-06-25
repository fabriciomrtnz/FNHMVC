using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace FNHMVC.Model
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(DTOCategory))]
    public class DTOExpense
    {
        [DataMember]
        public int ExpenseId { get; set; }
        [DataMember]
        public string TransactionDesc { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public DTOCategory Category { get; set; }
    }
}
