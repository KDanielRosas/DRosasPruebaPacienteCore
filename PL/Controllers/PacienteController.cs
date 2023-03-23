using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PacienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Paciente.GetAll();           
            return View(result);
        }

        [HttpGet]
        public ActionResult Form(int? idPaciente)
        {
            if (idPaciente != null)
            {
                ML.Result result = BL.Paciente.GetById(idPaciente.Value);
                return View(result);
            }
            else
            {
                return View();
            }                      
        }

        [HttpPost]
        public ActionResult Form(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();
            if (paciente.IdPaciente == 0)
            {
                result = BL.Paciente.Add(paciente);

                if (result.Correct)
                {
                    ViewBag.Message = "Exito al registrar al paciente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al registar";
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Paciente.Update(paciente);
                if (result.Correct)
                {
                    ViewBag.Message = "Exito al actualizar la información";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al actualizar";
                    return PartialView("Modal");
                }
            }
        }

        public ActionResult Delete(int idPaciente)
        {
            ML.Result result = BL.Paciente.Delete(idPaciente);
            if (result.Correct)
            {
                ViewBag.Message = "Registro eliminado";
            }
            else
            {
                ViewBag.Message = "Error al eliminar";
            }
            return PartialView("Modal");
        }
        
    }
}
