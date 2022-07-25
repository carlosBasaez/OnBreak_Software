using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class CalculadorValor
    {

        #region Main

        public float valor = 0;
        private Modulo moduloBase = null;

        public abstract class Modulo
        {
            public Modulo siguiente;
            public abstract float Suma();
        }

        public void AddModulo(Modulo m)
        {
            if (moduloBase == null)
            {
                moduloBase = m;
            }
            else
            {
                Modulo aux = moduloBase;
                while (aux.siguiente != null)
                {
                    aux = aux.siguiente;
                }
                aux.siguiente = m;
            }
        }

        public float Calcular()
        {
            float val = 0;
            if (moduloBase != null)
                val = moduloBase.Suma();

            return val;
        }

        public string CalcularString()
        {
            return Calcular().ToString();
        }
        #endregion

        #region Sub Clases Modulo

        public class ValorBase : Modulo
        {
            float valorBase;

            public ValorBase(float valorBase)
            {
                this.valorBase = valorBase;
            }

            public override float Suma()
            {
                if (siguiente == null)
                    return valorBase;
                else
                    return valorBase + siguiente.Suma();
            }
        }

        //coffee break
        public class CoffeeBreak_Asistentes : Modulo
        {
            int ca;

            public CoffeeBreak_Asistentes(int cantidadAsis)
            {
                ca = cantidadAsis;
            }

            public override float Suma()
            {
                float valor = 0;
                if (ca <= 20) valor = 3;
                else if (ca <= 50) valor = 5;
                else
                {
                    valor = 2*(1 + (float)Math.Floor(ca / 20f));
                }

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class CoffeeBreak_PersonalAdicional : Modulo
        {
            int cant;
            public CoffeeBreak_PersonalAdicional(int cantidad)
            {
                cant = cantidad;
            }

            public override float Suma()
            {
                float valor = 0;
                if (cant <= 2) valor = 2;
                else if (cant == 3) valor = 3;
                else if (cant == 4) valor = 3.5f;
                else valor = 3.5f + (cant - 4) * 0.5f;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        //cocktail
        public class Cocktail_Asistentes : Modulo
        {
            int ca;

            public Cocktail_Asistentes(int cantidadAsis)
            {
                ca = cantidadAsis;
            }

            public override float Suma()
            {
                float valor = 0;
                if (ca <= 20) valor = 4;
                else if (ca <= 50) valor = 6;
                else
                {
                    valor = 2 * (1 + (float)Math.Floor(ca / 20f));
                }

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class Cocktail_PersonalAdicional : Modulo
        {
            int cant;
            public Cocktail_PersonalAdicional(int cantidad)
            {
                cant = cantidad;
            }

            public override float Suma()
            {
                float valor = 0;
                if (cant <= 2) valor = 2;
                else if (cant == 3) valor = 3;
                else if (cant == 4) valor = 3.5f;
                else valor = 3.5f + (cant - 4) * 0.5f;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class Cocktail_Ambientacion : Modulo
        {
            public enum Tipo
            {
                Basica, Personalizada, No
            }

            Tipo tipo;

            public Cocktail_Ambientacion(Tipo _tipo)
            {
                this.tipo = _tipo;
            }

            public override float Suma()
            {
                float valor = 0;
                switch (tipo)
                {
                    case Tipo.Basica:
                        valor = 2;
                        break;
                    case Tipo.Personalizada:
                        valor = 5;
                        break;
                    case Tipo.No:
                        valor = 0;
                        break;
                    default:
                        Console.WriteLine(">>>>>Error Calculo Valor Cocktail Ambientacion");
                        break;
                }

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }

        }

        public class Cocktail_MusicaAmbiental : Modulo
        {
            bool si;

            public Cocktail_MusicaAmbiental(bool _si)
            {
                this.si = _si;
            }

            public override float Suma()
            {
                float valor = 0;
                if (si) valor = 1;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        //cenas
        public class Cenas_Asistentes : Modulo
        {
            int ca;

            public Cenas_Asistentes(int cantidadAsis)
            {
                ca = cantidadAsis;
            }

            public override float Suma()
            {
                float valor = 0;
                if (ca <= 20) valor = 1.5f*ca;
                else if (ca <= 50) valor = 1.2f*ca;
                else
                {
                    valor = ca;
                }

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class Cenas_PersonalAdicional : Modulo
        {
            int cant;
            public Cenas_PersonalAdicional(int cantidad)
            {
                cant = cantidad;
            }

            public override float Suma()
            {
                float valor = 0;
                if (cant <= 2) valor = 3;
                else if (cant == 3) valor = 4;
                else if (cant == 4) valor = 5;
                else valor = 5f + (cant - 4) * 0.5f;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class Cenas_Ambientacion : Modulo
        {
            public enum Tipo
            {
                Basica, Personalizada
            }

            Tipo tipo;

            public Cenas_Ambientacion(Tipo _tipo)
            {
                this.tipo = _tipo;
            }

            public override float Suma()
            {
                float valor = 0;
                switch (tipo)
                {
                    case Tipo.Basica:
                        valor = 3;
                        break;
                    case Tipo.Personalizada:
                        valor = 5;
                        break;
                    default:
                        Console.WriteLine(">>>>>Error Calculo Valor Cocktail Ambientacion");
                        break;
                }

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }

        }

        public class Cenas_MusicaAmbiental : Modulo
        {
            bool si;

            public Cenas_MusicaAmbiental(bool _si)
            {
                this.si = _si;
            }

            public override float Suma()
            {
                float valor = 0;
                if (si) valor = 1.5f;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }

        public class Cenas_Local : Modulo
        {
            public enum Tipo { LocalOnBreak, Otro}
            Tipo tipo;
            float otroValor;

            public Cenas_Local(Tipo _tipo, float _otroValor = 0f)
            {
                this.tipo = _tipo;
                this.otroValor = _otroValor;
            }

            public override float Suma()
            {
                float valor = 9;
                if (tipo == Tipo.Otro) valor = otroValor;

                if (siguiente == null)
                    return valor;
                else
                    return valor + siguiente.Suma();
            }
        }
        #endregion

    }
}
