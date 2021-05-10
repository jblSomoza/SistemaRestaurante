using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

namespace SistemaRestaurante.CONEXION
{
    public partial class ConexionManual : Form
    {
        private LIBRERIAS.AES aes = new LIBRERIAS.AES();
        string dbcnString;
        int idTabla;

        public ConexionManual()
        {
            InitializeComponent();
        }

        public void SavetoXML(Object dbcnString) 
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        public void ReadfromXML() 
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCadenaConexion.Text = (aes.Decrypt(dbcnString, LIBRERIAS.Desencryptacion.appPwdUnique, int.Parse("256")));
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void ConexionManual_Load(object sender, EventArgs e)
        {
            ReadfromXML();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            comprobarConexion();
        }

        private void comprobarConexion() 
        {
            SqlConnection cnn = new SqlConnection();
            try
            {                
                cnn.ConnectionString = txtCadenaConexion.Text;
                SqlCommand command = new SqlCommand("SELECT * FROM Salones", cnn);
                cnn.Open();
                idTabla = Convert.ToInt32(command.ExecuteScalar());
                cnn.Close();
                SavetoXML(aes.Encrypt(txtCadenaConexion.Text, LIBRERIAS.Desencryptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Conexion Realizada correctamente", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sin conexion", "Conexion Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message);
                cnn.Close();
            }
        }
    }
}
