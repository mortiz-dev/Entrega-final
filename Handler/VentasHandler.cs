using Entrega_final.Handler.DOTS;
using Entrega_final.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_1.Handler
{
    public static class VentasHandler
    {
        public const string ConnectionString = "Server=tcp:negociosbna.database.windows.net,1433;Initial Catalog=negocios-bna-app;Persist Security Info=False;User ID=n75052;Password=Bl@ckLotus1994;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static List<GetVentas> GetListVentas()
        {
            List<GetVentas> ventas = new List<GetVentas>();
            string query = "SELECT V.Id, V.Comentarios, P.Stock, O.Descripciones, O.PrecioVenta * P.Stock AS [Total de la venta] "+
                            "FROM Venta AS V "+
                            "INNER JOIN ProductoVendido AS P ON P.IdVenta = V.Id "+
                            "INNER JOIN Producto AS O ON P.IdProducto = O.Id";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetVentas venta = new GetVentas();
                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                                venta.Stock = Convert.ToInt32(dataReader["Stock"]);
                                venta.Descripcion = dataReader["Descripciones"].ToString();
                                venta.TotalVenta = Convert.ToDecimal(dataReader["Total de la venta"]);

                                ventas.Add(venta);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return ventas;
        }
        public static List<GetProductosVendidos> GetProductosVentas()
        {
            List<GetProductosVendidos> resultados = new List<GetProductosVendidos>();

            string query = "SELECT DISTINCT(P.IdProducto), Descripciones, SUM(P.Stock) AS [Cantidad de ventas] " +
                            "FROM ProductoVendido AS P " +
                            "INNER JOIN Producto ON Producto.Id = P.IdProducto " +
                            "GROUP BY P.IdProducto, Descripciones";
            using(SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    using(SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetProductosVendidos venta = new GetProductosVendidos();
                                venta.idProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                venta.Descripcion = dataReader["Descripciones"].ToString();
                                venta.CantidadVentas = Convert.ToInt32(dataReader["Cantidad de ventas"]);
                                resultados.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultados;
        }
        public static void DeleteVenta(int id)
        {
            string queryDeleteVenta = "DELETE FROM Venta WHERE Id = @id";
            string queryDeletePV = "DELETE FROM ProductoVendido WHERE IdVenta = @id";

            using(SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.Int) {Value = id};

                sqlConnection.Open();

                using (SqlCommand sqlCommand1 = new SqlCommand(queryDeletePV, sqlConnection))
                {
                    sqlCommand1.Parameters.Add(idParameter);

                    sqlCommand1.ExecuteNonQuery();

                    sqlCommand1.Parameters.Clear();
                }

                using (SqlCommand sqlCommand = new SqlCommand(queryDeleteVenta, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);

                    sqlCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }
    }
    
}
