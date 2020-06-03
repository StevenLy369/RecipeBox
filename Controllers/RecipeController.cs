using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
  [Authorize]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id);
      return View(userRecipes);
    }

    public ActionResult Create()
    {
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Word");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe recipe, int TagId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      _db.Recipes.Add(recipe);
      if (TagId != 0)
      {
        _db.RecipeTag.Add(new RecipeTag() { TagId = TagId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(recipe => recipe.Tags)
          .ThenInclude(join => join.Tag)
          .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost]
    public async Task<ActionResult> MarkAsMadeIt(Recipe recipe, int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      recipe.User = currentUser;
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      thisRecipe.MadeIt = true;
      _db.Entry(thisRecipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Word");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int TagId)
    {
      if (TagId != 0)
      {
        _db.RecipeTag.Add(new RecipeTag() { TagId = TagId, RecipeId = recipe.RecipeId });
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Word");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int TagId)
    {
      if (TagId != 0)
      {
        _db.RecipeTag.Add(new RecipeTag() { TagId = TagId, RecipeId = recipe.RecipeId });
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTag(int joinId)
    {
      var joinEntry = _db.RecipeTag.FirstOrDefault(entry => entry.RecipeTagId == joinId);
      _db.RecipeTag.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("/search")]
    public ActionResult Search(string search, string searchParam)
    {

      if (!string.IsNullOrEmpty(search))
      {
        if (searchParam == "Tags")
        {
          // List<Tag> model2 = _db.Tags.Include(Tags => Tags.Recipes).ToList();
          // Tag match2 = new Tag();
          // List<Tag> matches2 = new List<Tag> { };
          // foreach (Tag tag in model2)
          // {
          //   if (tag.Word.ToLower().Contains(search))
          //   {
          //     matches2.Add(tag);
          //   }
          // }
          // return View(matches2);

          // var model = from t in _db.Tags select t;
          // model = model.Where(t => t.Word.Contains(search));
          // List<Tag> matches = model.ToList();
          // return View(matches);

          var thisTag = _db.Tags
          .Include(tag => tag.Recipes)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(tag => tag.Word == search);
          return View(thisTag.Recipes);

        }
        else if (searchParam == "Name")
        {
          var model = from r in _db.Recipes select r;
          model = model.Where(r => r.Name.Contains(search));
          List<Recipe> matches = model.ToList();
          return View(matches);
        }
        else
        {
          return RedirectToAction("Index");
        }
      }
      else
      {
        return RedirectToAction("Index");
      }
    }

    // [HttpGet("/search")]
    // public ActionResult Search(string search, string searchParam)
    // {
    //   if (!string.IsNullOrEmpty(search))
    //   {
    //     if (searchParam == "Book")
    //     {
    //       var model = from m in _db.Books select m;
    //       model = model.Where(n => n.Title.Contains(search));
    //       List<Book> matchesBook = new List<Book> { };
    //       matchesBook = model.ToList();
    //       return View(matchesBook);

    //     }
    //     else
    //     {
    //       var model = from m in _db.Authors select m;
    //       model = model.Where(n => n.Name.Contains(search));
    //       List<Author> matchesAuthor = new List<Author> { };
    //       matchesAuthor = model.ToList();
    //       return View(matchesAuthor);
    //     }
    //   }
    //   else
    //   {
    //     var model = from m in _db.Books select m;
    //     List<Book> allBooks = new List<Book> { };
    //     allBooks = model.ToList();
    //     return View(allBooks);
    //   }
    // }

  }
}





























// public ViewResult Index(string searchString)
//     {
//       ViewBag.NameSortParm = String.IsNullOrEmpty() ? "name_desc" : "";
//       var recipes = from r in _db.Recipes 
//                      select r;
//       if (!String.IsNullOrEmpty(searchString))
//       {
//         students = students.Where(r => r.LastName.Contains(searchString)
//                                || r.FirstMidName.Contains(searchString));
//       }
//       return View(searchString);
//     }