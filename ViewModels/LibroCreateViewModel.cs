using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Models;
using SegundoParcialHerr.Utils;

namespace SegundoParcialHerr.ViewModels
{
    public class LibroCreateViewModel
    {
        public int Id{get;set;}
        public string Titulo {get;set;}
        public TipoGenero Genero {get;set;}
        public int Precio{get;set;}
        public int Stock{get;set;}
        public int AutorId{get;set;} //veo si saco esta
        public List<int> SucursalesId {get;set;} //agrego esta lista de int SucursalesId
    }
}