using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;
using System.Security.Principal;

namespace Expense_Tracker.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Username,Password,ConfirmPassword,Fullname")] RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                if (registerForm.Password == registerForm.ConfirmPassword)
                {
                    Account account = new Account();
                    account.Username = registerForm.Username;
                    account.Password = registerForm.Password;
                    account.Fullname = registerForm.Fullname;

                    var check = _context.Accounts.FirstOrDefault(x => x.Username == account.Username);
                    if (check == null) {
                        _context.Add(account);
                        await _context.SaveChangesAsync();

                        var find = _context.Accounts.FirstOrDefault(c => c.Username == registerForm.Username && c.Password == registerForm.Password);
                        if (find != null)
                        {
                            HttpContext.Session.SetInt32("id", find.AccountId);
                            return Redirect("../Dashboard/Index");
                        }
                    }              
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }

    
}
