using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class CountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(float money, float rate, int month)
        {
            double result = Math.Round(money * Math.Pow((1 + rate/100),month),2);
            return View(result);
        }
    }
}
