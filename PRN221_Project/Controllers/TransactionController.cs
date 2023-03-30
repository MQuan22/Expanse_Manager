using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Project.Models;

namespace PRN221_Project.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            int id = HttpContext.Session.GetInt32("id") ?? 0;
            if (id == 0) return Redirect("../Login/Index");
            var applicationDbContext = _context.Transactions.Include(t => t.Category).Where(x => x.AccountId == id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
                return View(new TransactionForm());
            else
            {
                TransactionForm transactionForm= new TransactionForm();
                Transaction transaction = _context.Transactions.Find(id);
                transactionForm.TransactionId = transaction.TransactionId;
                transactionForm.AccountId = transaction.AccountId;
                transactionForm.CategoryId = transaction.CategoryId;
                transactionForm.Amount = transaction.Amount;
                transactionForm.Date = transaction.Date;
                transactionForm.Note = transaction.Note;
                return View(transactionForm);
            }
                
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,AccountId,Amount,Note,Date")] TransactionForm transactionForm)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction();
                transaction.TransactionId = transactionForm.TransactionId;
                transaction.Amount = transactionForm.Amount;
                transaction.Note = transactionForm.Note;
                transaction.Date = transaction.Date;
                transaction.CategoryId = transactionForm.CategoryId;
                transaction.AccountId = HttpContext.Session.GetInt32("id") ?? 0;
                transaction.Account = _context.Accounts.FirstOrDefault(c => c.AccountId == transaction.AccountId);
                transaction.Category = _context.Categories.FirstOrDefault(c => c.CategoryId == transaction.CategoryId);
                if (transaction.TransactionId == 0)
                    _context.Add(transaction);
                else
                    _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transactionForm);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category", Type = "" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
