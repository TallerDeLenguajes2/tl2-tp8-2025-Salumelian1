using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.ViewModels;
using Proyecto.Repositorys;
using Proyecto.Models;
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
    public IActionResult Create(PresupuestoViewModel presu)
    {
        if (!ModelState.IsValid)
        {
            return View(presu);
        }
        var nuevoPresupuesto = new Presupuestos
        {
            NombreDestinatario = presu.NombreDestinatario,
            FechaCreacion = presu.FechaCreacion
        };
        presupuestoRepository.nuevoPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(int id, PresupuestoViewModel presu)
    {
        if (id != presu.idPresupuesto) return NotFound();
        if (!ModelState.IsValid)
        {
            return View(presu);
        }
        var PresupuestoModificado = new Presupuestos
        {
            idPresupuesto = presu.idPresupuesto,
            NombreDestinatario = presu.NombreDestinatario,
            FechaCreacion = presu.FechaCreacion,
        };
        presupuestoRepository.ActualizarPresupuesto(id, PresupuestoModificado);
        return RedirectToAction("Index");
    }

    [HttpDelete]

    public IActionResult Delete(int id)
    {
        presupuestoRepository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }
}