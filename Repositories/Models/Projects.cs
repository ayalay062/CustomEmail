using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Projects
    {
        public Projects()
        {
            FilterTable = new HashSet<FilterTable>();
            TablesForProject = new HashSet<TablesForProject>();
        }

        public int ProjectCode { get; set; }
        public int? CompanyCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string TableNameWithTheEmailField { get; set; }
        public string FieldNameWithTheEmailAddress { get; set; }
        public string Name { get; set; }

        public Companies CompanyCodeNavigation { get; set; }
        public ICollection<FilterTable> FilterTable { get; set; }
        public ICollection<TablesForProject> TablesForProject { get; set; }
    }
}
