using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

namespace Recipe.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {

        }
        public DbSet<RecipeModel> Recipes { get;set; }
        public DbSet<CommentModel> Comments { get;set; }
        
        
    }
}