using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SegundoParcialHerr.Data;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.ViewModels;

namespace SegundoParcialHerr.Controllers
{
    public class LibroController : Controller
    {
        private readonly AutorContext _context;

        public LibroController(AutorContext context)
        {
            _context = context;
        }

        // GET: Libro
        public async Task<IActionResult> Index(string NombreBuscado)
        {
            var query = from libro in _context.Libro select libro;
            if (!string.IsNullOrEmpty(NombreBuscado))
            {
                query = query.Where(x =>x.Titulo.ToLower().Contains(NombreBuscado));
            }
            var model = new LibroViewModel();
            model.Libros = await query.ToListAsync();
            var autorContext = _context.Libro.Include(l => l.Autor);
            return View(model);
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nombre");
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Genero,Precio,Stock,AutorId,autores")] LibroCreateViewModel libro)
        {
            var autores = from autor in _context.Autor select autor;

            var libroNuevo = new Libro();
            libroNuevo.Id = libro.Id;
            libroNuevo.Titulo = libro.Titulo;
            libroNuevo.Genero = libro.Genero;
            libroNuevo.Precio = libro.Precio;
            libroNuevo.Stock = libro.Stock;
            libroNuevo.AutorId = libro.AutorId;
            
            // ModelState.Remove("A");
            if (ModelState.IsValid)
            {
                _context.Add(libroNuevo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Genero,Precio,Stock,AutorId")] LibroCreateViewModel libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var libroNuevo = new Libro();
                libroNuevo.Id = libro.Id;
                libroNuevo.Titulo = libro.Titulo;
                libroNuevo.Genero = libro.Genero;
                libroNuevo.Precio = libro.Precio;
                libroNuevo.Stock = libro.Stock;
                libroNuevo.AutorId = libro.AutorId;
                try
                {
                    _context.Update(libroNuevo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libroNuevo.Id)) // tengo dudas aca
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
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libro == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libro == null)
            {
                return Problem("Entity set 'AutorContext.Libro'  is null.");
            }
            var libro = await _context.Libro.FindAsync(id);
            if (libro != null)
            {
                _context.Libro.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
