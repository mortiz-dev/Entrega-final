using Entrega_final.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Entrega_1.Handler
{
    public class VentasHandler : DbHandler
    {
        public List<Ventas> GetVentas()
        {
            List<Ventas> ventas = new List<Ventas>();
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", conn))
                {
                    conn.Open();

                    using(SqlDataReader dataReader = sqlCommand.ExecuteReader())
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
    }
}
