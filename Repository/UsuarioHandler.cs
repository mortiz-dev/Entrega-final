using Entrega_final.Controllers.DOTS;
using Entrega_final.Model;
using System.Data;
using System.Data.SqlClient;

namespace Entrega_final.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "";
        public static List<Usuario> GetUsuarios(int id)
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Usuario WHERE Id = @id";
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.Int) { Value = id };
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Password = dataReader["Contraseña"].ToString();
                                usuario.Email = dataReader["Mail"].ToString();
                                resultados.Add(usuario);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultados;
        }
        public static bool ModificarNombreDeUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "SET Nombre = @nombre" +
                    "WHERE Id = @id ";

                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
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
        public static void CrearUsuario(Usuario user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                    "VALUES(@nombre, @apellido, @nombreUsuario, @password, @email)";
                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = user.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = user.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = user.NombreUsuario };
                SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = user.Password };
                SqlParameter emailParamater = new SqlParameter("email", SqlDbType.VarChar) { Value = user.Email };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(passwordParameter);
                    sqlCommand.Parameters.Add(emailParamater);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void EliminarUsuario(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @password, Mail = @email WHERE Id = @id";
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.Int) { Value = usuario.Id };
                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = usuario.Password };
                SqlParameter emailParameter = new SqlParameter("email", SqlDbType.VarChar) { Value = usuario.Email };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(passwordParameter);
                    sqlCommand.Parameters.Add(emailParameter);

                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
        public static GetUserName GetNombreUsuario(int id)
        {
            GetUserName userName = new GetUserName();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Nombre FROM Usuario WHERE Id = @id";
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.Int) { Value = id };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                userName.Nombre = dataReader["Nombre"].ToString();
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return userName;
        }
        public static bool UserLogin(UserLogin userLogin)
        {
            bool login = false;
            string queryUser = "SELECT * FROM Usuario WHERE NombreUsuario = @usuario AND Contraseña = @password";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter idParamater = new SqlParameter("usuario", SqlDbType.VarChar) { Value = userLogin.UserName };
                SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = userLogin.Password };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUser, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParamater);
                    sqlCommand.Parameters.Add(passwordParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            login = true;
                        }
                    }
                }

                sqlConnection.Close();
            }

            return login;
        }
    }
}

