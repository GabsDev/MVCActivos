﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ActivosEntities3 : DbContext
    {
        public ActivosEntities3()
            : base("name=ActivosEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activo> Activo { get; set; }
        public virtual DbSet<Asegurador> Asegurador { get; set; }
        public virtual DbSet<Depreciacion> Depreciacion { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TipoActivo> TipoActivo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
    }
}
