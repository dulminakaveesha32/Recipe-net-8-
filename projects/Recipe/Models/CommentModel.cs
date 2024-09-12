using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get;set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get;set; }
        public RecipeModel Recipe { get;set; }
        [ForeignKey("User")]
        public int UserId { get;set; }
        public UserModel User { get;set; }
        public string Content { get;set; }
    }
}