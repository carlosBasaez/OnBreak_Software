using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Actividad_empresa
    {
        public int id_actividad_empresa;
        public string descripcion;

        public override string ToString()
        {
            return descripcion;
        }
    }
}
