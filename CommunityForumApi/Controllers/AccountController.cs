using CommunityForumApi.Dtos.Account;
using CommunityForumApi.Models;
using CommunityForumApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.Entity;
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

                var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUser != null) 
                {
                    return BadRequest("User already exists");
                }

                var existingEmail = await _userManager.FindByEmailAsync(registerDto.EmailAddress);
                if (existingEmail != null)
                {
                    return BadRequest("Email already exists");
                }
                
                var existingPhone = await _userManager.Users.AnyAsync(u=>u.PhoneNumber == registerDto.PhoneNumber);

                if (existingPhone != null)
                {
                    return BadRequest("PhoneNumber already in use");
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete( string user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedUser = await _userManager.FindByNameAsync(user); 

            if (deletedUser == null)
            {
                return NotFound($"{user} not found");
            }

            await _userManager.DeleteAsync(deletedUser);
          
          

            return Ok($"{deletedUser} has been deleted");

        }
    }
   
}