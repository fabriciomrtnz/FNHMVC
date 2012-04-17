using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FNHMVC.Data.Infrastructure
{
    public class PagedResult<T>
    {
        public IList<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }       
    }
}
