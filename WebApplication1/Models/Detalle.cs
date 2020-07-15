using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Detalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDetalle { get; set; }
        public int idFactura { get; set; }
        public Factura Factura { get; set; }
        public int idProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public long PrecioUnitario { get; set; }
    }
}