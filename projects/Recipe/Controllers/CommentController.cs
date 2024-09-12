using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Recipe.Data;
using Recipe.Models;

namespace Recipe.Controllers
{
    public class CommentController:Controller
    {
        private readonly ApplicationDbContext _context ;
        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int recipeId, [Bind("Content")] CommentModel comment)
        {
            var recipe = await _context.Comments.FirstOrDefaultAsync(i => i.RecipeId == recipeId);
            if(recipe == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                comment.RecipeId = recipeId;
                comment.UserId = recipe.UserId;


                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Recipe", new { id = recipeId });
            }
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }
        [Authorize]
        public async Task<IActionResult>Edit(int id )
        {
            var comment = await _context.Comments.FindAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id , [Bind("CommentId" , "Content")]CommentModel comment)
        {
            if(id != comment.CommentId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                var existingComment = await _context.Comments.FindAsync(id);
                if(existingComment == null )
                {
                    return NotFound();
                }
                existingComment.Content = comment.Content;

                _context.Comments.Update(existingComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details" , "Recipe");

            }
            return View(comment);
            
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments
            .Include(r =>r.Recipe)
            .FirstOrDefaultAsync(m => m.CommentId ==id);
            if(comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details" , "Recipe" , new { id = comment.CommentId});
        }
    }
}