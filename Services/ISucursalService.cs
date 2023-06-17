using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Services
{
    public interface ISucursalService
    {
        void Create(Sucursal obj);
        List<Sucursal> GetAll(string NombreBuscado);
        void Update(Sucursal obj );  
        void Delete(int id);
        Sucursal? GetById(int id);
    }
}