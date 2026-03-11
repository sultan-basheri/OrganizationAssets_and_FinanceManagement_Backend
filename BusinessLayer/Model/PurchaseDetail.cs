using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public virtual PurchaseMaster? PurchaseMaster { get; set; }
        public int PurchaseMasterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal GrossAmount { get; set; }
        public decimal GstAmount { get; set; }
        public decimal GstPercentage { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
