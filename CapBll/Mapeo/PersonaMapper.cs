using CapaDal.Entidad;
using CapBll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Mapeo
{
    public class PersonaMapper
    {
        public static List<PersonaModel> entidadToModel(List<Persona> entidades)
        {
            if (entidades != null)
            {
                List<PersonaModel> lista = new List<PersonaModel>();
                foreach (Persona entidad in entidades)
                {
                    PersonaModel model = new PersonaModel();

                    model.nombre = entidad.Nombre;
                    model.Id = entidad.IdPersona;
                    model.IdCiudad = entidad.CodigoCiudad;
                    model.IdDepartamento = entidad.CodigoDepartamento;
                    model.IdPais = entidad.CodigoPais;
                    model.telefono = entidad.Telefono;
                    model.direccion = entidad.Direccion;
                    model.Ciudad = entidad.NombreCiu;
                    model.Departamento = entidad.NombreDep;
                    model.Pais = entidad.NombrePais;

                    lista.Add(model);
                }

                return lista;

            }

            return null;
        }


        public static PersonaModel entidadToModel(Persona entidad)
        {
            if (entidad != null)
            {
                PersonaModel model = new PersonaModel();
                model.nombre = entidad.Nombre;
                model.Id = entidad.IdPersona;
                model.IdCiudad = entidad.CodigoCiudad;
                model.IdDepartamento = entidad.CodigoDepartamento;
                model.IdPais = entidad.CodigoPais;
                model.telefono = entidad.Telefono;
                model.direccion = entidad.Direccion;
                model.Ciudad =entidad.NombreCiu;
                model.Departamento=entidad.NombreDep;
                model.Pais = entidad.NombrePais;

                return model;
            }

            return null;
        }

        public static Persona modelToEntidad(PersonaModel model)
        {
            if (model != null)
            {
                Persona entidad = new Persona();
                entidad.Nombre = model.nombre;
                entidad.IdPersona = model.Id;
                entidad.CodigoCiudad = model.IdCiudad;
                entidad.CodigoDepartamento = model.IdDepartamento;
                entidad.CodigoPais = model.IdPais;
                entidad.Telefono = model.telefono;
                entidad.Direccion = model.direccion;
                entidad.NombreCiu = model.Ciudad;
                entidad.NombreDep = model.Departamento;
                entidad.NombrePais = model.Pais;

                return entidad;
            }

            return null;
        }

    }
}

