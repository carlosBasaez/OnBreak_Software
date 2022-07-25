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
using Controlador;

namespace InterfazUsuario
{
    /// <summary>
    /// Lógica de interacción para Administracion_contrato.xaml
    /// </summary>
    public partial class Crear_contrato : Window, DataBack
    {
        public Crear_contrato()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            crearContratoControl = new CrearContratoControl();
            cbo_modalidad.ItemsSource = crearContratoControl.list_modalidadServicio_string.ToArray();
            cbo_tipo_ambientacion.ItemsSource = crearContratoControl.list_tipoAbientacion_string.ToArray();
            cbo_tipo_ambientacion_cena.ItemsSource = crearContratoControl.list_tipoAbientacion_string.ToArray();
            calcularActive = true;
            Calcular();
            ActivarAutoguardado();
            if (crearContratoControl.cache.ExistFile()) CargarRespaldo();
        }

        CrearContratoControl crearContratoControl;
        List<TipoEvento> tipo_evento;
        List<string> tipo_evento_mostrar;
        Controlador.CalculadorValor calculadorValor;

        bool calcularActive = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {//Cancelar / Salir
            crearContratoControl.cache.Delete();
            timer.Enabled = false;
            Administracion_contrato adm = new Administracion_contrato();
            adm.Show();
            this.Close();
        }

