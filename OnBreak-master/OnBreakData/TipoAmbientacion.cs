using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class TipoAmbientacion
    {
        public int idTipoAmbientacion;
        public string descripcion;

        public override string ToString()
        {
            return $"TipoAmbientacion >> IdTipoAmbientacion: {idTipoAmbientacion}, Descripcion: {descripcion}";
        }
    }
}
