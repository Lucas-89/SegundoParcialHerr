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
    public class AutorController : Controller
    {
        private readonly AutorContext _context;

        public AutorController(AutorContext context)
        {
            _context = context;
        }

        // GET: Autor
        public async Task<IActionResult> Index(string NombreBuscado)
        {
            var query = from autor in _context.Autor select autor;

            if (!string.IsNullOrEmpty(NombreBuscado))
            {
                query = query.Where(x => x.Nombre.ToLower().Contains(NombreBuscado.ToLower()));
            }

            var model = new AutorViewModel();
            model.Autores= await query.ToListAsync();

              return _context.Autor != null ? 
                          View(model) :
                          Problem("Entity set 'AutorContext.Autor'  is null.");
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Autor == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor.Include(x=>x.Libros)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            var viewModel = new AutorDetailViewModel();
            viewModel.Nombre = autor.Nombre;
            viewModel.Nacionalidad = autor.Nacionalidad;
            viewModel.Contemporaneo = autor.Contemporaneo;
            viewModel.Libros= autor.Libros !=null? autor.Libros : new List<Libro>();

            return View(viewModel);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Nacionalidad,Contemporaneo")] AutorCreateViewModel autor)
        {
           var autorModel = new Autor();
            autorModel.Id = autor.Id;
            autorModel.Nombre = autor.Nombre;
            autorModel.Nacionalidad = autor.Nacionalidad;
            autorModel.Contemporaneo = autor.Contemporaneo;
            if (ModelState.IsValid)
            {
                _context.Add(autorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Autor == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Nacionalidad,Contemporaneo")] AutorCreateViewModel autorView)
        {
            if (id != autorView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var autor = new Autor();
                autor.Id = autorView.Id;
                autor.Nombre = autorView.Nombre;
                autor.Nacionalidad = autorView.Nacionalidad;
                autor.Contemporaneo = autorView.Contemporaneo;

                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
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
            return View(autorView);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Autor == null)
            {
                return NotFound();
            }

            var autor = await _context.Autor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Autor == null)
            {
                return Problem("Entity set 'AutorContext.Autor'  is null.");
            }
            var autor = await _context.Autor.FindAsync(id);
            if (autor != null)
            {
                _context.Autor.Remove(autor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
          return (_context.Autor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
