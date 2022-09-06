using MiApiEntrega.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiApiEntrega.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=WCX-DEV-0008;Initial Catalog=SistemaGestion;Trusted_Connection=true";
        public static List<Producto> GetProductos()
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
                                producto.Descripciones = dataReader["Descripciones"].ToString();
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
        public static bool EliminarProducto(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @id";

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
        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Producto " +
                    "(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES " +
                    "(@DescripcionesParameter, @CostoParameter, @PrecioVentaParameter, @StockParameter, @IdUsuarioParameter);";

                SqlParameter DescripcionesParameter = new SqlParameter("DescripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter CostoParameter = new SqlParameter("CostoParameter", SqlDbType.VarChar) { Value = producto.Costo };
                SqlParameter PrecioVentaParameter = new SqlParameter("PrecioVentaParameter", SqlDbType.VarChar) { Value = producto.PrecioVenta };
                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.VarChar) { Value = producto.Stock };
                SqlParameter IdUsuarioParameter = new SqlParameter("IdUsuarioParameter", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(DescripcionesParameter);
                    sqlCommand.Parameters.Add(CostoParameter);
                    sqlCommand.Parameters.Add(PrecioVentaParameter);
                    sqlCommand.Parameters.Add(StockParameter);
                    sqlCommand.Parameters.Add(IdUsuarioParameter);

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
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE Producto " +
                    "SET Descripciones = @DescripcionesParameter, Costo = @CostoParameter, PrecioVenta = @PrecioVentaParameter, Stock = @StockParameter, IdUsuario = @IdUsuarioParameter" +
                    "WHERE Id = @id ";

                SqlParameter DescripcionesParameter = new SqlParameter("DescripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter CostoParameter = new SqlParameter("CostoParameter", SqlDbType.VarChar) { Value = producto.Costo };
                SqlParameter PrecioVentaParameter = new SqlParameter("PrecioVentaParameter", SqlDbType.VarChar) { Value = producto.PrecioVenta };
                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.VarChar) { Value = producto.Stock };
                SqlParameter IdUsuarioParameter = new SqlParameter("IdUsuarioParameter", SqlDbType.BigInt) { Value = producto.IdUsuario };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(DescripcionesParameter);
                    sqlCommand.Parameters.Add(CostoParameter);
                    sqlCommand.Parameters.Add(PrecioVentaParameter);
                    sqlCommand.Parameters.Add(StockParameter);
                    sqlCommand.Parameters.Add(IdUsuarioParameter);
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
        public static List<Producto> GetProductoXUsuario(int id)
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
                            producto.Descripciones = row["Descripciones"].ToString();
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
    }
}
