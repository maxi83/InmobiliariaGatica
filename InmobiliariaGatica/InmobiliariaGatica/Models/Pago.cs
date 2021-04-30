using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaGatica.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int Nro { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Importe { get; set; }
        public int IdContrato { get; set; }
    }
}
