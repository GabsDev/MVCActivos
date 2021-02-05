using Infraestructure.Models;
using System.Collections.Generic;


namespace ApplicationCore.Services
{
    public interface IServiceTipoActivo
    {

        IEnumerable<TipoActivo> GetTipoActivo();
        TipoActivo GetTipoActivoByID(int id);
        void DeleteTipoActivo(int id);
        TipoActivo Save(TipoActivo tipoActivo);
    }
}
