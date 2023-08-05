using System.ComponentModel.DataAnnotations;

namespace BaddEcon.Web.Models;

// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio
public class Movie
{
   public int Id { get; set; }
   public string? Title { get; set; }
   [DataType(DataType.Date)]
   public DateTime ReleaseDate { get; set; }
   public string? Genre { get; set; }
   public decimal Price { get; set; }
}
