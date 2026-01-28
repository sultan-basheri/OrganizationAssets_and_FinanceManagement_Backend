using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Data Source=.;Initial Catalog=TankariaMissionDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
            .Options;
            return new ApplicationDbContext(option);
        }
    }
}
