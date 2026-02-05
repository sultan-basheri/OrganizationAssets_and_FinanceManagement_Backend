using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class Properties
    {
        public Properties()
        {
            EnrollProPRA = new HashSet<PropertyRentAgreement>();
        }
        [Key]
        public int Id { get; set; }   // PK

       
        public virtual Organization? Organization { get; set; }
        public int OrganizationId { get; set; }   // FK → Organization

        [Required(ErrorMessage = "Property type is required")]
        public string PropertyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner type is required")]
        public string OwnerType { get; set; } = string.Empty;
        // Example: Owned, Rented, Leased

        [Required(ErrorMessage = "Registration number is required")]
        public string RegNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Measurement is required")]
        public string Measurement { get; set; } = string.Empty;

        [Required(ErrorMessage = "Measurement unit is required")]
        public string MeasurementUnit { get; set; } = string.Empty;
        // Example: SqFt, SqM, Acre

        [Required]
        public DateOnly OwnershipDate { get; set; }

        [NotMapped]
        public string? base64Data { get; set; }
        public string? DocType { get; set; }
        public string? DocUrl { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   // FK → OfficeStaff
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<PropertyRentAgreement> EnrollProPRA { get; set; }
    }
}
