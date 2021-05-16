using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SistemaRestaurante.CONEXION
{
    class ConexionMaestra
    {

        public static string conexion = Convert.ToString(LIBRERIAS.Desencryptacion.checkServer());
        public static SqlConnection connection = new SqlConnection(conexion);

        public static void abrir() 
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static void cerrar() 
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
