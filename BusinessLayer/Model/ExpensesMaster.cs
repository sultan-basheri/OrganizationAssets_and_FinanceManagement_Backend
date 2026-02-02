using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class ExpensesMaster
    {
        [Key]
        public int Id { get; set; }   // PK

        public virtual ExpenseCategory? ExpenseCategory { get; set; }
        [Required]
        public int ExpenseCategoryId { get; set; }   // FK → ExpenseCategory

        [Required(ErrorMessage = "Paid To is required")]
        public string PaidTo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expense amount is required")]
        public decimal ExpenseAmount { get; set; }

        public string? Remark { get; set; }

        [Required(ErrorMessage = "Payment type is required")]
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, Bank, UPI

        public string? Reference { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public virtual FinancialYear? FinancialYear { get; set; }
        [Required]
        public int FinancialYearId { get; set; }   // FK → FinancialYear

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
