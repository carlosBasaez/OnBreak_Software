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
using System.Windows.Shapes;
using OnBreakData;
using Controlador;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para Listado_contrato.xaml
    /// </summary>
    public partial class Listado_contrato : MetroWindow
    {
        public Listado_contrato(DataBack dataBack = null)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            listaContratosControl = new ListaContratosControl();
            Filtro_tipoEvento_comboBox.ItemsSource = listaContratosControl.list_tipoEvento_show.ToArray();
            Filtro_ModalidadServicio_comboBox.ItemsSource = listaContratosControl.list_modalidadEvento_show.ToArray();
            Filtro_tipoEvento_comboBox.SelectedIndex = 0;
            Filtro_ModalidadServicio_comboBox.SelectedIndex = 0;
            Actualizar();
            this.dataBack = dataBack;
            if (this.dataBack == null) Btn_Aceptar.Visibility = Visibility.Collapsed;
        }

        #region Variables
        ListaContratosControl listaContratosControl;
        DataBack dataBack = null;
        #endregion

        public void Actualizar()
        {
            Tabla_dataGrid.ItemsSource = listaContratosControl.list_contrato_show.ToArray();
        }

        #region Acciones de movimiento

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

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            int index = Tabla_dataGrid.SelectedIndex;
            if (index >= 0)
            {
                dataBack.CallBack(listaContratosControl.list_contrato_show[index].Numero);
                this.Close();
            }
            else
            {
                MessageBox.Show("Tiene que seleccionar un contrato antes de aceptar.", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        #region Filtro
        
        private void Filtrar_Click(object sender, RoutedEventArgs e)
        {
            listaContratosControl.filtro_numeroContrato = Filtro_numeroContrato_Txt.Text;
            listaContratosControl.filtro_rutCliente = Filtro_rutCliente_Txt.Text;

            int index;

            index = Filtro_tipoEvento_comboBox.SelectedIndex;
            if (index > 0)
                listaContratosControl.filtro_tipoEvento = listaContratosControl.list_tipoEvento_show[index];
            else
                listaContratosControl.filtro_tipoEvento = "";

            index = Filtro_ModalidadServicio_comboBox.SelectedIndex;
            if (index > 0)
                listaContratosControl.filtro_modalidadServicio = listaContratosControl.list_modalidadEvento_show[index];
            else
                listaContratosControl.filtro_modalidadServicio = "";

            listaContratosControl.GenerarListas();
            Actualizar();
        }

        private void QuitarFiltro_Click(object sender, RoutedEventArgs e)
        {
            Filtro_numeroContrato_Txt.Text = "";
            Filtro_rutCliente_Txt.Text = "";
            Filtro_tipoEvento_comboBox.SelectedIndex = 0;
            Filtro_ModalidadServicio_comboBox.SelectedIndex = 0;

            Filtrar_Click(sender, e);
        }




        #endregion

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
        

        private void Tabla_dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = Tabla_dataGrid.SelectedIndex;
            if (index >= 0)
            {
                if (dataBack != null) dataBack.CallBack(listaContratosControl.list_contrato_show[index].Numero);
                else
                {
                    Actualizar_contrato ac = new Actualizar_contrato();
                    ac.Show();
                    ac.numero_txt.Text = listaContratosControl.list_contratos[index].numero;
                    ac.rellenar();
                }
                this.Close();
            }
        }
    }
}
