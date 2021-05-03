using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json.Linq;
using Repositories;

namespace Servieces
{
    public class ProjectService : IProjectService
    {
        IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public List<JObject> projectName(int code)
        {
            return projectRepository.projectName(code);
        }
        public Project tablesName(int code, string projectName)
        {
            return projectRepository.tablesName(code, projectName);
        }
        public List<string> getColum(int projectCode, string tableName)
        {
            return projectRepository.getColum(projectCode, tableName);
        }
        //public Boolean saveColum(int projectCode, string columName)
        //{
        //    return projectRepository.saveColum(projectCode, columName);
        //}
        public Boolean saveSelectedFilter(int projectCode, string tableName, List<string> columns)
        {
            return projectRepository.saveSelectedFilter(projectCode, tableName, columns);
        }
        public bool end(int projectCode, string columName, string tableOfMailAddress, string emailColumn)
        {
            return projectRepository.end(projectCode, columName, tableOfMailAddress, emailColumn);
        }
    }
}
