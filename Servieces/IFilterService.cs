using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Models;

namespace Servieces
{
  public  interface IFilterService
    {
        List<JObject> projectName(int companyCode);
        Project tablesName(int code, string projectName);
        List<string> getColumnName(int projectCode, string tableName);
        //Boolean saveFilter(int projectCode, string tableName, List<string> columns);
        FilterType getFilterTypes(int projectCode, string tableName, string column);
        int saveFilter(dynamic filter);
        List<string> getNames(dynamic filterToSend);
        List<FilterTables> getFilter(int projectCode);
        bool deleteFilter(int filterId);
       
    }
}
