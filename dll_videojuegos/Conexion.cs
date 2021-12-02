using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dll_videojuegos
{
    public class Conexion
    {
        public static string ConexionWinSQL()
        {
            string cadcon = "Data Source=DESKTOP-GJ80GTH;" + "Initial Catalog=juego2;" + "Integrated Security=true";
            return cadcon;
        }
        public static string ConexionMySQL()
        {
            string cadcon = "Server=localhodt;Port=3306;Database=JUEGOSMySQL;Uid=myUsername;Pwd=myPassword";
            return cadcon;
        }
    }
}
