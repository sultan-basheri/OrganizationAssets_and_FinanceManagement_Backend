using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IDonation
    {
        public Task<ResponseResult> getDonationList();
        public Task<ResponseResult> getDonationById(int Id);
        public Task<ResponseResult> addDonation(Donation donation);
        public Task<ResponseResult> updateDonation(int Id, Donation donation);
    }
}
