namespace Proyecto.Models
{
    public class PresupuestosDetalles
{
    public Productos producto { get; set; }
    public int cantidad { get; set; }

        public PresupuestosDetalles(Productos produc, int cant)
        {
            producto = produc;
            cantidad = cant;
        }
        public PresupuestosDetalles()
        {
            
        }

        public int getCantidad()
        {
            return cantidad;
        }
        public Productos getProducto()
        {
            return producto;
        }
}
}