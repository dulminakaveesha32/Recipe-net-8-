using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Recipe.Models
{
    public class UserModel:IdentityUser
    {
        [Key]
        public int UserId { get;set;}
        public string Avatar { get;set; }
        public string Email { get;set; }
        public string PasswordHash { get;set;}
        public string ProfileImageUrl { get;set; }
        public string Bio { get; set; }
        public DateTime CreatedDate { get;set; }
        
    }
}