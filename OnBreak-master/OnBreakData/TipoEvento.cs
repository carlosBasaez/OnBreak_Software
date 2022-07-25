using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class TipoEvento
    {
        public int idTipoEvento;
        public string descripcion;

        public override string ToString()
        {
            return idTipoEvento + ": " + descripcion;
        }
    }
}
