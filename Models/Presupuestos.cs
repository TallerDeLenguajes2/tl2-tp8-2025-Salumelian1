namespace Proyecto.Models
{
    public class Presupuestos
{
    public int idPresupuesto{ get; set; }
    public string? NombreDestinatario{ get; set; }
    public DateTime FechaCreacion{ get; set; }
    public List<PresupuestosDetalles>? detalle;

        public Presupuestos(int IdPresupuesto, string? NombreDesti, DateTime fecha, List<PresupuestosDetalles> detalles)
        {
            idPresupuesto = IdPresupuesto;
            NombreDestinatario = NombreDesti;
            FechaCreacion = fecha;
            detalle = detalles;
        }
    
        public Presupuestos()
        {
            detalle = new List<PresupuestosDetalles>();
        }
    
    public double MontoPresupuesto()
    {
        return detalle.Sum(item => (int)item.getProducto().getPrecio() * item.getCantidad());
    }

    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto() * 1.21;
    }
    
    public int cantindadProductos()
    {
        return detalle.Sum(item => (int)item.getCantidad());
    }
}
}