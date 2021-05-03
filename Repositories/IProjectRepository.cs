using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json.Linq;

namespace Repositories
{
   public interface IProjectRepository
    {
        List<JObject> projectName(int code);
        Project tablesName(int code, string projectName);
        List<string> getColum(int projectCode, string tableName);
        //Boolean saveColum(int projectCode, string columName);
        Boolean saveSelectedFilter(int projectCode, string tableName, List<string> columns);
        bool end(int projectCode, string columName, string tableOfMailAddress, string emailColumn);
       
    }
}
