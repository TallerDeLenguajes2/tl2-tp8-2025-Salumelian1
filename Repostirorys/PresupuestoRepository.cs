using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Proyecto.Models;

namespace Proyecto.Repositorys
{
    public class PresupuestoRepository
    {
        string cadenaConexion = "Data Source=Tienda_final.db";

        public void nuevoPresupuesto(Presupuestos presupuestos)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "INSERT INTO Presupuestos (idPresupuesto, NombreDestinatario, FechaCreacion) VALUES (@idPresupuesto, @NombreDestinatario, @FechaCreacion)";

            using var comando = new SqliteCommand(sql, conexion);

            comando.Parameters.Add(new SqliteParameter("@idPresupuesto", presupuestos.idPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuestos.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuestos.FechaCreacion));
            comando.ExecuteNonQuery();
        }

        public List<Presupuestos> mapeoPresupuestos(SqliteDataReader lector)
        {
            var presupuespos = new List<Presupuestos>();
            while (lector.Read())
            {
                var presupuesto = new Presupuestos
                {
                    idPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                    NombreDestinatario = lector["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"])
                };
                presupuespos.Add(presupuesto);
            }
            return presupuespos;
        }

        public List<Presupuestos> listarPresupuestos()
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "SELECT * FROM presupuestos";

            using var comando = new SqliteCommand(sql, conexion);
            using var lector = comando.ExecuteReader();
            var presupuespos = mapeoPresupuestos(lector);
            return presupuespos;
        }

        /// <summary>
        /// Obtiene un presupuesto por ID, incluyendo todos sus productos y cantidades.
        /// </summary>
        public Presupuestos presupuestoPorId(int id)
        {
            Presupuestos presupuesto = null;
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();
            string sql = @"SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
            var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.Add(new SqliteParameter("@id", id));
            var lectorHeader = comando.ExecuteReader();
            if (lectorHeader.Read())
            {
                presupuesto = new Presupuestos
                {
                    idPresupuesto = Convert.ToInt32(lectorHeader["idPresupuesto"]),
                    NombreDestinatario = lectorHeader["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(lectorHeader["FechaCreacion"])
                };
            }
            if (presupuesto != null)
            {
                string sql2 = @"
                    SELECT d.Cantidad, p.idProducto, p.Descripcion, p.Precio 
                    FROM PresupuestosDetalle d
                    INNER JOIN Productos p ON d.idProducto = p.idProducto
                    WHERE d.idPresupuesto = @id";

                var comandoDetail = new SqliteCommand(sql2, conexion);

                comandoDetail.Parameters.Add(new SqliteParameter("@id", id));
                var lectorDetail = comandoDetail.ExecuteReader();
                while (lectorDetail.Read())
                {
                    var productoLeido = new Productos
                    {
                        idProducto = Convert.ToInt32(lectorDetail["idProducto"]),
                        descripcion = lectorDetail["Descripcion"].ToString(),
                        precio = Convert.ToInt32(lectorDetail["Precio"])
                    };

                    var detalleItem = new PresupuestosDetalles
                    {
                        cantidad = Convert.ToInt32(lectorDetail["Cantidad"]),
                        producto = productoLeido
                    };
                    presupuesto.detalle.Add(detalleItem);
                }
            }
            return presupuesto;
        }

        public void agregarProductoYCantidades(int idPresupuesto, int cantidad, int idProducto)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();
            string sql = "UPDATE PresupuestosDetalle SET idProducto = @idProducto, Cantidad = @cantidad WHERE idPresupuesto = @idPresupuesto";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            comando.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
            comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
            comando.ExecuteNonQuery();
        }

        public void EliminarPresupuesto(int idBuscado)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "DELETE FROM Presupuestos WHERE idPresupuesto = @Id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@Id", idBuscado);

            comando.ExecuteNonQuery();
        }
        
        public void ActualizarPresupuesto(int id, Presupuestos presupuesto)
        {
            // Usamos el nombre de columna 'NombreDestinatario' de tu base de datos
            string sql = "UPDATE Presupuestos SET NombreDestinatario = @nombre WHERE idPresupuesto = @id";

            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();
            using var comando = new SqliteCommand(sql, conexion);

            // Asignamos los parámetros
            comando.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@id", id));

            comando.ExecuteNonQuery();
        }
    }
}