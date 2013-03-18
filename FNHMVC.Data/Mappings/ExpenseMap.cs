using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using FNHMVC.Model;
using FluentNHibernate.Mapping;

namespace FNHMVC.Data.Mappings
{
    public class ExpenseMap : ClassMap<Expense>
    {
        public ExpenseMap()
        {
            Id(x => x.ExpenseId).GeneratedBy.Identity();
            Map(x => x.TransactionDesc).Length(100).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.Amount).Not.Nullable();
            References(x => x.Category).Not.Nullable();
        }
    }
}
