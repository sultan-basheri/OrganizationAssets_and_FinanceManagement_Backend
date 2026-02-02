using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class SalaryMaster
    {
        [Key]
        public int Id { get; set; }   // PK

        public virtual Staff? Staff { get; set; }
        [Required]
        public int StaffId { get; set; }   // FK → Staff

        [Required(ErrorMessage = "Salary amount is required")]
        public decimal SalaryAmount { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateOnly GeneratedDate { get; set; }

        public virtual Organization? Organization { get; set; }
        [Required]
        public int OrganizationId { get; set; }   // FK → Organization

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public virtual FinancialYear? FinancialYear { get; set; }
        [Required]
        public int FinancialYearId { get; set; }   // FK → FinancialYear
    }
}
