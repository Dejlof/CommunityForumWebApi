using CommunityForumApi.Dtos.Comment;
using CommunityForumApi.Interface;
using CommunityForumApi.Mappers;
using CommunityForumApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityForumApi.Controllers
{
    [Route("CommForum/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IcommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        public CommentController(IcommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto()).ToList();
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{postId:int}")]
        public async Task<IActionResult> Create([FromRoute] int postId, [FromBody] CreateCommentDto comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _postRepository.PostExists(postId))
            {
                return BadRequest("Post does not exist");
            }

            var commentModel = comment.ToCreateFromCommentDto(postId);

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = postId }, commentModel.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var commentModel = await _commentRepository.UpdateAsync(id, comment);

            if (commentModel == null) 
            { 
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
         
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok("Comment deleted sucessfuly");
        }
       

        }
    } 

