using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.Data;
using Microsoft.EntityFrameworkCore;

namespace SegundoParcialHerr.Services
{
    public class LibroService : ILibroService
    {
        private readonly AutorContext _context;
        public LibroService(AutorContext context)
        {
            _context = context;
        }
        public void Create(Libro obj)
        {
            _context.Add(obj);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var obj =GetById(id);
            
            if (obj != null)
            {
                _context.Libro.Remove(obj);
                _context.SaveChanges();
            }
            
        }
        public List<Libro> GetAll()
        {
            var query = from libro in _context.Libro select libro;
            return query.ToList();
        }

        public List<Libro> GetAll(string NombreBuscado)
        {
            var query = from libro in _context.Libro select libro;

            if (!string.IsNullOrEmpty(NombreBuscado))
            {
                query = query.Where(x =>x.Titulo.ToLower().Contains(NombreBuscado));
            }
            return query.ToList();
        }

        public Libro? GetById(int id)
        {
            var libro = _context.Libro.Include(l => l.Autor)
                .FirstOrDefault(m => m.Id == id);
            
            return libro;
        }

        public void Update(Libro obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
    }
}