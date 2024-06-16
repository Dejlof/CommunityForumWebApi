﻿using CommunityForumApi.Dtos.Comment;
using CommunityForumApi.Models;

namespace CommunityForumApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto (this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedDate = commentModel.CreatedDate,
                PostId = commentModel.PostId,
            };
        }

        public static Comment ToCreateFromCommentDto (this CreateCommentDto createCommentModel)
        {
            return new Comment
            {
                Content = createCommentModel.Content,
            };
        }

        public static Comment ToUpdateFromCommentDto(this UpdateCommentDto updateCommentModel)
        {
            return new Comment
            {
                Content = updateCommentModel.Content,
            };
        }
    }
}
