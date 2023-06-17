using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Services
{
    public interface ILibroService
    {
        void Create(Libro obj);
        List<Libro> GetAll();
        List<Libro> GetAll(string NombreBuscado);
        void Update(Libro obj );  
        void Delete(int id);
        Libro? GetById(int id);
    }
}