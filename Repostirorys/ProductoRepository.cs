using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Proyecto.Models;

namespace Proyecto.Repositorys
{
    public class ProductoRepository
    {
        string cadenaConexion = "Data Source=Tienda_final.db";
        public void nuevoProducto(Productos producto)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "INSERT INTO Productos (Descripcion, Precio) VALUES(@descripcion, @precio)";
            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.Add(new SqliteParameter("@descripcion", producto.descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", producto.precio));

            comando.ExecuteNonQuery();
        }

        public void ActualizarProducto(int id, Productos producto)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "UPDATE Productos SET descripcion = @descripcion, precio = @precio WHERE idProducto = @id";

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.Parameters.Add(new SqliteParameter("@descripcion", producto.descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", producto.precio));

            comando.ExecuteNonQuery();
        }


        public List<Productos> mapearProductos(SqliteDataReader lector)
        {
            var productos = new List<Productos>();
            while (lector.Read())
            {
                var producto = new Productos
                {
                    idProducto = Convert.ToInt32(lector["idProducto"]),
                    descripcion = lector["Descripcion"].ToString(),
                    precio = Convert.ToInt32(lector["Precio"])
                };
                productos.Add(producto);
            }
            return productos;
        }

        public List<Productos> listarProductos()
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "SELECT * FROM Productos";

            using var comando = new SqliteCommand(sql, conexion);
            using var Lector = comando.ExecuteReader();
            var productos = mapearProductos(Lector);
            return productos;
        }

        public Productos productoPorId(int id)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = @"SELECT * FROM Productos WHERE idProducto = @id";
            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.Add(new SqliteParameter("@id", id));

            using var lector = comando.ExecuteReader();
            if (lector.Read())
            {
                var producto = new Productos
                {
                    idProducto = Convert.ToInt32(lector["idProducto"]),
                    descripcion = lector["Descripcion"].ToString(),
                    precio = Convert.ToInt32(lector["Precio"])
                };
                return producto;
            }
            else
            {
                return null;
            }
        }

        public bool eliminarProducto(int id)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "DELETE FROM Productos WHERE idProducto = @id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            int FilasAfectadas = comando.ExecuteNonQuery();
            return (FilasAfectadas > 0);
        }
    }
}