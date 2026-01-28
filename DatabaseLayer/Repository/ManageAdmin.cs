using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace DatabaseLayer.Repository
{
    public class ManageAdmin : IAdmin
    {
        private readonly ApplicationDbContext _context;
        public ManageAdmin(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> signUpAdmin(Admin admin)
        {
            try
            {
                List<string> error = new List<string>();
                var data = await _context.AdminMaster.ToListAsync();

                if (data.Any(o => o.Email == admin.Email))
                {
                    error.Add("Email is Already Exist");
                }
                if (data.Any(o => o.ContactNo == admin.ContactNo))
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
                    admin.Password = BCrypt.Net.BCrypt.HashPassword(autoPassword);


                    var mail = new MailMessage();
                    mail.To.Add(admin.Email);
                    mail.From = new MailAddress("TankariyaTrust@gmail.com");
                    mail.Subject = "Your Admin Login Credentials";
                    mail.Body = $@"
                        Hello {admin.FullName},

                        Your admin account has been created successfully.

                        You can login using ANY ONE of the following:

                        1️) Email + Password
                           Email    : {admin.Email}
                           Password : {autoPassword}

                        2️) Contact Number + Password
                           Contact  : {admin.ContactNo}
                           Password : {autoPassword}

                        Please change your password after first login.

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
                    var result = await _context.AdminMaster.AddAsync(admin);
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
        public async Task<ResponseResult> adminAuthentication(Authentication authentication)
        {
            try
            {
                var result = await _context.AdminMaster.Where(x => (x.ContactNo == authentication.userName || x.Email == authentication.userName) && x.Password == authentication.password)
                .Select(z => new Admin
                {
                    Id = z.Id,
                    FullName = z.FullName,
                    Email = z.Email,
                }).FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ResponseResult("Fail", "Wrong username or Password");
                }

                return new ResponseResult("OK", result);

            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> adminProfile(int Id)
        {
            try
            {
                var result = await _context.AdminMaster.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Admin Id = {Id} Is Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> getAdminList()
        {
            try
            {
                var result = await _context.AdminMaster.Select(o => new
                {
                    o.Id,
                    o.FullName,
                    o.ContactNo,
                    o.Email,
                    o.CreatedAt,
                    o.Status
                }).ToListAsync();
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> changeProfile(int Id, Admin admin)
        {
            try
            {
                List<string> error = new List<string>();
                var data = await _context.AdminMaster.ToListAsync();

                if (data.Any(o => o.Email == admin.Email && o.Id != Id))
                {
                    error.Add("Email is Already Exist");
                }
                if (data.Any(o => o.ContactNo == admin.ContactNo && o.Id != Id))
                {
                    error.Add("ContactNo Is Already Exist");
                }
                if (error.Count == 0)
                {
                    var result = await _context.AdminMaster.FirstOrDefaultAsync(x => x.Id == admin.Id);
                    if (result != null)
                    {
                        result.FullName = admin.FullName;
                        result.Email = admin.Email;
                        result.ContactNo = admin.ContactNo;
                        await _context.SaveChangesAsync();
                        return new ResponseResult("Ok", "Update Successful");
                    }
                }

                return new ResponseResult("Fail", string.Join(",", error, "Not Exist"));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> deactivateAdmin(int Id)
        {
            try
            {
                var admin = await _context.AdminMaster.FirstOrDefaultAsync(x => x.Id == Id);
                if (admin == null)
                {
                    return new ResponseResult("Fail", "Not Found");
                }

                admin.Status = "De-Activate";
                _context.AdminMaster.Update(admin);
                await _context.SaveChangesAsync();
                return new ResponseResult("Ok", "Successfully Saved");
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
                var result = await _context.AdminMaster.FirstOrDefaultAsync(x => x.Id == authentication.Id);
                if(result == null)
                {
                    return new ResponseResult("Fail", "Admin Not Found");
                }
                bool isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(authentication.oldPassword,result.Password);
                if (!isOldPasswordCorrect) 
                {
                    return new ResponseResult("Fail", "Old Password Incorrect");
                }
                result.Password = BCrypt.Net.BCrypt.HashPassword(authentication.newPassword);

                _context.AdminMaster.Update(result);
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
