using CapaDal;
using CapBll.Mapeo;
using CapBll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll
{
    public class PersonaServicio
    {
        private PersonaRepositorio PersonaRepositorio = new PersonaRepositorio();

        public PersonaModel ConsultarPersonaID(int idPersona, ApplicationDbContext application)
        {
            var resultado = PersonaRepositorio.ConsultarPersonaIDAsync(idPersona, application);

            if (resultado.Result != null)
            {
                PersonaModel PersonaModel = PersonaMapper.entidadToModel(resultado.Result);
                return PersonaModel;
            }
            else
            {
                return null;
            }
        }
        public List<PersonaModel> ConsultarPersonas(ApplicationDbContext application)
        {
            var resultado = PersonaRepositorio.ConsultarPersonas(application);
            if (resultado.Result != null)
            {
                List<PersonaModel> lista = new List<PersonaModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(PersonaMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public List<PersonaModel> ConsultarPersonasNombre(string nombre, ApplicationDbContext application)
        {
            var resultado = PersonaRepositorio.ConsultarPersonasNombre( nombre, application);
            if (resultado.Result != null)
            {
                List<PersonaModel> lista = new List<PersonaModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(PersonaMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public int AgregarPersona(PersonaModel Persona, ApplicationDbContext application)
        {

            var id = PersonaRepositorio.AgregarPersona(PersonaMapper.modelToEntidad(Persona), application);

            return id;
        }
        public bool ModificarPersona(int id, PersonaModel PersonaModel, ApplicationDbContext application)
        {

            int resultado = PersonaRepositorio.ModificarPersona(id, PersonaMapper.modelToEntidad(PersonaModel), application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarPersona(int id, ApplicationDbContext application)
        {

            int resultado = PersonaRepositorio.EliminarPersona(id, application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
