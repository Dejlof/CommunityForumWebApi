using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Interface;
using CommunityForumApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityForumApi.Controllers
{
    [Route("CommForum/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
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

            var postModel = postDto.ToPostFromCreateDto();

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
