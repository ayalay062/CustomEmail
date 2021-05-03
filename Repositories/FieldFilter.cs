using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;

namespace Repositories
{
   public class FieldFilter
    {
        public string FieldName;
        public string FieldType;
        public string FilterType;
         
        //public string tableName;

        //public List<FilterFields> fieldList;

        //public string FieldName;

        //public string fieldType;

        //public string filterType;

        public string stringValue;

        public DateTime dateValue;

        public bool boolValue;
        public int intValue;

    }
}
