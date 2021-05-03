using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class TablesForProject
    {
        public TablesForProject()
        {
            FieldsForTables = new HashSet<FieldsForTables>();
            FilterTable = new HashSet<FilterTable>();
        }

        public int TableCode { get; set; }
        public int? ProjectCode { get; set; }
        public string TableName { get; set; }

        public Projects ProjectCodeNavigation { get; set; }
        public ICollection<FieldsForTables> FieldsForTables { get; set; }
        public ICollection<FilterTable> FilterTable { get; set; }
    }
}
