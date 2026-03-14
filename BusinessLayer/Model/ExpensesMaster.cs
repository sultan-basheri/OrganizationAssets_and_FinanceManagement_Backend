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

        public virtual Organization? Organization { get; set; }

        public int OrganizationId { get; set; }
        public virtual ExpenseCategory? ExpenseCategory { get; set; }

        public int ExpenseCategoryId { get; set; }
        public virtual Mosque? Mosque { get; set; }

        public int MosqueId { get; set; } 
        public string PaidTo { get; set; } = string.Empty;

        public decimal ExpenseAmount { get; set; }

        public string? Remark { get; set; }

        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, Bank, UPI
        public string? Reference { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public virtual FinancialYear? FinancialYear { get; set; }
        public int FinancialYearId { get; set; }   // FK → FinancialYear

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
