using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Cenas
    {
        public string numero;
        public int idTipoAmbientacion;
        public bool musicaAmbiental;
        public bool localOnBreack;
        public bool otroLocalOnBreak;
        public double valorArriendo;

        public override string ToString()
        {
            return $"Cenas >> Numero: {numero}, IdTipoAmbientacion: {idTipoAmbientacion}, MusicaAmbiental: {musicaAmbiental}, " +
                $"LocalOnBreak: {localOnBreack}, OtroLocalOnBreak: {otroLocalOnBreak}, ValorArriendo: {valorArriendo}";
        }
    }
}
