using System.ComponentModel.DataAnnotations;
using System;
using Proyecto.Models;

namespace SistemaVentas.Web.ViewModels
{
    public class ProductoViewModel
    {
        public ProductoViewModel()
        {

        }
        
        public ProductoViewModel(Productos producto)
        {
            idProducto = producto.idProducto;
            Descripcion = producto.descripcion;
            Precio = producto.precio;
        }
        public int idProducto { get; set; }

        [Display(Name = "Descripcion del Producto")]
        [StringLength(250, ErrorMessage = "LA descripcion no puede superar los 250 caracteres.")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio unitario")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public int Precio { get; set; }
    }
}