using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        private RepositoryCoches repo;

        public CochesController(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int idcoche)
        {
            Coche car = this.repo.FindCoche(idcoche);
            return View(car);
        }

        // Necesitamos un IActioNResult que cargará los coches. Dicha petición
        // irá integrada en otra vista (Index)
        public IActionResult _CochesPartial()
        {
            // Si vamos a utilizar una vista parcial con AJAX
            // debemos devolver PartialView() y el model si lo necesitamos
            return PartialView("_CochesPartial", this.repo.GetCoches());
        }

        public IActionResult _DetailsCoche(int idcoche)
        {
            Coche coche = this.repo.FindCoche(idcoche);
            return PartialView("_DetailsCoche", coche);
        }
    }
}
