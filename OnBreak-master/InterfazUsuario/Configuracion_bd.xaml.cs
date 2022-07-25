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
    /// Lógica de interacción para Configuracion_bd.xaml
    /// </summary>
    public partial class Configuracion_bd : Window
    {
        public Configuracion_bd()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CacheLocal.CacheDicctionary cd = new CacheLocal.CacheDicctionary("ConfigBD.txt");
            if (cd.ExistFile())
            {
                config_bd_txt.Text = cd.Get("Server");
            }
            else
            {
                config_bd_txt.Text = @"DESKTOP-F8TM6K2\SQLEXPRESS";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //aceptar
            string aux_conexion = Conexion_bd.Conexion.connectionstring;
            Conexion_bd.Conexion.connectionstring = @"Server=" + config_bd_txt.Text + ";Database=OnBreak;Trusted_Connection=True";
            if (!Conexion_bd.Conexion.test())
            {
                Conexion_bd.Conexion.connectionstring = aux_conexion;
                MessageBox.Show("Servidor incorrecto");
                return;
            }
            CacheLocal.CacheDicctionary cd = new CacheLocal.CacheDicctionary("ConfigBD.txt");
            cd.Clear();
            cd.Add("Server",config_bd_txt.Text);
            cd.Save();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
