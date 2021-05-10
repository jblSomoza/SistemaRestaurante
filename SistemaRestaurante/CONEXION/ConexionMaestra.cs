using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRestaurante.CONEXION
{
    class ConexionMaestra
    {

        public static string conexion = Convert.ToString(LIBRERIAS.Desencryptacion.checkServer());
    }
}
