using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class FilterFields
    {
        public int Id { get; set; }
        public int? FilterTypesCode { get; set; }
        public int? FilterCode { get; set; }
        public int? FieldCode { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int? IntValue { get; set; }
        public bool? BoolValue { get; set; }

        public FieldsForTables FieldCodeNavigation { get; set; }
        public FilterTable FilterCodeNavigation { get; set; }
        public FilterTypes FilterTypesCodeNavigation { get; set; }
    }
}
