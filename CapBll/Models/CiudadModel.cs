using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Models
{
    public class CiudadModel
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int IdDepartamento { get; set; }
        public bool Activo {  get; set; }

        public int idPais {  get; set; }
    }
}
