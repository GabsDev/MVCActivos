using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDepreciacion : IServiceDepreciacion
    {
        public void DeleteDepreciacion(string id)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            repository.DeleteDepreciacion(id);
        }

        public Depreciacion Save(Depreciacion marca)
        {
            RepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.Save(marca);
        }

        IEnumerable<Depreciacion> IServiceDepreciacion.GetDepreciacion()
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.GetDepreciacion();
        }

        Depreciacion IServiceDepreciacion.GetDepreciacionByID(string id)
        {

            RepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.GetDepreciacionByID(id);
        }
    }
}
