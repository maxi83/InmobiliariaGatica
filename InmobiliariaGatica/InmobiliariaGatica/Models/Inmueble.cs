using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{
    public class Inmueble
    {
        public int Id { get; set; }
    public string Direccion { get; set; }
    public int Ambientes { get; set; }
    public int Superficie { get; set; }
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public int PropietarioId { get; set; }
    public Propietario propietario { get; set; }
}
}
