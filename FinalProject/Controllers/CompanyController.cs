using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Servieces;
using Newtonsoft.Json.Linq;
using System.Web;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]

    public class CompanyController : ControllerBase
    {
        ICompanyService companyService;
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        [HttpGet("aaa/{code}")]
        public bool IsExist(int code)
        {
            return companyService.IsExist(code);
        }
        [HttpGet("checkDetails/{name}/{password}")]
        public JObject checkDetails(string name, string password)
        {
            return companyService.checkDetails(name, password);

        }
        [HttpPost("Add")]
        public int Add([FromBody] Companies company)
        {
            return companyService.Add(company);
        }
        [HttpGet("readDB/{serverName}/{projectName}")]
        public List<JObject> readDB(string serverName, string projectName)
        {
            return companyService.readDB(serverName, projectName);
        }
        [HttpPost("uploadImage")]
        //public bool uploadImage([FromForm] Object f)
        public JObject uploadImage([FromBody] string imageUrl)
        {
            string a = "{'path':" + imageUrl + "}"; 
        string projectname = "{ 'path':'" + imageUrl+ "'}";
            //JObject j= JObject.Parse(projectname);
            //var b= System.Web.HttpContext.Current.Request.Params["filesToDelete"];
            string message = "{ 'url':'" + imageUrl + "'}";
            return JObject.Parse(message);
        }
    }
}