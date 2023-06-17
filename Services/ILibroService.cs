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
        void Update(Libro obj );  
        void Delete(Libro obj);
        Libro GetById(int id);
    }
}