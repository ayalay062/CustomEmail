using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Repositories.Models;

namespace Repositories
{
   public interface ICompanyRepository
    {
        bool IsExist(int code);
        int Add(Companies company);
        JObject checkDetails(string name, string password);
        List<JObject> readDB(string serverName, string projectName);

    }
}
