using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal.Entidad
{
    public class Departamento
    {
        public string Nombre  { get; set; }
        [Key] public int IdDepartamento { get; set; }
        public bool Activo { get; set; }
        public int IdPais { get; set; }
    }
}
