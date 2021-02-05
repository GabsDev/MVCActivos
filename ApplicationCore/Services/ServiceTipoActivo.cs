using Infraestructure.Models;
using Infraestructure.Repository;
using System.Collections.Generic;


namespace ApplicationCore.Services
{
    public class ServiceTipoActivo : IServiceTipoActivo
    {
        public void DeleteTipoActivo(int id)
        {
            IRepositoryTipoActivo repository = new RepositoryTipoActivo();
            repository.DeleteTipoActivo(id);
        }

        public IEnumerable<TipoActivo> GetTipoActivo()
        {
            IRepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.GetTipoActivo();
        }

        public TipoActivo GetTipoActivoByID(int id)
        {
            RepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.GetTipoActivoByID(id);
        }

        public TipoActivo Save(TipoActivo tipoActivo)
        {
            RepositoryTipoActivo repository = new RepositoryTipoActivo();
            return repository.Save(tipoActivo);
        }
    }
}
