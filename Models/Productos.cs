namespace Proyecto.Models
{
    public class Productos
    {
        public int idProducto { get; set; }
        public string? descripcion { get; set; }
        public int precio { get; set; }

        public Productos(int IdProducto, string descri, int Precio)
        {
            idProducto = IdProducto;
            descripcion = descri;
            precio = Precio;
        }

        public Productos(){}
        public int getPrecio()
        {
            return precio;
        }

        public string getDescripcion()
        {
            return descripcion;
        }

        public int getId()
        {
            return idProducto;
        }

        public void setId(int id)
        {
            idProducto = id;
        }
    }
}