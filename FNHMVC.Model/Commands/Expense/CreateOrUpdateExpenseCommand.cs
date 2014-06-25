using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using System.Collections.Generic;

namespace FNHMVC.Model.Commands
{
    public class CreateOrUpdateExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
        public Category Category { get; set; }
        public string TransactionDesc { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
