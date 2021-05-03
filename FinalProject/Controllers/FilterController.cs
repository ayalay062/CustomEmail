using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Models;
using Servieces;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class FilterController : ControllerBase
    {
        IFilterService filterService;
        public FilterController(IFilterService filterService)
        {
            this.filterService = filterService;
        }
        [HttpGet("projectName/{companyCode}")]
        [EnableCors("CorsPolicy")]
        public List<JObject> projectName(int companyCode)
        {
            return filterService.projectName(companyCode);
        }
        [HttpGet("tablesName/{code}/{projectName}")]
        [EnableCors("CorsPolicy")]
        public Project tablesName(int code, string projectName)
        {
            return filterService.tablesName(code, projectName);
        }
        [HttpGet("getColumnName/{projectCode}/{tableName}")]
        [EnableCors("CorsPolicy")]
        public List<string> getColumnName(int projectCode, string tableName)
        {
            return filterService.getColumnName(projectCode, tableName);
        }
        
//[HttpPost("saveFilter")]
//        [EnableCors("CorsPolicy")]
//        public bool saveFilter([FromBody] dynamic project)
//        {
//            var code = project.code.Value;
//            int projectCode = int.Parse(code);
//            string tableName = project.tableNameFilter;
//            List<string> columns = new List<string>();
//            for (int i = 0; i < project.columnsForSave.Count; i++)
//            {
//                columns.Add(project.columnsForSave[i].Name.Value);
//            }
//            return filterService.saveFilter(projectCode, tableName, columns);
//        }

        [HttpGet("getFilterTypes/{projectCode}/{tableName}/{column}")]
        [EnableCors("CorsPolicy")]
        public FilterType getFilterTypes(int projectCode, string tableName, string column)
        {
            return filterService.getFilterTypes(projectCode, tableName, column);
        }

        [HttpPost("filterToSend")]
        [EnableCors("CorsPolicy")]
        
           public List<string> getNames([FromBody] dynamic filterToSend)
        {
            //int a = 0;
            //for (int i = 0; i < filterToSend.Count; i++)
            //{
            //     a = filterToSend[i].id;
            //}
            //int b = a;
            //return 1;
            return filterService.getNames(filterToSend);

        }
        [HttpPost("saveFilter")]
        [EnableCors("CorsPolicy")]
        public int saveFilter([FromBody] dynamic filter)
        {
          
            return filterService.saveFilter(filter);

        }
        [HttpPost("saveField")]
        [EnableCors("CorsPolicy")]
        public int saveField([FromBody] dynamic filter)
        {

            return 3;

        }
[HttpDelete("deleteFilter/{filterId}")]
[EnableCors("CorsPolicy")]
public bool deleteFilter(int filterId)
        {
            return filterService.deleteFilter(filterId);
        }
        [HttpGet("getFilter/{projectCode}")]
        [EnableCors("CorsPolicy")]
        public List<FilterTables> getFilter(int projectCode)
        {
            return filterService.getFilter(projectCode);
        }


    }
}
//FieldFilter Filter = new FieldFilter();
//Filter.tableName = filter.TableName;
//            Filter.fieldList = new List<FilterFields>();
//            FilterFields FilterField = new FilterFields();
//FilterField.fFie = filter.FilterList[0].FieldName;
//            Filter.fieldType = filter.FilterList[0].FieldType;
//            Filter.filterType = filter.FilterList[0].FilterType;
//            switch (Filter.fieldType)
//            {
//                case "varchar":
//                case "nvarchar":
//                    Filter.stringValue = filter.FilterList[0].Filter;
//                    break;
//                case "date":
//                case "datetime":
//                    Filter.dateValue = filter.FilterList[0].Filter;
//                    break;
//                case "int":
//                case "decimal":
//                case "float":
//                case "money":
//                    Filter.intValue = filter.FilterList[0].Filter;
//                    break;
//                case "bit":
//                    Filter.boolValue = filter.FilterList[0].Filter;
//                    break;
//                    //default:
//                    // Console.WriteLine("Default case");
//                    // break;
//            }