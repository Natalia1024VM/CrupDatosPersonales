using CapBll.Models;
using System.ComponentModel;

namespace CapaWEB.ViewModels
{
    public class PaisViewModel
    {
        [DisplayName("Código Pais")]
        public int IdPais { get; set; }

        [DisplayName("Nombre Pais")]
        public string NombrePais { get; set; }
        public List<PaisModel> lista { get; set; }
    }
}
