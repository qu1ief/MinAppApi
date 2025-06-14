using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinAppApi.Data;
using MinAppApi.Dtos.User;
using MinAppApi.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        UserManager<AppUser> userManager,
        MiniAppApiDbContext dbContext,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
        ) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await userManager.FindByNameAsync(registerDto.UserName);
            if (user != null)
            {
                return Conflict("Username already exists");
            }

            user = mapper.Map<AppUser>(registerDto);

            IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, "Member");

            return Created();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
            {
                return Conflict();
            }

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return Conflict();

            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(roles.Select(R => new Claim(ClaimTypes.Role, R)).ToList());

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_keyyour_super_secret_keyyour_super_secret_keyyour_super_secret_key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
         issuer: "yourdomain.com",
         audience: "yourdomain.com",
         claims: claims,
         expires: DateTime.Now.AddMinutes(30),
         signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(new { token });
        }

        [HttpGet("AddRole")]

        public async Task<IActionResult> AddRole()
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Member"))
            {
                var role = new IdentityRole("Member");
                await roleManager.CreateAsync(role);
            }

            return NoContent();
        }
    }
}
