using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OnBreakData;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Conectar();
            Respaldo();
            //ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Dark.Green");
        }

        private void Conectar()
        {
            InitializeComponent();
            CacheLocal.CacheDicctionary cd = new CacheLocal.CacheDicctionary("ConfigBD.txt");
            if (cd.ExistFile())
            {
                Conexion_bd.Conexion.connectionstring = @"Server=" + cd.Get("Server") + ";Database=OnBreak;Trusted_Connection=True";
            }
            else
            {
                MessageBox.Show("No se encontro el archivo de configuracion de la Base de datos. Se recomienda ir a la configuracion para ingresar el nombre del servidor.");
            }
        }

        private void listado_contratos_Click(object sender, RoutedEventArgs e)
        {
            Listado_contrato lis = new Listado_contrato();
            lis.Show();
            this.Close();
        }

        private void listado_cliente_Click(object sender, RoutedEventArgs e)
        {
            Listado_cliente list = new Listado_cliente();
            list.Show();
            this.Close();
        }

        private void administracion_contrato_Click(object sender, RoutedEventArgs e)
        {
            Administracion_contrato adm = new Administracion_contrato();
            adm.Show();
            this.Close();
        }

        private void administracion_cliente_Click(object sender, RoutedEventArgs e)
        {
            Administracion_cliente cli = new Administracion_cliente();
            cli.Show();
            this.Close();
        }
        bool contraste = false;
        public void alto_contraste_Click(object sender, RoutedEventArgs e)
        {

            alto_contrast();
            
        }
        public void alto_contrast()
        {
            contraste = !contraste;
            if(!contraste)
            ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            if(contraste)
            ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Configuracion_bd config = new Configuracion_bd();
            config.Show();
            this.Close();
        }

        private void Respaldo()
        {
            CacheLocal.CacheDicctionary cd = new CacheLocal.CacheDicctionary("CacheCrearContrato.txt");
            if (cd.ExistFile())
            {
                Respaldo res = new Respaldo();
                res.Show();
                this.Close();
            }
        }
    }
}
