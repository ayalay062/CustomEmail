using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Projects = new HashSet<Projects>();
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public string Logo { get; set; }
        public string ServerName { get; set; }

        public ICollection<Projects> Projects { get; set; }
    }
}
