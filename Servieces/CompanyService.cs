using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Models;

namespace Servieces
{
  public  class CompanyService:ICompanyService
    {
        ICompanyRepository companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }
        public int Add(Companies company)
        {
            return companyRepository.Add(company);
        }

        public bool IsExist(int code)
        {
            return companyRepository.IsExist(code);
        }
       public JObject checkDetails(string name, string password)
        {
            return companyRepository.checkDetails(name, password);
        }
        public List<JObject> readDB(string serverName, string projectName)
        {
            return companyRepository.readDB(serverName, projectName);
        }
    }
}
