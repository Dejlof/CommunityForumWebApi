using CommunityForumApi.Dtos.Account;
using CommunityForumApi.Models;
using CommunityForumApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommunityForumApi.Controllers
{
    [Route("CommForum/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost("Register")]
        public async Task <IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.EmailAddress,
                    PhoneNumber = registerDto.PhoneNumber,
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var assignRole = await _userManager.AddToRoleAsync(appUser, "User");

                    if (assignRole.Succeeded) 
                    {
                        return Ok("User created");
                    }
                    else
                    {
                        return StatusCode(500, assignRole.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception ex) 
            { 
                return StatusCode (500, ex.Message);    
            }
        }
    }
}