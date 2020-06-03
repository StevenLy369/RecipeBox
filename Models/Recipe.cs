using System.Collections.Generic;
using System;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.Tags = new List<RecipeTag>();
    }

    public int RecipeId { get; set; }
    public string Name { get; set; }
    public int Minutes { get; set; }
    public string Instructions { get; set; }
    public string Ingredients { get; set; }
    public int Rating { get; set; }
    public bool MadeIt { get; set; }
    public virtual ApplicationUser User { get; set; }
    public ICollection<RecipeTag> Tags { get; }
  }
}