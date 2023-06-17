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
 public class SucursalController : Controller
    {
        // private readonly AutorContext _context;
        private ISucursalService _sucursalService;
        private ILibroService _librolService;

        public SucursalController(ISucursalService sucursalService, ILibroService libroService)
        {
            _sucursalService = sucursalService;
            _librolService = libroService;
        }

        // GET: Sucursal
        public async Task<IActionResult> Index(string NombreBuscado)
        {
            // var query = from sucursal in _context.Autor select sucursal;

            // if (!string.IsNullOrEmpty(NombreBuscado))
            // {
            //     query = query.Where(x => x.Nombre.ToLower().Contains(NombreBuscado.ToLower()));
            // }

            var model = new SucursalViewModel();
            model.Sucursales = _sucursalService.GetAll(NombreBuscado);

              return View(model);
        }

        // GET: Sucursal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = _sucursalService.GetById(id.Value);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // GET: Sucursal/Create
        public IActionResult Create()
        {
            ViewData["Libros"] = new SelectList(_librolService.GetAll(), "Id","Titulo"); 
            return View();
        }

        // POST: Sucursal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSucursal,Direccion,Localidad,LibroId")] SucursalCreateViewModel sucursalView)
        {
            

            if (ModelState.IsValid)
            {   
                var libros = _librolService.GetAll(); //traigo todos los libros y los guardo en la variable

                var sucursal = new Sucursal();
                sucursal.Id = sucursalView.Id;
                sucursal.NombreSucursal = sucursalView.NombreSucursal;
                sucursal.Direccion = sucursalView.Direccion;
                sucursal.Localidad = sucursalView.Localidad;
                sucursal.Libros = libros;

                _sucursalService.Create(sucursal);
                return RedirectToAction(nameof(Index));
            }
            return View(sucursalView);
        }

        // GET: Sucursal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = _sucursalService.GetById(id.Value);
            if (sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreSucursal,Direccion,Localidad")] SucursalCreateViewModel sucursalView)
        {
            if (id != sucursalView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var sucursal = new Sucursal();
                sucursal.Id = sucursalView.Id;
                sucursal.NombreSucursal = sucursalView.NombreSucursal;
                sucursal.Direccion = sucursalView.Direccion;
                sucursal.Localidad = sucursalView.Localidad;

                try
                {
                    _sucursalService.Update(sucursal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.Id))
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
            return View(sucursalView);
        }

        // GET: Sucursal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = _sucursalService.GetById(id.Value);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // if (_context.Sucursal == null)
            // {
            //     return Problem("Entity set 'AutorContext.Sucursal'  is null.");
            // }
            _sucursalService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalExists(int id)
        {
          return _sucursalService.GetById(id) != null;
        }
    }
}