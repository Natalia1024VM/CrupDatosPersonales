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
    public class PaisServicio
    {
        private PaisRepositorio PaisRepositorio = new PaisRepositorio();

        public PaisModel ConsultarPaisID(int idPais, ApplicationDbContext application)
        {
            var resultado = PaisRepositorio.ConsultarPaisIDAsync(idPais, application);

            if (resultado.Result != null)
            {
                PaisModel PaisModel = PaisMapper.entidadToModel(resultado.Result);
                return PaisModel;
            }
            else
            {
                return null;
            }
        }
        public List<PaisModel> ConsultarPaiss(ApplicationDbContext application)
        {
            var resultado = PaisRepositorio.ConsultarPaiss(application);
            if (resultado.Result != null)
            {
                List<PaisModel> lista = new List<PaisModel>();
                foreach (var item in resultado.Result)
                {
                    lista.Add(PaisMapper.entidadToModel(item));
                }
                return lista;
            }
            else
            {
                return null;
            }
        }

        public int AgregarPais(PaisModel Pais, ApplicationDbContext application)
        {

            var id = PaisRepositorio.AgregarPais(PaisMapper.modelToEntidad(Pais), application);

            return id;
        }
        public bool ModificarPais(int id, PaisModel PaisModel, ApplicationDbContext application)
        {

            int resultado = PaisRepositorio.ModificarPais(id, PaisMapper.modelToEntidad(PaisModel), application);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarPais(int id, ApplicationDbContext application)
        {

            int resultado = PaisRepositorio.EliminarPais(id, application);
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
