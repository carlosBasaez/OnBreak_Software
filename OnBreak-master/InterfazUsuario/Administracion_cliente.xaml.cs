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
    /// Lógica de interacción para Administracion_cliente.xaml
    /// </summary>
    public partial class Administracion_cliente : Window, DataBack
    {
        public Administracion_cliente()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tipo_empresa = tipo_empresa_conexion.SelectAll();
            tipo_empresa_mostrar = new List<string>();
            foreach (var item in tipo_empresa)
            {
                tipo_empresa_mostrar.Add(item.ToString());
            }
            cbo_tipo_empresa.ItemsSource = tipo_empresa_mostrar;

            act_empresa = Actividad_empresa_conexion.SelectAll();
            act_empresa_mostrar = new List<string>();
            foreach (var item in act_empresa)
            {
                act_empresa_mostrar.Add(item.ToString());
            }
            cbo_act.ItemsSource = act_empresa_mostrar;

            //
            InitializeComponent();
            tipo_empresa = tipo_empresa_conexion.SelectAll();
            tipo_empresa_mostrar = new List<string>();
            foreach (var item in tipo_empresa)
            {
                tipo_empresa_mostrar.Add(item.ToString());
            }
            cbo_tipo_empresa_act.ItemsSource = tipo_empresa_mostrar;

            act_empresa = Actividad_empresa_conexion.SelectAll();
            act_empresa_mostrar = new List<string>();
            foreach (var item in act_empresa)
            {
                act_empresa_mostrar.Add(item.ToString());
            }
            cbo_act_act.ItemsSource = act_empresa_mostrar;
            
        }

        private void reg_usuario_Click(object sender, RoutedEventArgs e)
        {
            grd_menu.Visibility = Visibility.Collapsed;
            grd_reg_usuario.Visibility = Visibility.Visible;
        }

        private void cancelar_grd_reg_Click(object sender, RoutedEventArgs e)
        {
            grd_reg_usuario.Visibility = Visibility.Collapsed;
            grd_menu.Visibility = Visibility.Visible;
        }

        private void cancelar_grd_borrar_Click(object sender, RoutedEventArgs e)
        {
            grd_borrar_usuario.Visibility = Visibility.Collapsed;
            grd_menu.Visibility = Visibility.Visible;
        }

        private void cancelar_grd_buscar_Click(object sender, RoutedEventArgs e)
        {
            grd_buscar_usuario.Visibility = Visibility.Collapsed;
            grd_menu.Visibility = Visibility.Visible;
        }

        private void mantencion_usu_Click(object sender, RoutedEventArgs e)
        {
            grd_menu.Visibility = Visibility.Collapsed;
            grd_mantencion.Visibility = Visibility.Visible;
            
        }

        private void borrar_usuario_Click(object sender, RoutedEventArgs e)
        {
            grd_menu.Visibility = Visibility.Collapsed;
            grd_borrar_usuario.Visibility = Visibility.Visible;
        }

        private void busc_usuario_Click(object sender, RoutedEventArgs e)
        {
            grd_menu.Visibility = Visibility.Collapsed;
            grd_buscar_usuario.Visibility = Visibility.Visible;
        }

        private void cancelar_grd_reg_Click_1(object sender, RoutedEventArgs e)
        {
            grd_reg_usuario.Visibility = Visibility.Collapsed;
            grd_menu.Visibility = Visibility.Visible;
        }

        private void cancelar_grd_mant_Click(object sender, RoutedEventArgs e)
        {
            grd_mantencion.Visibility = Visibility.Collapsed;
            grd_menu.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grd_mantencion.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Atencion, se borrara solo si no posee un contrato");
            Cliente cli = new Cliente()
            {
                Rut_cliente =rut_borrar_txt.Text
            };
            //Cliente existe?
            if (Cliente_conexion.Select(cli.Rut_cliente) != null)
            {
                int num = Cliente_conexion.eliminarSQL(cli);
                if (num > 0)
                    MessageBox.Show("Cliente eliminado", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                else
                    MessageBox.Show("Cliente no se pudo eliminar, porque posee contratos", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("No existe cliente", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Listado_cliente lisc = new Listado_cliente();
            lisc.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public List<Actividad_empresa> act_empresa;
        public List<string> act_empresa_mostrar;

        public List<Tipo_empresa> tipo_empresa;
        public List<string> tipo_empresa_mostrar;

        private void Aceptar_usuario_Click(object sender, RoutedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(rut_txt.Text, @"[0-9]{8}\-[0-9]|k|K"))
            {
                MessageBox.Show("El rut del cliente debe ser: 12345678-k", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            
            if (Cliente_conexion.Select(rut_txt.Text) != null)
            {
                MessageBox.Show("El usuario ya existe en el sistema", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (cbo_tipo_empresa.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un tipo de empresa", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (cbo_act.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione la actividad de la empresa", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (razon_txt.Text == "")
            {
                MessageBox.Show("Ingrese una razon social", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (nombre_txt.Text == "")
            {
                MessageBox.Show("Ingrese el nombre", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (mail_txt.Text== "")
            {
                MessageBox.Show("Ingrese un mail", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if(direccion_txt.Text == "")
            {
                MessageBox.Show("Ingrese una direccion", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (telefono_txt.Text == "")
            {
                MessageBox.Show("Ingrese un telefono", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            Cliente cli = new Cliente()
            {
                Rut_cliente = rut_txt.Text,
                Razon_social = razon_txt.Text,
                Nombre_contacto = nombre_txt.Text,
                mail = mail_txt.Text,
                direccion = direccion_txt.Text,
                telefono = telefono_txt.Text,
                id_tipo_empresa = tipo_empresa[cbo_tipo_empresa.SelectedIndex].id_tipo_empresa,
                id_actividad_empresa = act_empresa[cbo_act.SelectedIndex].id_actividad_empresa
            };
            int num = Cliente_conexion.insertarSQL(cli);          
            if (num > 0)
                MessageBox.Show("Cliente Creado con exito", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
                MessageBox.Show("Cliente no se pudo crear, rellene todos los datos", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            if (!System.Text.RegularExpressions.Regex.IsMatch(rut_act_txt.Text, @"[0-9]{8}\-[0-9]|k|K"))
            {
                MessageBox.Show("El rut del cliente debe ser: 12345678-k", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (Cliente_conexion.Select(rut_act_txt.Text) == null)
            {
                MessageBox.Show("El usuario no existe en el sistema", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (cbo_tipo_empresa_act.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un tipo de empresa", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (cbo_act_act.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione la actividad de la empresa", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (nombre_actualizar_txt.Text == "")
            {
                MessageBox.Show("Ingrese el nombre", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (mail_actualizar_txt.Text == "")
            {
                MessageBox.Show("Ingrese un mail", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (direccion_actualizar_txt.Text == "")
            {
                MessageBox.Show("Ingrese una direccion", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (telefono_actualizar_txt.Text == "")
            {
                MessageBox.Show("Ingrese un telefono", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }


            Cliente cli = new Cliente()
            {
                Rut_cliente = rut_act_txt.Text,
                Nombre_contacto = nombre_actualizar_txt.Text,
                mail = mail_actualizar_txt.Text,
                direccion = direccion_actualizar_txt.Text,
                telefono = telefono_actualizar_txt.Text,
                id_tipo_empresa = tipo_empresa[cbo_tipo_empresa_act.SelectedIndex].id_tipo_empresa,
                Razon_social = RazonSocial_Actualizar_Txt.Text,
                id_actividad_empresa = act_empresa[cbo_act_act.SelectedIndex].id_actividad_empresa
            };
            int num = Cliente_conexion.updateSQL(cli);
            if (num > 0)
                MessageBox.Show("Cliente actualizado", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
                MessageBox.Show("Cliente no pudo ser actualizado", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Listado_cliente list = new Listado_cliente();
            list.Show();
            this.Close();
        }

        private void BuscarEnListaCliente_Click(object sender, RoutedEventArgs e)
        {
            Listado_cliente lc = new Listado_cliente(this);
            lc.Show();
            this.Visibility = Visibility.Collapsed;
        }

        void DataBack.CallBack(string data)
        {
            this.Visibility = Visibility.Visible;
            if (data != null)
            {
                rut_borrar_txt.Text = data;
                rut_act_txt.Text = data;
                search_rut.Text = data;

                Cliente cl = Cliente_conexion.Select(data);
                nombre_actualizar_txt.Text = cl.Nombre_contacto;
                mail_actualizar_txt.Text = cl.mail;
                direccion_actualizar_txt.Text = cl.direccion;
                telefono_actualizar_txt.Text = cl.telefono;
                RazonSocial_Actualizar_Txt.Text = cl.Razon_social;

                //cbo_tipo_empresa_act
                string tipo_empresa_desc = tipo_empresa_conexion.Select(cl.id_tipo_empresa).descripcion;
                for (int i = 0; i < tipo_empresa_mostrar.Count; i++)
                {
                    if (tipo_empresa_mostrar[i].Equals(tipo_empresa_desc))
                    {
                        cbo_tipo_empresa_act.SelectedIndex = i;
                        break;
                    }
                }

                //cbo_acttividad empresa
                string actividad_empresa_desc = Actividad_empresa_conexion.Select(cl.id_actividad_empresa).descripcion;
                for (int i = 0; i < act_empresa_mostrar.Count; i++)
                {
                    if (act_empresa_mostrar[i].Equals(actividad_empresa_desc))
                    {
                        cbo_act_act.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void aceptar_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (Cliente_conexion.Select(search_rut.Text) == null)
            {
                MessageBox.Show("El cliente no existe", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            Cliente cl = Cliente_conexion.Select(search_rut.Text);
            rut_act_txt.Text = search_rut.Text;
            nombre_actualizar_txt.Text = cl.Nombre_contacto;
            mail_actualizar_txt.Text = cl.mail;
            direccion_actualizar_txt.Text = cl.direccion;
            telefono_actualizar_txt.Text = cl.telefono;
            RazonSocial_Actualizar_Txt.Text = cl.Razon_social;

            //cbo_tipo_empresa_act
            string tipo_empresa_desc = tipo_empresa_conexion.Select(cl.id_tipo_empresa).descripcion;
            for (int i = 0; i < tipo_empresa_mostrar.Count; i++)
            {
                if (tipo_empresa_mostrar[i].Equals(tipo_empresa_desc))
                {
                    cbo_tipo_empresa_act.SelectedIndex = i;
                    break;
                }
            }

            //cbo_acttividad empresa
            string actividad_empresa_desc = Actividad_empresa_conexion.Select(cl.id_actividad_empresa).descripcion;
            for (int i = 0; i < act_empresa_mostrar.Count; i++)
            {
                if (act_empresa_mostrar[i].Equals(actividad_empresa_desc))
                {
                    cbo_act_act.SelectedIndex = i;
                    break;
                }
            }

            grd_buscar_usuario.Visibility = Visibility.Collapsed;
            grd_mantencion.Visibility = Visibility.Visible;
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
