using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcContract.Models;


namespace MvcContract.Controllers
{
    public class ContractsController : Controller
    {
        private readonly MvcContractContext _context;

        public ContractsController(MvcContractContext context)
        {
            _context = context;
        }

        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public async Task<IActionResult> Index(string contractJobTitle, string searchString)
        {
            // Use LINQ to get list of jobtitles.
            IQueryable<string> jobtitleQuery = from m in _context.Contract
                                            orderby m.JobTitle
                                            select m.JobTitle;
            
            var contracts = from m in _context.Contract
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                contracts = contracts.Where(s => s.FirstName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(contractJobTitle))
            {
                contracts = contracts.Where(x => x.JobTitle == contractJobTitle);
            }

            var contractJobTitleVM = new ContractJobTitleViewModel();
            contractJobTitleVM.jobtitles = new SelectList(await jobtitleQuery.Distinct().ToListAsync());
            contractJobTitleVM.contracts = await contracts.ToListAsync();

            return View(contractJobTitleVM);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,JobTitle,Phone,Email")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ID == id);
            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,JobTitle,Phone,Email")] Contract contract)
        {
            if (id != contract.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ID))
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
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ID == id);
            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ID == id);
        }
    }
}
