using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Conexion_bd
{
    public class Conexion
    {
        public static string connectionstring = @"Server=DESKTOP-F8TM6K2\SQLEXPRESS;Database=OnBreak;Trusted_Connection=True";

        public static bool test()
        {
            bool isOpen = false;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    
                    isOpen = (conn.State == System.Data.ConnectionState.Open);
                }
                catch
                {
                    Console.WriteLine("No se pudo hacer la conexion con la BD");
                    isOpen = false;
                }
                conn.Close();

            }
            return isOpen;
        }
    }
}
