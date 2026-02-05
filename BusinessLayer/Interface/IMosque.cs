using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IMosque
    {
        public Task<ResponseResult> GetMosqueList();
        public Task<ResponseResult> GetMosqueById(int Id);
        public Task<ResponseResult> AddMosque(Mosque mosque);
        public Task<ResponseResult> UpdateMosque(int Id,Mosque mosque);
    }
}
