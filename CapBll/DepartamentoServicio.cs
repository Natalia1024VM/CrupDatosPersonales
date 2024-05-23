using CapaDal;
using CapaDal.Entidad;
using CapBll.Mapeo;
using CapBll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CapBll
{
    public class DepartamentoServicio
    {

        private DepartamentoRepositorio DepartamentoRepositorio = new DepartamentoRepositorio();

        public DepartamentoModel ConsultarDepartamentoID(int idDepartamento, int idpais, ApplicationDbContext application)
        {
            var resultado = DepartamentoRepositorio.ConsultarDepartamentoIDAsync(idDepartamento,idpais, application);

            if (resultado.Result != null)
            {
                DepartamentoModel DepartamentoModel = DepartamentoMapper.entidadToModel(resultado.Result);
                return DepartamentoModel;
            }
            else
            {
                return null;
            }
        }
        public List<DepartamentoModel> ConsultarDepartamentosPorPais(int idPais, ApplicationDbContext conection)
        {
            var resultado = DepartamentoRepositorio.ConsultarDepartamentosPorPais(idPais, conection);
            if (resultado.Result != null)
            {
                List<DepartamentoModel> lista = new List<DepartamentoModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(DepartamentoMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }
        public List<DepartamentoModel> ConsultarDepartamentos(ApplicationDbContext application)
        {
            var resultado = DepartamentoRepositorio.ConsultarDepartamentos(application);
            if (resultado.Result != null)
            {
                List<DepartamentoModel> lista = new List<DepartamentoModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(DepartamentoMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public int AgregarDepartamento(DepartamentoModel Departamento, ApplicationDbContext application)
        {

            var id = DepartamentoRepositorio.AgregarDepartamento(DepartamentoMapper.modelToEntidad(Departamento), application);

            return id;
        }
        public bool ModificarDepartamento(int id, DepartamentoModel DepartamentoModel, ApplicationDbContext application)
        {

            int resultado = DepartamentoRepositorio.ModificarDepartamento(id,DepartamentoModel.idPais, DepartamentoMapper.modelToEntidad(DepartamentoModel), application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarDepartamento(int id, int idPais, ApplicationDbContext application)
        {

            int resultado = DepartamentoRepositorio.EliminarDepartamento(id, idPais, application);
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
