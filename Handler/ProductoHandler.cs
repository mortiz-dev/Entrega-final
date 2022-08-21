using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Pre_Entrega_1.Model;
using System.Data.SqlClient;

namespace Pre_Entrega_1.Handler
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=tcp:negociosbna.database.windows.net,1433;Initial Catalog=negocios-bna-app;Persist Security Info=False;User ID=n75052;Password=Bl@ckLotus1994;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
                        // Me aseguro que haya filas que leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                productos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productos;
        }
        public static void DeleteProducto(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void CrearProducto(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " + 
                    "VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario)";
                SqlParameter descrParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuarioParamater = new SqlParameter("idUsuario", SqlDbType.Int) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descrParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParamater);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void EliminarProducto(int id)
        {
            using(SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @id";
                SqlParameter idParamater = new SqlParameter("id", SqlDbType.Int) { Value = id };
                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParamater);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        public static void ModificarProducto(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock WHERE Id = @id";
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.Int) { Value = producto.Id };
                SqlParameter descrParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descrParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);

                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
    }
}
