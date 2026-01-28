using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IMember
    {
        public Task<ResponseResult> GetList();
        public Task<ResponseResult> GetMemberList();
        public Task<ResponseResult> GetMemberById(int Id);
        public Task<ResponseResult> AddMember(Member member);
        public Task<ResponseResult> UpdateMember(Member member);
        public Task<ResponseResult> DeleteMember(int Id);
    }
}
