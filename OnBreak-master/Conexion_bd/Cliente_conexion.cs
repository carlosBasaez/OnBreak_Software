using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OnBreakData;
using System.Data;

namespace Conexion_bd
{
    public class Cliente_conexion
    {
        public static List<Cliente> usuario;

        public static int insertarSQL(Cliente newCliente)
        {
            Int32 rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);

                command.CommandText = "INSERT INTO Cliente (RutCliente, RazonSocial, NombreContacto, MailContacto, Direccion, Telefono, IdActividadEmpresa, IdTipoEmpresa) " +
                    "VALUES(@rut_cli,@razon_social, @nombre_contacto, @mail, @direccion,@telefono , @id_act_empresa, @id_tipo_empresa)";
                command.Parameters.AddWithValue("@rut_cli",newCliente.Rut_cliente.ToLower());
                command.Parameters.AddWithValue("@razon_social", newCliente.Razon_social);
                command.Parameters.AddWithValue("@nombre_contacto", newCliente.Nombre_contacto);
                command.Parameters.AddWithValue("@mail",newCliente.mail);
                command.Parameters.AddWithValue("@direccion", newCliente.direccion);
                command.Parameters.AddWithValue("@telefono", newCliente.telefono);
                command.Parameters.AddWithValue("@id_act_empresa", newCliente.id_actividad_empresa);
                command.Parameters.AddWithValue("@id_tipo_empresa", newCliente.id_tipo_empresa);

