using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
//using Microsoft.Extensions.DependencyInjection;
//using Repositories;
using Repositories.Models;

namespace Servieces
{
   public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection collection)
        {
            collection.AddScoped<ICompanyRepository, CompanyRepository>();
            collection.AddScoped<IProjectRepository, ProjectRepository>();
            collection.AddScoped<IFilterRepository, FilterRepository>();
            collection.AddScoped<ISendEmailRepository, SendEmailRepository>();
            collection.AddDbContext<FinalProject>(Options =>
   Options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chaim\Desktop\פרויקט גמר\DB\Final_Project_.mdf;Integrated Security=True;Connect Timeout=30"));

            return collection;

        }
    }
}
