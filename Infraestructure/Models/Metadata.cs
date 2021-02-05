using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Metadata
    {
        internal partial class UsuarioMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Login { get; set; }
            [Display(Name = "Perfil Usuario")]
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public int IdRol { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Password { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Nombre { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Apellidos { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public bool Estado { get; set; }
        }

        internal partial class MarcaMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Marca")]
            public int IdMarca { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Marca")]
            public string Descripcion { get; set; }

        }


        internal partial class TipoActivoMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Tipo Activo")]
            public int IdTipoActivo { get; set; }

            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Tipo Activo")]
            public string Descripcion { get; set; }
        }

        internal partial class ActivoMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "ID Activo")]
            public string IdActivo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Numero de Serie")]
            public decimal NumSerie { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Tipo Activo")]
            public int IdTipoActivo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Modelo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de Compra")]
            public System.DateTime FechCompra { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public decimal Costo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Costo en Dolares")]
            public decimal CostoDolares { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Valor Actual")]
            public decimal ValorActual { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.Date)]
            [Display(Name = "Vencimiento Garantia")]
            public System.DateTime FechVenceGarantia { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Asegurador { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.Date)]
            [Display(Name = "Vencimiento Seguro")]
            public System.DateTime FechVenceSeguro { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Condicion Activo")]
            public string CondicionActivo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Foto de Factura")]
            public byte[] FotoFactura { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Foto de Activo")]
            public byte[] FotoActivo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Marca")]
            public int IdMarca { get; set; }
        }

        internal partial class VendedorMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Cedula Juridica")]
            public int Cedula_Juridica { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Direccion { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.PhoneNumber)]
            public Nullable<int> Telefono { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [DataType(DataType.EmailAddress)]
            public string Correo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Nombre Contacto")]
            public string Nombre_Contacto { get; set; }
            [DataType(DataType.MultilineText)]
            public string Otros { get; set; }
        }

        internal partial class AseguradorMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Consecutivo")]
            public int IdAsegurador { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            public string Descripcion { get; set; }
        }

        internal partial class DepreciacionMetadata
        {
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Consecutivo")]
            public string IdDepreciacion { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Activo")]
            public string IdActivo { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Depreciacion Mensual")]
            public Nullable<decimal> DepreciacionMensual { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Valor Actual")]
            public Nullable<decimal> ValorActual { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Numero Mes")]
            public Nullable<int> NumeroMes { get; set; }
            [Required(ErrorMessage = "{0} es un dato requerido")]
            [Display(Name = "Valor Final")]
            public Nullable<decimal> ValorFinal { get; set; }
        }
    }
}
