using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaniloLucena_PrimerDesafio.ADO_.NET
{
    public class VentasHandler : DBHandler
    {
        public List<Venta> GetVentas()
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
                                venta.Comentarios = dataReader["NombreUsuario"].ToString();
                                ventas.Add(venta);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return ventas;
        }

        public List<Venta> GetVentasXId(int id)
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
