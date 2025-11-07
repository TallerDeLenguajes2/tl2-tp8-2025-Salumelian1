using System.ComponentModel.DataAnnotations;
using System;

namespace SistemaVentas.Web.ViewModels
{
    public class PresupuestoViewModel
    {
        public int idPresupuesto { get; set; }
        [Display(Name = "Nombre o Email del destinatario")]
        [Required(ErrorMessage = "El nombre o Email es obligatorio")]
        public string NombreDestinatario{ get; set; }
    }
}