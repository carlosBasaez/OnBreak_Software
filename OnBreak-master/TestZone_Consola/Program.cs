using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreakData;
using Conexion_bd;

namespace TestZone_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOnBreakData();
            

            Console.ReadLine();
        }

        static void Print(string imprimir)
        {
            if (imprimir != null)
                Console.WriteLine(imprimir);
            else
                Console.WriteLine("null");
        }

        static void TestOnBreakData()
        {
            Print(">>Actividad empresa");
            Print(">SelectAll");
            foreach (var item in Actividad_empresa_conexion.SelectAll())
            {
                Print(item.ToString());
            }
            Print(">Select");
            var sel = Actividad_empresa_conexion.Select(3);
            if (sel != null)
                Print(sel.ToString());
            else
                Print("nullo");
            Print("");
            ////////////////////////////////////////////////////////////////////
            Print(">>Cenas");
            Print(">Insert");

            Print(">SelectAll");
            foreach (var item in Actividad_empresa_conexion.SelectAll())
            {
                Print(item.ToString());
            }
            Print(">Select");
            //var sel = Actividad_empresa_conexion.Select(3);
            if (sel != null)
                Print(sel.ToString());
            else
                Print("nullo");
            Print("");





        }
    }
}
