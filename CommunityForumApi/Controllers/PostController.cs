using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Extensions;
using CommunityForumApi.Interface;
using CommunityForumApi.Mappers;
using CommunityForumApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityForumApi.Controllers
{
    [Route("CommForum/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<AppUser> _userManager;

        public PostController(IPostRepository postRepository, UserManager<AppUser> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = posts.Select(s => s.ToPostDto()).ToList();
            return Ok(postDto);
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task <IActionResult> GetMyPost()
        {
            var user = User.GetUsername();

            if (user == null) 
            { 
                return Unauthorized();
            }
            var posts = await _postRepository.GetMyPostsAsync(user);
            var postDto = posts.Select(s => s.ToPostDto()).ToList();
            return Ok(postDto);

        }






        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToPostDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);


            var postModel = postDto.ToPostFromCreateDto();
            postModel.AppUserId = appUser.Id;

            await _postRepository.CreatePostAsync(postModel);
            return CreatedAtAction(nameof(GetPostById), new { id = postModel.Id }, postModel.ToPostDto());
        }


        [HttpPut("{id:int}")]
        [Authorize]
        public async Task <IActionResult> updatePost([FromRoute] int id, [FromBody] UpdatePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = await _postRepository.UpdatePostAsync(id, postDto);

            if(postModel == null)
            {
                return NotFound();
            }
            return Ok(postModel.ToPostDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> deletePost([FromRoute] int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = await _postRepository.DeletePostAsync(id);
            if(postModel == null)
            {
                return NotFound();
            }

            return Ok($"Post with id: {id} sucessfully deleted");
        }
    }

}
