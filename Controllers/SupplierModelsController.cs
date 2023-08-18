using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceManagementWebApp.Data;
using InvoiceManagementWebApp.Models;

namespace InvoiceManagementWebApp.Controllers
{
    public class SupplierModelsController : Controller
    {
        private readonly InvoiceModelContext _context;

        public SupplierModelsController(InvoiceModelContext context)
        {
            _context = context;
        }

        // GET: SupplierModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.SupplierModel.ToListAsync());
        }

        // GET: SupplierModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierModel = await _context.SupplierModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierModel == null)
            {
                return NotFound();
            }

            return View(supplierModel);
        }

        // GET: SupplierModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplierModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierName,SupplierAdress,BankAccountNumber")] SupplierModel supplierModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierModel);
        }

        // GET: SupplierModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierModel = await _context.SupplierModel.FindAsync(id);
            if (supplierModel == null)
            {
                return NotFound();
            }
            return View(supplierModel);
        }

        // POST: SupplierModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierName,SupplierAdress,BankAccountNumber")] SupplierModel supplierModel)
        {
            if (id != supplierModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierModelExists(supplierModel.Id))
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
            return View(supplierModel);
        }

        // GET: SupplierModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierModel = await _context.SupplierModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierModel == null)
            {
                return NotFound();
            }

            return View(supplierModel);
        }

        // POST: SupplierModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplierModel = await _context.SupplierModel.FindAsync(id);
            _context.SupplierModel.Remove(supplierModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierModelExists(int id)
        {
            return _context.SupplierModel.Any(e => e.Id == id);
        }
    }
}
