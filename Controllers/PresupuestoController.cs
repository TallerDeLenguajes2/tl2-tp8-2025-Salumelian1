using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repositorys;
namespace Presupuesto.Controllers;

public class PresupuestoController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuestos> presupuesto = presupuestoRepository.listarPresupuestos();
        return View(presupuesto);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var presupuesto = presupuestoRepository.presupuestoPorId(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Presupuestos presu)
    {
        presupuestoRepository.nuevoPresupuesto(presu);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(int id, Presupuestos presu)
    {
        presupuestoRepository.ActualizarPresupuesto(id, presu);
        return RedirectToAction("Index");
    }

    [HttpDelete]

    public IActionResult Delete(int id)
    {
        presupuestoRepository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }
}