using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using OnBreakData;

namespace Conexion_bd
{
    public class Contrato_conexion
    {
        public static List<Contrato> contrato;
        public static List<TipoEvento> tipo_evento;
        public static List<ModalidadServicio> modalidad;

        public static int InsertarSQL(Contrato newContrato)
        {
            Int32 rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                command.CommandText = "INSERT INTO Contrato (Numero,Creacion,Termino,RutCliente,IdModalidad,IdTipoEvento,FechaHoraInicio, FechaHoraTermino, Asistentes, PersonalAdicional, Realizado, ValorTotalContrato,Observaciones)" +
                "VALUES(@numero, @creacion, @termino, @rut_cliente, @id_modalidad, @id_tipo_evento, @fecha_hora_inicio, @fecha_hora_termino, @asistentes, @personal_adicional, @realizado, @valorTotalContrato, @observaciones)";
                command.Parameters.AddWithValue("@numero", newContrato.numero);
                command.Parameters.AddWithValue("@creacion", newContrato.creacion);
                command.Parameters.AddWithValue("@termino", newContrato.termino);
                command.Parameters.AddWithValue("@rut_cliente", newContrato.rutCliente);
                command.Parameters.AddWithValue("@id_modalidad", newContrato.idModalidad);
                command.Parameters.AddWithValue("@id_tipo_evento", newContrato.idTipoEvento);
                command.Parameters.AddWithValue("@fecha_hora_inicio", newContrato.fechaHoraInicio);
                command.Parameters.AddWithValue("@fecha_hora_termino", newContrato.fechaHoraTermino);
                command.Parameters.AddWithValue("@asistentes", newContrato.asistentes);
                command.Parameters.AddWithValue("@personal_adicional", newContrato.personalAdicional);
                command.Parameters.AddWithValue("@realizado", newContrato.realizado);
                command.Parameters.AddWithValue("@valorTotalContrato", newContrato.valorTotalContrato);
                command.Parameters.AddWithValue("@observaciones", newContrato.observaciones);

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

        public static int UpdateSQL(Contrato newContrato)
        {
            Int32 rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);

                command.CommandText = "UPDATE Contrato SET Realizado=@realizado WHERE Numero=@numero";
                command.Parameters.AddWithValue("@realizado", newContrato.realizado);
                command.Parameters.AddWithValue("@numero", newContrato.numero);

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

        public static int eliminarSQL(Contrato newContrato)
        {
            Int32 rowAffected = 0;

            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                //query
                command.CommandText = "DELETE FROM Contrato WHERE Numero=@numero";

                command.Parameters.AddWithValue("@numero", newContrato.numero);

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

        public static Contrato Select(string numero)
        {
            Contrato _contrato = new Contrato();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Contrato where Numero = '{numero}'";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    
                    foreach (DataRow item in dt.Rows)
                    {
                        _contrato = new Contrato();
                        _contrato.numero = item["Numero"].ToString();
                        _contrato.creacion = (DateTime)item["Creacion"];
                        _contrato.termino = (DateTime)item["Termino"];
                        _contrato.rutCliente = item["RutCLiente"].ToString();
                        _contrato.idModalidad = item["IdModalidad"].ToString();
                        _contrato.idTipoEvento = int.Parse(item["IdTipoEvento"].ToString());
                        _contrato.fechaHoraInicio = (DateTime)item["FechaHoraInicio"];
                        _contrato.fechaHoraTermino = (DateTime)item["FechaHoraTermino"];
                        _contrato.asistentes = int.Parse(item["Asistentes"].ToString());
                        _contrato.personalAdicional = int.Parse(item["PersonalAdicional"].ToString());
                        _contrato.realizado = (Boolean)item["Realizado"];
                        _contrato.valorTotalContrato = (double)item["ValorTotalContrato"];
                        _contrato.observaciones = item["Observaciones"].ToString();
                        
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Contrato_Conexion/Select\n"+ ex.Message);
                    _contrato = null;
                }
            }
            return _contrato;

        }

        public static List<Contrato> searchSQLrut(string RutCliente)
        {
            contrato = new List<Contrato>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Contrato where RutCliente = '{RutCliente}'";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    Contrato _contrato;
                    foreach (DataRow item in dt.Rows)
                    {
                        _contrato = new Contrato();
                        _contrato.numero = item["Numero"].ToString();
                        _contrato.creacion = (DateTime)item["Creacion"];
                        _contrato.termino = (DateTime)item["Termino"];
                        _contrato.rutCliente = item["RutCLiente"].ToString();
                        _contrato.idModalidad = item["IdModalidad"].ToString();
                        _contrato.idTipoEvento = int.Parse(item["IdTipoEvento"].ToString());
                        _contrato.fechaHoraInicio = (DateTime)item["FechaHoraInicio"];
                        _contrato.fechaHoraTermino = (DateTime)item["FechaHoraTermino"];
                        _contrato.asistentes = int.Parse(item["Asistentes"].ToString());
                        _contrato.personalAdicional = int.Parse(item["PersonalAdicional"].ToString());
                        _contrato.realizado = (Boolean)item["Realizado"];
                        _contrato.valorTotalContrato = (double)item["ValorTotalContrato"];
                        _contrato.observaciones = item["Observaciones"].ToString();
                        contrato.Add(_contrato);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return contrato;
        }

        public static List<TipoEvento> searchSQLtipoEvento(int IdTipoEvento)
        {
            tipo_evento = new List<TipoEvento>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Contrato where IdTipoEvento = '{IdTipoEvento}";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    Contrato _contrato;
                    foreach (DataRow item in dt.Rows)
                    {
                        _contrato = new Contrato();
                        _contrato.numero = item["Numero"].ToString();
                        _contrato.creacion = (DateTime)item["Creacion"];
                        _contrato.termino = (DateTime)item["Termino"];
                        _contrato.rutCliente = item["RutCLiente"].ToString();
                        _contrato.idModalidad = item["IdModalidad"].ToString();
                        _contrato.idTipoEvento = int.Parse(item["IdTipoEvento"].ToString());
                        _contrato.fechaHoraInicio = (DateTime)item["FechaHoraInicio"];
                        _contrato.fechaHoraTermino = (DateTime)item["FechaHoraTermino"];
                        _contrato.asistentes = int.Parse(item["Asistentes"].ToString());
                        _contrato.personalAdicional = int.Parse(item["PersonalAdicional"].ToString());
                        _contrato.realizado = (Boolean)item["Realizado"];
                        _contrato.valorTotalContrato = (double)item["ValorTotalContrato"];
                        _contrato.observaciones = item["Observaciones"].ToString();
                        contrato.Add(_contrato);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return tipo_evento;
        }

        public static List<ModalidadServicio> searchSQLtipoModalidad(int IdModalidad)
        {
            modalidad = new List<ModalidadServicio>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                SqlCommand command = new SqlCommand(null, conn);
                DataTable dt = new DataTable();

                command.CommandText = $"select * from Contrato where IdModalidad = '{IdModalidad}";

                try
                {
                    conn.Open();
                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    Contrato _contrato;
                    foreach (DataRow item in dt.Rows)
                    {
                        _contrato = new Contrato();
                        _contrato.numero = item["Numero"].ToString();
                        _contrato.creacion = (DateTime)item["Creacion"];
                        _contrato.termino = (DateTime)item["Termino"];
                        _contrato.rutCliente = item["RutCLiente"].ToString();
                        _contrato.idModalidad = item["IdModalidad"].ToString();
                        _contrato.idTipoEvento = int.Parse(item["IdTipoEvento"].ToString());
                        _contrato.fechaHoraInicio = (DateTime)item["FechaHoraInicio"];
                        _contrato.fechaHoraTermino = (DateTime)item["FechaHoraTermino"];
                        _contrato.asistentes = int.Parse(item["Asistentes"].ToString());
                        _contrato.personalAdicional = int.Parse(item["PersonalAdicional"].ToString());
                        _contrato.realizado = (Boolean)item["Realizado"];
                        _contrato.valorTotalContrato = (double)item["ValorTotalContrato"];
                        _contrato.observaciones = item["Observaciones"].ToString();
                        contrato.Add(_contrato);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("error al mostrar1");
                }
            }
            return modalidad;
        }

        public static List<Contrato> SelectAll()
        {
            List<Contrato> c = new List<Contrato>();
            using (SqlConnection conn = new SqlConnection(Conexion.connectionstring))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(null, conn);
                    DataTable dt = new DataTable();

                    command.CommandText = "select * from Contrato";

                    var DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dt);
                    Contrato _contrato;
                    foreach (DataRow item in dt.Rows)
                    {
                        _contrato = new Contrato();
                        _contrato.numero = item["Numero"].ToString();
                        _contrato.creacion = (DateTime)item["Creacion"];
                        _contrato.termino = (DateTime)item["Termino"];
                        _contrato.rutCliente = item["RutCLiente"].ToString();
                        _contrato.idModalidad = item["IdModalidad"].ToString();
                        _contrato.idTipoEvento = int.Parse(item["IdTipoEvento"].ToString());
                        _contrato.fechaHoraInicio = (DateTime)item["FechaHoraInicio"];
                        _contrato.fechaHoraTermino = (DateTime)item["FechaHoraTermino"];
                        _contrato.asistentes = int.Parse(item["Asistentes"].ToString());
                        _contrato.personalAdicional = int.Parse(item["PersonalAdicional"].ToString());
                        _contrato.realizado = (Boolean)item["Realizado"];
                        _contrato.valorTotalContrato = (double)item["ValorTotalContrato"];
                        _contrato.observaciones = item["Observaciones"].ToString();
                        c.Add(_contrato);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Error en Contrato_Conexion/SelectAll\n"+ex.Message);
                }
                conn.Close();
            }
            return c;
        }
    }
}
