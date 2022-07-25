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
    /// Lógica de interacción para Actualizar_contrato.xaml
    /// </summary>
    public partial class Actualizar_contrato : Window
    {
        public Actualizar_contrato()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administracion_contrato con = new Administracion_contrato();
            con.Show();
            this.Close();
        }

        public void rellenar()
        {
            if (Contrato_conexion.Select(numero_txt.Text) == null)
            {
                MessageBox.Show("El contrato no existe", "Mensaje emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            Contrato con = Contrato_conexion.Select(numero_txt.Text);
            txt_rut.Text = con.rutCliente;
            fecha_ini.Text = con.creacion.ToString("MM/dd/yyyy");
            fecha_termino.Text = con.termino.ToString("MM/dd/yyyy");
            hora_inicio.Text = con.fechaHoraInicio.ToString("HH");
            hora_termino.Text = con.fechaHoraTermino.ToString("HH");
            asistente_txt.Text = con.asistentes.ToString();
            personal_add_text.Text = con.personalAdicional.ToString();
            observaciones.Text = con.observaciones;
            modalidad_txt.Text = ModalidadServicio_Conexion.Select(con.idModalidad).nombre;
            valor_total_txt.Text = con.valorTotalContrato.ToString();
            //tipos de modalidad
            if(con.idTipoEvento == 10)
            {
               CoffeeBreak coffee = CoffeeBreak_Conexion.Select(con.numero);
               grd_coffee_break.Visibility = Visibility.Visible;
                grd_cocktail.Visibility = Visibility.Collapsed;
                grd_cenas.Visibility = Visibility.Collapsed;
                
               string opc_vegetariana = "";
               if(coffee.vegetariana == false)
                {
                    opc_vegetariana = "No";
                    opc_veg.Text = opc_vegetariana;
                }
                else
                {
                    opc_vegetariana = "Si";
                    opc_veg.Text = opc_vegetariana;
                }

            }



            else if (con.idTipoEvento == 20)
            {
                Cocktail cocktail = Cocktail_Conexion.Select(con.numero);
                grd_coffee_break.Visibility = Visibility.Collapsed;
                grd_cocktail.Visibility = Visibility.Visible;
                grd_cenas.Visibility = Visibility.Collapsed;
                //tipo amb
                string tipo_amb = "";
                if(cocktail.idTipoAmbientacion == 10)
                {
                    tipo_ambientacion.Text = tipo_amb = "Basica";
                }
                else
                {
                    tipo_ambientacion.Text = tipo_amb = "Personalizada";
                }
                //musica
                string musica = "";
                if (cocktail.musicaAmbiental == true && cocktail.musicaCliente==false)
                {
                    musica_txt.Text = musica = "Musica ambiental";
                }
                else if (cocktail.musicaCliente == true && cocktail.musicaAmbiental==false)
                {
                    musica_txt.Text = musica = "Musica cliente";
                }
                else if (cocktail.musicaCliente == false && cocktail.musicaAmbiental == false)
                {
                    musica_txt.Text = musica = "Sin musica";
                }
            }

            else if (con.idTipoEvento == 30)
            {
                Cenas cena = Cenas_Conexion.Select(con.numero);
                grd_coffee_break.Visibility = Visibility.Collapsed;
                grd_cocktail.Visibility = Visibility.Collapsed;
                grd_cenas.Visibility = Visibility.Visible;
                //ambientacion
                string tipo_amb_cena = "";
                if (cena.idTipoAmbientacion == 10)
                {
                    txt_amb_cenas.Text = tipo_amb_cena = "Basica";
                }
                else
                {
                    txt_amb_cenas.Text = tipo_amb_cena = "Personalizada";
                }
                //musica
                string tiene_musica = "";
                if (cena.musicaAmbiental == true) musica_cena_txt.Text = tiene_musica = "Si";
                else
                {
                    tiene_musica = "No";
                }
                //tipo local
                string tipo_local = "";
                if (cena.localOnBreack == true && cena.otroLocalOnBreak == false) local_cena_txt.Text = tipo_local = "Local OnBreak";
                else if (cena.localOnBreack == false && cena.otroLocalOnBreak == true)
                {
                    local_cena_txt.Text = tipo_local = "Otro local OnBreak";
                }
                else if (cena.otroLocalOnBreak==false && cena.localOnBreack == true)
                {
                    local_cena_txt.Text = tipo_local = "Local Externo";
                }
                valor_txt.Text = cena.valorArriendo.ToString();
            }
            else
            {
                MessageBox.Show("no hay datos asociados");
                return;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
