using CapBll.Models;
using System.ComponentModel;

namespace CapaWEB.ViewModels
{
    public class PersonaViewModel
    {
        public List<PersonaModel> lista { get; set;}
        [DisplayName("Nombre de la persona a consultar")]
        public string nombreBuscar { get; set; }
        
        [DisplayName("Nombre Persona")]
        public string nombre { get; set; }

        [DisplayName("Teléfono Persona")]
        public string telefono { get; set; }

        [DisplayName("Dirección Persona")]
        public string direccion { get; set; }
        
        [DisplayName("País Persona")]
        public int IdPais { get; set; }
        
        [DisplayName("Departamento Persona")]
        public int IdDepartamento { get; set; }
        
        [DisplayName("Ciudad Persona")]
        public int IdCiudad { get; set; }
        public int idPersona{ get; set; }
    }
}
