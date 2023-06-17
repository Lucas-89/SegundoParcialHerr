using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.ViewModels
{
    public class AutorViewModel
    {
        public List<Autor> Autores {get;set;} = new List<Autor>();
        public string NombreBuscado {get;set;}
    }
}