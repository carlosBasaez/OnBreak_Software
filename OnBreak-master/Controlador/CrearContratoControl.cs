using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conexion_bd;
using OnBreakData;

namespace Controlador
{
    public class CrearContratoControl
    {
        public List<ModalidadServicio> list_modalidadServicio;
        public List<string> list_modalidadServicio_string;

        public List<TipoAmbientacion> list_TipoAmbientacion;
        public List<string> list_tipoAbientacion_string;

        public CacheLocal.CacheDicctionary cache;

        public CrearContratoControl()
        {
            GenerarLista();
            cache = new CacheLocal.CacheDicctionary("CacheCrearContrato.txt");
        }

        public void GenerarLista()
        {
            list_modalidadServicio = ModalidadServicio_Conexion.SelectAll();
            list_modalidadServicio_string = new List<string>();
            foreach (var item in list_modalidadServicio)
            {
                list_modalidadServicio_string.Add(tipoEvento_conexion.Select(item.idTipoEvento).descripcion + ", " + item.nombre);
            }

            list_TipoAmbientacion = TipoAmbientacion_Conexion.SelectAll();
            list_tipoAbientacion_string = new List<string>();
            foreach (var item in list_TipoAmbientacion)
            {
                list_tipoAbientacion_string.Add(item.descripcion);
            }
        }

        
        public void SaveTxt(string key, string value)
        {
            cache.Add(key, value);
        }

        public void SaveCheckBox(string key, bool value)
        {
            cache.Add(key, value ? "TRUE" : "FALSE");
        }

        public void SaveComboBox(string key, int value)
        {
            cache.Add(key, value.ToString());
        }

        public string GetTxt(string key)
        {
            return cache.Get(key);
        }

        public bool GetCheckBox(string key)
        {
            return cache.Get(key) == "TRUE";
        }

        public int GetComboBox(string key)
        {
            if (!cache.ExistKey(key)) return -1;
            return int.Parse(cache.Get(key));
        }
        

    }
}
