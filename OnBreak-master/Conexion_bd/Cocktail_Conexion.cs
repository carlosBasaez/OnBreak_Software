using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class Cocktail_Conexion
    {
        public static List<Cocktail> SelectAll()
        {
            List<Cocktail> list = new List<Cocktail>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT Numero, idTipoAmbientacion, Ambientacion, MusicaAmbiental, MusicaCliente FROM Cocktail";

                    SqlDataReader reader = command.ExecuteReader();

                    Cocktail c;
                    while (reader.Read())
                    {
                        c = new Cocktail();
                        c.numero = reader.GetString(0);
                        c.idTipoAmbientacion = reader.GetInt32(1);
                        c.ambientacion = reader.GetBoolean(2);
                        c.musicaAmbiental = reader.GetBoolean(3);
                        c.musicaCliente = reader.GetBoolean(4);
                        list.Add(c);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cocktail_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static Cocktail Select(string numero)
        {
            Cocktail c = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT Numero, idTipoAmbientacion, Ambientacion, MusicaAmbiental, MusicaCliente FROM Cocktail WHERE Numero='{numero}'";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        c = new Cocktail();
                        c.numero = reader.GetString(0);
                        c.idTipoAmbientacion = reader.GetInt32(1);
                        c.ambientacion = reader.GetBoolean(2);
                        c.musicaAmbiental = reader.GetBoolean(3);
                        c.musicaCliente = reader.GetBoolean(4);
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cocktail_Conexion/Select\n" + ex.Message);
                    c = null;
                }

                conn.Close();
            }
            return c;
        }

        public static bool Insert(Cocktail cocktail)
        {
            bool exito = false;

            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"INSERT INTO Cocktail (Numero, idTipoAmbientacion, Ambientacion, MusicaAmbiental, MusicaCliente) VALUES (" +
                        $"@Numero, @idTipoAmbientacion, @Ambientacion, @MusicaAmbiental, @MusicaCliente" +
                        $")";
                    command.Parameters.AddWithValue("@Numero",cocktail.numero);
                    command.Parameters.AddWithValue("@idTipoAmbientacion", cocktail.idTipoAmbientacion);
                    command.Parameters.AddWithValue("@Ambientacion", cocktail.ambientacion);
                    command.Parameters.AddWithValue("@MusicaAmbiental", cocktail.musicaAmbiental);
                    command.Parameters.AddWithValue("@MusicaCliente", cocktail.musicaCliente);


                    int num = command.ExecuteNonQuery();
                    if (num > 0) exito = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cocktail_Conexion/Insert\n" + ex.Message);
                }

                conn.Close();
            }

            return exito;
        }

    }
}
