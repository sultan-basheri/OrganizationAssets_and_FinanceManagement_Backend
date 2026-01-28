using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public static class DALService
    {
        public static IServiceCollection AddDBService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer("Data Source=.;Initial Catalog=TankariaMissionDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
            return services;
        }
    }
}
