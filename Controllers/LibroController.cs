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
using Microsoft.AspNetCore.Authorization;

namespace SegundoParcialHerr.Controllers
{
    public class LibroController : Controller
    {
        // private readonly AutorContext _context;
        private readonly ILibroService _libroService;
        private readonly IAutorService _autorService;
        private readonly ISucursalService _sucursalService;

        public LibroController(ILibroService libroService, IAutorService autorService, ISucursalService sucursalService)
        {
            _libroService = libroService;
            _autorService = autorService;
            _sucursalService = sucursalService;
        }

        // GET: Libro
        public IActionResult Index(string NombreBuscado)
        {
            // var query = from libro in _context.Libro select libro;
            // if (!string.IsNullOrEmpty(NombreBuscado))
            // {
            //     query = query.Where(x =>x.Titulo.ToLower().Contains(NombreBuscado));
            // }
            // var model = new LibroViewModel();
            var libros =  _libroService.GetAll(NombreBuscado);

            var listaVM = new List<LibroViewModel>();
            foreach (var item in libros)
            {
                var libroVM = new LibroViewModel(){
                    Id = item.Id,
                    Titulo = item.Titulo,
                    Genero = item.Genero,
                    Precio = item.Precio,
                    Stock = item.Stock,
                    AutorNombre = item.Autor.Nombre,
                    // Libros = libros
                };
                listaVM.Add(libroVM);
            }

            // var autorContext = _context.Libro.Include(l => l.Autor);
            return View(listaVM);
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
        [Authorize(Roles = "Administrador")]
        
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
        [Authorize(Roles = "Administrador, Usuario")]

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


        public IActionResult UpdateStock(int? id){
           if (id == null )
            {
                return NotFound();
            }

            var libro = _libroService.GetById(id.Value);
            if (libro == null)
            {
                return NotFound();
            }
            var libroUp = new LibroUpdateStockViewModel(){
                Id = libro.Id,
                Titulo = libro.Titulo,
                Precio = libro.Precio,
                Stock = libro.Stock
            };
            ViewData ["Sucursales"] = new SelectList(_sucursalService.GetAll(),"Id","Nombre");
            return View(libroUp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStock(LibroUpdateStockViewModel model){
            var libro = _libroService.GetById(model.Id);
            if (libro == null)
            {
                return NotFound();
            }

            if (model.CantidadCompra< libro.Stock)
            {
                libro.Stock = libro.Stock - model.CantidadCompra;
                _libroService.Update(libro); 
            }

            return RedirectToAction("Index");
        }
        // GET: Libro/Delete/5
        [Authorize(Roles = "Administrador")]

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
