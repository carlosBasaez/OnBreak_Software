using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InterfazUsuario
{
    public class Utilities
    {
        public static int TextBoxToInt(TextBox tb)
        {
            int value = -1;

            if (tb.Text != "")
            {
                if (!int.TryParse(tb.Text, out value))
                {
                    value = -1;
                    tb.Text = "";
                }
            }

            return value;
        }
    }
}
