using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;
using Conexion_bd;

namespace Controlador
{
    public class ListaClienteControl
    {
        public List<Cliente> list_clientes;
        public List<ClienteShow> list_clienteShow;
        
        public List<Tipo_empresa> list_tipoEmpresa;
        public List<string> list_tipoEmpresa_string;
        public List<Actividad_empresa> list_ActividadEmpresa;
        public List<string> list_ActividadEmpresa_string;

        //Filtros
        public string filtro_rut = "";
        public string filtro_actividad = "";
        public string filtro_tipoempresa = "";

        public ListaClienteControl()
        {
            GenerarListasOtras();
            GenerarListas();
        }

        public void GenerarListas()
        {
            list_clientes = Cliente_conexion.SelectAll();
            Filtrar();
            list_clienteShow = new List<ClienteShow>();
            foreach (var item in list_clientes)
            {
                list_clienteShow.Add(new ClienteShow(item));
            }
        }

        public void GenerarListasOtras()
        {
            //tipo empresa
            list_tipoEmpresa = tipo_empresa_conexion.SelectAll();
            list_tipoEmpresa_string = new List<string>();
            list_tipoEmpresa_string.Add("-");
            foreach (var item in list_tipoEmpresa)
            {
                list_tipoEmpresa_string.Add(item.descripcion);
            }
            
            //actividad empresa
            list_ActividadEmpresa = Actividad_empresa_conexion.SelectAll();
            list_ActividadEmpresa_string = new List<string>();
            list_ActividadEmpresa_string.Add("-");
            foreach (var item in list_ActividadEmpresa)
            {
                list_ActividadEmpresa_string.Add(item.descripcion);
            }
        }

        void Filtrar()
        {
            var aux_list = list_clientes.ToArray();
            foreach (var item in aux_list)
            {
                if (filtro_rut != "")
                {
                    if (!item.Rut_cliente.Equals(filtro_rut))
                    {
                        list_clientes.Remove(item);
                        continue;
                    }
                }

                if (filtro_actividad != "")
                {
                    if (!Actividad_empresa_conexion.Select(item.id_actividad_empresa).descripcion.Equals(filtro_actividad))
                    {
                        list_clientes.Remove(item);
                        continue;
                    }
                }

                if (filtro_tipoempresa != "")
                {
                    if (!tipo_empresa_conexion.Select(item.id_tipo_empresa).descripcion.Equals(filtro_tipoempresa))
                    {
                        list_clientes.Remove(item);
                        continue;
                    }
                }
            }
        }

        public class ClienteShow
        {
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public string ActividadEmpresa { get; set; }
            public string TipoEmpresa { get; set; }

            public ClienteShow(Cliente cliente)
            {
                Rut = cliente.Rut_cliente;
                Nombre = cliente.Nombre_contacto;
                ActividadEmpresa = Actividad_empresa_conexion.Select(cliente.id_actividad_empresa).descripcion;
                TipoEmpresa = tipo_empresa_conexion.Select(cliente.id_tipo_empresa).descripcion;
            }
        }

        
    }
}
