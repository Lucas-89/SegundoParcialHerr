using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels;

public class LibroViewModel
{

    public List<Libro>? Libros{get;set;} = new List<Libro>();

    public string NombreBuscado{get;set;}
}