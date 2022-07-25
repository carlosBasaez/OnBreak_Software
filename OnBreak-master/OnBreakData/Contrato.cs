using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakData
{
    public class Contrato
    {
        public string numero;
        public DateTime creacion;
        public DateTime termino;
        public string rutCliente;
        public string idModalidad;
        public int idTipoEvento;
        public DateTime fechaHoraInicio;
        public DateTime fechaHoraTermino;
        public int asistentes;
        public int personalAdicional;
        public bool realizado;
        public double valorTotalContrato;
        public string observaciones;

        public override string ToString()
        {
            return $"Contrato >> Numero: {numero}, Creacion: {creacion}, Termino: {termino}, " +
                $"RutCliente: {rutCliente}, IdModalidad: {idModalidad}, IdTipoEvento: {idTipoEvento}, " +
                $"FechaHoraInicio: {fechaHoraInicio}, FechaHoraTermino: {fechaHoraTermino}, Asistentes: {asistentes}, " +
                $"PersonalAdicional: {personalAdicional}, Realizado: {realizado}, ValorTotalContrato: {valorTotalContrato}, " +
                $"Observaciones: {observaciones}";
        }
    }
}
