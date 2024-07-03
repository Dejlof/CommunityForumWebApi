﻿using System.ComponentModel.DataAnnotations;

namespace CommunityForumApi.Dtos.Account
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }

       
        public string? EmailAddress { get; set; }

       
        public string? PhoneNumber { get; set; }

    }
}
