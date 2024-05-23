using CapBll.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;

namespace CapaWEB.ViewModels
{
    public class CiudadViewModel
    {
        public List<CiudadModel> lista { get; set; }
        
        [DisplayName("Nombre Ciudad")]
        public string nombre { get; set; }
        
        [DisplayName("Codigo Departamento")]
        public int idDepartamento { get; set; }
        
        [DisplayName("Código Ciudad")]
        public int idCiudad { get; set; }

        [DisplayName("Nombre Departamento")]
        public string nombreDepartamento { get; set; }
        public int idpais { get; set; }

    }
}
