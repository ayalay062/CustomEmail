using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Repositories;
using Repositories.Models;

namespace Servieces
{
   public class FilterService: IFilterService
    {
        IFilterRepository filterRepository;
        public FilterService(IFilterRepository filterRepository)
        {
            this.filterRepository = filterRepository;
        }

        public List<JObject> projectName(int companyCode)
        {
            return filterRepository.projectName(companyCode);
        }
        public Project tablesName(int code, string projectName)
        {
            return filterRepository.tablesName(code, projectName);
        }
  public List<string> getColumnName(int projectCode, string tableName)
        {
            return filterRepository.getColumnName(projectCode, tableName);
        }
        //  public Boolean saveFilter(int projectCode,string tableName,List<string> columns)
        //{
        //    return filterRepository.saveFilter(projectCode, tableName, columns);
        //}
        public FilterType getFilterTypes(int projectCode, string tableName, string column)
        {
            return filterRepository.getFilterTypes(projectCode, tableName, column);
        }
       public int saveFilter(dynamic filter)
        {
            return filterRepository.saveFilter(filter);
        }
       public List<string> getNames(dynamic filterToSend)
        {
            return filterRepository.getNames(filterToSend);
        }
        public List<FilterTables> getFilter(int projectCode)
        {
            return filterRepository.getFilter(projectCode);
        }
        public bool deleteFilter(int filterId)
        {
            return filterRepository.deleteFilter(filterId);
        }
    }
}
