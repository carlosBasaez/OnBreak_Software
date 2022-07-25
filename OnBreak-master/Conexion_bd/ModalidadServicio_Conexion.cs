using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class ModalidadServicio_Conexion
    {
        public static List<ModalidadServicio> SelectAll()
        {
            List<ModalidadServicio> list = new List<ModalidadServicio>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT idModalidad, idTipoEvento, Nombre, ValorBase, PersonalBase FROM ModalidadServicio";

                    SqlDataReader reader = command.ExecuteReader();
                    ModalidadServicio ms;

                    while (reader.Read())
                    {
                        ms = new ModalidadServicio();
                        ms.idModalidad = reader.GetString(0);
                        ms.idTipoEvento = reader.GetInt32(1);
                        ms.nombre = reader.GetString(2);
                        ms.valorBase = reader.GetDouble(3);
                        ms.personalBase = reader.GetInt32(4);
                        list.Add(ms);
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(">>>Error en ModalidadServicio_Conexion/SelectAll\n" + ex.Message);
                }
                conn.Close();    
            }
            return list;
        }

        public static ModalidadServicio Select(string idModalidad)
        {
            ModalidadServicio ms = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT idModalidad, idTipoEvento, Nombre, ValorBase, PersonalBase FROM ModalidadServicio WHERE idModalidad='{idModalidad}'";

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ms = new ModalidadServicio();
                        ms.idModalidad = reader.GetString(0);
                        ms.idTipoEvento = reader.GetInt32(1);
                        ms.nombre = reader.GetString(2);
                        ms.valorBase = reader.GetDouble(3);
                        ms.personalBase = reader.GetInt32(4);
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en ModalidadServicio_Conexion/Select\n" + ex.Message);
                    ms = null;
                }
                conn.Close();
            }
            return ms;
        }
    }
}
