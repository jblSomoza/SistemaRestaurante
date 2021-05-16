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
    public partial class ConfigurarMesas : Form
    {
        int idSalon;
        public static int idMesa;

        string estado;
        public static string nombreMesa;

        public ConfigurarMesas()
        {
            InitializeComponent();
        }

        private void ConfigurarMesas_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            dibujarSalones();
        }

        private void dibujarMesas()
        {
            try
            {
                panelMesas.Controls.Clear();
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("proc_Mostrar_Mesas_Salon", CONEXION.ConexionMaestra.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idSalon", idSalon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Button b = new Button();
                    Panel panel = new Panel();

                    int alto = Convert.ToInt32(reader["y"].ToString());
                    int ancho = Convert.ToInt32(reader["x"].ToString());
                    int tamanioLetra = Convert.ToInt32(reader["tamanioLetra"].ToString());

                    Point tamanio = new Point(ancho, alto);

                    panel.BackgroundImage = Properties.Resources.mesa_vacia;
                    panel.BackgroundImageLayout = ImageLayout.Zoom;
                    panel.Cursor = Cursors.Hand;
                    panel.Tag = reader["idMesa"].ToString();
                    panel.Size = new System.Drawing.Size(tamanio);

                    b.Text = reader["mesa"].ToString();
                    b.Name = reader["idMesa"].ToString();

                    if (b.Text != "NULO")
                    {
                        b.Size = new System.Drawing.Size(tamanio);
                        b.BackColor = Color.FromArgb(5, 179, 90);
                        b.Font = new System.Drawing.Font("Microsoft Sans Serif", tamanioLetra);
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.ForeColor = Color.White;

                        panelMesas.Controls.Add(b);
                    }
                    else
                    {
                        panelMesas.Controls.Add(panel);
                    }

                    b.Click += new EventHandler(miEvento);
                    panel.Click += new EventHandler(miEventoPanel_Click);
                }
                CONEXION.ConexionMaestra.cerrar();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void miEvento(object sender, EventArgs e)
        {
            nombreMesa = ((Button)sender).Text;
            idMesa = Convert.ToInt32(((Button)sender).Name);
            AgregarMesa frm = new AgregarMesa();
            frm.FormClosed += new FormClosedEventHandler(frmAgregarMesa_FormClosed);
            frm.ShowDialog();
        }

        private void miEventoPanel_Click(object sender, EventArgs e)
        {
            idMesa = Convert.ToInt32(((Panel)sender).Tag);
            AgregarMesa frm = new AgregarMesa();
            frm.FormClosed += new FormClosedEventHandler(frmAgregarMesa_FormClosed);
            frm.ShowDialog();
        }

        private void frmAgregarMesa_FormClosed(object sender, FormClosedEventArgs e)
        {
            dibujarMesas();
        }

        private void dibujarSalones()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand("proc_Mostrar_Salon", CONEXION.ConexionMaestra.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", txtSalon.Text.Trim());
                //Se utiliza para hacer recorridos
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string estado;
                    Button b = new Button();
                    Panel panelC1 = new Panel();

                    b.Text = reader["Salon"].ToString();
                    b.Name = reader["idSalon"].ToString();
                    b.Tag = reader["estado"].ToString();
                    b.Dock = DockStyle.Fill;
                    b.BackColor = Color.Transparent;
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
                    b.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 43, 43);
                    b.TextAlign = ContentAlignment.MiddleLeft;

                    panelC1.Size = new Size(290, 58);
                    panelC1.Name = reader["idSalon"].ToString();
                    estado = reader["estado"].ToString();
                    if (estado == "ELIMINADO")
                    {
                        b.Text = reader["Salon"].ToString() + "- Eliminado";
                        b.ForeColor = Color.FromArgb(231, 63, 67);
                    }
                    else
                    {
                        b.ForeColor = Color.White;
                    }

                    panelC1.Controls.Add(b);
                    flowLayoutPanel1.Controls.Add(panelC1);
                    b.Click += new EventHandler(miEventoSalon);
                }

                CONEXION.ConexionMaestra.cerrar();

            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void miEventoSalon(object sender, EventArgs e)
        {
            panelBienvenida.Visible = false;
            panelBienvenida.Dock = DockStyle.None;
            panelMesas.Visible = true;
            panelMesas.Dock = DockStyle.Fill;
            idSalon = Convert.ToInt32(((Button)sender).Name);
            estado = Convert.ToString(((Button)sender).Tag);
            dibujarMesas();

            foreach (Panel panelC2 in flowLayoutPanel1.Controls)
            {
                if (panelC2 is Panel)
                {
                    foreach (Button boton in panelC2.Controls)
                    {
                        if (boton is Button)
                        {
                            boton.BackColor = Color.Transparent;
                            break;
                        }
                    }
                }
            }

            string nombre = Convert.ToString(((Button)sender).Name);
            foreach (Panel panelC1 in flowLayoutPanel1.Controls)
            {
                if (panelC1 is Panel)
                {
                    foreach (Button boton in panelC1.Controls)
                    {
                        if (boton is Button)
                        {
                            if (boton.Name == nombre)
                            {
                                boton.BackColor = Color.OrangeRed;
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void btnAgregarSalones_Click(object sender, EventArgs e)
        {
            Salones frm = new Salones();
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.ShowDialog();
        }

        void frm_FormClosed(Object sender, FormClosedEventArgs e)
        {
            dibujarSalones();
        }

        private void btnAumentoMesa_Click(object sender, EventArgs e)
        {
            aumentarTamanioMesa();
        }

        private void btnReducirMesa_Click(object sender, EventArgs e)
        {
            reducirTamanioMesa();
        }

        private void btnAumentarLetra_Click(object sender, EventArgs e)
        {
            aumentarTamanioLetra();
        }

        private void btnReducirLetra_Click(object sender, EventArgs e)
        {
            reducirTamanioLetra();
        }

        #region Aumentar Y Disminuir Tamaños
        internal void aumentarTamanioMesa()
        {
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("proc_Aumento_Tamanio_Mesa", CONEXION.ConexionMaestra.connection);
                cmd.ExecuteNonQuery();
                CONEXION.ConexionMaestra.cerrar();
                dibujarMesas();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        internal void reducirTamanioMesa()
        {
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("proc_Reducir_Tamanio_Mesa", CONEXION.ConexionMaestra.connection);
                cmd.ExecuteNonQuery();
                CONEXION.ConexionMaestra.cerrar();
                dibujarMesas();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.Message);
            }
        }

        internal void aumentarTamanioLetra() 
        {            
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("proc_Aumentar_Tamanio_Letra", CONEXION.ConexionMaestra.connection);
                cmd.ExecuteNonQuery();
                CONEXION.ConexionMaestra.cerrar();
                dibujarMesas();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.StackTrace);
            }
        }

        internal void reducirTamanioLetra()
        {
            try
            {
                CONEXION.ConexionMaestra.abrir();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("proc_Reducir_Tamanio_Letra", CONEXION.ConexionMaestra.connection);
                cmd.ExecuteNonQuery();
                CONEXION.ConexionMaestra.cerrar();
                dibujarMesas();
            }
            catch (Exception ex)
            {
                CONEXION.ConexionMaestra.cerrar();
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
