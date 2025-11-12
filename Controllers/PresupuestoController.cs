using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.ViewModels;
using Proyecto.Repositorys;
using Proyecto.Models;
using Proyecto.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Presupuesto.Controllers;

public class PresupuestoController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    private ProductoRepository productoRepository;
    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository();
        productoRepository = new ProductoRepository();
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
    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        List<Productos> productos = productoRepository.listarProductos();
        AgregarProductoViewModel modelo = new AgregarProductoViewModel
        {
            IdPresupuesto = id,
            ListaProductos = new SelectList(productos, "idProducto", "descripcion")
        };
        return View(modelo);
    }

    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var productos = productoRepository.listarProductos();
            model.ListaProductos = new SelectList(productos, "idProdcto", "descripcion");
            return View(model);
        }
        presupuestoRepository.createProducto(model.IdPresupuesto, model.IdProducto, model.Cantidad);
        return RedirectToAction("Index");
    }
}