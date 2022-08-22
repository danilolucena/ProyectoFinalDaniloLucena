using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaniloLucena_PrimerDesafio.ADO_.NET
{
    public class ProductoHandler : DBHandler
    {
        public List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripcion = dataReader["Descripcion"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                productos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return productos;
        }

        public List<Producto> GetProductoXUsuario(int id)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "SELECT * FROM Producto WHERE IdUsuario = @Id";
                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table); //Se ejecuta el Select
                        sqlCommand.Connection.Close();

                        foreach (DataRow row in table.Rows)
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(row["Id"]);
                            producto.Descripcion = row["Descripciones"].ToString();
                            producto.Costo = Convert.ToInt32(row["Costo"]);
                            producto.PrecioVenta = Convert.ToInt32(row["PrecioVenta"]);
                            producto.Stock = Convert.ToInt32(row["Stock"]);
                            producto.IdUsuario = Convert.ToInt32(row["IdUsuario"]);
                            resultado.Add(producto);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
        public List<ProductoVendido> GetProductoVendidoXUsuario(int id)
        {
            List<ProductoVendido> resuladoPV = new List<ProductoVendido>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "SELECT pv.Stock, pv.IdProducto, pv.IdVenta FROM Producto p INNER JOIN ProductoVendido pv ON p.Id = pv.IdProducto INNER JOIN Usuario u ON p.IdUsuario = u.Id  WHERE u.Id = @Id";
                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table); //Se ejecuta el Select
                        sqlCommand.Connection.Close();

                        foreach (DataRow row in table.Rows)
                        {
                            ProductoVendido productoVendido = new ProductoVendido();

                            productoVendido.Stock = Convert.ToInt32(row["Stock"]);
                            productoVendido.IdProducto = Convert.ToInt32(row["IdProducto"]);
                            productoVendido.IdVenta = Convert.ToInt32(row["IdVenta"]);
                            resuladoPV.Add(productoVendido);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resuladoPV;
        }
    }
}
