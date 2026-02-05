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
        [Required]
        public int PropertyId { get; set; }   // FK → Property

        [Required]
        public DateOnly DateFrom { get; set; }

        [Required]
        public DateOnly DateTo { get; set; }

        [Required(ErrorMessage = "Rent amount is required")]
        public decimal RentAmount { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid Alternate number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Alternate number must be 10 digits")]
        public string? AlternateNo { get; set; }

        [Required(ErrorMessage = "Deposit amount is required")]
        public decimal Deposite { get; set; }

        [Required(ErrorMessage = "Aadhar number is required")]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "Aadhar number must be 12 digits")]
        public string AadharNo { get; set; } = string.Empty;

        public string? DocumentType { get; set; }

        [Required(ErrorMessage = "Document number is required")]
        public string DocumentNo { get; set; } = string.Empty; // PAN,Passport

        [NotMapped]
        public string? base64Data { get; set; }
        public string? DocExtension { get; set; } // PDF,PNG,JPG
        public string? DocUrl { get; set; }

        public string? ReferenceName { get; set; }

        [Required(ErrorMessage = "Rent type is required")]
        public string RentType { get; set; } = string.Empty;
        // Example: Monthly, Yearly, Lease

        public virtual OfficeStaff? OfficeStaff { get; set; }
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<RentMaster> RentMasterPRA { get; set; }
    }
}
