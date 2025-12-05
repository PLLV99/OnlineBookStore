using System.ComponentModel.DataAnnotations;
namespace OnlineBookStore.Models
{
    public class Book
    {
        [Key] public int BookId { get; set; }
        [Required] public string ISBN { get; set; }
        [Required] public string Name { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
    }
}