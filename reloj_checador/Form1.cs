using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using Mysqlx.Crud;

namespace reloj_checador
{
    public partial class Checador : Form
    {
        // Conexión a la base de datos
        readonly string servidor = "localhost"; //Nombre o ip del servidor de MySQL
        readonly string bd = "reloj_checador"; //Nombre de la base de datos
        readonly string usuario = "root"; //Usuario de acceso a MySQL
        readonly string password = ""; //Contraseña de usuario de acceso a MySQL

        private MySqlConnection connection;

        List<string> entradas_1 = new List<string>();
        List<string> salidas_1 = new List<string>();
        //DateTime localDate = DateTime.Now;
        DateTime utcDate = DateTime.UtcNow;
        //string time = DateTime.Now.ToLongTimeString();

        
        public Checador()
        {
            InitializeComponent();
            ActualizarFecha();
            ConfigurarBaseDatos();

            // Declarar Colores
            Color colorPrincipal = ColorTranslator.FromHtml("#284D99"); // Azul Marino
            Color colorSecundario = ColorTranslator.FromHtml("#4267B2"); // Azul Suave
            Color colorTexto = ColorTranslator.FromHtml("#2C3E50"); // Gris azulado
            Color colorFondo = ColorTranslator.FromHtml("#F5F6F7"); // Gris muy claro
            Color colorAcento = ColorTranslator.FromHtml("#3B82F6"); // Color base del botón
            Color colorHover = ColorTranslator.FromHtml("#2563EB"); // Color hover

            timer_lab.ForeColor = colorPrincipal; // Color del reloj
            fechaCompleta.ForeColor = colorTexto; // Color de la fecha

            // Campos de entrada
            Id.BackColor = colorFondo;
            Id.ForeColor = colorTexto;

            // Lista de registros
            entradas.BackColor = colorFondo;
            entradas.ForeColor = colorTexto;

            // Botones
            checar.FlatStyle = FlatStyle.Flat;
            checar.BackColor = Color.LightGreen;  // Color base del botón
            checar.ForeColor = Color.White;
            checar.FlatAppearance.BorderSize = 0;

            // Efecto hover
            checar.MouseEnter += (s, e) => checar.BackColor = Color.Green;  // Color hover
            checar.MouseLeave += (s, e) => checar.BackColor = Color.LightGreen; // Color base del botón

            // Configurar el comportamiento del Label Id
            Id.Text = "Ingrese ID";
            Id.ForeColor = Color.Gray; // Color inicial del texto placeholder

            // Eventos para el Label Id
            Id.Click += Id_Click;
            Id.Leave += Id_Leave;

            // Agregar Timer para transición
            Timer transitionTimer = new Timer();
            transitionTimer.Interval = 50;
        }

        private void Id_Click(object sender, EventArgs e)
        {
            if (Id.Text == "Ingrese ID")
            {
                Id.Text = "";
                Id.ForeColor = ColorTranslator.FromHtml("#2C3E50"); // Color normal del texto
            }
        }

