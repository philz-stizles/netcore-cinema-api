using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationPlugin;
using AutoMapper;
using Cinema.API.Data;
using Cinema.API.Dtos;
using Cinema.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null) return BadRequest(new { Status = false, Message = "User already exists" });

            var newUser = _mapper.Map<User>(dto);
            newUser.Password = SecurePasswordHasherHelper.Hash(dto.Password);
            newUser.Role = "User";

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok(new { Status = true, Message = "User registration successful" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser == null) return BadRequest(new { Status = false, Message = "User does not exist" });

            var isValidPassword = SecurePasswordHasherHelper.Verify(dto.Password, existingUser.Password);
            if (!isValidPassword) return BadRequest(new { Status = false, Message = "User credentials is invalid" });

            var loggedInUser = _mapper.Map<LoggedInUserDto>(existingUser);

            return Ok(new { Status = true, Data = loggedInUser, Token = "" });
        }
    }
}
