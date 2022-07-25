using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Biblioteca
{
    public class Conexion
    {
        public static String connectionstring = @"Server=DESKTOP-RHN94QU\SQLEXPRESS;Database=OnBreak;Trusted_Connection=True";

        public static bool test()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                var conn = new SqlConnection(connectionstring);
                conn.Open();
                bool isOpen = conn.State == System.Data.ConnectionState.Open;
                conn.Close();
                return isOpen;
            }
        }
    }
}
