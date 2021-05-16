using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaRestaurante.MODULOS.Mesas_Salones
{
    public partial class Salones : Form
    {

        int idSalon;

        public Salones()
        {
            InitializeComponent();
        }

        private void Salones_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            insertarSalon();
        }

        private void insertarMesasVacias() 
        {
            for (int x = 1; x <= 80 ; x++)
            {
                try
                {
                    CONEXION.ConexionMaestra.abrir();
                    SqlCommand cmd = new SqlCommand("proc_Insertar_Mesas", CONEXION.ConexionMaestra.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mesa", "NULO");
                    cmd.Parameters.AddWithValue("@idSalon", idSalon);
                    cmd.ExecuteNonQuery();
                    CONEXION.ConexionMaestra.cerrar();
                }
                catch (Exception ex)
                {
                    CONEXION.ConexionMaestra.cerrar();
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }

        private void mostrarIdSalonReciente() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("mostrarIdSalonReciente", CONEXION.ConexionMaestra.connection);
                CONEXION.ConexionMaestra.abrir();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@salon", txtNombre.Text.Trim());
                idSalon = Convert.ToInt32(cmd.ExecuteScalar());
                CONEXION.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void insertarSalon() 
        {
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("proc_Insertar_Salones", CONEXION.ConexionMaestra.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@salon", txtNombre.Text.Trim());
                cmd.ExecuteNonQuery();
                CONEXION.ConexionMaestra.cerrar();

                mostrarIdSalonReciente();
                insertarMesasVacias();

                Close();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
