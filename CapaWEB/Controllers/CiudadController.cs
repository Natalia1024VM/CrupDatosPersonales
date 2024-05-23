using CapaDal;
using CapaDal.Entidad;
using CapaWEB.ViewModels;
using CapBll;
using CapBll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CapaWEB.Controllers
{
    public class CiudadController : Controller
    {
        private List<DepartamentoModel> listaDep = new List<DepartamentoModel>();
        PaisServicio paisServicio = new PaisServicio();
        DepartamentoServicio departamentoServicio = new DepartamentoServicio();
        CiudadServicio ciudadServicio = new CiudadServicio();
        private readonly ApplicationDbContext conection;

        public CiudadController(ApplicationDbContext conection)
        {
            this.conection = conection;
        }

        // GET: CiudadController
        public ActionResult Index(string mensaje, int idDep, int idPais)
        {
            CiudadViewModel viewModel = new CiudadViewModel();
            listaDep = departamentoServicio.ConsultarDepartamentosPorPais(idPais, conection) ;

            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento");
            viewModel.idDepartamento = idDep;
            viewModel.idpais = idPais;
            viewModel.lista = ciudadServicio.ConsultarCiudadesDep(idDep, idPais,conection);
            viewModel.nombreDepartamento = departamentoServicio.ConsultarDepartamentoID(idDep, idPais, conection).NombreDepartamento;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string consulta, CiudadViewModel viewModel)
        {
            listaDep = departamentoServicio.ConsultarDepartamentosPorPais(viewModel.idpais, conection);

            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento");
            try
            {
                CiudadModel ciuExistente  = new CiudadModel();
                ciuExistente = ciudadServicio.ConsultarCiudadID(viewModel.idCiudad, viewModel.idDepartamento, viewModel.idpais, conection);
                if (ciuExistente == null)
                {
                    CiudadModel model = new CiudadModel();
                    model = ConvertirViewModelToModel(viewModel);
                    int resultado = ciudadServicio.AgregarCiudad(model, conection);
                    if (resultado > 0)
                    {
                        ModelState.AddModelError("", "Se adiciono correctamente la ciudad");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al adicionar la ciudad");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "La ciudad ya existe");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            viewModel.lista = ciudadServicio.ConsultarCiudadesDep(viewModel.idDepartamento,viewModel.idpais, conection);

            return View(viewModel);
        }


        // GET: CiudadController/Edit/5
        public ActionResult Edit(int IdCiu, int idDep, int idPais)
        {
            CiudadViewModel viewModel = new CiudadViewModel();
            viewModel.idCiudad = IdCiu;
            viewModel.idDepartamento = idDep;
            viewModel.idpais = idPais;

            viewModel.nombreDepartamento = departamentoServicio.ConsultarDepartamentoID(idDep,idPais, conection).NombreDepartamento;
            viewModel.nombre = ciudadServicio.ConsultarCiudadID(IdCiu, idDep, idPais, conection).NombreCiudad;
            return View(viewModel);
        }

        // POST: CiudadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string consulta, CiudadViewModel viewModel)
        {

            try
            {
                if (consulta.Equals("Modificar"))
                {
                    CiudadModel ciudadModel = ConvertirViewModelToModel(viewModel);
                    if (ciudadServicio.ModificarCiudad(viewModel.idCiudad, ciudadModel, conection))
                    {
                        return RedirectToAction("Index", new { mensjae = "Se modifico correctamente la ciudad", idDep = viewModel.idDepartamento, idPais = viewModel.idpais });

                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrio un error al modificar la ciudad");
                    }
                }
                else if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("", new {   idDep =viewModel.idDepartamento, idPais = viewModel.idpais});
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(viewModel);
        }

        // GET: CiudadController/Delete/5
        public ActionResult Delete(int IdCiu, int idDep, int idPais)
        {
            CiudadViewModel viewModel = new CiudadViewModel();
            viewModel.idCiudad = IdCiu;
            viewModel.idDepartamento = idDep;
            viewModel.idpais = idPais;

            viewModel.nombreDepartamento = departamentoServicio.ConsultarDepartamentoID(idDep, idPais, conection).NombreDepartamento;
            viewModel.nombre = ciudadServicio.ConsultarCiudadID(IdCiu, idDep, idPais, conection).NombreCiudad;
            return View(viewModel);
        }

        // POST: CiudadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string consulta, CiudadViewModel viewModel)
        {
            try
            {
                if (consulta.Equals("Eliminar"))
                {
                    if (ciudadServicio.EliminarCiudad(viewModel.idCiudad, viewModel.idDepartamento,viewModel.idpais, conection) )
                    {
                        return RedirectToAction("Index", new { mensjae= "Se elimino correctamente la ciudad",idDep = viewModel.idDepartamento, idPais = viewModel.idpais });

                    }
                    else
                    {
                        ModelState.AddModelError("","Ocurrio un error al eliminar la ciudad");
                    }
                }
                else if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index", new { idDep = viewModel.idDepartamento, idPais = viewModel.idpais });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(viewModel); 
        }
        private CiudadModel ConvertirViewModelToModel(CiudadViewModel viewModel)
        { 
            CiudadModel model = new CiudadModel();

            model.NombreCiudad = viewModel.nombre;
            model.IdCiudad = viewModel.idCiudad;
            model.IdDepartamento = viewModel.idDepartamento;
            model.idPais = viewModel.idpais;
            return model;
        }
    }
}
