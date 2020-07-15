using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  idFactura { get; set; }

        public long NumeroFactura { get; set; }

        public DateTime Fecha { get; set; }

        public string TipodePago { get; set; }

        public long DocumentoCliente { get; set; }

        public string NombreCliente { get; set; }

        public long Subtotal { get; set; }

        public int Descuento { get; set; }

        public int IVA { get; set; }

        public long TotalDescuento { get; set; }

        public long TotalImpuesto { get; set; }

        public long Total { get; set; }
    }
}