using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OnBreakData;

namespace Conexion_bd
{
    public class Actividad_empresa_conexion
    {
        public static List<Actividad_empresa> SelectAll()
        {
            List<Actividad_empresa> all_Act = new List<Actividad_empresa>();
            Actividad_empresa _act_empr = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM ActividadEmpresa", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _act_empr = new Actividad_empresa();
                        _act_empr.id_actividad_empresa = Int32.Parse(reader[0].ToString());
                        _act_empr.descripcion = reader.GetString(1);
                        all_Act.Add(_act_empr);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en ActividadEmpresa_Conexion/SelectAll\n" + ex.Message);
                }
                conn.Close();

            }
            return all_Act;
        }

        public static Actividad_empresa Select(int idActividadEmpresa)
        {
            Actividad_empresa _act_empr = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM ActividadEmpresa WHERE idActividadEmpresa={idActividadEmpresa}", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _act_empr = new Actividad_empresa();
                        _act_empr.id_actividad_empresa = Int32.Parse(reader[0].ToString());
                        _act_empr.descripcion = reader.GetString(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en ActividadEmpresa_Conexion/Select\n" + ex.Message);
                    _act_empr = null;
                }
                conn.Close();

            }
            return _act_empr;
        }
    }
}
