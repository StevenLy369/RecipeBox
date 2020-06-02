using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Tag
  {
    public Tag()
    {
      this.Recipes = new HashSet<RecipeTag>();
    }

    public int TagId { get; set; }
    public string Word { get; set; }
    public virtual ICollection<RecipeTag> Recipes { get; set; }
  }
}