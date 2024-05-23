using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal.Entidad
{
    public class Pais
    {
        [Key] public int idPais { get; set; }

        public string NombrePais { get; set; }
        public bool Activo { get; set; }
    }
}
