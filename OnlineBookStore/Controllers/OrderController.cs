using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context) { _context = context; }

        public IActionResult AddToCart(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            HttpContext.Session.SetString("SelectedBook", JsonConvert.SerializeObject(book));

            Response.Cookies.Append("LastBookId", id.ToString(), new CookieOptions { Expires = DateTime.Now.AddMinutes(10) });

            return RedirectToAction("ConfirmOrder");
        }

        public IActionResult ConfirmOrder()
        {
           

            var bookJson = HttpContext.Session.GetString("SelectedBook");
            if (string.IsNullOrEmpty(bookJson)) return RedirectToAction("Index", "Book");

            var book = JsonConvert.DeserializeObject<Book>(bookJson);
            ViewBag.Book = book;
            ViewBag.Tax = book.Price * 0.13;
            ViewBag.Total = book.Price * 1.13;
            ViewBag.CookieValue = Request.Cookies["LastBookId"];

            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            try
            {
                var bookJson = HttpContext.Session.GetString("SelectedBook");
                if (string.IsNullOrEmpty(bookJson)) return RedirectToAction("Index", "Book");

                var book = JsonConvert.DeserializeObject<Book>(bookJson);
                _context.Orders.Add(new Order { BookId = book.BookId, Total = book.Price * 1.13 });
                _context.SaveChanges();

                HttpContext.Session.Remove("SelectedBook");
                return View("ThankYou");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }
    }
}