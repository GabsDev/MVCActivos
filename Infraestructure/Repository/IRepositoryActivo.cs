using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryActivo
    {
        IEnumerable<Activo> GetActivo();
        IEnumerable<Activo> GetActivoByName(String name);
        Activo GetActivoByID(string id);
        void DeleteActivo(string id);
        Activo Save(Activo producto);
    }
}
