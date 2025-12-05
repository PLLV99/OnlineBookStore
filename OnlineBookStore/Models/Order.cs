using System.ComponentModel.DataAnnotations;
namespace OnlineBookStore.Models
{
    public class Order
    {
        [Key] public int OrderNum { get; set; }
        public int BookId { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}