using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPurchaseDetail
    {
        public Task<ResponseResult> getPurchaseDetailList();
        public Task<ResponseResult> getPurchaseDetailById(int Id);
        public Task<PurchaseDetail?> getPurchaseDetailEntityById(int id);
        public Task<ResponseResult> addPurchaseDetail(PurchaseDetail purchaseDetail);
        public Task<ResponseResult> updatePurchaseDetail(int Id, PurchaseDetail purchaseDetail);
    }
}
