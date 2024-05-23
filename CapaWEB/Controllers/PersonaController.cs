using CapaDal;
using CapaWEB.ViewModels;
using CapBll;
using CapBll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CapaWEB.Controllers
{
    public class PersonaController : Controller
    {
        private List<PaisModel> listaPais = new List<PaisModel>();
        private List<DepartamentoModel> listaDep = new List<DepartamentoModel>();
        private List<CiudadModel> listaCiu = new List<CiudadModel>();

        CiudadServicio CiudadServicio = new CiudadServicio();   
        DepartamentoServicio DepartamentoServicio = new DepartamentoServicio();
        PaisServicio paisServicio = new PaisServicio();
        PersonaServicio personaServicio = new PersonaServicio();

        private readonly ApplicationDbContext conection;

        public PersonaController(ApplicationDbContext conection)
        {
            this.conection = conection;
            listaPais = paisServicio.ConsultarPaiss(conection);
        }
        // GET: PersonaController
        public ActionResult Index(string mensaje)
        {
            PersonaViewModel viewModel = new PersonaViewModel();
            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais");
            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento");
            ViewBag.IdCiudad = new SelectList(listaCiu, "IdCiudad", "NombreCiudad");

            viewModel.lista = personaServicio.ConsultarPersonas(conection);
            if(mensaje != null)
            {
                ModelState.AddModelError("", mensaje);
            }
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Index(string consulta, PersonaViewModel viewModel)
        {
            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais", viewModel.IdPais);
            ViewBag.IdDepartamento = new SelectList(listaDep.Where(x => x.idPais == viewModel.IdPais), "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);
            ViewBag.IdCiudad = new SelectList(listaCiu.Where(x => x.idPais == viewModel.IdPais && x.IdDepartamento == viewModel.IdDepartamento), "IdCiudad", "NombreCiudad", viewModel.IdCiudad);

            try
            {
                if (consulta.Equals("Adicionar"))
                {
                    ModelState.Remove("nombreBuscar");
                    bool datosvalidi = true;
                    if(viewModel.nombre == null)
                    {
                        ModelState.AddModelError("nombre", "El dato es obligatorio");
                        datosvalidi = false;
                    }

                    if(viewModel.direccion == null)
                    {
                        ModelState.AddModelError("direccion", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.telefono == null)
                    {
                        ModelState.AddModelError("telefono", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdCiudad== 0)
                    {
                        ModelState.AddModelError("IdCiudad", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdPais == 0)
                    {
                        ModelState.AddModelError("IdPais", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdDepartamento == 0)
                    {
                        ModelState.AddModelError("IdDepartamento", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (datosvalidi)
                    {
                        PersonaModel personaModel = new PersonaModel();
                        personaModel = convertirViewModelToModel(viewModel);
                        int resultado = personaServicio.AgregarPersona(personaModel, conection);
                        if (resultado > 0)
                        {
                            ModelState.AddModelError("", "Se adiciono correctamente la persona");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error al adicionar la persona");

                        }
                    }

                }
                else if(consulta.Equals("Consultar"))
                {
                    viewModel.lista = personaServicio.ConsultarPersonasNombre(viewModel.nombreBuscar, conection);
                    if(viewModel.lista.Count == 0)
                    {
                        ModelState.AddModelError("","No existen registros con ese nombre");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }

            viewModel.lista = personaServicio.ConsultarPersonas(conection);


            return View(viewModel);
        }

        
        // GET: PersonaController/Edit/5
        public ActionResult Edit(int id)
        {
            PersonaViewModel viewModel = new PersonaViewModel();
            PersonaModel personaModel = personaServicio.ConsultarPersonaID(id, conection);
            viewModel.IdCiudad = id;
            viewModel.IdPais = personaModel.IdPais;
            viewModel.IdCiudad = personaModel.IdCiudad;
            viewModel.IdDepartamento = personaModel.IdDepartamento;
            viewModel.nombre = personaModel.nombre;
            viewModel.telefono = personaModel.telefono;
            viewModel.direccion = personaModel.direccion;
            viewModel.idPersona = personaModel.Id;

            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais", viewModel.IdPais);
            listaDep = DepartamentoServicio.ConsultarDepartamentosPorPais(viewModel.IdPais, conection);
            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);
            listaCiu = CiudadServicio.ConsultarCiudadesDep(viewModel.IdDepartamento, viewModel.IdPais, conection);
            ViewBag.IdCiudad = new SelectList(listaCiu, "IdCiudad", "NombreCiudad", viewModel.IdCiudad);

            return View(viewModel);
        }

        // POST: PersonaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string consulta, PersonaViewModel viewModel)
        {

            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais", viewModel.IdPais);
            listaDep = DepartamentoServicio.ConsultarDepartamentosPorPais(viewModel.IdPais, conection);
            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);
            listaCiu = CiudadServicio.ConsultarCiudadesDep(viewModel.IdDepartamento, viewModel.IdPais, conection);
            ViewBag.IdCiudad = new SelectList(listaCiu, "IdCiudad", "NombreCiudad", viewModel.IdCiudad);
            try
            {
                if (consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index");
                }
                else if (consulta.Equals("Modificar"))
                {
                    PersonaModel personaModel = new PersonaModel();
                    personaModel = convertirViewModelToModel(viewModel);
                    bool datosvalidi = true;
                    if (viewModel.nombre == null)
                    {
                        ModelState.AddModelError("nombre", "El dato es obligatorio");
                        datosvalidi = false;
                    }

                    if (viewModel.direccion == null)
                    {
                        ModelState.AddModelError("direccion", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.telefono == null)
                    {
                        ModelState.AddModelError("telefono", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdCiudad == 0)
                    {
                        ModelState.AddModelError("IdCiudad", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdPais == 0)
                    {
                        ModelState.AddModelError("IdPais", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (viewModel.IdDepartamento == 0)
                    {
                        ModelState.AddModelError("IdDepartamento", "El dato es obligatorio");
                        datosvalidi = false;
                    }
                    if (datosvalidi)
                    {
                        if (personaServicio.ModificarPersona(viewModel.idPersona, personaModel, conection))
                        {
                            return RedirectToAction("Index", new { mensaje = "Se modifico correctamente la persona" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error al modificar la persona|");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(viewModel);
        }

        // GET: PersonaController/Delete/5
        public ActionResult Delete(int id)
        {
            PersonaViewModel viewModel = new PersonaViewModel();
            PersonaModel personaModel = personaServicio.ConsultarPersonaID(id, conection);
            viewModel.idPersona = id;
            viewModel.IdCiudad = personaModel.Id;
            viewModel.IdPais = personaModel.IdPais;
            viewModel.IdCiudad = personaModel.IdCiudad;
            viewModel.IdDepartamento = personaModel.IdDepartamento;
            viewModel.nombre = personaModel.nombre;
            viewModel.telefono = personaModel.telefono;
            viewModel.direccion = personaModel.direccion;

            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais", viewModel.IdPais);
            listaDep = DepartamentoServicio.ConsultarDepartamentosPorPais(viewModel.IdPais, conection);
            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);
            listaCiu = CiudadServicio.ConsultarCiudadesDep(viewModel.IdDepartamento, viewModel.IdPais, conection);
            ViewBag.IdCiudad = new SelectList(listaCiu, "IdCiudad", "NombreCiudad", viewModel.IdCiudad);

            return View(viewModel);
        }

        // POST: PersonaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string consulta, PersonaViewModel viewModel)
        {
            try
            {
                if(consulta.Equals("Regresar"))
                {
                    return RedirectToAction("Index");
                }
                else if (consulta.Equals("Eliminar"))
                {
                    if(personaServicio.EliminarPersona(viewModel.idPersona, conection))
                    {
                        return RedirectToAction("Index", new {mensaje ="Se elimino correctamente la persona"});
                    }
                    else {
                        ModelState.AddModelError("","Error al eliminar la persona|");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
            ViewBag.IdPais = new SelectList(listaPais, "idPais", "NombrePais", viewModel.IdPais);
            listaDep = DepartamentoServicio.ConsultarDepartamentosPorPais(viewModel.IdPais, conection);
            ViewBag.IdDepartamento = new SelectList(listaDep, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);
            listaCiu = CiudadServicio.ConsultarCiudadesDep(viewModel.IdDepartamento, viewModel.IdPais, conection);
            ViewBag.IdCiudad = new SelectList(listaCiu, "IdCiudad", "NombreCiudad", viewModel.IdCiudad);
            return View(viewModel);

        }


        public JsonResult GetDepartamento(string state)
        {
            var departamentos = new List<DepartamentoModel>();
            int codpais = Convert.ToInt16(state);

            departamentos = DepartamentoServicio.ConsultarDepartamentosPorPais(codpais, conection);

            return Json(departamentos);
        }


        public JsonResult GetCiudades(string state, string pais)
        {
            var ciudades = new List<CiudadModel>();
            int depto = Convert.ToInt16(state);

            int codpais = Convert.ToInt16(pais);

            ciudades = CiudadServicio.ConsultarCiudadesDep( depto,codpais,conection);

            return Json(ciudades);
        }

        public PersonaModel convertirViewModelToModel(PersonaViewModel viewModel)
        {
            PersonaModel model = new PersonaModel();
            model.telefono = viewModel.telefono;
            model.nombre= viewModel.nombre;
            model.IdCiudad = viewModel.IdCiudad;
            model.IdDepartamento = viewModel.IdDepartamento;
            model.IdPais= viewModel.IdPais;
            model.direccion = viewModel.direccion;

            return model;
        }

    }
}
