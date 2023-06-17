using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Utils;

namespace SegundoParcialHerr.Models
{
    public class Libro
    {
        public int Id {get;set;}
        public string Titulo {get;set;}
        public TipoGenero Genero {get;set;}
        public int Precio{get;set;}
        public int Stock {get;set;}
        public int AutorId{get;set;}
        public virtual Autor Autor{get;set;}
        public virtual List<Sucursal> Sucursales {get;set;}
    }
}