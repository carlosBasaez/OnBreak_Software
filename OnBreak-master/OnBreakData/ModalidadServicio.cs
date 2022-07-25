using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class ModalidadServicio
    {
        public string idModalidad;
        public int idTipoEvento;
        public string nombre;
        public double valorBase;
        public int personalBase;

        public override string ToString()
        {
            return $"ModalidadServicio >> IdModalidad: {idModalidad}, IdTipoEvento: {idTipoEvento}, Nombre: {nombre}, " +
                $"ValorBase: {valorBase}, PersonalBase: {personalBase}";
        }
    }
}
