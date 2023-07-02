using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class LibroUpdateStockViewModel
    {
         public int Id {get;set;}
        public string Titulo {get;set;}
        public int Precio{get;set;}

        [Display(Name ="Cantidad")]
        public int Stock {get;set;}
        public int CantidadCompra{get; set;}
        public List<Sucursal> Sucursales {get;set;}

    }
}