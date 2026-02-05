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

namespace DatabaseLayer.Repository
{
    public class ManageOfficeStaff : IOfficeStaff
    {
        private readonly ApplicationDbContext _context;
        public ManageOfficeStaff(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ResponseResult> changeProfile(int Id, OfficeStaff officeStaff)
        {
            try
            {
                List<string> errors = new List<string>();

                var existing = await _context.OfficeStaffs.FirstOrDefaultAsync(x => x.Id == Id);

                if (existing == null)
                    return new ResponseResult("Fail", "OfficeStaff not found");

                if (await _context.OfficeStaffs.AnyAsync(x => x.Email == officeStaff.Email && x.Id != Id))
                {
                    errors.Add("Email already exists");
                }

                if (await _context.OfficeStaffs.AnyAsync(x => x.ContactNo == officeStaff.ContactNo && x.Id != Id))
                {
                    errors.Add("Contact number already exists");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // update
                existing.FullName = officeStaff.FullName;
                existing.Email = officeStaff.Email;
                existing.ContactNo = officeStaff.ContactNo;
                existing.Address = officeStaff.Address;
                existing.Gender = officeStaff.Gender;

                await _context.SaveChangesAsync();

                return new ResponseResult("OK", "Update successful");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> deactivateofficeStaff(int Id)
        {
            try
            {
                var result = await _context.OfficeStaffs.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"OfficeStaff Id = {Id} Is Not Found");
                }

                result.Status = "De-Activate";
                _context.OfficeStaffs.Update(result);
                await _context.SaveChangesAsync();
                return new ResponseResult("OK", "Successfully Saved");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getOfficeStaffList()
        {
            try
            {
                var result = await _context.OfficeStaffs.Select(o => new
                {
                    o.Id,
                    o.FullName,
                    o.Address,
                    o.ContactNo,
                    o.Email,
                    o.DateOfJoining,
                    o.Gender,
                    o.CreatedAt,
                    o.UpdatedAt,
                    o.Status
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Empty");
                }
                return new ResponseResult("OK", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> officeStaffAuthentication(Authentication authentication)
        {
            try
            {
                var result = await _context.OfficeStaffs.FirstOrDefaultAsync(x => x.Email == authentication.userName || x.ContactNo == authentication.userName);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Not Exist");
                }
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                    authentication.password,
                    result.Password   // hashed password from DB
                );

                if (!isPasswordValid)
                {
                    return new ResponseResult("Fail", "Wrong username or password");
                }
                var office = new OfficeStaff
                {
                    Id = result.Id,
                    FullName = result.FullName,
                    ContactNo = result.ContactNo,
                    Email = result.Email
                };

                return new ResponseResult("OK", office);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> officeStaffProfile(int Id)
        {
            try
            {
                var result = await _context.OfficeStaffs.Select(x => new
                {
                    x.Id,
                    x.FullName,
                    x.Address,
                    x.Email,
                    x.ContactNo,
                    x.DateOfJoining,
                    x.CreatedAt,
                    x.Status
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"OfficeStaff Id = {Id} Is Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> signUpofficeStaff(OfficeStaff officeStaff)
        {
            try
            {
                if (officeStaff == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Fields");
                }
                List<string> error = new List<string>();
                var data = await _context.OfficeStaffs.ToListAsync();

                if (data.Any(o => o.Email == officeStaff.Email))
                {
                    error.Add("Email is Already Exist");
                }
                if (data.Any(o => o.ContactNo == officeStaff.ContactNo))
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
                    officeStaff.Password = BCrypt.Net.BCrypt.HashPassword(autoPassword);


                    var mail = new MailMessage();
                    mail.To.Add(officeStaff.Email);
                    mail.From = new MailAddress("TankariyaTrust@gmail.com");
                    mail.Subject = "Your OfficeStaff Login Credentials";
                    mail.Body = $@"
                        Hello {officeStaff.FullName},

                        Your Office Staff account has 
                        been created successfully.

                        You can login using ANY ONE 
                        of the following:

                        1️) Email + Password
                           Email    : {officeStaff.Email}
                           Password : {autoPassword}

                        2️) Contact Number + Password
                           Contact  : {officeStaff.ContactNo}
                           Password : {autoPassword}

                        Please change your password after 
                        first login.

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
                    var result = await _context.OfficeStaffs.AddAsync(officeStaff);
                    _context.SaveChanges();
                    return new ResponseResult("OK", "Successfully Saved");
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
                var result = await _context.OfficeStaffs.FirstOrDefaultAsync(x => x.Id == authentication.Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "OfficeStaff Not Found");
                }
                bool isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(authentication.oldPassword, result.Password);
                if (!isOldPasswordCorrect)
                {
                    return new ResponseResult("Fail", "Old Password Incorrect");
                }
                result.Password = BCrypt.Net.BCrypt.HashPassword(authentication.newPassword);

                _context.OfficeStaffs.Update(result);
                await _context.SaveChangesAsync();
                return new ResponseResult("OK", "Password Update Successfully");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
