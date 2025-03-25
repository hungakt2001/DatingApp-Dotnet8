using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context) : BaseAPIController
    {
        [HttpPost("register")]   // acount/register
        public async Task<ActionResult<AppUser>> Register(RegisterDTO register)
        {
            if (await UserExists(register.UserName)) return BadRequest("User Name had Exists");
            using var hmac = new HMACMD5();

            var user = new AppUser
            {
                UserName = register.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.PassWord)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}