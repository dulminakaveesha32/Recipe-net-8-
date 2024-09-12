using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe.Data;
using Recipe.Models;

namespace Recipe.Controllers
{
    public class RecipeController:Controller
    {
        private readonly ApplicationDbContext _context ;
        public RecipeController(ApplicationDbContext context)
        {
            _context =context;
        }
        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes
            .Include(t =>t.User)
            .ToListAsync();
            return View(recipes);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var recipe =await _context.Recipes
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        } 
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("RecipeId,Title,Description,Ingredients,Steps,ImageUrl")] RecipeModel model)
        {
            if(ModelState.IsValid)
            {
                _context.Recipes.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
         {
            if(id == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if(recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
         }
         [HttpPost]
         [ValidateAntiForgeryToken]
         [Authorize]
        
        public async Task<IActionResult> Edit(int id , [Bind("RecipeId,Title,Description,Ingredients,Steps,ImageUrl")] RecipeModel model)
        {
            if(id != model.RecipeId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var recipe = _context.Recipes.FindAsync(id);
            if(recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id )
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}