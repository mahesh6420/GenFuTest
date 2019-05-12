using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenFuTest.Data;
using GenFuTest.Models;
using GenFu;

namespace GenFuTest.Controllers
{
    public class LoanAmountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanAmountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GenFuInsertData()
        {
            A.Configure<LoanAmount>().Fill(a=>a.Id, () => { return 0; });
            A.Configure<LoanAmount>().Fill(a=>a.LoanId).WithinRange(1,25);
            A.Configure<LoanAmount>().Fill(a=>a.CustomerId).WithinRange(1,25);

            List<LoanAmount> loanAmountList = A.ListOf<LoanAmount>();

            await _context.LoanAmount.AddRangeAsync(loanAmountList);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: LoanAmount
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoanAmount.Include(l => l.Customer).Include(l => l.Loan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LoanAmount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanAmount = await _context.LoanAmount
                .Include(l => l.Customer)
                .Include(l => l.Loan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanAmount == null)
            {
                return NotFound();
            }

            return View(loanAmount);
        }

        // GET: LoanAmount/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "City");
            ViewData["LoanId"] = new SelectList(_context.Loan, "Id", "Name");
            return View();
        }

        // POST: LoanAmount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoanId,LoanNumber,CustomerId")] LoanAmount loanAmount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanAmount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "City", loanAmount.CustomerId);
            ViewData["LoanId"] = new SelectList(_context.Loan, "Id", "Name", loanAmount.LoanId);
            return View(loanAmount);
        }

        // GET: LoanAmount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanAmount = await _context.LoanAmount.FindAsync(id);
            if (loanAmount == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "City", loanAmount.CustomerId);
            ViewData["LoanId"] = new SelectList(_context.Loan, "Id", "Name", loanAmount.LoanId);
            return View(loanAmount);
        }

        // POST: LoanAmount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoanId,LoanNumber,CustomerId")] LoanAmount loanAmount)
        {
            if (id != loanAmount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanAmount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanAmountExists(loanAmount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "City", loanAmount.CustomerId);
            ViewData["LoanId"] = new SelectList(_context.Loan, "Id", "Name", loanAmount.LoanId);
            return View(loanAmount);
        }

        // GET: LoanAmount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanAmount = await _context.LoanAmount
                .Include(l => l.Customer)
                .Include(l => l.Loan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanAmount == null)
            {
                return NotFound();
            }

            return View(loanAmount);
        }

        // POST: LoanAmount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanAmount = await _context.LoanAmount.FindAsync(id);
            _context.LoanAmount.Remove(loanAmount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanAmountExists(long id)
        {
            return _context.LoanAmount.Any(e => e.Id == id);
        }
    }
}
