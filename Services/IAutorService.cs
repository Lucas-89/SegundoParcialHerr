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
        void Update(Autor obj );  
        void Delete(Autor obj);
        Autor GetById(int id);
    }
}