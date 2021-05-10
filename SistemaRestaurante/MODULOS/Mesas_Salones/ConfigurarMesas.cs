using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaRestaurante.MODULOS.Mesas_Salones
{
    public partial class ConfigurarMesas : Form
    {
        public ConfigurarMesas()
        {
            InitializeComponent();
        }

        private void ConfigurarMesas_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
        }

        private void btnAgregarSalones_Click(object sender, EventArgs e)
        {
            
        }
    }
}
