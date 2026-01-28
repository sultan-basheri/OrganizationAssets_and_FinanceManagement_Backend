using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Model
{
    public class Mosque
    {
        public int Id { get; set; }

        // Navigation Property
        public Organization? OrgMosque { get; set; }

        [Required(ErrorMessage = "Organization is required")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Area is required")]
        public string Area { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imam name is required")]
        public string MosqueImam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid Contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Established Date is required")]
        public DateOnly EstablishedDate { get; set; }

        [Required(ErrorMessage = "Hijri Established date is required")]
        public DateOnly Established_Hijri { get; set; }

        [Required(ErrorMessage = "Mosque type is required")]
        public string MosqueType { get; set; } = string.Empty;
    }
}
