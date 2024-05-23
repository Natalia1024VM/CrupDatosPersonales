using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Models
{
    public class DepartamentoModel
    {
        public string NombreDepartamento { get; set; }
        public int IdDepartamento { get; set; }
        public int idPais { get; set; }
        public bool Activo { get; set; }
    }
}
