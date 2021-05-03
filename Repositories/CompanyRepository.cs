using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
//using Microsoft.TeamFoundation.Framework.Client.Catalog.Objects;
using Newtonsoft.Json.Linq;
using Repositories.Models;
using System.Web;

namespace Repositories
{
   public class CompanyRepository: ICompanyRepository
    {       
        public int Add(Companies company)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                var IsExist = context.Companies.Any(c => c.Name == company.Name && c.Code == company.Code);
                if (!IsExist)
                {
                    var result = context.Companies.Add(company);
                    context.SaveChanges();
                    return result.Entity.Code;
                }
                return 0;
            }
        }
        public bool IsExist(int code)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                var result = context.Companies.Where(compay => compay.Code == code).FirstOrDefault();
                if (result != null)
                    return true;
                return false;
            }
        }
        public List<JObject> readDB(string serverName, string projectName)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {                
                ServerConnection connection = new ServerConnection();                
                Console.WriteLine(serverName.GetType());
                Server server = new Server(serverName);
                //DESKTOP - F2Q5BP6
                server = new Server(connection);
                Database myAdventureWorks = server.Databases[projectName];
               // DataTable dtlSQLServers = SmoApplication.EnumAvailableSqlServers(false);
                List<JObject> tablelistjo = new List<JObject>();
                foreach (Table myTable in myAdventureWorks.Tables)
                {
                    string tableName = "{ 'table':'" + myTable.Name + "'}";
                    tablelistjo.Add(JObject.Parse(tableName));
                }
                return tablelistjo.ToList();
            }
        }
        public JObject checkDetails(string name, string password)
        {
            
            string message;
            using (Models.FinalProject context = new Models.FinalProject())
            {
               Companies c = context.Companies.Where(compay => compay.Name == name).FirstOrDefault();
                var companyCode = c.Code;
                if (c != null)
                {
                    if (c.Password == password)
                    {
                        message = "the password is corentlly";
                        companyCode = c.Code;
                    }
                    else
                        message = "not corentlly";
                }
                else
                {
                    message = "worng try again";
                }
                var x = new { text = message, code = companyCode };
                string a = x.text;
                var b = x.code;
                return JObject.FromObject(x);
            }
                
          
        }
    }
}

