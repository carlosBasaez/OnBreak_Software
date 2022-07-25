using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OnBreakData;

namespace Conexion_bd
{
    public class tipo_empresa_conexion
    {
        public static List<Tipo_empresa> SelectAll()
        {
            List<Tipo_empresa> all_empresa = new List<Tipo_empresa>();
            Tipo_empresa _tipoEmpresa = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM TipoEmpresa", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _tipoEmpresa = new Tipo_empresa();
                        _tipoEmpresa.id_tipo_empresa = Int32.Parse(reader[0].ToString());
                        _tipoEmpresa.descripcion = reader.GetString(1);
                        all_empresa.Add(_tipoEmpresa);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoEmpresa_Conexion/SelectAll\n" + ex.Message);
                }
                conn.Close();

            }
            return all_empresa;
        }

        public static Tipo_empresa Select(int idTipoEmpresa)
        {
            Tipo_empresa _tipoEmpresa = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM TipoEmpresa WHERE idTipoEmpresa={idTipoEmpresa}", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _tipoEmpresa = new Tipo_empresa();
                        _tipoEmpresa.id_tipo_empresa = Int32.Parse(reader[0].ToString());
                        _tipoEmpresa.descripcion = reader.GetString(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en TipoEmpresa_Conexion/Select\n" + ex.Message);
                    _tipoEmpresa = null;
                }
                conn.Close();

            }
            return _tipoEmpresa;
        }
    } 
}
