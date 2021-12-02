using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dll_videojuegos
{
    public class OperacionesCartas
    {
        #region Atributos
        private byte _Id;
        private string _Nombre;
        private string _Conexion;
        private string _Password;
        private int _TorresRestantes;
        private int _TorresEliminadas;
        private int _Cartas;
        #endregion

        #region Propiedades
        public byte Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public int TorresRestantes
        {
            get { return _TorresRestantes; }
            set { _TorresRestantes = value; }
        }
        public int TorresEliminadas
        {
            get { return _TorresEliminadas; }
            set { _TorresEliminadas = value; }
        }
        public int Cartas
        {
            get { return _Cartas; }
            set { _Cartas = value; }
        }
        #endregion

        #region metodos
        public OperacionesCartas()
        {
            _Id = 0;
            _Nombre = "";
            _Password = "";
            _TorresRestantes = 0;
            _Cartas = 0;
            _TorresEliminadas = 0;
            _Conexion = Conexion.ConexionMySQL();
        }
        public bool Actualizar()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_ActualizarEspecies", cn);
                cmd.Parameters.Add("@nombre", MySqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("@ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }

        public bool AgregarLogin()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_AgregarLogin", cn);
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("_contrasena", MySqlDbType.VarChar, 15).Value = _Password;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }
        public bool AgregarGanador()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_AgregarDatosGanador", cn);
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("_torresRestantes", MySqlDbType.Int32).Value = _TorresRestantes;
                cmd.Parameters.Add("_cartas", MySqlDbType.Int32).Value = _Cartas;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }
        public bool AgregarPerdedor()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_AgregarDatosPerdedor", cn);
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("_cartas", MySqlDbType.Int32).Value = _Cartas;
                cmd.Parameters.Add("_torresEliminadas", MySqlDbType.Int32).Value = _TorresEliminadas;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }

        public System.Data.DataTable BuscarLogin()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_BuscarLogin", cn);
                cmd.Parameters.Add("_ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public System.Data.DataTable BuscarGanador()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_BuscarGanador", cn);
                cmd.Parameters.Add("_ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public System.Data.DataTable BuscarPerdedor()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_BuscarPerdedor", cn);
                cmd.Parameters.Add("_ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }

        public DataTable ConsultarUsuario()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("select * from Usuario", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarGanador()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("select * from ganador", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarPerdedor()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("select * from perdedor", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }

        public bool EliminarGanador()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_EliminarGanador", cn);
                cmd.Parameters.Add("@ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }
        public bool EliminarPerdedor()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_EliminarPerdedor", cn);
                cmd.Parameters.Add("@ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }
        public bool EliminarLogin()
        {
            bool resultado = false;

            using (MySqlConnection cn = new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd = new MySqlCommand("usp_EliminarLogin", cn);
                cmd.Parameters.Add("@ide", MySqlDbType.Int32).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                cn.Close();
            }
            return resultado;
        }
        public bool login()
        {
            bool resultado = false;
            using (MySqlConnection cn= new MySqlConnection(_Conexion))
            {
                MySqlCommand cmd= new MySqlCommand("SELECT Usuario,Contrasena FROM Usuario WHERE Usuario='"+_Nombre+"' AND Contrasena='"+_Password+"'",cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                return resultado;
            }
        }
        #endregion
    }
}
