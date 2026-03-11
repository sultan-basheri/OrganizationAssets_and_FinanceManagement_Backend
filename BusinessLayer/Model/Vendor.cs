using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class Vendor
    {
        public Vendor()
        {
            purVendor = new HashSet<PurchaseMaster>();
        }
        public int Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? GstIn { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<PurchaseMaster> purVendor {  get; set; }
    }
}
