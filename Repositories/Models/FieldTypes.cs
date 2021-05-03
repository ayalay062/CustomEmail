using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class FieldTypes
    {
        public FieldTypes()
        {
            FieldsForTables = new HashSet<FieldsForTables>();
            FilterTypes = new HashSet<FilterTypes>();
        }

        public int FieldCode { get; set; }
        public string Description { get; set; }

        public ICollection<FieldsForTables> FieldsForTables { get; set; }
        public ICollection<FilterTypes> FilterTypes { get; set; }
    }
}
