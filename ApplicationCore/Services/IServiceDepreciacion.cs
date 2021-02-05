using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public  interface IServiceDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacion();
        Depreciacion GetDepreciacionByID(string id);
        void DeleteDepreciacion(string id); 
        Depreciacion Save(Depreciacion marca);
    }
}
