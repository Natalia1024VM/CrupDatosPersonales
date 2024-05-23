using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal.Entidad
{
    public class Ciudad
    {
        [Key] public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public int IdDepartamento { get; set; }
        public bool Activo { get; set; }
        public int idPais { get; set; }
    }
}
