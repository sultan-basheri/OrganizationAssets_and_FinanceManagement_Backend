using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabaseLayer.Repository
{
    public class ManageMember : IMember
    {
        private readonly ApplicationDbContext _context;
        public ManageMember(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> changeProfile(int Id, Member member)
        {
            try
            {
                List<string> errors = new List<string>();

                var existing = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id);
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == member.OrganizationId);

                
                if (existing == null)
                    return new ResponseResult("Fail", "Member not found");

                if (await _context.Members.AnyAsync(x => x.Email == member.Email && x.Id != Id))
                {
                    errors.Add("Email already exists");
                }
                if (!orgExists)
                {
                    errors.Add("Invalid Organization. Organization does not exist.");
                }
                if (await _context.Members.AnyAsync(x => x.ContactNo == member.ContactNo && x.Id != Id))
                {
                    errors.Add("Contact number already exists");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                existing.OrganizationId = member.OrganizationId;
                existing.FullName = member.FullName;
                existing.Email = member.Email;
                existing.ContactNo = member.ContactNo;
                existing.AlternateNo = member.AlternateNo;
                existing.Role = member.Role;


                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Update successful");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }

        }

        public async Task<ResponseResult> deactivateMember(int Id)
        {
            try
            {
                var result = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Member Not Found");
                }

                result.Status = "De-Activate";
                _context.Members.Update(result);
                await _context.SaveChangesAsync();
                return new ResponseResult("Ok", "Successfully Saved");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getMemberList()
        {
            try
            {
                var result = await _context.Members.Select(o => new
                {
                    o.Id,
                    o.OrganizationId,
                    o.FullName,
                    o.ContactNo,
                    o.AlternateNo,
                    o.Email,
                    o.Role,
                    o.JoiningDate,
                    o.CreatedAt,
                    o.Status
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Empty");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> memberAuthentication(Authentication authentication)
        {
            try
            {
                var result = await _context.Members.FirstOrDefaultAsync(x => x.Email == authentication.userName || x.ContactNo == authentication.userName);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Member Not Exist");
                }
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                    authentication.password,
                    result.Password   // hashed password from DB
                );

                if (!isPasswordValid)
                {
                    return new ResponseResult("Fail", "Wrong username or password");
                }
                var member = new Member
                {
                    Id = result.Id,
                    FullName = result.FullName,
                    Email = result.Email,
                    ContactNo = result.ContactNo,
                };

                return new ResponseResult("OK", member);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        

        public async Task<ResponseResult> memberProfile(int Id)
        {
            try
            {
                var result = await _context.Members.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Member Id = {Id} Is Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> signUpMember(Member member)
        {
            try
            {
                if(member == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Fields");
                }
                List<string> error = new List<string>();
                var data = await _context.Members.ToListAsync();
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == member.OrganizationId);

                if (!orgExists)
                {
                    error.Add("Invalid Organization. Organization does not exist.");
                }
                if (data.Any(o => o.Email == member.Email))
                {
                    error.Add("Email is Already Exist");
                }
                if (data.Any(o => o.ContactNo == member.ContactNo))
                {
                    error.Add("ContactNo Is Already Exist");
                }
                if (error.Count == 0)
                {
                    const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789@#";
                    Random random = new Random();
                    string autoPassword = new string(
                        Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray()
                    );
                    member.Password = BCrypt.Net.BCrypt.HashPassword(autoPassword);


                    var mail = new MailMessage();
                    mail.To.Add(member.Email);
                    mail.From = new MailAddress("TankariyaTrust@gmail.com");
                    mail.Subject = "Your Member Login Credentials";
                    mail.Body = $@"
                        Hello {member.FullName},

                        Your admin account has been created
                        successfully.

                        You can login using ANY ONE 
                        of the following:

                        1️) Email + Password
                           Email    : {member.Email}
                           Password : {autoPassword}

                        2️) Contact Number + Password
                           Contact  : {member.ContactNo}
                           Password : {autoPassword}

                        Please change your password after
                        your first login.

                        Thank you.
                        ";
                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential(
                            "sultantankariawala@gmail.com",   // Gmail ID
                            "jkkfzlqobpoamecv"  // App Password (no spaces)
                        ),
                        EnableSsl = true
                    };

                    smtp.Send(mail);
                    var result = await _context.Members.AddAsync(member);
                    _context.SaveChanges();
                    return new ResponseResult("Ok", "Successfully Saved");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updatePassword(Authentication authentication)
        {
            try
            {
                var result = await _context.Members.FirstOrDefaultAsync(x => x.Id == authentication.Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Member Not Found");
                }
                bool isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(authentication.oldPassword, result.Password);
                if (!isOldPasswordCorrect)
                {
                    return new ResponseResult("Fail", "Old Password Incorrect");
                }
                result.Password = BCrypt.Net.BCrypt.HashPassword(authentication.newPassword);

                _context.Members.Update(result);
                await _context.SaveChangesAsync();
                return new ResponseResult("Ok", "Password Update Successfully");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
