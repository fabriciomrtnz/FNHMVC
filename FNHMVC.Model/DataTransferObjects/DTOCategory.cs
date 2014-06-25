using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace FNHMVC.Model
{
    [DataContract(IsReference=true)]
    [KnownType(typeof(DTOExpense))]
    public class DTOCategory
    {
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<DTOExpense> Expenses { get; set; }
    }
}