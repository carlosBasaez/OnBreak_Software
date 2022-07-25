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
using Conexion_bd;
using Controlador;
using OnBreakData;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para Administracion_contrato.xaml
    /// </summary>
    public partial class Administracion_contrato : Window, Controlador.DataBack
    {
        public Administracion_contrato()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public void CallBack(string data = null)
        {
            if(data != null)
            {
                nro_contrato.Text = data;

            }
            this.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Crear_contrato cr = new Crear_contrato();
            cr.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            grd_mantencion_contrato.Visibility = Visibility.Collapsed;
            grd_menu_contrato.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            grd_mantencion_contrato.Visibility = Visibility.Visible;
            grd_menu_contrato.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            Actualizar_contrato act = new Actualizar_contrato();
            act.Show();
            if (Contrato_conexion.Select(nro_contrato.Text) == null)
            {
                MessageBox.Show("El contrato no existe", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            act.numero_txt.Text = nro_contrato.Text;
            act.rellenar();
            this.Close();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            Listado_contrato list = new Listado_contrato(this);
            list.Show();
            this.Visibility = Visibility.Collapsed;

        }

        bool contraste = false;
        public void alto_contraste_Click(object sender, RoutedEventArgs e)
        {

            alto_contrast();

        }
        public void alto_contrast()
        {
            contraste = !contraste;
            if (!contraste)
                ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Light.Blue");
            if (contraste)
                ControlzEx.Theming.ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
        }
    }
}
