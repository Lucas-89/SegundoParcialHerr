using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SegundoParcialHerr.Data;
using SegundoParcialHerr.Models;

namespace SegundoParcialHerr.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly AutorContext _context;
        public SucursalService(AutorContext context)
        {
            _context = context;
        }

        public void Create(Sucursal obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                _context.Sucursal.Remove(obj);
                _context.SaveChanges();
            }
        }
        public List<Sucursal> GetAll()
        {
            var query = from sucursal in _context.Sucursal select sucursal;

            return query.ToList();
        }
        public List<Sucursal> GetAll(string NombreBuscado)
        {
            var query = from sucursal in _context.Sucursal select sucursal;

            if (!string.IsNullOrEmpty(NombreBuscado))
            {
                query = query.Where(x => x.NombreSucursal.ToLower().Contains(NombreBuscado.ToLower()));
            }
            return query.ToList();
        }

        public Sucursal? GetById(int id)
        {
            var sucursal = _context.Sucursal
                .FirstOrDefault(m => m.Id == id);
            return sucursal;
        }

        public void Update(Sucursal obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
    }
}