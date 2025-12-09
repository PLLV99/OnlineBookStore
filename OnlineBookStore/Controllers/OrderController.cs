using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;
                var bookJson = HttpContext.Session.GetString("SelectedBook");
                if (string.IsNullOrEmpty(bookJson)) return RedirectToAction("Index", "Book");

                var book = JsonConvert.DeserializeObject<Book>(bookJson);
                _context.Orders.Add(new Order
                {
                    BookId = book.BookId,
                    UserId = userId,
                    Total = book.Price * 1.13
                });
                _context.SaveChanges();

                HttpContext.Session.Remove("SelectedBook");
                return View("ThankYou");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = _context.Orders
                                 .Where(o => o.UserId == user.Id)
                                 .Join(_context.Books,
                                       o => o.BookId,
                                       b => b.BookId,
                                       (o, b) => new { o.OrderNum, b.Name, o.Total })
                                 .ToList();

            return View(orders);
        }

    }
}