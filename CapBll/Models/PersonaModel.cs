using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Models
{
    public class PersonaModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
    }
}
