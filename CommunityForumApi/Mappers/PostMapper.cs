﻿using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Models;

namespace CommunityForumApi.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post postModel)
        {
            return new PostDto
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Content = postModel.Content,
                CreatedDate = postModel.CreatedDate,
                CreatedBy = postModel.AppUser.UserName,
                Comments = postModel.Comments.Select(c => c.ToCommentDto()).ToList()

            };
        }

     public static Post ToPostFromCreateDto (this CreatePostDto createStockModel)
        {
            return new Post
            {
                Title = createStockModel.Title,
                Content = createStockModel.Content,
            };
        }

        public static Post ToPostFromUpdateDto (this UpdatePostDto updateStockModel)
        {
            return new Post
            {
                Title = updateStockModel.Title,
                Content = updateStockModel.Content,
            };
        }
    }
}
