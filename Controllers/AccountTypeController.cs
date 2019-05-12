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
    public class AccountTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GenFuInsertData()
        {
            A.Configure<AccountType>().Fill(a=>a.Id, () => { return 0; });

            List<AccountType> accountTypeList = A.ListOf<AccountType>();

            await _context.AccountType.AddRangeAsync(accountTypeList);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: AccountType
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountType.ToListAsync());
        }

        // GET: AccountType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountType = await _context.AccountType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountType == null)
            {
                return NotFound();
            }

            return View(accountType);
        }

        // GET: AccountType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountType);
        }

        // GET: AccountType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountType = await _context.AccountType.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }
            return View(accountType);
        }

        // POST: AccountType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AccountType accountType)
        {
            if (id != accountType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountTypeExists(accountType.Id))
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
            return View(accountType);
        }

        // GET: AccountType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountType = await _context.AccountType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountType == null)
            {
                return NotFound();
            }

            return View(accountType);
        }

        // POST: AccountType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountType = await _context.AccountType.FindAsync(id);
            _context.AccountType.Remove(accountType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountTypeExists(long id)
        {
            return _context.AccountType.Any(e => e.Id == id);
        }
    }
}
