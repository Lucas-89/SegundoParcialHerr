using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class SucursalViewModel
    {
        public List<Sucursal> Sucursales {get; set;} =new List<Sucursal>();
        public string NombreBuscado{get;set;} 
        public Libro Libro{get;set;} //probar cambiar esto por una lista

        public List<Libro>Libros {get;set;} = new List<Libro>();
    }
}