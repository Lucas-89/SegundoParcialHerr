using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SegundoParcialHerr.Data;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Services
{
    public class AutorService : IAutorService
    {
        private readonly AutorContext _context;
        public AutorService(AutorContext context)
        {
            _context = context;
        }
        public void Create(Autor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = GetById(id);
            
            if (obj != null)
            {
                _context.Autor.Remove(obj);
                _context.SaveChanges();
            }
           
        }
        public List<Autor> GetAll()
        {
            var query = from autor in _context.Autor select autor;
            return query.ToList();
        }

        public List<Autor> GetAll(string NombreBuscado)
        {
            var query = from autor in _context.Autor select autor;

            if (!string.IsNullOrEmpty(NombreBuscado))
            {
                query = query.Where(x => x.Nombre.ToLower().Contains(NombreBuscado.ToLower()));
            }
            return query.ToList();
        }

        public Autor? GetById(int id)
        {
            var autor = _context.Autor.Include(x=>x.Libros)
                .FirstOrDefault(m => m.Id == id);
            return autor;    
        }

        public void Update(Autor obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
    }
}