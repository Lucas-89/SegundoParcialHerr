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
using SegundoParcialHerr.Services;


namespace SegundoParcialHerr.Controllers
{
    public class LibroController : Controller
    {
        // private readonly AutorContext _context;
        private readonly ILibroService _libroService;
        private readonly IAutorService _autorService;

        public LibroController(ILibroService libroService, IAutorService autorService)
        {
            _libroService = libroService;
            _autorService = autorService;
        }

        // GET: Libro
        public IActionResult Index(string NombreBuscado)
        {
            // var query = from libro in _context.Libro select libro;
            // if (!string.IsNullOrEmpty(NombreBuscado))
            // {
            //     query = query.Where(x =>x.Titulo.ToLower().Contains(NombreBuscado));
            // }
            var model = new LibroViewModel();
            model.Libros = _libroService.GetAll(NombreBuscado);
            // var autorContext = _context.Libro.Include(l => l.Autor);
            return View(model);
        }

        // GET: Libro/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            // var libro = await _context.Libro
            //     .Include(l => l.Autor)
            //     .FirstOrDefaultAsync(m => m.Id == id);
            var libro = _libroService.GetById(id.Value);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Titulo,Genero,Precio,Stock,AutorId,autores")] LibroCreateViewModel libro)
        {
            var autores = _autorService.GetAll(); //VER ESTA PARTE

            var libroNuevo = new Libro();
            libroNuevo.Id = libro.Id;
            libroNuevo.Titulo = libro.Titulo;
            libroNuevo.Genero = libro.Genero;
            libroNuevo.Precio = libro.Precio;
            libroNuevo.Stock = libro.Stock;
            libroNuevo.AutorId = libro.AutorId; // ACA TAMBIEN
            
            // ModelState.Remove("A");
            if (ModelState.IsValid)
            {
                // _context.Add(libroNuevo);
                // await _context.SaveChangesAsync();
                _libroService.Create(libroNuevo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var libro = _libroService.GetById(id.Value);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre", libro.AutorId);
            return View(libro);
        } 

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Titulo,Genero,Precio,Stock,AutorId")] LibroCreateViewModel libro)
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
                    _libroService.Update(libroNuevo);
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
            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre", libro.AutorId);
            return View(libro);
        }

        // GET: Libro/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = _libroService.GetById(id.Value);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // if (_context.Libro == null)
            // {
            //     return Problem("Entity set 'AutorContext.Libro'  is null.");
            // }

            _libroService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return _libroService.GetById(id) != null;
        }
    }
}
