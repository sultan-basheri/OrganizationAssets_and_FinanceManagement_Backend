using BusinessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Admin> AdminMaster { get; set; }
        public DbSet<Organization> OrganizationMaster { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Mosque> Mosques { get; set; }
    }
}
