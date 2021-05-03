using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Repositories.Models;

namespace Servieces
{
   public interface ICompanyService
    {
        bool IsExist(int code);
        int Add(Companies company);
        JObject checkDetails(string name, string password);
        List<JObject> readDB(string serverName, string projectName);
       
        
    }
}
