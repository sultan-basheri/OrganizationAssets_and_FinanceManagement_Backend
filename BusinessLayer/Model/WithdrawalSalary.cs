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
        [Required]
        public int StaffId { get; set; }   // FK → Staff

        [Required(ErrorMessage = "Withdrawal amount is required")]
        public decimal WithdrawalAmount { get; set; }

        public string? Remark { get; set; }

        [Required(ErrorMessage = "Payment type is required")]
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, Bank, UPI

        public string? Reference { get; set; }   // Txn / Cheque / Ref No

        [Required]
        public DateOnly PaymentDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public virtual FinancialYear? FinancialYear { get; set; }
        [Required]
        public int FinancialYearId { get; set; }   // FK → FinancialYear
    }
}
