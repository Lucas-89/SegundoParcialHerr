using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Services
{
    public interface IAutorService
    {
        void Create(Autor obj);
        List<Autor> GetAll();
        List<Autor> GetAll(string NombreBuscado);
        void Update(Autor obj );  
        void Delete(int id);
        Autor? GetById(int id);
    }
}