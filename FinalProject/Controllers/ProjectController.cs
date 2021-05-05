using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.SqlServer.Management.Smo;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repositories;
using Servieces;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]

    public class ProjectController : ControllerBase
    {
        IProjectService projectService;
        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        [HttpGet("projectName/{code}")]
        [EnableCors("CorsPolicy")]
        public List<JObject> projectName(int code)
        {
            return projectService.projectName(code);
        }
        [HttpGet("tablesName/{code}/{projectName}")]
        [EnableCors("CorsPolicy")]
        public Project tablesName(int code, string projectName)
        {
            return projectService.tablesName(code, projectName);
        }
        [HttpGet("getColum/{projectCode}/{tableName}")]
        [EnableCors("CorsPolicy")]
        public List<string> getColum(int projectCode, string tableName)
        {
            return projectService.getColum(projectCode, tableName);
        }
        //[HttpGet("saveColum/{projectCode}/{columName}")]
        //[EnableCors("CorsPolicy")]
        //public Boolean saveColum(int projectCode, string columName)
        //{
        //    return projectService.saveColum(projectCode, columName);
        //}
        //[HttpPost("saveSelectedFilter/{projectCode}/{nameTableOfFilter}")]
        [HttpPost("saveSelectedFilter")]
        [EnableCors("CorsPolicy")]
        public bool saveSelectedFilter([FromBody] dynamic project)
        {
            //var projectCode = project.code.Value;
            int projectCode = int.Parse(project.code.Value);
            string tableName = project.tableNameFilter;
            List<string> columns = new List<string>();
            for (int i = 0; i < project.columnsForSave.Count; i++)
            {
               
                columns.Add(project.columnsForSave[i].Name.Value);
            }
            return projectService.saveSelectedFilter(projectCode, tableName, columns);
        }
        [HttpGet("end/{projectCode}/{columName}/{tableOfMailAddresss}/{emailColumn}")]
        [EnableCors("CorsPolicy")]
        public bool end(int projectCode,string columName, string tableOfMailAddresss,string emailColumn)
        {
            return projectService.end(projectCode, columName, tableOfMailAddresss, emailColumn);
        }
       
        
    }
}
