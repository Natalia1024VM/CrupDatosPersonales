using CapBll.Models;
using CapaDal.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll.Mapeo
{
    public class DepartamentoMapper
    {
        public static List<DepartamentoModel> entidadToModel(List<Departamento> entidades)
        {
            if (entidades != null)
            {
                List<DepartamentoModel> departamentos = new List<DepartamentoModel>();
                foreach (Departamento entidad in entidades)
                {
                    DepartamentoModel model = new DepartamentoModel();

                    model.NombreDepartamento = entidad.Nombre;
                    model.IdDepartamento = entidad.IdDepartamento;
                    model.Activo = entidad.Activo;
                    model.idPais = entidad.IdPais;
                    departamentos.Add(model);
                }

                return departamentos;

            }

            return null;
        }


        public static DepartamentoModel entidadToModel(Departamento entidad)
        {
            if (entidad != null)
            {
                DepartamentoModel model = new DepartamentoModel();
                model.NombreDepartamento = entidad.Nombre;
                model.IdDepartamento = entidad.IdDepartamento;
                model.Activo = entidad.Activo;
                model.idPais = entidad.IdPais;
                return model;
            }

            return null;
        }

        public static Departamento modelToEntidad(DepartamentoModel model)
        {
            if (model != null)
            {
                Departamento entidad = new Departamento();
                entidad.IdDepartamento = model.IdDepartamento;
                entidad.Nombre= model.NombreDepartamento;
                entidad.Activo= model.Activo;
                entidad.IdPais = model.idPais;
                return entidad;
            }

            return null;
        }

    }
}

