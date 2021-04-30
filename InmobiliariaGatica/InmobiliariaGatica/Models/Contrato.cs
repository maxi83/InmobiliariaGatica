using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int IdInquilino { get; set; }
        public Inquilino Inquilino { get; set; }
        public int IdInmueble { get; set; }
        public Inmueble Inmueble { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Importe { get; set; }
        public bool Estado { get; set; }
        
    }
}