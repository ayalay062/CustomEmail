using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json.Linq;
using Repositories.Models;
using System.Data;

namespace Repositories
{
    public class FilterRepository : IFilterRepository
    {
        public static DataSet dsMail;
        public List<JObject> projectName(int companyCode)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Companies c = context.Companies.Where(compay => compay.Code == companyCode).FirstOrDefault();
               
                List<Projects> projectList = new List<Projects>();
                projectList = context.Projects.Where(p => p.CompanyCode == companyCode).ToList();
                List<JObject> dbProjectsName = new List<JObject>();
                foreach (var project in projectList)
                {
                    string projectname = "{ 'projectname':'" + project.ProjectName + "'}";
                    dbProjectsName.Add(JObject.Parse(projectname));
                }
                return dbProjectsName;
            }
        }
        public Project tablesName(int code, string projectName)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Companies c = context.Companies.Where(company => company.Code == code).FirstOrDefault();
                Server server = new Server(c.ServerName);
                Projects p = context.Projects.Where(proj => proj.ProjectName == projectName && proj.CompanyCode == code).FirstOrDefault();
                Project project = new Project();
                project.TableList = new List<string>();
                List<TablesForProject> tableList = new List<TablesForProject>();
                tableList = context.TablesForProject.Where(table => table.ProjectCode == p.ProjectCode).ToList();
                project.ProjectCode = p.ProjectCode;
                foreach (var tableName in tableList)
                {
                    project.TableList.Add(tableName.TableName);
                }
                return project;
            }
        }

        public List<string> getColumnName(int projectCode, string tableName)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                Projects p = context.Projects.Where(project => project.ProjectCode == projectCode).FirstOrDefault();
                TablesForProject table = new TablesForProject();
                table = context.TablesForProject.Where(t => t.ProjectCode == p.ProjectCode && t.TableName==tableName).FirstOrDefault();
                List<FieldsForTables> fieldList = new List<FieldsForTables>();
                fieldList = context.FieldsForTables.Where(f => f.TableCode == table.TableCode).ToList();
                List<string> columsName = new List<string>();
                //foreach (var f in context.FieldsForTables.Where(ff => ff.TableCode == table.TableCode))
                //{
                //    columsName.Add(f.FieldName);
                //}

                foreach (var columnName in fieldList)
                {
                    columsName.Add(columnName.FieldName);
                }
                return columsName;
            }

        }

        //public bool saveFilter(int projectCode, string tableName, List<string> columns)
        //{
        //    using (Models.FinalProject context = new Models.FinalProject())
        //    {
        //        TablesForProject table = context.TablesForProject.Where(tbl => tbl.ProjectCode == projectCode && tbl.TableName == tableName).FirstOrDefault();
        //        int tableCode = table.TableCode;
        //        foreach (var field in columns)
        //        {
        //            FieldsForTables f = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode && fi.FieldName == field).FirstOrDefault();
        //            int fieldCode = f.FieldCode;
        //            FilterFields filterFields = new FilterFields();
        //            filterFields.TableCode = tableCode;
        //            filterFields.FieldCode = fieldCode;
        //            context.FilterFields.Add(filterFields);
        //            context.SaveChanges();
        //            //מפה הוא מחזיר את הרשימה של הסינונים המתאים לאותו שדה
        //            int? fieldType = f.FieldType;

        //            FieldTypes fieldtypes = context.FieldTypes.Where(ft => ft.FieldCode == fieldType).FirstOrDefault();
        //            if (fieldtypes != null)
        //            {
        //                List<FilterTypes> filterTypeslist = context.FilterTypes.Where(fil => fil.FilterCode == fieldtypes.FieldCode).ToList();
        //                /*return filterTypeslist*/
        //            }

        //        }
        //        return true;

        //    }
        //    return false;
        //}

        public FilterType getFilterTypes(int projectCode, string tableName, string column)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                FilterType filter = new FilterType();
                filter.FilterTypeList = new List<string>();
                TablesForProject table = context.TablesForProject.Where(tbl => tbl.ProjectCode == projectCode && tbl.TableName == tableName).FirstOrDefault();
                int tableCode = table.TableCode;
                FieldsForTables f = context.FieldsForTables.Where(fi => fi.TableCode == table.TableCode && fi.FieldName == column).FirstOrDefault();
                int? fieldType = f.FieldType;
                FieldTypes fieldtypes = context.FieldTypes.Where(ft => ft.FieldCode == fieldType).FirstOrDefault();
                List<FilterTypes> filterTypeslist = context.FilterTypes.Where(fil => fil.FilterCode == (fieldtypes.FieldCode / 10) * 10).ToList();
                foreach (var filterType in filterTypeslist)
                {
                    filter.FilterTypeList.Add(filterType.Description);
                }
                filter.FieldType = fieldtypes.Description;
                return filter;
            }
        }
        public int saveFilter(dynamic filter)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {

                //FieldFilter FieldFilter = new FieldFilter();
                FilterTable filterTable = new FilterTable();
                string tableName= (string)filter.TableName;
                int projectCode = (int)filter.ProjectCode;
                TablesForProject tablesForProject = context.TablesForProject.Where(table => table.TableName ==tableName&& table.ProjectCode==projectCode).FirstOrDefault();
                if (filter.id==null)
                {
                    filterTable.ProjectCode = projectCode;
                    filterTable.TableCode = tablesForProject.TableCode;
                    context.FilterTable.Add(filterTable);
                    context.SaveChanges();
                    foreach (var item in filter.FilterList)
                    {
                        saveFieldForFilter(item, filterTable.Id, (int)filterTable.TableCode);
                        //FilterFields filterField = new FilterFields();
                        //filterField.FilterCode = filterTable.Id;
                        ////string type = item.FieldType;
                        ////FilterTypes filterType = context.FilterTypes.Where(fType => fType.Description == type).FirstOrDefault();
                        ////filterField.FilterTypesCode = filterType.FilterId;
                        //string fieldName = item.FieldName;
                        //FieldsForTables fieldsForTables = context.FieldsForTables.Where(fft => fft.FieldName == fieldName&& fft.TableCode==filterTable.TableCode).FirstOrDefault();
                        //filterField.FieldCode = fieldsForTables.FieldCode;
                        //FieldTypes fieldType = context.FieldTypes.Where(ft => ft.FieldCode == fieldsForTables.FieldType).FirstOrDefault();
                        //string filterType = item.FilterType;
                        //FilterTypes filterTypes = context.FilterTypes.Where(ft => ft.Description == filterType).FirstOrDefault();
                        //filterField.FilterTypesCode = filterTypes.FilterId;
                        //string type = item.FieldType;
                        //switch (type)
                        //{
                        //    case "varchar":
                        //    case "nvarchar":
                        //        filterField.StringValue = item.Filter;
                        //        break;
                        //    case "date":
                        //    case "datetime":
                        //        filterField.DateValue = item.Filter;
                        //        break;
                        //    case "int":
                        //    case "decimal":
                        //    case "float":
                        //    case "money":
                        //        filterField.IntValue = item.Filter;
                        //        break;
                        //    case "bit":
                        //        filterField.BoolValue = item.Filter;
                        //        break;
                        //        //default:
                        //        // Console.WriteLine("Default case");
                        //        // break;
                        //}
                        //context.FilterFields.Add(filterField);
                        //context.SaveChanges();
                    }
                    return filterTable.Id;
                }
                else
                {
                    //int i = 1;
                    //foreach (var item in filter.FilterList)
                    //{
                    //    int id = filter.id;
                    //    if(i== filter.FilterList.Count)
                    //        saveFieldForFilter(item, (int)filter.id, (int)tablesForProject.TableCode);
                    //    i++;
                    //}
                    int lengh = filter.FilterList.Count;
                    saveFieldForFilter(filter.FilterList[lengh - 1], (int)filter.id, (int)tablesForProject.TableCode);
                    return 1;
                }
                    
               // FilterTypes filterType = context.FilterTypes.Where(fType => fType.Description == Filter.filterType).FirstOrDefault();

               // Filter.fieldList = new List<FilterFields>();
               // FilterFields FilterField = new FilterFields();
               // FilterField.fFie = filter.FilterList[0].FieldName;
               // Filter.fieldType = filter.FilterList[0].FieldType;
               // Filter.filterType = filter.FilterList[0].FilterType;
               // switch (Filter.fieldType)
               // {
               //     case "varchar":
               //     case "nvarchar":
               //         Filter.stringValue = filter.FilterList[0].Filter;
               //         break;
               //     case "date":
               //     case "datetime":
               //         Filter.dateValue = filter.FilterList[0].Filter;
               //         break;
               //     case "int":
               //     case "decimal":
               //     case "float":
               //     case "money":
               //         Filter.intValue = filter.FilterList[0].Filter;
               //         break;
               //     case "bit":
               //         Filter.boolValue = filter.FilterList[0].Filter;
               //         break;
               //         //default:
               //         // Console.WriteLine("Default case");
               //         // break;
               // }
               // //Filter..filterType
               //// FilterTypes filterType = context.FilterTypes.Where(fType => fType.Description == Filter..filterType).FirstOrDefault();
               // FilterFields filterFields = new FilterFields();
               // filterFields.FilterTypesCode = filterType.FilterId;
               // TablesForProject tablesForProject = context.TablesForProject.Where(table => table.TableName == Filter.tableName).FirstOrDefault();
               // filterFields.TableCode = tablesForProject.TableCode;
               // FieldTypes fieldtypes = context.FieldTypes.Where(ft => ft.Description == Filter.fieldType).FirstOrDefault();
               // FieldsForTables fieldsForTables = context.FieldsForTables.Where(fft => fft.FieldType == fieldtypes.FieldCode).FirstOrDefault();
               // filterFields.FieldCode = fieldsForTables.FieldCode;
               // filterFields.StringValue = Filter.stringValue;
               // filterFields.DateValue = Filter.dateValue;
               // filterFields.IntValue = Filter.intValue;
               // context.FilterFields.Add(filterFields);
               // context.SaveChanges();
               // return filterFields.Id;
            }
            //return false;
        }
        public void saveFieldForFilter(dynamic item,int id,int tableCode)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                FilterFields filterField = new FilterFields();
                filterField.FilterCode = id;
                //string type = item.FieldType;
                //FilterTypes filterType = context.FilterTypes.Where(fType => fType.Description == type).FirstOrDefault();
                //filterField.FilterTypesCode = filterType.FilterId;
                string fieldName = item.FieldName;
                FieldsForTables fieldsForTables = context.FieldsForTables.Where(fft => fft.FieldName == fieldName && fft.TableCode == tableCode).FirstOrDefault();
                filterField.FieldCode = fieldsForTables.FieldCode;
                FieldTypes fieldType = context.FieldTypes.Where(ft => ft.FieldCode == fieldsForTables.FieldType).FirstOrDefault();
                string filterType = item.FilterType;
                FilterTypes filterTypes = context.FilterTypes.Where(ft => ft.Description == filterType).FirstOrDefault();
                filterField.FilterTypesCode = filterTypes.FilterId;
                string type = item.FieldType;
                switch (type)
                {
                    case "varchar":
                    case "nvarchar":
                        filterField.StringValue = item.Filter;
                        break;
                    case "date":
                    case "datetime":
                        filterField.DateValue = item.Filter;
                        break;
                    case "int":
                    case "decimal":
                    case "float":
                    case "money":
                        filterField.IntValue = item.Filter;
                        break;
                    case "bit":
                        filterField.BoolValue = item.Filter;
                        break;
                        //default:
                        // Console.WriteLine("Default case");
                        // break;
                }
                context.FilterFields.Add(filterField);
                context.SaveChanges();
            }
        }
        public List<FilterTables> getFilter (int projectCode)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                List<TablesForProject> lstTable = new List<TablesForProject>();
                lstTable = context.TablesForProject.Where(tfp => tfp.ProjectCode == projectCode).ToList();
                List<FilterTables> lstFilterTables = new List<FilterTables>();
                List<FilterTable> lstFilterTable = new List<FilterTable>();
                foreach (var item in lstTable)
                {
                    List<FilterTable> list = new List<FilterTable>();
                    list = context.FilterTable.Where(ft => ft.TableCode == item.TableCode).ToList();
                    foreach (var table in list)
                    {
                        lstFilterTable.Add(table);
                    }
                }
                foreach (var item in lstFilterTable)
                {
                    FilterTables filterTable = new FilterTables();
                    filterTable.Id = item.Id;
                    filterTable.TableName = (context.TablesForProject.Where(tfp => tfp.TableCode == item.TableCode).FirstOrDefault()).TableName;
                    filterTable.LstField = new List<FieldFilter>();
                    List<FilterFields> lstFilterField = new List<FilterFields>();
                    lstFilterField = context.FilterFields.Where(ff => ff.FilterCode == item.Id).ToList();
                    foreach (var ff in lstFilterField)
                    {
                        FieldsForTables fieldsForTables = context.FieldsForTables.Where(fft => fft.FieldCode == ff.FieldCode).FirstOrDefault();
                        FieldFilter fieldFilter = new FieldFilter();
                        fieldFilter.FieldName =fieldsForTables.FieldName;
                        fieldFilter.FieldType = (context.FieldTypes.Where(ft => ft.FieldCode == fieldsForTables.FieldType).FirstOrDefault()).Description;
                        fieldFilter.FilterType = (context.FilterTypes.Where(ft => ft.FilterId == ff.FilterTypesCode).FirstOrDefault()).Description;
                        switch (fieldFilter.FieldType)
                        {
                            case "varchar":
                            case "nvarchar":
                                fieldFilter.stringValue =ff.StringValue;
                                break;
                            case "date":
                            case "datetime":
                                fieldFilter.dateValue =(DateTime) ff.DateValue;
                                break;
                            case "int":
                            case "decimal":
                            case "float":
                            case "money":
                                fieldFilter.intValue = (int)ff.IntValue;
                                break;
                            case "bit":
                                fieldFilter.boolValue = (bool)ff.BoolValue;
                                break;
                                //default:
                                // Console.WriteLine("Default case");
                                // break;
                        }
                        filterTable.LstField.Add(fieldFilter);
                    }
                    lstFilterTables.Add(filterTable);

                }
                return lstFilterTables;
            }
           
            }
        public bool deleteFilter(int filterId)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                List<FilterFields> ffList = new List<FilterFields>();
               ffList= context.FilterFields.Where(ff => ff.FilterCode == filterId).ToList();
                foreach (var item in ffList)
                {
                    context.FilterFields.Remove(item);
                }
                context.FilterTable.Remove(context.FilterTable.Where(ft => ft.Id == filterId).FirstOrDefault());
                context.SaveChanges();
                return true;
            }
            return false; 
        }
        public List<string> getNames(dynamic filterToSend)
        {
            using (Models.FinalProject context = new Models.FinalProject())
            {
                int a = (int)filterToSend.id;
                FilterTable filterTable = context.FilterTable.Where(ft => ft.Id == a).FirstOrDefault();
                List<FilterFields> filterFields = new List<FilterFields>();
                filterFields = context.FilterFields.Where(ff => ff.FilterCode == a).ToList();
                TablesForProject table = context.TablesForProject.Where(t => t.TableCode == filterTable.TableCode).FirstOrDefault();
                Projects project = context.Projects.Where(p => p.ProjectCode == table.ProjectCode).FirstOrDefault();
                Companies company = context.Companies.Where(c => c.Code == project.CompanyCode).FirstOrDefault();
                Server server = new Server(company.ServerName);
                ServerConnection connection = new ServerConnection();
                server = new Server(connection);
                Database myAdventureWorks = server.Databases[project.ProjectName];
                string select = $"Select {project.Name} from {table.TableName} where ";
                string comand = "";
                int flag = 0;
                foreach (var item in filterFields)
                {
                    FieldsForTables field = context.FieldsForTables.Where(fft => fft.FieldCode == item.FieldCode).FirstOrDefault();
                   
                    switch (item.FilterTypesCode)
                    {
                        case (4):
                            comand = field.FieldName+ " Like '" + item.StringValue+"'";
                            break;
                        case (5):
                            comand = field.FieldName + " Like '" + item.StringValue + "%'";
                            break;
                        case (6):
                            comand = field.FieldName + " Like '%" + item.StringValue + "%'";
                            break;
                        case (7):
                            comand = field.FieldName + " = " + item.IntValue;
                            break;
                        case (17):
                            comand = field.FieldName + " > " + item.IntValue;
                            break;
                        case (18):
                            comand = field.FieldName + " < " + item.IntValue;
                            break;
                        case (20):
                            int date =int.Parse( item.DateValue?.ToString("yyyyMMdd"));
                            comand = field.FieldName + " = " + "'"+ date+"'";
                            break;
                        default:
                            break;
                    }
                    if (flag == 0)
                    {
                        select = select + comand;
                    }
                    else select = select + " and " + comand;
                    flag++;
                }
                List<string> ss = new List<string>();
               
                DataSet dsName = myAdventureWorks.ExecuteWithResults(select);
                for(int i=0; i<dsName.Tables[0].Rows.Count; i++)
                {
                    ss.Add(dsName.Tables[0].Rows[i][0].ToString());
                }
                 dsMail = myAdventureWorks.ExecuteWithResults
                    ($"select {project.FieldNameWithTheEmailAddress} from {project.TableNameWithTheEmailField} where {project.Name} in ({select})");
                return ss;
                var ab=dsName.Tables[0].Rows[0][0].ToString();
                //    foreach (Table t in myAdventureWorks.Tables)
                //    {
                //        if (t.Name == project.TableNameWithTheEmailField)
                //        {
                //            DataSet dsName = myAdventureWorks.ExecuteWithResults("Select * from MailAddress");
                //           var s= dsName.Tables[0].Rows[0][0].ToString();
                //            foreach (Column col in t.Columns)
                //            {



                    //            }

                    //        }
                    //    }


                }
            
        }

    }
}
