using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(string id);
        IEnumerable<Usuario> GetUsuarioByName(String name);

        Usuario GetUsuario(string id,string password);
        void DeleteUsuario(string id);
        Usuario Save(Usuario usuario);
    }
}
