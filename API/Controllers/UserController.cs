using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] //API User
public class UserController(DataContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id:int}")] // api/users/id
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var users = await context.Users.FindAsync(id);
        if (users == null) return NotFound();
        return users;
    }
}
