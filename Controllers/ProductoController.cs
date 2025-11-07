using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repositorys;
namespace Proyecto.Controllers;

public class ProductoController : Controller
{
    private ProductoRepository productoRepository;
    public ProductoController()
    {
        productoRepository = new ProductoRepository();
    }

    public IActionResult Index()
    {
        List<Productos> productos = productoRepository.listarProductos();
        return View(productos);
    }

    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    public IActionResult Create(Productos produc)
    {
        productoRepository.nuevoProducto(produc);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]

    public IActionResult Edit(int id, Productos produc)
    {
        productoRepository.ActualizarProducto(id, produc);
        return RedirectToAction("Index");
    }

    [HttpGet]

    public IActionResult Delete(int id)
    {
        productoRepository.eliminarProducto(id);
        return RedirectToAction("Index");
    }

}