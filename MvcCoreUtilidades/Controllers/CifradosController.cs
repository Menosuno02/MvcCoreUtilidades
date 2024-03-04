using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {

        public IActionResult CifradoBasico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoBasico
            (string contenido, string resultado, string accion)
        {
            // Ciframos el contenido
            string response =
                HelperCryptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                // Comparamos la respuesta cifrada con el valor
                // de la caja resultado
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "Contenidos diferentes";
                }
                else
                {
                    ViewData["MENSAJE"] = "Contenidos iguales";
                }
            }
            return View();
        }

        public IActionResult CifradoEficiente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoEficiente
            (string contenido, string resultado, string accion)
        {
            if (accion.ToLower() == "cifrar")
            {
                // Ciframos con nuevo SALT
                string response =
                    HelperCryptography.EncriptarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                string response =
                    HelperCryptography.EncriptarContenido(contenido, true);
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "<h1 style='color:red'>Contenidos diferentes</h1>";
                }
                else
                {
                    ViewData["MENSAJE"] = "<h1 style='color:green'>Contenidos iguales</h1>";
                }
            }
            return View();
        }
    }
}
