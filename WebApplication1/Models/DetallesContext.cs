using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class DetallesContext:DbContext
    {
        public DbSet<Detalle> Detalles { get; set; }
    }
}