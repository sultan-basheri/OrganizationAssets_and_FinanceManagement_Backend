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
        public DbSet<OfficeStaff> OfficeStaffs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<PropertyRentAgreement> PropertyRentAgreements { get; set; }
        public DbSet<RentMaster> RentMasters { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<SalaryMaster> SalaryMaster { get; set; }
        public DbSet<WithdrawalSalary> WithdrawalSalaries { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpensesMaster> ExpensesMaster { get; set; }
        public DbSet<PurchaseMaster> PurchaseMasters { get; set; }
        public DbSet<PurchasePayment> PurchasePayments { get; set; }
    }
}
