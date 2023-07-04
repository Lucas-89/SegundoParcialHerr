using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;


namespace SegundoParcialHerr.ViewModels
{
    public class SucursalDetailViewModel
    {
        public int Id{get;set;}
        public string NombreSucursal{get;set;}
        public string Direccion{get;set;}
        public string Localidad{get;set;}
        // public bool Contemporaneo{get;set;}
        public List<Libro> Libros{get;set;} = new List<Libro>();
    }
}