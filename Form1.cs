using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Importante para la conexión a MySQL

namespace AdministradorDocente
{
    public partial class Form1 : Form
    {
        // Cadena de conexión a MySQL (ajusta Pwd= tu_contraseña si root tiene clave)
        private string connectionString = "Server=localhost;Port=3306;Database=ControlDocenteDB;Uid=root;Pwd=admin;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtDepartamento_TextChanged(object sender, EventArgs e)
        {

        }

        // 1. Evento para Guardar un nuevo Docente (INSERT)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDepartamento.Text))
            {
                MessageBox.Show("Por favor ingresa Nombre y Departamento.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO CatPersonalDocente (nombre, departamento) VALUES (@nombre, @departamento)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@departamento", txtDepartamento.Text.Trim().ToUpper());
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Docente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar cajas de texto
                    txtNombre.Clear();
                    txtDepartamento.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 2. Evento para Mostrar la Carga Académica con INNER JOIN (SELECT de las 3 tablas)
        private void btnMostrarAsignaciones_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                                        a.id AS Folio,
                                        d.nombre AS Docente,
                                        d.departamento AS Departamento,
                                        m.nombre_materia AS Materia,
                                        m.creditos AS Creditos,
                                        a.aula AS Aula
                                    FROM TblAsignacionClases a
                                    INNER JOIN CatPersonalDocente d ON a.id_docente = d.id
                                    INNER JOIN CatAsignaturas m ON a.id_asignatura = m.id;";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Reemplaza dgRegistros por el nombre real de tu DataGridView si es diferente
                    dbRegistros.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar la consulta relacional: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 3. Evento para Probar la Conexión a la BD
        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("¡Conexión exitosa a MySQL (ControlDocenteDB)! Base de datos relacional lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a MySQL: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}