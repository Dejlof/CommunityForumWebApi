using CommunityForumApi.Dtos.Account;
using CommunityForumApi.Interface;
using CommunityForumApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CommunityForumApi.Controllers
{
    [Route("CommForum/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> GetAll ()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          var users = await _userManager.Users.Select(u=> new UserDto
          {
              Username = u.UserName,
              EmailAddress = u.Email,
              PhoneNumber = u.PhoneNumber,
          }).ToListAsync();

            return Ok(users);

        }


        [HttpGet("Getbyuser")]
        [Authorize]
        public async Task<IActionResult> Get(string userName) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) 
            { 
                return NotFound("User not found");
            }

            var userDto = new UserDto
            {
             
                Username = user.UserName,
                EmailAddress = user.Email,
                PhoneNumber = user.PhoneNumber,
            };


            return Ok(userDto);
        }








        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(loginDto.Login);

            if (user == null && new EmailAddressAttribute().IsValid(loginDto.Login)) 
            {
                user = await _userManager.FindByEmailAsync(loginDto.Login);
            }

            if (user == null && new PhoneAttribute().IsValid(loginDto.Login)) 
            { 
                user = await _userManager.Users.FirstOrDefaultAsync(u=> u.PhoneNumber == loginDto.Login);
            }

            if (user == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            var verifiedUser = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!verifiedUser.Succeeded) 
            { 
                return Unauthorized("Username/Password incorrect");
            }

            var Token = await _tokenService.CreateToken(user);

            return Ok(Token);
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
                
                var existingPhone = await _userManager.Users.FirstOrDefaultAsync(u=>u.PhoneNumber == registerDto.PhoneNumber);

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
                        return Ok(new NewUserDto
                        {
                            Username = registerDto.Username,
                            EmailAddress = registerDto.EmailAddress,
                            PhoneNumber = registerDto.PhoneNumber,
                            Token = await _tokenService.CreateToken(appUser)
                        });
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
        [Authorize(Roles = "Admin")]
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