using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;

namespace Conexion_bd
{
    public class CoffeeBreak_Conexion
    {
        public static List<CoffeeBreak> SelectAll()
        {
            List<CoffeeBreak> list = new List<CoffeeBreak>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "SELECT Numero, Vegetariana FROM CoffeeBreak";

                    SqlDataReader reader = command.ExecuteReader();

                    CoffeeBreak c;
                    while (reader.Read())
                    {
                        c = new CoffeeBreak();
                        c.numero = reader.GetString(0);
                        c.vegetariana = reader.GetBoolean(1);
                        
                        list.Add(c);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en CoffeBreak_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static CoffeeBreak Select(string numero)
        {
            CoffeeBreak c = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = $"SELECT Numero, Vegetariana FROM CoffeeBreak WHERE Numero='{numero}'";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        c = new CoffeeBreak();
                        c.numero = reader.GetString(0);
                        c.vegetariana = reader.GetBoolean(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en CoffeBreak_Conexion/Select\n" + ex.Message);
                    c = null;
                }

                conn.Close();
            }
            return c;
        }

        public static bool Insert(CoffeeBreak coffeeBreak)
        {
            bool exito = false;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "INSERT INTO CoffeeBreak (Numero, Vegetariana) VALUES (" +
                        "@Numero, @Vegetariana" +
                        ")";
                    command.Parameters.AddWithValue("@Numero", coffeeBreak.numero);
                    command.Parameters.AddWithValue("@Vegetariana", coffeeBreak.vegetariana);

                    int num = command.ExecuteNonQuery();

                    if (num > 0) exito = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en CoffeBreak_Conexion/Insert\n" + ex.Message);
                }

                conn.Close();
            }
            return exito;
        }
    }
}
