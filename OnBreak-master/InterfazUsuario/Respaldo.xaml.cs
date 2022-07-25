using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para Respaldo.xaml
    /// </summary>
    public partial class Respaldo : Window
    {
        public Respaldo()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CacheLocal.CacheDicctionary cd = new CacheLocal.CacheDicctionary("CacheCrearContrato.txt");
            cd.Delete();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Crear_contrato cc = new Crear_contrato();
            cc.Show();
            this.Close();
        }
    }
}
