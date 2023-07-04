using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SegundoParcialHerr.Data;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.Services;
using SegundoParcialHerr.ViewModels;

namespace SegundoParcialHerr.Controllers
{
    public class AutorController : Controller
    {
        private IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        // GET: Autor
        public IActionResult Index(string NombreBuscado)
        {
            var model = new AutorViewModel();
            model.Autores= _autorService.GetAll(NombreBuscado);

              return View(model);
        }

        // GET: Autor/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var autor = _autorService.GetById(id.Value);
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
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,Nacionalidad,Contemporaneo")] AutorCreateViewModel autor) 
        {
           var autorModel = new Autor();
            autorModel.Id = autor.Id;
            autorModel.Nombre = autor.Nombre;
            autorModel.Nacionalidad = autor.Nacionalidad;
            autorModel.Contemporaneo = autor.Contemporaneo;
            if (ModelState.IsValid)
            {
                // _context.Add(autorModel);
                // await _context.SaveChangesAsync();
                _autorService.Create(autorModel);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Edit/5
        [Authorize(Roles = "Administrador, Usuario")]

        public IActionResult Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var autor = _autorService.GetById(id.Value);
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
        public IActionResult Edit(int id, [Bind("Id,Nombre,Nacionalidad,Contemporaneo")] AutorCreateViewModel autorView)
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
                    _autorService.Update(autor);
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

        [Authorize(Roles = "Administrador")]

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var autor = _autorService.GetById(id.Value);
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
            // if (_context.Autor == null)
            // {
            //     return Problem("Entity set 'AutorContext.Autor'  is null.");
            // }  PONER LAS VALIDACIONES EN EL SERVICE
            
           _autorService.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
          return _autorService.GetById(id) != null;
        }
    }
}
