using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class Cenas_Conexion
    {
        public static List<Cenas> SelectAll()
        {
            List<Cenas> list = new List<Cenas>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT Numero, idTipoAmbientacion, MusicaAmbiental, LocalOnBreak, OtroLocalOnBreak, ValorArriendo FROM Cenas";

                    SqlDataReader reader = command.ExecuteReader();

                    Cenas c;
                    while (reader.Read())
                    {
                        c = new Cenas();
                        c.numero = reader.GetString(0);
                        c.idTipoAmbientacion = reader.GetInt32(1);
                        c.musicaAmbiental = reader.GetBoolean(2);
                        c.localOnBreack = reader.GetBoolean(3);
                        c.otroLocalOnBreak = reader.GetBoolean(4);
                        c.valorArriendo = reader.GetDouble(5);

                        list.Add(c);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cenas_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static Cenas Select(string numero)
        {
            Cenas c = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT Numero, idTipoAmbientacion, MusicaAmbiental, LocalOnBreak, OtroLocalOnBreak, ValorArriendo FROM Cenas WHERE Numero='{numero}'";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        c = new Cenas();
                        c.numero = reader.GetString(0);
                        c.idTipoAmbientacion = reader.GetInt32(1);
                        c.musicaAmbiental = reader.GetBoolean(2);
                        c.localOnBreack = reader.GetBoolean(3);
                        c.otroLocalOnBreak = reader.GetBoolean(4);
                        c.valorArriendo = reader.GetDouble(5);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cenas_Conexion/Select\n" + ex.Message);
                    c = null;
                }

                conn.Close();
            }
            return c;
        }

        public static bool Insert(Cenas cenas)
        {
            bool exito = false;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "INSERT INTO Cenas (Numero, idTipoAmbientacion, MusicaAmbiental, LocalOnBreak, OtroLocalOnBreak, ValorArriendo) VALUES (" +
                        "@Numero, @idTipoAmbientacion, @MusicaAmbiental, @LocalOnBreak, @OtroLocalOnBreak, @ValorArriendo" +
                        ")";
                    command.Parameters.AddWithValue("@Numero", cenas.numero);
                    command.Parameters.AddWithValue("@idTipoAmbientacion", cenas.idTipoAmbientacion);
                    command.Parameters.AddWithValue("@MusicaAmbiental", cenas.musicaAmbiental);
                    command.Parameters.AddWithValue("@LocalOnBreak", cenas.localOnBreack);
                    command.Parameters.AddWithValue("@OtroLocalOnBreak", cenas.otroLocalOnBreak);
                    command.Parameters.AddWithValue("@ValorArriendo", cenas.valorArriendo);


                    int num = command.ExecuteNonQuery();

                    if (num > 0) exito = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cenas_Conexion/Insert\n" + ex.Message);
                }

                conn.Close();
            }
            return exito;
        }
    }
}
