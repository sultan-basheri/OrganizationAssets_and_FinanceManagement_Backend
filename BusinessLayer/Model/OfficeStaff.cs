using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class OfficeStaff
    {
        public OfficeStaff()
        {
            EnrollStaffOff = new HashSet<Staff>();
            FinancialYearOff = new HashSet<FinancialYear>();
            PropertiesOff = new HashSet<Properties>();
            PRAOff = new HashSet<PropertyRentAgreement>();
            RentMasterOff = new HashSet<RentMaster>();
            DonationsOff = new HashSet<Donation>();
            SalaryMasterOff = new HashSet<SalaryMaster>();
            WithdrawalOff = new HashSet<WithdrawalSalary>();
            ExpCategoriesOff = new HashSet<ExpenseCategory>();
            ExpensesMasterOff = new HashSet<ExpensesMaster>();
            PurchaseMasterOff = new HashSet<PurchaseMaster>();
            PurchasePaymentOff = new HashSet<PurchasePayment>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid Contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of joining is required")]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = string.Empty;

        public ICollection<Staff> EnrollStaffOff { get; set; }
        public ICollection<FinancialYear> FinancialYearOff { get;set; }
        public ICollection<Properties> PropertiesOff { get; set; }
        public ICollection<PropertyRentAgreement> PRAOff { get; set; }
        public ICollection<RentMaster> RentMasterOff { get; set; }
        public ICollection<Donation> DonationsOff { get; set; }
        public ICollection<SalaryMaster> SalaryMasterOff { get; set;}
        public ICollection<WithdrawalSalary> WithdrawalOff { get; set; }
        public ICollection<ExpenseCategory> ExpCategoriesOff { get; set; }
        public ICollection<ExpensesMaster> ExpensesMasterOff { get; set; }
        public ICollection<PurchaseMaster> PurchaseMasterOff { get; set; }
        public ICollection<PurchasePayment> PurchasePaymentOff { get; set; }
    }
}
