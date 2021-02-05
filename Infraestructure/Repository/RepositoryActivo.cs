using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryActivo : IRepositoryActivo
    {
        public void DeleteActivo(string id)
        {
            int returno;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Activo oActivo = new Activo()
                {
                    IdActivo = id
                };
                ctx.Entry(oActivo).State = EntityState.Deleted;
                returno = ctx.SaveChanges();
            }
        }

        public IEnumerable<Activo> GetActivo()
        {
            IEnumerable<Activo> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Activo.ToList();
            }

            return lista;
        }

        public Activo GetActivoByID(string id)
        {
            Activo oActivo = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oActivo = ctx.Activo.
                            Include("Marca").
                            Include("TipoActivo").
                            Where(p => p.IdActivo == id).FirstOrDefault<Activo>();
            }
            return oActivo;
        }

        public IEnumerable<Activo> GetActivoByName(string name)
        {

            IEnumerable<Activo> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Activo.ToList<Activo>().FindAll(p => p.Descripcion.ToLower().Contains(name.ToLower()));
            }
            return lista;
        }

        public Activo Save(Activo activo)
        {
            int retorno = 0;
            Activo oActivo = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oActivo = GetActivoByID((string)activo.IdActivo);
                if (oActivo == null)
                {
                    ctx.Activo.Add(activo);
                }
                else
                {
                    ctx.Entry(activo).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oActivo = GetActivoByID((string)activo.IdActivo);

            return oActivo;
        }
    }
}
