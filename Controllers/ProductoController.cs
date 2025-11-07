using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.ViewModels;
using Proyecto.Repositorys;
using Proyecto.Models;
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

    public IActionResult Create(ProductoViewModel produc)
    {
        if (!ModelState.IsValid)
        {
            return View(produc);
        }
        var nuevoProducto = new Productos
        {
            descripcion = produc.Descripcion,
            precio = produc.Precio
        };

        productoRepository.nuevoProducto(nuevoProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]

    public IActionResult Edit(int id, ProductoViewModel produc)
    {
        if (id != produc.idProducto) return NotFound();
        if (!ModelState.IsValid)
        {
            return View(produc);
        }
        var ProductoEditado = new Productos
        {
            idProducto = produc.idProducto,
            descripcion = produc.Descripcion,
            precio = produc.Precio
        };
        productoRepository.ActualizarProducto(id, ProductoEditado);
        return RedirectToAction("Index");
    }

    [HttpGet]

    public IActionResult Delete(int id)
    {
        productoRepository.eliminarProducto(id);
        return RedirectToAction("Index");
    }

}