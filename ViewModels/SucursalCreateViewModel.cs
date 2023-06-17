using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class SucursalCreateViewModel
    {
        // implementar al final
        public int Id {get;set;}
        public string NombreSucursal {get;set;}
        public string Direccion {get;set;}
        public string Localidad {get;set;}
        public List<int> LibroId {get;set;}
       // public Libro Libro {get;set;}
    }
}