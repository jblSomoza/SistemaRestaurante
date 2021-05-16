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
    public partial class AgregarMesa : Form
    {
        public AgregarMesa()
        {
            InitializeComponent();
        }

        private void AgregarMesa_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            txtNombre.Text = ConfigurarMesas.nombreMesa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                editarMesa();
            }
        }

        private void editarMesa() 
        {
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("proc_Editar_Mesa", CONEXION.ConexionMaestra.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMesa", ConfigurarMesas.idMesa);
                cmd.Parameters.AddWithValue("@mesa", txtNombre.Text.Trim());
                cmd.ExecuteNonQuery();

                CONEXION.ConexionMaestra.cerrar();
                Close();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
