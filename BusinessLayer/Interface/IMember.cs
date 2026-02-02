using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IMember
    {
        public Task<ResponseResult> memberAuthentication(Authentication authentication);
        public Task<ResponseResult> changeProfile(int Id, Member member);
        public Task<ResponseResult> getMemberList();
        public Task<ResponseResult> memberProfile(int Id);
        public Task<ResponseResult> signUpMember(Member member);
        public Task<ResponseResult> deactivateMember(int Id);
        public Task<ResponseResult> updatePassword(Authentication authentication);
    }
}