        private void cbo_modalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cbo_modalidad.SelectedIndex;
            int idTipoEvento = crearContratoControl.list_modalidadServicio[index].idTipoEvento;
            if (idTipoEvento == 10)
            {
                grd_coffee_break.Visibility = Visibility.Visible;
                grd_cocktail.Visibility = Visibility.Collapsed;
                grd_cena.Visibility = Visibility.Collapsed;
            }
            if (idTipoEvento == 20)
            {
                grd_coffee_break.Visibility = Visibility.Collapsed;
                grd_cocktail.Visibility = Visibility.Visible;
                grd_cena.Visibility = Visibility.Collapsed;
            }
            if (idTipoEvento == 30)
            {
                grd_coffee_break.Visibility = Visibility.Collapsed;
                grd_cocktail.Visibility = Visibility.Collapsed;
                grd_cena.Visibility = Visibility.Visible;
            }
            Calcular();
        }
        private string generar_numero_contrato()
        {
            DateTime dt = DateTime.Now;
            string salida = "";
            salida += dt.Year;
            salida += dt.Month;
            salida += dt.Day;
            salida += dt.Hour;
            salida += dt.Minute;
            return salida;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//Aceptar Creacion

            ////////////////////////////Comprobar si los datos son correctos
            ////////////Contrato
            /*if (!System.Text.RegularExpressions.Regex.IsMatch(rut_txt.Text, @"[0-9]{8}\-[0-9]|k|K"))
            {
                MessageBox.Show("El rut del cliente debe ser: 12345678-k", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }*/

            if (Cliente_conexion.Select(txt_rut_cliente.Text) == null)
            {
                MessageBox.Show("El usuario no existe en el sistema", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (fechaInicio_datetime.SelectedDate == null)
            {
                MessageBox.Show("La fecha de inicio del evento no se ha seleccionado", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (hora_inicio.Text == "")
            {
                MessageBox.Show("La hora de inicio del evento no se ha indicado", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            DateTime fechaHoraInicio = fechaInicio_datetime.SelectedDate.Value;
            fechaHoraInicio = fechaHoraInicio.AddHours(int.Parse(hora_inicio.Text));
            if (fechaHoraInicio <= DateTime.Now)
            {
                MessageBox.Show("La fecha y hora de inicio del evento tiene que ser en una fecha y hora posterior a la actual", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (fechaHoraInicio > DateTime.Now.AddMonths(10))
            {
                MessageBox.Show("La fecha y hora de inicio del evento debe ser antes de 10 meses de la fecha y hora actual", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            //
            if (fechaTermino_datetime.SelectedDate == null)
            {
                MessageBox.Show("La fecha de termino del evento no se ha seleccionado", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (hora_termino.Text == "")
            {
                MessageBox.Show("La hora de termino del evento no se ha indicado", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            DateTime fechaHoraTermino = fechaTermino_datetime.SelectedDate.Value;
            fechaHoraTermino = fechaHoraTermino.AddHours(int.Parse(hora_termino.Text));
            if (fechaHoraTermino < fechaHoraInicio.AddHours(1))
            {
                MessageBox.Show("La hora de termino del evento tiene que ser al menos 1 hora despues del inicio", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            if (fechaHoraTermino > fechaHoraInicio.AddDays(1))
            {
                MessageBox.Show("La hora de termino del evento tiene que ser antes de las 24 horas posteriores del inicio del evento", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }


            /////////// Tipo Evento
            int index = cbo_modalidad.SelectedIndex;
            int idTipoEvento = crearContratoControl.list_modalidadServicio[index].idTipoEvento;
            if (idTipoEvento == 10)
            {
                //coffebreak
                //ninguna comprobacion necesaria
            }
            if (idTipoEvento == 20)
            {
                //cocktail
                if (cbo_tipo_ambientacion.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione el tipo de ambientacion", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
            if (idTipoEvento == 30)
            {
                //cena
                if (cbo_tipo_ambientacion_cena.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione el tipo de ambientacion", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                if(cbo_tipo_local.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione el tipo de local", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                if (cbo_tipo_local.SelectedIndex < 2 && int.Parse(Valor_txt_local_cena.Text) <= 0)
                {
                    MessageBox.Show("Como no es un local externo, ingrese el valor del local", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }

            //Preparar Insert
            Contrato con = new Contrato
            {
                numero = generar_numero_contrato(),
                creacion = DateTime.Now,
                termino = fechaTermino_datetime.SelectedDate.Value.AddDays(7),
                rutCliente = txt_rut_cliente.Text,
                idModalidad = crearContratoControl.list_modalidadServicio[cbo_modalidad.SelectedIndex].idModalidad,
                idTipoEvento = crearContratoControl.list_modalidadServicio[cbo_modalidad.SelectedIndex].idTipoEvento,
                fechaHoraInicio = fechaInicio_datetime.SelectedDate.Value.AddHours(int.Parse(hora_inicio.Text)),
                fechaHoraTermino = fechaTermino_datetime.SelectedDate.Value.AddHours(int.Parse(hora_termino.Text)),
                asistentes = int.Parse(asistente_txt.Text),
                personalAdicional = int.Parse(personal_add_text.Text),
                realizado = false,
                valorTotalContrato = float.Parse(ValorFinalTexto.Text),
                observaciones = Observaciones_txt.Text
            };

            int numComtratoRow = Contrato_conexion.InsertarSQL(con);
            if (numComtratoRow < 1)
            {
                MessageBox.Show("Error en la insercion del contrato, debe esperar un minuto entre inserciones para generar un nuevo numero de contrato", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            if (con.idTipoEvento == 10)
            {
                //coffee
                CoffeeBreak cb = new CoffeeBreak()
                {
                    numero = con.numero,
                    vegetariana = CheckVegetariana.IsChecked.Value
                };

                if (!CoffeeBreak_Conexion.Insert(cb))
                {
                    MessageBox.Show("Error en la insercion del CoffeBreak", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

            }

            if (con.idTipoEvento == 20)
            {
                //cocktail
                Cocktail c = new Cocktail()
                {
                    numero = con.numero,
                    idTipoAmbientacion = crearContratoControl.list_TipoAmbientacion[cbo_tipo_ambientacion.SelectedIndex].idTipoAmbientacion,
                    ambientacion = true,
                    musicaAmbiental = check_musicaAmbiental.IsChecked.Value,
                    musicaCliente = check_musicaCliente.IsChecked.Value
                };

                if (!Cocktail_Conexion.Insert(c))
                {
                    MessageBox.Show("Error en la insercion del cocktail", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }

            if (con.idTipoEvento == 30)
            {
                //cenas
                Cenas c = new Cenas()
                {
                    numero = con.numero,
                    idTipoAmbientacion = crearContratoControl.list_TipoAmbientacion[cbo_tipo_ambientacion_cena.SelectedIndex].idTipoAmbientacion,
                    musicaAmbiental = check_musicaAmbiental_cena.IsChecked.Value,
                    localOnBreack = cbo_tipo_local.SelectedIndex == 0,
                    otroLocalOnBreak = cbo_tipo_local.SelectedIndex == 1,
                    valorArriendo = float.Parse(Valor_txt_local_cena.Text)
                };

                if (!Cenas_Conexion.Insert(c))
                {
                    MessageBox.Show("Error en la insercion de cenas", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

            }

            MessageBox.Show("Se creo correctamente el contrato", "Mensaje Emergente", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            

            crearContratoControl.cache.Delete();
        }

        public void CallBack(string data = null)
        {
            if (data != null) txt_rut_cliente.Text = data;
            this.Visibility = Visibility.Visible;
        }

        void TextBoxInt(TextBox tb, int min = 0, int max = int.MaxValue)
        {
            if (tb.Text == "") return;
            if (int.TryParse(tb.Text, out int h))
            {
                if (h < min) tb.Text = min.ToString();
                if (h > max) tb.Text = max.ToString();
                if (h > min && tb.Text[0] == '0')
                {
                    tb.Text = tb.Text.Substring(1);
                }
            }
            else
            {
                tb.Text = "0";
            }
            tb.SelectionStart = tb.Text.Length;
        }

        public void Calcular()
        {
            if (!calcularActive) return;
            calculadorValor = new Controlador.CalculadorValor();

            int index_modalidad = cbo_modalidad.SelectedIndex;
            if (index_modalidad < 0) return;
            calculadorValor.AddModulo(new CalculadorValor.ValorBase((float)crearContratoControl.list_modalidadServicio[index_modalidad].valorBase));

            int idTipoEvento = crearContratoControl.list_modalidadServicio[index_modalidad].idTipoEvento;
            if (idTipoEvento == 10)
            {
                //coffe break
                calculadorValor.AddModulo(new CalculadorValor.CoffeeBreak_Asistentes(int.Parse(asistente_txt.Text)));
                calculadorValor.AddModulo(new CalculadorValor.CoffeeBreak_PersonalAdicional(int.Parse(personal_add_text.Text)));
            }

            if (idTipoEvento == 20)
            {
                //cocktail
                calculadorValor.AddModulo(new CalculadorValor.Cocktail_Asistentes(int.Parse(asistente_txt.Text)));
                calculadorValor.AddModulo(new CalculadorValor.Cocktail_PersonalAdicional(int.Parse(personal_add_text.Text)));
                if (cbo_tipo_ambientacion.SelectedIndex >= 0)
                {
                    if (cbo_tipo_ambientacion.SelectedIndex == 0)
                        calculadorValor.AddModulo(new CalculadorValor.Cocktail_Ambientacion(CalculadorValor.Cocktail_Ambientacion.Tipo.Basica));
                    if (cbo_tipo_ambientacion.SelectedIndex == 1)
                        calculadorValor.AddModulo(new CalculadorValor.Cocktail_Ambientacion(CalculadorValor.Cocktail_Ambientacion.Tipo.Personalizada));
                }
                calculadorValor.AddModulo(new CalculadorValor.Cocktail_MusicaAmbiental(check_musicaAmbiental.IsChecked.Value));
            }

            if (idTipoEvento == 30)
            {
                //cenas
                calculadorValor.AddModulo(new CalculadorValor.Cenas_Asistentes(int.Parse(asistente_txt.Text)));
                calculadorValor.AddModulo(new CalculadorValor.Cenas_PersonalAdicional(int.Parse(personal_add_text.Text)));
                if (cbo_tipo_ambientacion_cena.SelectedIndex >= 0)
                {
                    if (cbo_tipo_ambientacion_cena.SelectedIndex == 0)
                        calculadorValor.AddModulo(new CalculadorValor.Cenas_Ambientacion(CalculadorValor.Cenas_Ambientacion.Tipo.Basica));
                    if (cbo_tipo_ambientacion_cena.SelectedIndex == 1)
                        calculadorValor.AddModulo(new CalculadorValor.Cenas_Ambientacion(CalculadorValor.Cenas_Ambientacion.Tipo.Personalizada));
                }
                calculadorValor.AddModulo(new CalculadorValor.Cenas_MusicaAmbiental(check_musicaAmbiental_cena.IsChecked.Value));
                if (cbo_tipo_local.SelectedIndex == 0)
                    calculadorValor.AddModulo(new CalculadorValor.Cenas_Local(CalculadorValor.Cenas_Local.Tipo.LocalOnBreak));
                else if (cbo_tipo_local.SelectedIndex > 0)
                    calculadorValor.AddModulo(new CalculadorValor.Cenas_Local(CalculadorValor.Cenas_Local.Tipo.Otro, int.Parse(Valor_txt_local_cena.Text)));
            }


            ValorFinalTexto.Text = Math.Round(calculadorValor.Calcular(), 2).ToString();
        }

        private void Hora_inicio_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxInt(hora_inicio, 0, 23);
        }


        private void Hora_termino_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxInt(hora_termino, 0, 23);
        }

        private void Asistente_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxInt(asistente_txt, 1);
            Calcular();
        }

        private void Personal_add_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxInt(personal_add_text, 0);
            Calcular();
        }

        
        private void ValorLocal_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxInt(Valor_txt_local_cena, 0);
            Calcular();
        }
        
        
        private void cbo_tipo_musica_changed(object sender, SelectionChangedEventArgs e)
        {
            Calcular();
        }
        
        private void cbo_tipo_ambientacion_changed(object sender, SelectionChangedEventArgs e)
        {
            Calcular();
        }

        private void Check_musicaAmbiental_Checked(object sender, RoutedEventArgs e)
        {
            Calcular();
        }

        private void Check_musicaAmbiental_Checked_1(object sender, RoutedEventArgs e)
        {
            
            Calcular();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Calcular();
        }

        private void Cbo_tipo_local_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calcular();
        }

        private void Check_musicaAmbiental_Checked_2(object sender, RoutedEventArgs e)
        {
            Calcular();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Listado_cliente lc = new Listado_cliente(this);
            this.Visibility = Visibility.Collapsed;
            lc.Show();
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

        public void GuardarRespaldo()
        {
            crearContratoControl.cache.Clear();
            crearContratoControl.SaveTxt("rut", txt_rut_cliente.Text);

            if (fechaInicio_datetime.SelectedDate != null)
                crearContratoControl.SaveTxt("fechaInicio", fechaInicio_datetime.SelectedDate.Value.ToString());
            if (fechaTermino_datetime.SelectedDate != null)
                crearContratoControl.SaveTxt("fechaTermino", fechaTermino_datetime.SelectedDate.Value.ToString());

            crearContratoControl.SaveTxt("horaInicio", hora_inicio.Text);
            crearContratoControl.SaveTxt("horaTermino", hora_termino.Text);

            crearContratoControl.SaveTxt("asistentes", asistente_txt.Text);
            crearContratoControl.SaveTxt("personalAdd", personal_add_text.Text);

            crearContratoControl.SaveTxt("observaciones", Observaciones_txt.Text);

            crearContratoControl.SaveComboBox("modalidad", cbo_modalidad.SelectedIndex);

            crearContratoControl.SaveCheckBox("vegetariana", CheckVegetariana.IsChecked.Value);

            crearContratoControl.SaveComboBox("cena_tipoAmbientacion", cbo_tipo_ambientacion_cena.SelectedIndex);
            crearContratoControl.SaveCheckBox("cena_musicaAmbiental", check_musicaAmbiental_cena.IsChecked.Value);
            crearContratoControl.SaveComboBox("cena_tipoLocal", cbo_tipo_local.SelectedIndex);
            crearContratoControl.SaveTxt("valorLocal", Valor_txt_local_cena.Text);

            crearContratoControl.SaveComboBox("cocktail_tipoAmbientacion", cbo_tipo_ambientacion.SelectedIndex);
            crearContratoControl.SaveCheckBox("cocktail_musicaAmbiental", check_musicaAmbiental.IsChecked.Value);
            crearContratoControl.SaveCheckBox("cocktail_musicaCliente", check_musicaCliente.IsChecked.Value);

            crearContratoControl.cache.Save();
        }

        public void CargarRespaldo()
        {

            txt_rut_cliente.Text = crearContratoControl.GetTxt("rut");

            if (crearContratoControl.cache.ExistKey("fechaInicio"))
                fechaInicio_datetime.SelectedDate = System.Convert.ToDateTime(crearContratoControl.GetTxt("fechaInicio"));
            if (crearContratoControl.cache.ExistKey("fechaTermino"))
                fechaTermino_datetime.SelectedDate = System.Convert.ToDateTime(crearContratoControl.GetTxt("fechaTermino"));


            hora_inicio.Text = crearContratoControl.GetTxt("horaInicio");
            hora_termino.Text = crearContratoControl.GetTxt("horaTermino");

            asistente_txt.Text = crearContratoControl.GetTxt("asistentes");
            personal_add_text.Text = crearContratoControl.GetTxt("personalAdd");

            Observaciones_txt.Text = crearContratoControl.GetTxt("observaciones");

            cbo_modalidad.SelectedIndex = crearContratoControl.GetComboBox("modalidad");

            CheckVegetariana.IsChecked = crearContratoControl.GetCheckBox("vegetariana");

            cbo_tipo_ambientacion_cena.SelectedIndex = crearContratoControl.GetComboBox("cena_tipoAmbientacion");
            check_musicaAmbiental_cena.IsChecked = crearContratoControl.GetCheckBox("cena_musicaAmbiental");
            cbo_tipo_local.SelectedIndex = crearContratoControl.GetComboBox("cena_tipoLocal");
            Valor_txt_local_cena.Text = crearContratoControl.GetTxt("valorLocal");

            cbo_tipo_ambientacion.SelectedIndex = crearContratoControl.GetComboBox("cocktail_tipoAmbientacion");
            check_musicaAmbiental.IsChecked = crearContratoControl.GetCheckBox("cocktail_musicaAmbiental");
            check_musicaCliente.IsChecked = crearContratoControl.GetCheckBox("cocktail_musicaCliente");
        }

        private void Respaldar_Click(object sender, RoutedEventArgs e)
        {
            GuardarRespaldo();
            Console.WriteLine("Guardado Manual");
        }

        System.Timers.Timer timer;
        public static Crear_contrato cc;

        private void ActivarAutoguardado()
        {
            cc = this;
            timer = new System.Timers.Timer();
            timer.Interval = 300000;
            timer.Elapsed += GuardarAutomatico;
            //timer.Enabled = true;
        }

        private static void GuardarAutomatico(Object source, System.Timers.ElapsedEventArgs e)
        {
            cc.GuardarRespaldo();
            Console.WriteLine("Guardado Automatico");
        }

        
    }
}
