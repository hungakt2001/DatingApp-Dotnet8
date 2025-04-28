using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseAPIController
    {
        [HttpPost("register")]   // acount/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (await UserExists(register.UserName)) return BadRequest("User Name had Exists");
            return Ok();
            // using var hmac = new HMACMD5();

            // var user = new AppUser
            // {
            //     UserName = register.UserName.ToLower(),
            //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.PassWord)),
            //     PasswordSalt = hmac.Key
            // };
            // context.Users.Add(user);
            // await context.SaveChangesAsync();

            // return new UserDTO
            // {
            //     UserName = user.UserName,
            //     Token = tokenService.CreateToken(user)
            // };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == login.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid UserName");
            using var hmac = new HMACMD5(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.PassWord));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid PassWord");
            }
            return new UserDTO
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}