using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class PropertyRentAgreement
    {
        public PropertyRentAgreement()
        {
            RentMasterPRA = new HashSet<RentMaster>();
        }
        [Key]
        public int Id { get; set; }   // PK

        public virtual Properties? Property { get; set; }
        public int PropertyId { get; set; }   // FK → Property
        public DateOnly DateFrom { get; set; }

        [Required]
        public DateOnly DateTo { get; set; }
        public decimal RentAmount { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string? AlternateNo { get; set; }

        public decimal Deposite { get; set; }
        public string AadharNo { get; set; } = string.Empty;

        public string? DocumentType { get; set; }
        public string DocumentNo { get; set; } = string.Empty; // PAN,Passport

        [NotMapped]
        public string? base64Data { get; set; }
        public string? DocExtension { get; set; } // PDF,PNG,JPG
        public string? DocUrl { get; set; }

        public string? ReferenceName { get; set; }
        public string RentType { get; set; } = string.Empty;
        // Example: Monthly, Yearly, Lease

        public virtual OfficeStaff? OfficeStaff { get; set; }
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<RentMaster> RentMasterPRA { get; set; }
    }
}
