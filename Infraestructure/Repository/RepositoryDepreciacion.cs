using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryDepreciacion : IRepositoryDepreciacion
    {
        public void DeleteDepreciacion(string id)
        {
              int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Depreciacion depreciacion = new Depreciacion()
                    {
                        IdDepreciacion = id
                    };
                    ctx.Entry(depreciacion).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void DeleteDepreciacion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Depreciacion> GetDepreciacion()
        {
            try
            {

                IEnumerable<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Depreciacion.Include("Activo").ToList<Depreciacion>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

       public Depreciacion GetDepreciacionByID(string id)
        {
            
            Depreciacion depreciacion = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    depreciacion = ctx.Depreciacion.
                              Include("Activo").
                              Where(p => p.IdDepreciacion == id).
                              FirstOrDefault<Depreciacion>();
                }

                return depreciacion;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Depreciacion Save(Depreciacion depreciacion)
        {
          int retorno = 0;
            Depreciacion oDepreciacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oDepreciacion = GetDepreciacionByID(depreciacion.IdDepreciacion);
                    if (oDepreciacion == null)
                    {
                        ctx.Depreciacion.Add(depreciacion);
                    }
                    else
                    {
                        ctx.Entry(depreciacion).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oDepreciacion = GetDepreciacionByID(depreciacion.IdDepreciacion);

                return oDepreciacion;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
