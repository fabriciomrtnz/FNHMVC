using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using System.Collections.Generic;

namespace FNHMVC.Domain.Commands
{
    public class CreateOrUpdateExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
        public Category Category { get; set; }
        public string Transaction { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
