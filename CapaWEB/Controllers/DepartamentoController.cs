using CapaDal;
using CapaDal.Entidad;
using CapaWEB.ViewModels;
using CapBll;
using CapBll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace CapaWEB.Controllers
{
    public class DepartamentoController : Controller
    {
        private List<PaisModel> listaPais = new List<PaisModel>();
        PaisServicio paisServicio = new PaisServicio();
        DepartamentoServicio departamentoServicio = new DepartamentoServicio();
        private readonly ApplicationDbContext conection;

        public DepartamentoController(ApplicationDbContext conection)
        {
            this.conection = conection;
            listaPais = paisServicio.ConsultarPaiss(conection).Where(x => x.Activo).ToList();
        }
        
        // GET: DepartamentoController
        public ActionResult Index(int idPais, string mensaje)
        {
            DepartamentoViewModel viewModel = new DepartamentoViewModel();
            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais");
            viewModel.idPais = idPais;
            viewModel.nombrePais = paisServicio.ConsultarPaisID(idPais, conection).NombrePais;

            viewModel.lista = departamentoServicio.ConsultarDepartamentosPorPais(idPais, conection);
            if (mensaje != null)
            {
                ModelState.AddModelError("", mensaje);
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(string consulta, DepartamentoViewModel viewModel)
        {
            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais");
            try
            {
                DepartamentoModel modelExiste = departamentoServicio.ConsultarDepartamentoID(viewModel.idDepartamento, viewModel.idPais, conection);
                if (modelExiste == null)
                {
                    DepartamentoModel model = new DepartamentoModel();

                    model = convertirViewModelToModel(viewModel);
                    int resultado = departamentoServicio.AgregarDepartamento(model, conection);
                    if (resultado > 0)
                    {
                        return RedirectToAction("Index", new { idPais= viewModel.idPais, mensaje = "Se adiciono correctamente el departamento" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al adicionar el departamento");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El departamento a agregar ya existe");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            viewModel.lista = departamentoServicio.ConsultarDepartamentosPorPais(viewModel.idPais, conection);
                
            return View(viewModel);
        }
        
        // GET: DepartamentoController/Edit/5
        public ActionResult Edit(int idDep, int idpais)
        {
            DepartamentoViewModel viewModel = new DepartamentoViewModel();
            DepartamentoModel model= departamentoServicio.ConsultarDepartamentoID(idDep, idpais, conection);
            viewModel.nombre = model.NombreDepartamento;
            viewModel.idPais = idpais;
            viewModel.idDepartamento = idDep;
            viewModel.nombrePais = paisServicio.ConsultarPaisID(idpais, conection).NombrePais;

            return View(viewModel);
        }

        // POST: DepartamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string consulta, DepartamentoViewModel viewModel)
        {
            
            try
            {
                if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index",new { idPais  = viewModel.idPais});
                }
                else if(consulta.Equals("Modificar"))
                {
                    DepartamentoModel model = convertirViewModelToModel(viewModel);
                    if(departamentoServicio.ModificarDepartamento(viewModel.idDepartamento, model, conection ))
                    {
                        return RedirectToAction("Index", new { idPais = viewModel.idPais, mensaje = "Se modifico correctamente el Departamento" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hubo un erro al modificar el departamento");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);

        }

        // GET: DepartamentoController/Delete/5
        public ActionResult Delete(int idDep, int idPais)
        {
            DepartamentoViewModel viewModel = new DepartamentoViewModel();
            DepartamentoModel model = departamentoServicio.ConsultarDepartamentoID(idDep, idPais, conection);
            viewModel.nombre = model.NombreDepartamento;
            viewModel.idPais = idPais;
            viewModel.idDepartamento = idDep;
            viewModel.nombrePais = paisServicio.ConsultarPaisID(idPais, conection).NombrePais;

            return View(viewModel);
        }

        // POST: DepartamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string consulta, DepartamentoViewModel viewModel)
        {
            try
            {
                if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index", new { idPais = viewModel.idPais });
                }
                else if (consulta.Equals("Eliminar"))
                {
                    if (departamentoServicio.EliminarDepartamento(viewModel.idDepartamento, viewModel.idPais, conection))
                    {
                        return RedirectToAction("Index", new { idPais = viewModel.idPais, mensaje = "Se eliminó correctamente el Departamento" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hubo un error al eliminar el departamento");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);

        }

        public DepartamentoModel convertirViewModelToModel(DepartamentoViewModel viewModel)
        {
            DepartamentoModel model = new DepartamentoModel();

            model.NombreDepartamento = viewModel.nombre;
            model.idPais = viewModel.idPais;
            model.IdDepartamento = viewModel.idDepartamento;

            return model;
        }
    }
}
