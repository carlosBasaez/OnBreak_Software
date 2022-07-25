using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Cliente
    {
        public string Rut_cliente { get; set; }
        public string Razon_social { get; set; }
        public string Nombre_contacto { get; set; }
        public string mail { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int id_tipo_empresa { get; set; }
        public int id_actividad_empresa { get; set; }

        public override string ToString()
        {
            return $"Cliente >> RutCliente: {Rut_cliente}, RazonSocial: {Razon_social}, NombreContacto: {Nombre_contacto}, " +
                $"Mail: {mail}, Direccion: {direccion}, Telefono: {telefono}, IdTipoEmpresa: {id_tipo_empresa}, IdActividadEmpresa: {id_actividad_empresa}";
        }
    }
}
