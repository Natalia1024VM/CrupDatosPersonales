using CapBll.Models;
using CapaDal.Entidad;
using System;

namespace CapBll.Mapeo
{
    public class CiudadMapper
    {
        public static List<CiudadModel> entidadToModel(List<Ciudad> entidades)
        {
            if (entidades != null)
            {
                List<CiudadModel> models = new List<CiudadModel>();
                foreach (Ciudad entidad in entidades)
                {
                    CiudadModel model = new CiudadModel();

                    model.IdCiudad = entidad.IdCiudad;
                    model.NombreCiudad = entidad.Nombre;
                    model.IdDepartamento = entidad.IdDepartamento;
                    model.Activo = entidad.Activo;
                    models.Add(model);
                }

                return models;
            }

            return null;
        }

        public static CiudadModel entidadToModel(Ciudad entidad)
        {
            if (entidad != null)
            {

                CiudadModel model = new CiudadModel();
                model.IdCiudad = entidad.IdCiudad;
                model.NombreCiudad = entidad.Nombre;
                model.IdDepartamento = entidad.IdDepartamento;
                model.Activo = entidad.Activo;
                return model;
            }

            return null;
        }

        public static Ciudad modelToEntidad(CiudadModel model)
        {
            if (model != null)
            {

                Ciudad entidad = new Ciudad();
                entidad.IdCiudad = model.IdCiudad;
                entidad.IdDepartamento = model.IdDepartamento;
                entidad.Nombre = model.NombreCiudad;
                entidad.Activo = model.Activo;
                entidad.idPais = model.idPais;
                return entidad;
            }

            return null;
        }
    }
}

    