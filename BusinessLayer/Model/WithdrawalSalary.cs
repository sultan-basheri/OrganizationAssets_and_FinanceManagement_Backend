using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class WithdrawalSalary
    {
        [Key]
        public int Id { get; set; }   

        public virtual Staff? Staff { get; set; }
        
        public int StaffId { get; set; }   // FK → Staff
        public decimal WithdrawalAmount { get; set; }

        public string? Remark { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, Bank, UPI

        public string? Reference { get; set; }   // Txn / Cheque / Ref No
        public DateOnly PaymentDate { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual OfficeStaff? OfficeStaff { get; set; }
   
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public virtual FinancialYear? FinancialYear { get; set; }
        public int FinancialYearId { get; set; }   // FK → FinancialYear
    }
}
