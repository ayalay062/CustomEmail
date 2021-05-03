using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json.Linq;
using Repositories.Models;

namespace Repositories
{
    public class ProjectRepository:IProjectRepository
    {
        public List<JObject> projectName(int code)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Companies c = context.Companies.Where(compay => compay.Code == code).FirstOrDefault();
               
                Server server = new Server(c.ServerName);
                ServerConnection connection = new ServerConnection();
                server = new Server(connection);
                List<JObject> dbProjectsName = new List<JObject>();
                foreach (Database myDatabase in server.Databases)
                {
                    string projectname = "{ 'projectname':'" + myDatabase.Name + "'}";
                    dbProjectsName.Add(JObject.Parse(projectname));
                }
                return dbProjectsName.ToList();
            }
        }
        public Project tablesName(int code, string projectName)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Companies c = context.Companies.Where(company => company.Code == code).FirstOrDefault();
                Server server = new Server(c.ServerName);
                Projects p = context.Projects.Where(proj => proj.ProjectName == projectName && proj.CompanyCode == code).FirstOrDefault();
                if (p == null)
                {
                    p = new Projects();
                    p.CompanyCode = c.Code;
                    p.ProjectName = projectName;
                    p.DateCreated = DateTime.Now;
                    context.Projects.Add(p);
                    context.SaveChanges();
                }
                //DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                //DataTable t = SmoApplication.EnumAvailableSqlServers(false);
                //DataTable dtlSQLServers = SmoApplication.EnumAvailableSqlServers(false);
                ServerConnection connection = new ServerConnection();
                server = new Server(connection);
                Database myAdventureWorks = server.Databases[projectName];
                Project project = new Project();
                project.TableList = new List<string>();
                foreach (Table myTable in myAdventureWorks.Tables)
                {
                    project.TableList.Add(myTable.Name);
                }
                //string codeProject = "{ 'code':'" + p.ProjectCode + "'}";
                //return tablelistjo.ToList();
                project.ProjectCode = p.ProjectCode;
                return project;

            }
        }
        public List<string> getColum(int projectCode, string tableName)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Projects p = context.Projects.Where(project => project.ProjectCode == projectCode).FirstOrDefault();
                //p.TableNameWithTheEmailField = tableName;
                //context.SaveChanges();
                int? companyCode = p.CompanyCode;
                Companies c = context.Companies.Where(compay => compay.Code == companyCode).FirstOrDefault();
                Server server = new Server(c.ServerName);
                ServerConnection connection = new ServerConnection();
                server = new Server(connection);
                Database myAdventureWorks = server.Databases[p.ProjectName];
                List<string> columName = new List<string>();
                foreach (Table myTable in myAdventureWorks.Tables)
                {
                    if (myTable.Name == tableName)
                    {
                        foreach (Column col in myTable.Columns)
                        {
                            columName.Add(col.Name);
                        }
                    }
                }
                return columName;
            }
        }
        //public Boolean saveSelectedFilter(int projectCode, string tableName, List<string> columns)
        //{
        //    using (Models.FinalProject context = new Models.FinalProject())
        //    {
        //        TablesForProject table = context.TablesForProject.Where(tbl => tbl.ProjectCode == projectCode && tbl.TableName == tableName).FirstOrDefault();
        //        if (table == null)
        //        {
        //            table = new TablesForProject();
        //            table.ProjectCode = projectCode;
        //            table.TableName = tableName;
        //            context.TablesForProject.Add(table);
        //        }
        //        var fieldsList = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode).ToList();
        //        foreach (var field in columns)
        //        {
        //            FieldsForTables f = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode && fi.FieldName == field).FirstOrDefault();
        //            if (f == null)
        //            {
        //                f = new FieldsForTables();
        //                f.FieldName = field;
        //                f.TableCode = table.TableCode;
        //                context.FieldsForTables.Add(f);
        //            }
        //            else
        //            {
        //                fieldsList.Remove(fieldsList.Where(a => a.FieldName == field).FirstOrDefault());
        //            }
        //        }
        //        if (fieldsList != null)
        //        {
        //            foreach (var item in fieldsList)
        //            {
        //                context.FieldsForTables.Remove(context.FieldsForTables.Where(fi => item.FieldCode == fi.FieldCode).FirstOrDefault());
        //            }
        //        }
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;

        //}

        public Boolean saveSelectedFilter(int projectCode, string tableName, List<string> columns)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                TablesForProject table = context.TablesForProject.Where(tbl => tbl.ProjectCode == projectCode && tbl.TableName == tableName).FirstOrDefault();
                if (table == null)
                {
                    table = new TablesForProject();
                    table.ProjectCode = projectCode;
                    table.TableName = tableName;
                    context.TablesForProject.Add(table);
                }
                var fieldsList = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode).ToList();
                foreach (var field in columns)
                {
                    FieldsForTables f = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode && fi.FieldName == field).FirstOrDefault();
                    Projects p = context.Projects.Where(project => project.ProjectCode == projectCode).FirstOrDefault();
                    int? companyCode = p.CompanyCode;
                    Companies c = context.Companies.Where(compay => compay.Code == companyCode).FirstOrDefault();
                    Server server = new Server(c.ServerName);
                    ServerConnection connection = new ServerConnection();
                    server = new Server(connection);
                    Database myAdventureWorks = server.Databases[p.ProjectName];
                    if (f == null)
                    {
                        f = new FieldsForTables();
                        f.FieldName = field;
                        f.TableCode = table.TableCode;
                        foreach (Table myTable in myAdventureWorks.Tables)
                        {
                            if (myTable.Name == tableName)
                                foreach (Column col in myTable.Columns)
                                {
                                    if (col.Name == field)
                                    {
                                        string descriptionType = col.DataType.Name;
                                        FieldTypes fieldtypes = context.FieldTypes.Where(fType => fType.Description == descriptionType).FirstOrDefault();
                                        f.FieldType = fieldtypes.FieldCode;
                                    }
                                }
                        }

                        context.FieldsForTables.Add(f);
                    }
                    else
                    {
                        fieldsList.Remove(fieldsList.Where(a => a.FieldName == field).FirstOrDefault());
                    }
                }
                if (fieldsList != null)
                {
                    foreach (var item in fieldsList)
                    {
                        context.FieldsForTables.Remove(context.FieldsForTables.Where(fi => item.FieldCode == fi.FieldCode).FirstOrDefault());
                    }
                }
                context.SaveChanges();
                return true;
            }
            return false;


        }


        public bool end(int projectCode, string columName, string tableOfMailAddress, string emailColumn)
        {
            using(Models.FinalProject context = new Models.FinalProject())
            {
                Projects project = context.Projects.Where(p => p.ProjectCode == projectCode).FirstOrDefault();
                project.FieldNameWithTheEmailAddress = columName;
                project.TableNameWithTheEmailField = tableOfMailAddress;
                project.Name = emailColumn;
                context.SaveChanges();
                return true;

            }
           
        }
    }
}
