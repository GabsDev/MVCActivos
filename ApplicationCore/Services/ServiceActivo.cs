using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceActivo : IServiceActivo
    {
        public void DeleteActivo(string id)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            repository.DeleteActivo(id);
        }

        public IEnumerable<Activo> GetActivo()
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivo();
        }

        public Activo GetActivoByID(string id)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivoByID(id);
        }

        public IEnumerable<Activo> GetActivoByName(string name)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivoByName(name);
        }

        public Activo Save(Activo producto)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.Save(producto);
        }
    }
}
