using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Cocktail
    {
        public string numero;
        public int idTipoAmbientacion;
        public bool ambientacion;
        public bool musicaAmbiental;
        public bool musicaCliente;

        public override string ToString()
        {
            return $"Cocktail >> Numero: {numero}, IdTipoAmbientacion: {idTipoAmbientacion}, Ambientacion: {ambientacion}, MusicaAmbiental: {musicaAmbiental}, " +
                $"MusicaCliente: {musicaCliente}";
        }
    }
}
