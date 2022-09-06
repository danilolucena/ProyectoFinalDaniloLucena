using MiApiEntrega.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiApiEntrega.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=WCX-DEV-0008;Initial Catalog=SistemaGestion;Trusted_Connection=true";

        public static List<ProductoVendido> GetProductoVendidos()
        {
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(
                        "SELECT * FROM ProductoVendido", sqlConnection))
                    {
                        sqlConnection.Open();

                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ProductoVendido productoVendido = new ProductoVendido();

                                    productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                    productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                    productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                    productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                    productosVendidos.Add(productoVendido);
                                }
                            }
                        }

                        sqlConnection.Close();
                    }
                }
                return productosVendidos;
            }
        public static bool EliminarProductoVendido(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }
        public static bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO ProductoVendido " +
                    "(Stock, IdProducto, IdVenta) VALUES " +
                    "(@StockParamenter, @IdProductoParameter, @IdVentaParamenter);";

                SqlParameter StockParamenter = new SqlParameter("StockParamenter", SqlDbType.BigInt) { Value = productoVendido.Stock };
                SqlParameter IdProductoParameter = new SqlParameter("IdProductoParameter", SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter IdVentaParamenter = new SqlParameter("IdVentaParamenter", SqlDbType.BigInt) { Value = productoVendido.IdVenta };
                

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(StockParamenter);
                    sqlCommand.Parameters.Add(IdProductoParameter);
                    sqlCommand.Parameters.Add(IdVentaParamenter);
                   

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); // Se ejecuta la sentencia sql

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }
        public static bool ModificarProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE ProductoVendido " +
                    "SET Stock = @stock, IdProducto = @idProducto, IdVenta = @idVenta" +
                    "WHERE Id = @id ";

                SqlParameter StockParamenter = new SqlParameter("StockParamenter", SqlDbType.BigInt) { Value = productoVendido.Stock };
                SqlParameter IdProductoParameter = new SqlParameter("IdProductoParameter", SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter IdVentaParamenter = new SqlParameter("IdVentaParamenter", SqlDbType.BigInt) { Value = productoVendido.IdVenta };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendido.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(StockParamenter);
                    sqlCommand.Parameters.Add(IdProductoParameter);
                    sqlCommand.Parameters.Add(IdVentaParamenter);
                    sqlCommand.Parameters.Add(idParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); // Se ejecuta la sentencia sql

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        public static List<ProductoVendido> GetProductoVendidoXUsuario(int id)
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
