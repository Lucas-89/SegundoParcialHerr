using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class SucursalViewModel
    {
        public List<Sucursal> Sucursales {get; set;} =new();
        public string NombreBuscado{get;set;} 
        public Libro Libro{get;set;}
    }
}