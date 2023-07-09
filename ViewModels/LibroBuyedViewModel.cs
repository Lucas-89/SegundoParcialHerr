using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class LibroBuyedViewModel
    {
        public int Id {get;set;}
        public string Titulo {get;set;}
        public int Precio{get;set;}
        public int CantidadCompra{get; set;}
        public Sucursal SucursalNombre {get;set;}

        public int CantTotal{get;set;}

    }

}    