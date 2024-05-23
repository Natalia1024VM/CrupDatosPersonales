using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal.Entidad
{
    public class Persona
    {
        [Key] public int IdPersona {  get; set; }
        public string Nombre { get; set; } 
        public string Telefono { get; set; }
        public string Direccion { get;  set; }
        public int CodigoPais { get; set; }
        public int CodigoDepartamento { get; set; }
        public int CodigoCiudad { get; set; }
        public string NombreDep { get; set; }
        public string NombreCiu { get; set; }
        public string NombrePais { get; set; }


    }
}
