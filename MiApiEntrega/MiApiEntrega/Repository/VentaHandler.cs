using MiApiEntrega.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiApiEntrega.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=WCX-DEV-0008;Initial Catalog=SistemaGestion;Trusted_Connection=true";
        public static List<Venta> GetVentas()
        {
            List<Venta> ventas = new List<Venta>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                                ventas.Add(venta);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return ventas;
        }
        public static bool EliminarVenta(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Venta WHERE Id = @id";

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
        public static bool CrearVenta(Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Venta " +
                    "(Comentarios) VALUES " +
                    "(@ComentariosParamenter);";

                SqlParameter ComentariosParamenter = new SqlParameter("ComentariosParamenter", SqlDbType.VarChar) { Value = venta.Comentarios};
               

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(ComentariosParamenter);
                   
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
        public static bool ModificarVenta(Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE Venta " +
                    "SET Comentarios = @comentarios" +
                    "WHERE Id = @id ";

                SqlParameter ComentariosParamenter = new SqlParameter("ComentariosParamenter", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = venta.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(ComentariosParamenter);
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

        public static List<Venta> GetVentasXId(int id)
        {
            List<Venta> resultado = new List<Venta>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "  SELECT v.Id, v.Comentarios FROM Venta v INNER JOIN ProductoVendido pv ON v.Id = pv.IdVenta INNER JOIN Producto p ON pv.IdProducto = p.Id WHERE p.IdUsuario = @Id";
                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table); //Se ejecuta el Select
                        sqlCommand.Connection.Close();

                        foreach (DataRow row in table.Rows)
                        {
                            Venta venta = new Venta();
                            venta.Id = Convert.ToInt32(row["Id"]);
                            venta.Comentarios = row["Comentarios"].ToString();
                            resultado.Add(venta);

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
