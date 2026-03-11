using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPurchaseMaster
    {
        public Task<ResponseResult> getPurchaseList();
        public Task<ResponseResult> getPurchaseById(int Id);
        public Task<PurchaseMaster?> getPurchaseEntityById(int id);
        public Task<ResponseResult> addPurchase(PurchaseMaster purchaseMaster);
        public Task<ResponseResult> updatePurchase(int Id, PurchaseMaster purchaseMaster);
    }
}
