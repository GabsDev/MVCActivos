using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceActivo
    {
        IEnumerable<Activo> GetActivo();
        IEnumerable<Activo> GetActivoByName(String name);
        Activo GetActivoByID(string id);
        void DeleteActivo(string id);
        Activo Save(Activo producto);
    }
}
