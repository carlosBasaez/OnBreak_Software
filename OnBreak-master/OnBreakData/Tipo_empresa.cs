using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Tipo_empresa
    {
        public int id_tipo_empresa;
        public string descripcion;

        public override string ToString()
        {
            return descripcion;
        }
    }
}
