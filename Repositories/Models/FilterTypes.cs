using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class FilterTypes
    {
        public FilterTypes()
        {
            FilterFields = new HashSet<FilterFields>();
        }

        public int FilterId { get; set; }
        public int? FilterCode { get; set; }
        public string Description { get; set; }

        public FieldTypes FilterCodeNavigation { get; set; }
        public ICollection<FilterFields> FilterFields { get; set; }
    }
}
