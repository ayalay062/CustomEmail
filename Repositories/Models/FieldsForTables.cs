using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class FieldsForTables
    {
        public FieldsForTables()
        {
            FilterFields = new HashSet<FilterFields>();
        }

        public int FieldCode { get; set; }
        public int? TableCode { get; set; }
        public string FieldName { get; set; }
        public int? FieldType { get; set; }

        public FieldTypes FieldTypeNavigation { get; set; }
        public TablesForProject TableCodeNavigation { get; set; }
        public ICollection<FilterFields> FilterFields { get; set; }
    }
}
