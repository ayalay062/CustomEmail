using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class FilterTable
    {
        public FilterTable()
        {
            FilterFields = new HashSet<FilterFields>();
        }

        public int Id { get; set; }
        public int? TableCode { get; set; }
        public int? ProjectCode { get; set; }

        public Projects ProjectCodeNavigation { get; set; }
        public TablesForProject TableCodeNavigation { get; set; }
        public ICollection<FilterFields> FilterFields { get; set; }
    }
}
