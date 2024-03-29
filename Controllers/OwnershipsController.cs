using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryFinal.Controllers
{
    public class OwnershipsController : Controller
    {
        private readonly LibraryFinalContext _context;

        public OwnershipsController(LibraryFinalContext context)
        {
            _context = context;
        }

        // GET: Ownerships
        public async Task<IActionResult> Index()
        {
            var libraryFinalContext = _context.Ownerships.Include(o => o.Book).Include(o => o.User);
            return View(await libraryFinalContext.ToListAsync());
        }

        // GET: Ownerships/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownerships
                .Include(o => o.Book)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownership == null)
            {
                return NotFound();
            }

            return View(ownership);
        }

        // GET: Ownerships/Create
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            var books = await _context.Books.Select(b => new SelectListItem { Text = b.Title, Value = b.Id.ToString() }).ToListAsync();
            ViewBag.Books = books;

            var users = await _context.Users.Select(u => new SelectListItem { Text = u.UserName, Value = u.Id.ToString() }).ToListAsync();
            ViewBag.Users = users;

            return View();
        }

        // POST: Ownerships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(Ownership ownership)
        {
            if (ModelState.IsValid)
            {
                ownership.Id = Guid.NewGuid(); // Genera un nuevo GUID para el Id
                _context.Add(ownership);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Manejar las excepciones de integridad aquí
                    ModelState.AddModelError(string.Empty, "Error al guardar el ownership.");
                }
            }
            return View(ownership);
        }


        // GET: Ownerships/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownerships.FindAsync(id);
            if (ownership == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", ownership.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ownership.UserId);
            return View(ownership);
        }

        // POST: Ownerships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BookId")] Ownership ownership)
        {
            if (id != ownership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnershipExists(ownership.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", ownership.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ownership.UserId);
            return View(ownership);
        }

        // GET: Ownerships/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownership = await _context.Ownerships
                .Include(o => o.Book)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ownership == null)
            {
                return NotFound();
            }

            return View(ownership);
        }

        // POST: Ownerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ownership = await _context.Ownerships.FindAsync(id);
            if (ownership != null)
            {
                _context.Ownerships.Remove(ownership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnershipExists(Guid id)
        {
            return _context.Ownerships.Any(e => e.Id == id);
        }
    }
}
