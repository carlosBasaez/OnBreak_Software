using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class TipoAmbientacion_Conexion
    {
        public static List<TipoAmbientacion> SelectAll()
        {
            List<TipoAmbientacion> list = new List<TipoAmbientacion>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT idTipoAmbientacion, Descripcion FROM TipoAmbientacion";

                    SqlDataReader reader = command.ExecuteReader();

                    TipoAmbientacion ta;
                    while (reader.Read())
                    {
                        ta = new TipoAmbientacion();
                        ta.idTipoAmbientacion = reader.GetInt32(0);
                        ta.descripcion = reader.GetString(1);
                        list.Add(ta);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoAmbientacion_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static TipoAmbientacion Select(int idTipoAmbientacion)
        {
            TipoAmbientacion ta = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT idTipoAmbientacion, Descripcion FROM TipoAmbientacion WHERE idTipoAmbientacion={idTipoAmbientacion}";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        ta = new TipoAmbientacion();
                        ta.idTipoAmbientacion = reader.GetInt32(0);
                        ta.descripcion = reader.GetString(1);
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoAmbientacion_Conexion/Select\n" + ex.Message);
                    ta = null;
                }

                conn.Close();
            }
            return ta;
        }
    }
}
