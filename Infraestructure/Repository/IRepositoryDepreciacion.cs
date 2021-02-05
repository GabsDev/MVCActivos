using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacion();
        Depreciacion GetDepreciacionByID(string id);
        void DeleteDepreciacion(string id);
        Depreciacion Save(Depreciacion marca);
    }
}
