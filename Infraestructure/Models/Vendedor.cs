//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Infraestructure.Models.Metadata;

    [MetadataType(typeof(VendedorMetadata))]
    public partial class Vendedor
    {
        public int Cedula_Juridica { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Correo { get; set; }
        public string Nombre_Contacto { get; set; }
        public string Otros { get; set; }
    }
}
