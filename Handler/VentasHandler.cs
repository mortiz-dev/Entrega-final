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
        public static List<Ventas> GetVentas()
        {
            List<Ventas> ventas = new List<Ventas>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", conn))
                {
                    conn.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Ventas venta = new Ventas();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

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
    }
    
}
