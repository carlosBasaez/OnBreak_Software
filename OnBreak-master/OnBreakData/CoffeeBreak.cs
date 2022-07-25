using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class CoffeeBreak
    {
        public string numero;
        public bool vegetariana;

        public override string ToString()
        {
            return $"CoffeeBreak >> Numero: {numero}, Vegetariana: {vegetariana}";
        }
    }
}
