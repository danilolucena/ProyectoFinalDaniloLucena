using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaniloLucena_PrimerDesafio.ADO_.NET
{
    public class UsuarioHandler : DBHandler
    {
        public List <Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                                usuarios.Add(usuario);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuarios;
        }

        public Usuario GetUsuariosPorNombre (string nombreUsuario)
        {
            Usuario usuarios = new Usuario();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";
                        sqlCommand.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table); //Se ejecuta el Select
                        sqlCommand.Connection.Close();

                        foreach (DataRow row in table.Rows)
                        {
                            
                            usuarios.Id = Convert.ToInt32(row["Id"]);
                            usuarios.Nombre = row["Nombre"]?.ToString();
                            usuarios.Apellido = row["Apellido"]?.ToString();
                            usuarios.Contraseña = row["Contraseña"]?.ToString();
                            usuarios.Mail = row["Mail"]?.ToString();
                           

                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuarios;
        }

        public Usuario InicioSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuarios = new Usuario();
            List<Usuario> resultado = new List<Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña";
                        sqlCommand.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        sqlCommand.Parameters.AddWithValue("@Contraseña", contraseña);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = sqlCommand;
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table); //Se ejecuta el Select
                        sqlCommand.Connection.Close();

                        
                        foreach (DataRow row in table.Rows)
                        {
                            
                            usuarios.Id = Convert.ToInt32(row["Id"]);
                            usuarios.Nombre = row["Nombre"]?.ToString();
                            usuarios.Apellido = row["Apellido"]?.ToString();
                            usuarios.NombreUsuario = row["NombreUsuario"]?.ToString();
                            usuarios.Contraseña = row["Contraseña"]?.ToString();
                            usuarios.Mail = row["Mail"]?.ToString();
                            resultado.Add(usuarios);

                        }

                        bool vacio = resultado.Any();
                        if (vacio == false)
                        {
                            usuarios.Id = 0;
                            usuarios.Nombre = "";
                            usuarios.Apellido = "";
                            usuarios.NombreUsuario = "";
                            usuarios.Contraseña = "";
                            usuarios.Mail = "";
                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return usuarios;
        }
    }
}
