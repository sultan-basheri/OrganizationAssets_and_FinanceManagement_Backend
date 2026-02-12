using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPurchasePayment
    {
        public Task<ResponseResult> getPurchasePaymentList();
        public Task<ResponseResult> getPurchasePaymentDetailById(int Id);
        public Task<ResponseResult> addPurchasePayment(PurchasePayment purchasePayment);
        public Task<ResponseResult> updatePurchasePaymentDetails(int Id, PurchasePayment purchasePayment);
    }
}
