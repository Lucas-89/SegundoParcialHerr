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
            var libroVM = new LibroViewModel();
            libroVM.Libros =  _libroService.GetAll(NombreBuscado);

            // var listaVM = new List<LibroViewModel>();
            // foreach (var item in libros)
            // {
            //     var libroVM = new LibroViewModel(){
            //         Id = item.Id,
            //         Titulo = item.Titulo,
            //         Genero = item.Genero,
            //         Precio = item.Precio,
            //         Stock = item.Stock,
            //         AutorNombre = item.Autor.Nombre,
            //         // Libros = libros
            //     };
            //     listaVM.Add(libroVM);
            // }

            // var autorContext = _context.Libro.Include(l => l.Autor);
            return View(libroVM);
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
            ViewData["Sucursales"] = new SelectList(_sucursalService.GetAll(), "Id", "NombreSucursal");
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Titulo,Genero,Precio,Stock,AutorId,autores,SucursalesId")] LibroCreateViewModel libro)
        {
            var autores = _autorService.GetAll(); // esto lo tengo que usar en el ViewData. Es mi lista de Autores

            //ahora necesito mi lista de Sucursales

            var sucursales = _sucursalService.GetAll().Where(x=> libro.SucursalesId.Contains(x.Id)).ToList(); // Tengo que hacer un segundo VData para usar esto
                                                        // Pero ¿puede que como uso el servicio no necesite el VData?

            var libroNuevo = new Libro();
            libroNuevo.Id = libro.Id;
            libroNuevo.Titulo = libro.Titulo;
            libroNuevo.Genero = libro.Genero;
            libroNuevo.Precio = libro.Precio;
            libroNuevo.Stock = libro.Stock;
            libroNuevo.AutorId = libro.AutorId; 
            libroNuevo.Sucursales = sucursales; // uso la lista de sucursales a ver si se puede

            // ModelState.Remove("A");
            if (ModelState.IsValid)
            {
                // _context.Add(libroNuevo);
                // await _context.SaveChangesAsync();
                _libroService.Create(libroNuevo);
                return RedirectToAction(nameof(Index));
            }

            // este VData sirve de algo? en caso que el modelo no sea valido, al recargar la pagina
            // vuelve a rellenar la lista de autores
            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre", libro.AutorId);
            ViewData["Sucursales"] = new SelectList(_sucursalService.GetAll(), "Id", "NombreSucursal", libro.SucursalesId);
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

            // tengo que crear un libroVM y completar los campos.
            // Tambien tengo que revisar que recibe la vista: Model o VModel?

            var libroVM = new LibroCreateViewModel();
            libroVM.Id = libro.Id;
            libroVM.Titulo = libro.Titulo;
            libroVM.Genero = libro.Genero;
            libroVM.Precio = libro.Precio;
            libroVM.Stock = libro.Stock;
            libroVM.AutorId = libro.AutorId;
            // libroVM.SucursalesId = ;  ACA DEBERIA HACER UN VData para mostrar la lista? SI
            // ademas lo tengo que mostrar en el View Edit

            ViewData["AutorId"] = new SelectList(_autorService.GetAll(), "Id", "Nombre", libro.AutorId);

            var sucursalList = _sucursalService.GetAll();
            ViewData["Sucursales"] = new SelectList(sucursalList,"Id", "NombreSucursal",libro.Sucursales);

            return View(libroVM);
        } 

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Titulo,Genero,Precio,Stock,AutorId,SucursalesId")] LibroCreateViewModel libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // traigo las sucursales que tiene el libro
                var sucursales = _sucursalService.GetAll().Where(x=>libro.SucursalesId.Contains(x.Id)).ToList();

                // deberia poder traer el autor con el servicio de _autor
                var autor = _autorService.GetById(libro.AutorId);

                var libroNuevo = _libroService.GetById(libro.Id);
                // libroNuevo.Id = libro.Id;
                libroNuevo.Titulo = libro.Titulo;
                libroNuevo.Genero = libro.Genero;
                libroNuevo.Precio = libro.Precio;
                libroNuevo.Stock = libro.Stock;
                // libroNuevo.Autor = autor;
                libroNuevo.AutorId= libro.AutorId;
                // libroNuevo.Sucursales = sucursales;
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
            ViewData["Sucursales"]= new SelectList(_sucursalService.GetAll(),"Id","NombreSucursal",libro.SucursalesId);
            return View(libro);
        }


        public IActionResult UpdateStock(int? id, int cantCompra){
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

            if (model.CantidadCompra<= libro.Stock)
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
