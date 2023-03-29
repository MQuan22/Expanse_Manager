using Microsoft.AspNetCore.Mvc;
using PRN221_Project.Models;

namespace PRN221_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password")] LoginForm account)
        {
            if (ModelState.IsValid)
            {
                var find = _context.Accounts.FirstOrDefault(c => c.Username == account.Username && c.Password == account.Password);
                if (find != null)
                {
                    HttpContext.Session.SetInt32("id", find.AccountId);
                    return Redirect("../Dashboard/Index");
                }
                else return Redirect("../Home/Privacy");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
