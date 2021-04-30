using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Models
{
    public class Inquilino
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Direccion_Trabajo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Dni_Garante { get; set; }
        public string Nombre_Garante { get; set; }
        public string Apellido_Garante { get; set; }
        public string Telefono_Garante { get; set; }
       
    }
}