        private void Id_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Id.Text))
            {
                Id.Text = "Ingrese ID";
                Id.ForeColor = Color.Gray;
            }
        }
        private void MostrarNotificacion(string mensaje, bool esExito)
        {
            notificacion.Text = mensaje;
            notificacion.ForeColor = esExito ? Color.Green : Color.Red;
        }
        
        private async void checar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Id.Text) || Id.Text == "Ingrese ID")
            {
                MostrarNotificacion("Ingrese su ID primero", false);
                return;
            }

            await VerificarYRegistrar(Id.Text);
        }

        private async Task VerificarYRegistrar(string userId)
        {
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            try
            {
                using (var conn = new MySqlConnection(cadenaConexion))
                {
                    await conn.OpenAsync();

                    // Primero verificamos si existe una entrada sin salida
                    string checkQuery = @"SELECT e.user_id, r.user_id, e.nombre, e.apellidos, r.hora_entrada, r.hora_salida 
                                        FROM empleados e INNER JOIN registros r 
                                        ON r.user_id = e.user_id 
                                        WHERE r.user_id = @userId LIMIT 1;";

                    using (var cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@userId", userId);
                        int count = Convert.ToInt32(await cmdCheck.ExecuteScalarAsync());
                        var result = await cmdCheck.ExecuteScalarAsync();

                        if (result == null)
                        {
                            MostrarNotificacion("No hay usuario registrada para este ID", false);
                            return;
                        }

                        // Solo si no hay entrada sin salida, procedemos a registrar la nueva entrada
                        string insertQuery = @"INSERT INTO registros (user_id, hora_entrada) 
                                     VALUES (@userId, DATE_FORMAT(DATE_ADD(@horaEntrada, INTERVAL -6 HOUR), '%Y-%m-%d %H:%i:%s'))";

                        using (var cmdInsert = new MySqlCommand(insertQuery, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@userId", userId);
                            cmdInsert.Parameters.AddWithValue("@horaEntrada", DateTime.Now);

                            await cmdInsert.ExecuteNonQueryAsync();

                            // Actualizamos la lista de entradas
                            string dato = "ID: " + userId + " - Entrada: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            entradas_1.Add(dato);
                            entradas.DataSource = null;
                            entradas.DataSource = entradas_1;

                            Id.Text = "";
                            MostrarNotificacion("Entrada registrada correctamente", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al registrar entrada: " + ex.Message, false);
            }
        }

        private async Task RegistrarEntrada(string userId)
        {
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            try
            {
                using (var conn = new MySqlConnection(cadenaConexion))
                {
                    await conn.OpenAsync();

                    // Verificar si ya existe una entrada sin salida para hoy
                    string checkQuery = @"SELECT COUNT(*) 
                                FROM registros 
                                WHERE user_id = @userId 
                                AND DATE(hora_entrada) = CURDATE() 
                                AND hora_salida IS NULL";

                    using (var cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@userId", userId);
                        int count = Convert.ToInt32(await cmdCheck.ExecuteScalarAsync());

                        if (count > 0)
                        {
                            MostrarNotificacion("Falta registrar la Salida", false);
                            Id.Text = "";
                            return;
                        }
                    }

                    // Obtener salidas del día
                    string salidasQuery = @"SELECT user_id, hora_salida 
                                  FROM registros 
                                  WHERE DATE(hora_salida) = CURDATE()
                                  AND user_id = @userId";

                    using (var cmdSalidas = new MySqlCommand(salidasQuery, conn))
                    {
                        cmdSalidas.Parameters.AddWithValue("@userId", userId);
                        using (var reader = await cmdSalidas.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                string dato = "ID: " + reader["user_id"].ToString() +
                                            " - Salida: " + Convert.ToDateTime(reader["hora_salida"]).ToString("yyyy-MM-dd HH:mm:ss");
                                if (!salidas_1.Contains(dato))
                                {
                                    salidas_1.Add(dato);
                                }
                            }
                        }
                    }

                    // Si no existe entrada, procedemos a registrarla
                    string insertQuery = @"INSERT INTO registros (user_id, hora_entrada) 
                                 VALUES (@userId, DATE_FORMAT(DATE_ADD(@horaEntrada, INTERVAL -6 HOUR), '%Y-%m-%d %H:%i:%s'))";

                    using (var cmdInsert = new MySqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@userId", userId);
                        cmdInsert.Parameters.AddWithValue("@horaEntrada", DateTime.Now);

                        await cmdInsert.ExecuteNonQueryAsync();

                        string dato = "ID: " + userId + " - Entrada: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if (!entradas_1.Contains(dato))
                        {
                            entradas_1.Add(dato);
                            entradas.DataSource = null;
                            entradas.DataSource = entradas_1;
                        }

                        Id.Text = "";
                        MostrarNotificacion("Entrada registrada correctamente", true);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al registrar entrada: " + ex.Message, false);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer_lab.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void ActualizarFecha()
        {
            // Obtener la fecha actual y formatearla
            DateTime fechaActual = DateTime.Now;
            fechaCompleta.Text = fechaActual.ToString("dd 'de' MMMM yyyy",
                                new System.Globalization.CultureInfo("es-ES"));
        }

        private void ConfigurarBaseDatos()
        {
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            try
            {
                using (connection = new MySqlConnection(cadenaConexion))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand("SELECT 1", connection))
                    {
                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("No se puede conectar al servidor.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1045:
                        MessageBox.Show("Credenciales de acceso incorrectas.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Error al conectar con la base de datos: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
    }
}
