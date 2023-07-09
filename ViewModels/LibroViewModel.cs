using System.ComponentModel.DataAnnotations;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.Utils;


namespace SegundoParcialHerr.ViewModels;

public class LibroViewModel
{

    public List<Libro>? Libros{get;set;} = new List<Libro>();

        // public int Id {get;set;}
        // public string Titulo {get;set;}
        // public TipoGenero Genero {get;set;}
        // public int Precio{get;set;}
        // [Display(Name ="Cantidad")]
        // public int Stock {get;set;}
        // [Display(Name ="Autor")]
        // public string AutorNombre{get;set;}

        public string NombreBuscado{get;set;}

        // deberia tener una 
        public List<Sucursal> Sucursales {get;set;} = new List<Sucursal>(); //  AGREGUE ESTA PARTE
}