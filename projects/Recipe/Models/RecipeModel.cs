using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Models
{
    public class RecipeModel
    {
        [Key]
        public int RecipeId { get;set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string Title { get;set; }
        public string Description { get;set; }
        public string Ingradients { get;set; }
        public string Steps { get;set; }
        public string ImageUrl { get;set; }
        public DateTime CreatedDate { get;set; }
        
    }
}