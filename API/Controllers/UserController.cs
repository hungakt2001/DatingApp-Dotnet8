using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UserController(IUserRepository userRepository, IMapper mapper) : BaseAPIController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")] // api/users/id
    public async Task<ActionResult<MemberDTO>> GetUser(int id)
    {
        var users = await userRepository.GetUserByIdAsync(id);
        if (users == null) return NotFound();
        return mapper.Map<MemberDTO>(users);
    }
    [HttpGet("{username}")] // api/users/username
    public async Task<ActionResult<MemberDTO>> GetUserName(string username)
    {
        var users = await userRepository.GetMemberAsync(username);
        if (users == null) return NotFound();
        return users;
    }
}
