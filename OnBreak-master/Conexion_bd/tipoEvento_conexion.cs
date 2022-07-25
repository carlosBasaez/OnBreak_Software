using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class tipoEvento_conexion
    {
        public static List<TipoEvento> SelectAll()
        {
            List<TipoEvento> list = new List<TipoEvento>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    
                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT idTipoEvento, Descripcion FROM TipoEvento";

                    SqlDataReader reader = command.ExecuteReader();

                    TipoEvento te;
                    while (reader.Read())
                    {
                        te = new TipoEvento();
                        te.idTipoEvento = reader.GetInt32(0);
                        te.descripcion = reader.GetString(1);
                        list.Add(te);
                    }
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoEvento_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static TipoEvento Select(int id)
        {
            TipoEvento te = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT idTipoEvento, Descripcion FROM TipoEvento WHERE idTipoEvento={id}";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        te = new TipoEvento();
                        te.idTipoEvento = reader.GetInt32(0);
                        te.descripcion = reader.GetString(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoEvento_Conexion/Select\n" + ex.Message);
                    te = null;
                }

                conn.Close();
            }
            return te;
        }

    }
}
