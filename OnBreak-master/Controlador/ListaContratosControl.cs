using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;
using Conexion_bd;

namespace Controlador
{
    public class ListaContratosControl
    {
        public ListaContratosControl()
        {
            GenerarListas();
            GenerarOtrasListas();
        }


        #region Variables

        public List<Contrato> list_contratos;
        public List<ContratoShow> list_contrato_show;

        public string filtro_numeroContrato = "";
        public string filtro_rutCliente = "";
        public string filtro_tipoEvento = "";
        public string filtro_modalidadServicio = "";

        public List<TipoEvento> list_tipoEvento;
        public List<string> list_tipoEvento_show;
        public List<ModalidadServicio> list_modalidadServicio;
        public List<string> list_modalidadEvento_show;

        #endregion

        #region Listas

        public void GenerarListas()
        {
            list_contratos = Contrato_conexion.SelectAll();
            Filtro();
            list_contrato_show = new List<ContratoShow>();
            foreach (var item in list_contratos)
            {
                list_contrato_show.Add(new ContratoShow(item));
            }
        }

        void Filtro()
        {
            var aux_list_contratos = list_contratos.ToArray();
            foreach (var item in aux_list_contratos)
            {
                //filtro numero
                if (filtro_numeroContrato != "")
                {
                    if (!item.numero.Equals(filtro_numeroContrato))
                    {
                        list_contratos.Remove(item);
                        continue;
                    }
                }

                //filtro rut
                if (filtro_rutCliente != "")
                {
                    if (!item.rutCliente.Equals(filtro_rutCliente))
                    {
                        list_contratos.Remove(item);
                        continue;
                    }
                }

                //filtro tipo evento
                if (filtro_tipoEvento != "")
                {
                    if (!tipoEvento_conexion.Select(item.idTipoEvento).descripcion.Equals(filtro_tipoEvento))
                    {
                        list_contratos.Remove(item);
                        continue;
                    }
                }

                //filtro modalidad servicio
                if (filtro_modalidadServicio != "")
                {
                    if (!ModalidadServicio_Conexion.Select(item.idModalidad).nombre.Equals(filtro_modalidadServicio))
                    {
                        list_contratos.Remove(item);
                        continue;
                    }
                }

            }
        }

        public void GenerarOtrasListas()
        {
            list_tipoEvento = tipoEvento_conexion.SelectAll();
            list_tipoEvento_show = new List<string>();
            list_tipoEvento_show.Add("-");
            foreach (var item in list_tipoEvento)
            {
                list_tipoEvento_show.Add(item.descripcion);
            }

            list_modalidadServicio = ModalidadServicio_Conexion.SelectAll();
            list_modalidadEvento_show = new List<string>();
            list_modalidadEvento_show.Add("-");
            foreach (var item in list_modalidadServicio)
            {
                list_modalidadEvento_show.Add(item.nombre);
            }
        }

        #endregion


        #region Clases

        public class ContratoShow
        {
            public string Numero { get; set; }
            public string RutCliente { get; set; }
            public string TipoEvento { get; set; }
            public string ModalidadServicio { get; set; }

            public ContratoShow(Contrato c)
            {
                Numero = c.numero;
                RutCliente = c.rutCliente;
                TipoEvento = tipoEvento_conexion.Select(c.idTipoEvento).descripcion;
                ModalidadServicio = ModalidadServicio_Conexion.Select(c.idModalidad).nombre;
            }

        }

        #endregion
    }
}
