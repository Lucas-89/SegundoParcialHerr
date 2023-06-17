using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SegundoParcialHerr.Models
{
    public class Sucursal
    {
        public int Id {get;set;}
        public string NombreSucursal {get;set;}
        public string Direccion {get;set;}
        public string Localidad{get;set;}

        public virtual List<Libro> Libros {get;set;}
    }
}