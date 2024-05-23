using CapaDal;
using CapBll;
using CapBll.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapaWEB.Servicios
{

    [ApiController]
    [Route("api/Persona/")]
    public class PersonaController : ControllerBase
    {
        private readonly ApplicationDbContext conection;
        PersonaServicio personaServicio = new PersonaServicio();
        public PersonaController(ApplicationDbContext conection)
        {
            this.conection = conection;
        }

        [HttpGet]
        public IEnumerable<PersonaModel> Get()
        {
            List<PersonaModel> PersonasModel = personaServicio.ConsultarPersonas(conection);

            return PersonasModel;
        }

        [HttpPost]
        public int Post(PersonaModel Persona)
        {
            var id = personaServicio.AgregarPersona(Persona, conection);
            return id;
        }

        [HttpGet("{idPersona:int}")]
        public PersonaModel Get(int idPersona)
        {
            PersonaModel PersonaModel = personaServicio.ConsultarPersonaID(idPersona, conection);

            return PersonaModel;
        }

        [HttpPost("PorNombre")]
        public List<PersonaModel> PostPersona(PersonaModel model)
        {
            List<PersonaModel> PersonaModel = personaServicio.ConsultarPersonasNombre(model.nombre, conection);

            return PersonaModel;
        }

        [HttpPut("{idPersona:int}")]
        public string actualiza(int idPersona, PersonaModel Persona)
        {
            bool PersonaModel = personaServicio.ModificarPersona(idPersona, Persona, conection);
            if (PersonaModel)
            {
                return "El Persona se modifico correctamente";
            }
            else
            {
                return "Error al modificar el Persona";
            }
        }

        [HttpDelete("{id:int}")]
        public string elimina(int id)
        {
            bool PersonaModel = personaServicio.EliminarPersona(id, conection);
            if (PersonaModel)
            {
                return "El Persona se elimino correctamente";
            }
            else
            {
                return "Error al eliminar el Persona";
            }
        }
    }
}
