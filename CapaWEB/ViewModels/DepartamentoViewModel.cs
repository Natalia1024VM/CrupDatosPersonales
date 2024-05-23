using CapBll.Models;
using System.ComponentModel;

namespace CapaWEB.ViewModels
{
    public class DepartamentoViewModel
    {
        public List<DepartamentoModel> lista { get; set; }
        [DisplayName("Nombre Departamento")]
        public string nombre { get; set; }

        [DisplayName("Código Departamento")]
        public int idDepartamento { get; set; }
        
        [DisplayName("Nombre Pais")]
        public string nombrePais { get; set; }

        public int idPais { get; set; }

    }
}
