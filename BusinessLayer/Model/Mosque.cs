using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Model
{
    public class Mosque
    {
        public Mosque()
        {
            EnrollStaffMosque = new HashSet<Staff>();
            EnrollMosqDonation= new HashSet<Donation>();
            EnrollMosqExpMast = new HashSet<ExpensesMaster>();
        }
        public int Id { get; set; }

        // Navigation Property
        public Organization? OrgMosque { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string ContactNo { get; set; } = string.Empty;
        public DateOnly EstablishedDate { get; set; } 
        public DateOnly Established_Hijri { get; set; }
        public string MosqueType { get; set; } = string.Empty;
        public ICollection<Staff> EnrollStaffMosque { get; set; }
        public ICollection<Donation> EnrollMosqDonation { get; set; }
        public ICollection<ExpensesMaster> EnrollMosqExpMast { get; set; }
    }
}
