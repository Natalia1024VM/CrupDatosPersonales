using CapaDal.Entidad;
using CapBll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Mapeo
{
    public class PaisMapper
    {
        public static List<PaisModel> entidadToModel(List<Pais> entidades)
        {
            if (entidades != null)
            {
                List<PaisModel> lista = new List<PaisModel>();
                foreach (Pais entidad in entidades)
                {
                    PaisModel model = new PaisModel();

                    model.NombrePais = entidad.NombrePais;
                    model.idPais = entidad.idPais;
                    model.Activo = entidad.Activo;
                    lista.Add(model);
                }

                return lista;

            }

            return null;
        }


        public static PaisModel entidadToModel(Pais entidad)
        {
            if (entidad != null)
            {
                PaisModel model = new PaisModel();
                model.NombrePais = entidad.NombrePais;
                model.idPais = entidad.idPais;
                model.Activo = entidad.Activo;

                return model;
            }

            return null;
        }

        public static Pais modelToEntidad(PaisModel model)
        {
            if (model != null)
            {
                Pais entidad = new Pais();
                entidad.idPais = model.idPais;
                entidad.NombrePais = model.NombrePais;
                entidad.Activo = model.Activo;
                return entidad;
            }

            return null;
        }

    }
}

