using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class FinancialYear
    {
        public FinancialYear()
        {
            DonationFYear = new HashSet<Donation>();
            SalaryMasterFYear = new HashSet<SalaryMaster>();
            WithdrawalFYear = new HashSet<WithdrawalSalary>();
            ExpensesMasterFYear = new HashSet<ExpensesMaster>();
            PurchaseMasterFYear = new HashSet<PurchaseMaster>();
            PurchasePaymentFYear = new HashSet<PurchasePayment>();
        }
        [Key]
        public int Id { get; set; }  

        [Required]
        public DateOnly DateFrom { get; set; }   

        [Required]
        public DateOnly DateTo { get; set; }  

        [Required]
        public string YearName { get; set; } = string.Empty;

        public virtual OfficeStaff? OfficeStaff { get; set; }
        
        public int OfficeStaffId { get; set; }   

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Donation> DonationFYear { get; set; }
        public ICollection<SalaryMaster> SalaryMasterFYear { get; set; }
        public ICollection<WithdrawalSalary> WithdrawalFYear { get; set; }
        public ICollection<ExpensesMaster> ExpensesMasterFYear { get; set; }
        public ICollection<PurchaseMaster> PurchaseMasterFYear { get; set; }
        public ICollection<PurchasePayment> PurchasePaymentFYear { get; set; }
    }
}
