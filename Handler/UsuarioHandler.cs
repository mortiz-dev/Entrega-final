using Pre_Entrega_1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_1.Handler
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "Server=tcp:negociosbna.database.windows.net,1433;Initial Catalog=negocios-bna-app;Persist Security Info=False;User ID=n75052;Password=Bl@ckLotus1994;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static Usuario GetUsuarioByUser(String user)
        {
            List<Usuario> lUsuario = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using(SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @user";

                    sqlCommand.Parameters.AddWithValue("@user", user);

                    SqlDataAdapter dataAdaptar = new SqlDataAdapter();
                    dataAdaptar.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdaptar.Fill(table);
                    sqlCommand.Connection.Close();

                    foreach(DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Password = row["Contraseña"]?.ToString();
                        usuario.Email = row["Mail"]?.ToString();

                        lUsuario.Add(usuario);
                    }
                }
            }
            return lUsuario?.FirstOrDefault();
        }

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", conn))
                {
                    conn.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario user = new Usuario();

                                user.Id = Convert.ToInt32(dataReader["Id"]);
                                user.Nombre = dataReader["Nombre"].ToString();
                                user.Apellido = dataReader["Apellido"].ToString();
                                user.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                user.Password = dataReader["Contraseña"].ToString();
                                user.Email = dataReader["Mail"].ToString();

                                usuarios.Add(user);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return usuarios;
        }
    }
}
