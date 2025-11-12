using System.ComponentModel.DataAnnotations;
using System;
using Proyecto.Models;

namespace SistemaVentas.Web.ViewModels
{
    public class PresupuestoViewModel
    {
        public PresupuestoViewModel()
        {

        }
        public PresupuestoViewModel(Presupuestos presupuesto)
        {
            idPresupuesto = presupuesto.idPresupuesto;
            NombreDestinatario = presupuesto.NombreDestinatario;
            FechaCreacion = presupuesto.FechaCreacion;
        }
        public int idPresupuesto { get; set; }
        [Display(Name = "Nombre o Email del destinatario")]
        [Required(ErrorMessage = "El nombre o Email es obligatorio")]
        public string NombreDestinatario { get; set; }
        [Display(Name = "Fecha de Creación")] 
        [Required(ErrorMessage = "La fecha es obligatoria.")] 
        [DataType(DataType.Date)]  
        public DateTime FechaCreacion { get; set; }  

    }
}