                try
                {
                    conn.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"rowsaffected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cliente_Conexion/Insertar\n" + ex.Message);
                }


            }           
            return rowsAffected;
        }

        public static int updateSQL(Cliente newCliente)
        {
            Int32 rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);

                //query
                command.CommandText = "UPDATE Cliente SET NombreContacto=@nombre, " +
                    "MailContacto=@mail, Direccion=@direccion, Telefono=@telefono, IdActividadEmpresa=@idactemp, IdTipoEmpresa=@tipo_empresa, RazonSocial=@razon_social WHERE Lower(RutCliente)=@rut";
                command.Parameters.AddWithValue("@nombre", newCliente.Nombre_contacto);
                command.Parameters.AddWithValue("@mail", newCliente.mail);
                command.Parameters.AddWithValue("@direccion", newCliente.direccion);
                command.Parameters.AddWithValue("@telefono", newCliente.telefono);
                command.Parameters.AddWithValue("@idactemp", newCliente.id_actividad_empresa);
                command.Parameters.AddWithValue("@tipo_empresa", newCliente.id_tipo_empresa);
                command.Parameters.AddWithValue("@rut", newCliente.Rut_cliente.ToLower());
                command.Parameters.AddWithValue("@razon_social", newCliente.Razon_social);

                try
                {
                    conn.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("rowsaffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cliente_Conexion/Update\n" + ex.Message);
                }
                conn.Close();

            }

            return rowsAffected;
        }

        public static int eliminarSQL(Cliente newcliente)
        {
            Int32 rowAffected = 0;

            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                //query
                command.CommandText = "DELETE FROM Cliente WHERE Lower(RutCliente)=@rut;";

                command.Parameters.AddWithValue("@rut", newcliente.Rut_cliente.ToLower());

                try
                {
                    conn.Open();
                    rowAffected = command.ExecuteNonQuery();
                    Console.WriteLine("rowsaffected: {0}", rowAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cliente_Conexion/Delete\n" + ex.Message);
                }
                conn.Close();
            }

            return rowAffected;
        }

        public static Cliente searchSQLRut(string Rut_cliente)
        {
            Cliente _cliente = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Cliente where Lower(RutCliente) = '{Rut_cliente.ToLower()}'";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);

                    _cliente = new Cliente();

                    _cliente.Rut_cliente = dt.Rows[0]["RutCliente"].ToString().ToLower();
                    _cliente.Razon_social = dt.Rows[0]["RazonSocial"].ToString();
                    _cliente.Nombre_contacto = dt.Rows[0]["NombreContacto"].ToString();
                    _cliente.mail = dt.Rows[0]["MailContacto"].ToString();
                    _cliente.direccion = dt.Rows[0]["Direccion"].ToString();
                    _cliente.telefono = dt.Rows[0]["Telefono"].ToString();
                    _cliente.id_actividad_empresa = int.Parse((dt.Rows[0]["IdActividadEmpresa"].ToString()));
                    _cliente.id_tipo_empresa = int.Parse((dt.Rows[0]["IdTipoEmpresa"].ToString()));
                    usuario.Add(_cliente);
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return _cliente;

        }

        [Obsolete]
        public static Cliente searchSQLEmpresa(string id_tipo_empresa)
        {
            Cliente _cliente = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Cliente where RutCliente = '{id_tipo_empresa}'";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);

                    _cliente = new Cliente();

                    _cliente.Rut_cliente = dt.Rows[0]["RutCliente"].ToString();
                    _cliente.Razon_social = dt.Rows[0]["RazonSocial"].ToString();
                    _cliente.Nombre_contacto = dt.Rows[0]["NombreContacto"].ToString();
                    _cliente.mail = dt.Rows[0]["MailContacto"].ToString();
                    _cliente.direccion = dt.Rows[0]["Direccion"].ToString();
                    _cliente.telefono = dt.Rows[0]["Telefono"].ToString();
                    _cliente.id_actividad_empresa = int.Parse((dt.Rows[0]["IdActividadEmpresa"].ToString()));
                    _cliente.id_tipo_empresa = int.Parse((dt.Rows[0]["IdTipoEmpresa"].ToString()));
                    usuario.Add(_cliente);
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return _cliente;

        }

        [Obsolete]
        public static Cliente searchSQLActividad(string id_actividad_empresa)
        {
            Cliente _cliente = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Cliente where Lower(RutCliente) = '{id_actividad_empresa}'";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);

                    _cliente = new Cliente();

                    _cliente.Rut_cliente = dt.Rows[0]["RutCliente"].ToString();
                    _cliente.Razon_social = dt.Rows[0]["RazonSocial"].ToString();
                    _cliente.Nombre_contacto = dt.Rows[0]["NombreContacto"].ToString();
                    _cliente.mail = dt.Rows[0]["MailContacto"].ToString();
                    _cliente.direccion = dt.Rows[0]["Direccion"].ToString();
                    _cliente.telefono = dt.Rows[0]["Telefono"].ToString();
                    _cliente.id_actividad_empresa = int.Parse((dt.Rows[0]["IdActividadEmpresa"].ToString()));
                    _cliente.id_tipo_empresa = int.Parse((dt.Rows[0]["IdTipoEmpresa"].ToString()));
                    usuario.Add(_cliente);
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return _cliente;

        }

        public static List<Cliente> SelectAll()
        {
            List<Cliente> list = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "select Lower(RutCliente), RazonSocial, NombreContacto, MailContacto, Direccion, Telefono, IdActividadEmpresa, IdTipoEmpresa From Cliente";

                    SqlDataReader reader = command.ExecuteReader();

                    Cliente cl;
                    while (reader.Read())
                    {
                        cl = new Cliente();
                        cl.Rut_cliente = reader.GetString(0);
                        cl.Razon_social = reader.GetString(1);
                        cl.Nombre_contacto = reader.GetString(2);
                        cl.mail = reader.GetString(3);
                        cl.direccion = reader.GetString(4);
                        cl.telefono = reader.GetString(5);
                        cl.id_actividad_empresa = reader.GetInt32(6);
                        cl.id_tipo_empresa = reader.GetInt32(7);
                        list.Add(cl);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cliente_Conexion/SelectAll\n" + ex.Message);
                }

                conn.Close();
            }
            return list;
        }

        public static Cliente Select(string rut)
        {
            Cliente cl = null;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(null, conn);
                    command.CommandText = "select Lower(RutCliente), RazonSocial, NombreContacto, MailContacto, Direccion, Telefono, IdActividadEmpresa, IdTipoEmpresa From Cliente " +
                        $" WHERE Lower(RutCliente) = '{rut.ToLower()}'";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        cl = new Cliente();
                        cl.Rut_cliente = reader.GetString(0);
                        cl.Razon_social = reader.GetString(1);
                        cl.Nombre_contacto = reader.GetString(2);
                        cl.mail = reader.GetString(3);
                        cl.direccion = reader.GetString(4);
                        cl.telefono = reader.GetString(5);
                        cl.id_actividad_empresa = reader.GetInt32(6);
                        cl.id_tipo_empresa = reader.GetInt32(7);
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Cliente_Conexion/Select\n" + ex.Message);
                    cl = null;
                }

                conn.Close();
            }
            return cl;
        }
    }
}
