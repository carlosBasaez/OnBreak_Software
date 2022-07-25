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
using OnBreakData;
using Conexion_bd;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para Listado_cliente.xaml
    /// </summary>
    public partial class Listado_cliente : Window
    {
        Controlador.ListaClienteControl listaClientes = new Controlador.ListaClienteControl();
        Controlador.DataBack dataBack = null;

        public Listado_cliente(Controlador.DataBack dataBack = null)
        {
            InitializeComponent();
            ActualizarLista();
            Filtro_tActividad_comboBox.ItemsSource = listaClientes.list_ActividadEmpresa_string;
            Filtro_tEmpresa_comboBox.ItemsSource = listaClientes.list_tipoEmpresa_string;
            Filtro_rut_textBox.Text = "";
            Filtro_tActividad_comboBox.SelectedIndex = 0;
            Filtro_tEmpresa_comboBox.SelectedIndex = 0;
            this.dataBack = dataBack;
            if (this.dataBack == null) Btn_Aceptar.Visibility = Visibility.Collapsed;
        }

        

        void ActualizarLista()
        {
            listaClientes.GenerarListas();
            grid_datos.ItemsSource = listaClientes.list_clienteShow.ToArray(); 
        }

        private void Filtro_rut_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaClientes.filtro_rut = Filtro_rut_textBox.Text;
        }

        private void Filtro_tEmpresa_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filtro_tEmpresa_comboBox.SelectedIndex > 0)
            {
                listaClientes.filtro_tipoempresa = listaClientes.list_tipoEmpresa_string[Filtro_tEmpresa_comboBox.SelectedIndex];
            }
            else
            {
                listaClientes.filtro_tipoempresa = "";
            }
        }

        private void Filtro_tActividad_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Filtro_tActividad_comboBox.SelectedIndex;

            if (index > 0)
            {
                listaClientes.filtro_actividad = listaClientes.list_ActividadEmpresa_string[index];
            }
            else
            {
                listaClientes.filtro_actividad = "";
            }
        }

        private void Btn_Fitrar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarLista();
        }

        private void Btn_QuitarFitro_Click(object sender, RoutedEventArgs e)
        {
            Filtro_rut_textBox.Text = "";
            Filtro_tActividad_comboBox.SelectedIndex = 0;
            Filtro_tEmpresa_comboBox.SelectedIndex = 0;
            ActualizarLista();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            int index = grid_datos.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Tiene que seleccionar un cliente antes de aceptar.", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                dataBack.CallBack(listaClientes.list_clientes[index].Rut_cliente);
                this.Close();
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (dataBack == null)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                dataBack.CallBack();
                this.Close();
            }
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

        private void Grid_datos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = grid_datos.SelectedIndex;
            if (index >= 0)
            {
                if (dataBack != null) dataBack.CallBack(listaClientes.list_clientes[index].Rut_cliente);
                else
                {
                    Administracion_cliente ac = new Administracion_cliente();

                    Cliente cl = Cliente_conexion.Select(listaClientes.list_clientes[index].Rut_cliente);
                    ac.rut_act_txt.Text = cl.Rut_cliente;
                    ac.nombre_actualizar_txt.Text = cl.Nombre_contacto;
                    ac.mail_actualizar_txt.Text = cl.mail;
                    ac.direccion_actualizar_txt.Text = cl.direccion;
                    ac.telefono_actualizar_txt.Text = cl.telefono;
                    ac.RazonSocial_Actualizar_Txt.Text = cl.Razon_social;

                    //cbo_tipo_empresa_act
                    string tipo_empresa_desc = tipo_empresa_conexion.Select(cl.id_tipo_empresa).descripcion;
                    for (int i = 0; i < ac.tipo_empresa_mostrar.Count; i++)
                    {
                        if (ac.tipo_empresa_mostrar[i].Equals(tipo_empresa_desc))
                        {
                            ac.cbo_tipo_empresa_act.SelectedIndex = i;
                            break;
                        }
                    }

                    //cbo_acttividad empresa
                    string actividad_empresa_desc = Actividad_empresa_conexion.Select(cl.id_actividad_empresa).descripcion;
                    for (int i = 0; i < ac.act_empresa_mostrar.Count; i++)
                    {
                        if (ac.act_empresa_mostrar[i].Equals(actividad_empresa_desc))
                        {
                            ac.cbo_act_act.SelectedIndex = i;
                            break;
                        }
                    }

                    ac.grd_menu.Visibility = Visibility.Collapsed;
                    ac.grd_mantencion.Visibility = Visibility.Visible;

                    ac.Show();

                }
                this.Close();
            }
        }
    }
}
