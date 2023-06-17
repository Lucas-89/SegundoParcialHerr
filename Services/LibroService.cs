using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.Data;


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
            throw new NotImplementedException();
        }

        public void Delete(Libro obj)
        {
            _context.Libro.Remove(obj);
           _context.SaveChanges();
        }

        public List<Libro> GetAll()
        {
            throw new NotImplementedException();
        }

        public Libro GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Libro obj)
        {
            throw new NotImplementedException();
        }
    }
}