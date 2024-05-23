using CapaDal;
using CapaWEB.ViewModels;
using CapBll;
using CapBll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace CapaWEB.Controllers
{
    public class PaisController : Controller
    {
        private PaisServicio PaisServicio = new PaisServicio();

        private readonly ApplicationDbContext conection;

        public PaisController(ApplicationDbContext conection)
        {
            this.conection = conection;
        }

        // GET: PariController
        public ActionResult Index(string mensaje)
        {
            PaisViewModel viewModel = new PaisViewModel();

            viewModel.lista = PaisServicio.ConsultarPaiss(conection);
            if(mensaje != null)
            {
                ModelState.AddModelError("", mensaje);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string consulta, PaisViewModel viewModel)
        {
            viewModel.lista = new List<PaisModel>();
            try
            {
                //validar si ya existe el codigo del pais
                PaisModel modelExiste = new PaisModel();

                modelExiste = PaisServicio.ConsultarPaisID(viewModel.IdPais, conection);
                if (modelExiste == null)
                {

                    PaisModel paisModel = ConvertirViewModelModel(viewModel);
                    int resultado = PaisServicio.AgregarPais(paisModel, conection);
                    if (resultado > 0)
                    {

                        ModelState.AddModelError("", "Se adiciono correctamente el pais");
                    }
                    else
                    {

                        ModelState.AddModelError("", "Error al adicionar el pais");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "EL codigo del pais ya existe");
                }
                viewModel.lista = PaisServicio.ConsultarPaiss(conection);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(viewModel);
        }
        
        // GET: PariController/Edit/5
        public ActionResult Edit(int idPais)
        {
            PaisViewModel viewModel = new PaisViewModel();
            PaisModel paisModel = PaisServicio.ConsultarPaisID(idPais, conection);
            viewModel.IdPais = paisModel.idPais;
            viewModel.NombrePais = paisModel.NombrePais;

            return View(viewModel);
        }

        // POST: PariController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string consulta, PaisViewModel viewModel)
        {
            try
            {
                if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index");

                }
                else if (consulta.Equals("Modificar"))
                {
                    PaisModel paisModel = ConvertirViewModelModel(viewModel);
                    if (PaisServicio.ModificarPais(viewModel.IdPais, paisModel, conection))
                    {
                        return RedirectToAction("Index", new { mensaje= "Se modifico correctamente el pais" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al modificar el pais");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);

        }

        // GET: PariController/Delete/5
        public ActionResult Delete(int idPais)
        {
            PaisViewModel viewModel = new PaisViewModel();
            PaisModel paisModel = PaisServicio.ConsultarPaisID(idPais, conection);
            viewModel.IdPais = paisModel.idPais;
            viewModel.NombrePais = paisModel.NombrePais;

            return View(viewModel);
        }

        // POST: PariController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string consulta, PaisViewModel viewModel)
        {
            try
            {
                if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index");

                }
                else if (consulta.Equals("Eliminar"))
                {
                    if (PaisServicio.EliminarPais(viewModel.IdPais, conection))
                    {
                        return RedirectToAction("Index", new { mensaje = "Se modifico correctamente el pais" });
                    }
                    else
                    {

                        ModelState.AddModelError("", "Error la eliminar el pais");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);

        }

        public PaisModel ConvertirViewModelModel(PaisViewModel viewModel)
        {
            PaisModel paisModel = new PaisModel();

            paisModel.idPais = viewModel.IdPais;
            paisModel.NombrePais = viewModel.NombrePais;

            return paisModel;
        }
    }
}
