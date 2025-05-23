using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAysnc();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUserNameAsync(string username);
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO?> GetMemberAsync(string username);
    }
}