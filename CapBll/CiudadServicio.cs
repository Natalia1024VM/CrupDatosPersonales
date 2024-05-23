using CapaDal;
using CapBll.Mapeo;
using CapBll.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapBll
{
    public class CiudadServicio
    {
        private CiudadRepositorio CiudadRepositorio = new CiudadRepositorio();

        public CiudadModel ConsultarCiudadID(int idCiudad, int idDep, int idPais, ApplicationDbContext application)
        {
            var resultado = CiudadRepositorio.ConsultarCiudadIDAsync(idCiudad, idDep, idPais,  application);

            if (resultado.Result != null)
            {
                CiudadModel CiudadModel = CiudadMapper.entidadToModel(resultado.Result);
                return CiudadModel;
            }
            else
            {
                return null;
            }
        }
        public List<CiudadModel> ConsultarCiudades( ApplicationDbContext application)
        {
            var resultado = CiudadRepositorio.ConsultarCiudades(application);
            if (resultado.Result != null)
            {
                List<CiudadModel> lista = new List<CiudadModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(CiudadMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public List<CiudadModel> ConsultarCiudadesDep(int iddep,int idPais, ApplicationDbContext application)
        {
            var resultado = CiudadRepositorio.ConsultarCiudadsPorDep(iddep, idPais, application);
            if (resultado.Result != null)
            {
                List<CiudadModel> lista = new List<CiudadModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(CiudadMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public int AgregarCiudad(CiudadModel Ciudad, ApplicationDbContext application)
        {

            var id = CiudadRepositorio.AgregarCiudad(CiudadMapper.modelToEntidad(Ciudad), application);

            return id;
        }
        public bool ModificarCiudad(int id, CiudadModel CiudadModel, ApplicationDbContext application)
        {

            int resultado = CiudadRepositorio.ModificarCiudad(id, CiudadMapper.modelToEntidad(CiudadModel), application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarCiudad(int id,int idDep,int idPais, ApplicationDbContext application)
        {

            int resultado = CiudadRepositorio.EliminarCiudad(id, idDep, idPais,application);
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
