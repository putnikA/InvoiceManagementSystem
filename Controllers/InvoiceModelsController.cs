using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceManagementWebApp.Data;
using InvoiceManagementWebApp.Models;
using System.Diagnostics;

namespace InvoiceManagementWebApp.Controllers
{
    public class InvoiceModelsController : Controller
    {
        private readonly InvoiceModelContext _context;

        public InvoiceModelsController(InvoiceModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Paid(int id)
        {
            var invoice = await _context.InvoiceModel.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            // Logic to mark the invoice as paid (e.g., set IsPaid property)
            invoice.IsPaid = true;

            _context.Update(invoice);
            await _context.SaveChangesAsync();

            // Remove the paid invoice from the list
            var invoices = await _context.InvoiceModel
                .Where(i => !i.IsPaid)
                .Include(i => i.SelectedSupplier)
                .ToListAsync();

            return View("Index", invoices);
        }


        public IActionResult PaidInvoices()
        {
            var paidInvoices = _context.InvoiceModel
                                     .Where(i => i.IsPaid)
                                     .Include(i => i.SelectedSupplier) // Include the navigation property
                                     .ToList();

            return View(paidInvoices);
        }

        public async Task<IActionResult> Index(string searchString, DateTime paymentDeadline)
        {
            if (_context.InvoiceModel == null)
            {
                return Problem("Entity set 'InvoiceModelContext.InvoiceModel' is null.");
            }

            var invoices = from i in _context.InvoiceModel
                           select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(i => i.SelectedSupplier.SupplierName.Contains(searchString));
            }

            if (paymentDeadline != default(DateTime))
            {
                invoices = invoices.Where(i => i.PaymentDeadline.Date == paymentDeadline.Date);
            }

            invoices = invoices.OrderBy(i => i.PaymentDeadline);

            return View(await invoices.Include(i => i.SelectedSupplier).ToListAsync());
        }


        // GET: InvoiceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceModel = await _context.InvoiceModel
                .Include(i => i.SelectedSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoiceModel == null)
            {
                return NotFound();
            }

            return View(invoiceModel);
        }



        // GET: InvoiceModels/Create
        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_context.SupplierModel, "Id", "SupplierName");
            return View();
        }


        // POST: InvoiceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedSupplierId,InvoiceNum,PaymentDeadline,DeliveryDate,ReferenceNumber,Amount")] InvoiceModel invoiceModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new InvoiceModel instance
                var newInvoice = new InvoiceModel
                {
                    SelectedSupplierId = invoiceModel.SelectedSupplierId,
                    InvoiceNum = invoiceModel.InvoiceNum,
                    PaymentDeadline = invoiceModel.PaymentDeadline,
                    DeliveryDate = invoiceModel.DeliveryDate,
                    ReferenceNumber = invoiceModel.ReferenceNumber,
                    Amount = invoiceModel.Amount
                };

                _context.Add(newInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the suppliers dropdown in case of validation error
            ViewBag.Suppliers = new SelectList(_context.SupplierModel, "Id", "SupplierName", invoiceModel.SelectedSupplierId);
            return View(invoiceModel);
        }


        // GET: InvoiceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceModel = await _context.InvoiceModel.FindAsync(id);
            if (invoiceModel == null)
            {
                return NotFound();
            }

            ViewBag.Suppliers = new SelectList(_context.SupplierModel, "Id", "SupplierName", invoiceModel.SelectedSupplierId);
            return View(invoiceModel);
        }

        // POST: InvoiceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectedSupplierId,InvoiceNum,PaymentDeadline,DeliveryDate,ReferenceNumber,Amount")] InvoiceModel invoiceModel)
        {
            if (id != invoiceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Load the existing invoice from the database
                    var existingInvoice = await _context.InvoiceModel.FindAsync(id);
                    if (existingInvoice == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing invoice with the new values
                    existingInvoice.SelectedSupplierId = invoiceModel.SelectedSupplierId;
                    existingInvoice.InvoiceNum = invoiceModel.InvoiceNum;
                    existingInvoice.PaymentDeadline = invoiceModel.PaymentDeadline;
                    existingInvoice.DeliveryDate = invoiceModel.DeliveryDate;
                    existingInvoice.ReferenceNumber = invoiceModel.ReferenceNumber;
                    existingInvoice.Amount = invoiceModel.Amount;

                    _context.Update(existingInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceModelExists(invoiceModel.Id))
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

            ViewBag.Suppliers = new SelectList(_context.SupplierModel, "Id", "SupplierName", invoiceModel.SelectedSupplierId);
            return View(invoiceModel);
        }


        // GET: InvoiceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceModel = await _context.InvoiceModel
                .Include(i => i.SelectedSupplier) // Include the navigation property
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoiceModel == null)
            {
                return NotFound();
            }

            return View(invoiceModel);
        }


        // POST: InvoiceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceModel = await _context.InvoiceModel.FindAsync(id);
            _context.InvoiceModel.Remove(invoiceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceModelExists(int id)
        {
            return _context.InvoiceModel.Any(e => e.Id == id);
        }
    }
}
