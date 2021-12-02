using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll_videojuegos
{
    public class OperacionesTanques
    {
        #region Atributos
        private byte _Id;
        private string _Nombre;
        private string _Conexion;
        private string _Password;
        private int _VictoriasJ1;
        private int _VictoriasJ2;
        private string _Jugador1;
        private string _Jugador2;
        private int _DisparosRealizadosJ1;
        private int _DisparosRealizadosJ2;
        private int _DisparosDadosJ1;
        private int _DisparosDadosJ2;
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
        public int VictoriasJ1
        {
            get { return _VictoriasJ1; }
            set { _VictoriasJ1 = value; }
        }
        public int VictoriasJ2
        {
            get { return _VictoriasJ2; }
            set { _VictoriasJ2 = value; }
        }
        public string Jugador1
        {
            get { return _Jugador1; }
            set { _Jugador1 = value; }
        }
        public string Jugador2
        {
            get { return _Jugador2; }
            set { _Jugador2 = value; }
        }
        public int DisparosRealizadosJ1
        {
            get { return _DisparosRealizadosJ1; }
            set { _DisparosRealizadosJ1 = value; }
        }
        public int DisparosRealizadosJ2
        {
            get { return _DisparosRealizadosJ2; }
            set { _DisparosRealizadosJ2 = value; }
        }
        public int DisparosDadosJ1
        {
            get { return _DisparosDadosJ1; }
            set { _DisparosDadosJ1 = value; }
        }
        public int DisparosDadosJ2
        {
            get { return _DisparosDadosJ2; }
            set { _DisparosDadosJ2 = value; }
        }
        #endregion

        #region metodos
        public OperacionesTanques()
        {
            _Id = 0;
            _Nombre = "";
            _Password = "";
            _DisparosDadosJ1 = 0;
            _DisparosDadosJ2 = 0;
            _DisparosRealizadosJ1 = 0;
            _DisparosRealizadosJ2 = 0;
            _Jugador1 = "";
            _Jugador2 = "";
            _VictoriasJ1 = 0;
            _VictoriasJ2 = 0;
            _Conexion = Conexion.ConexionWinSQL();
        }
        public bool Actualizar()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_ActualizarEspecies", cn);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_AgregarLogin", cn);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("@contrasena", SqlDbType.VarChar, 15).Value = _Password;
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
        public bool AgregarPartidas()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_AgregarPartidas", cn);
                cmd.Parameters.Add("@jugador1", SqlDbType.VarChar, 30).Value = _Jugador1;
                cmd.Parameters.Add("@jugador2", SqlDbType.VarChar, 30).Value = _Jugador2;
                cmd.Parameters.Add("@victoriasJ1", SqlDbType.SmallInt).Value = _VictoriasJ1;
                cmd.Parameters.Add("@victoriasJ2", SqlDbType.SmallInt).Value = _VictoriasJ2;
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
        public bool AgregarDisparos()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.uspAgregarDisparos", cn);
                cmd.Parameters.Add("@disparosDadosJ1", SqlDbType.SmallInt).Value = _DisparosDadosJ1;
                cmd.Parameters.Add("@disparosDadosJ2", SqlDbType.SmallInt).Value = _DisparosDadosJ2;
                cmd.Parameters.Add("@disparosRealizadosJ1", SqlDbType.SmallInt).Value = _DisparosRealizadosJ1;
                cmd.Parameters.Add("@disparosRealizadosJ2", SqlDbType.SmallInt).Value = _DisparosRealizadosJ2;
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
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_BuscarLogin", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public System.Data.DataTable BuscarPartida()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_BuscarPartida", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public System.Data.DataTable BuscarDisparos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_BuscarDisparos", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from secondBase.Usuario", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarPartidas()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from secondBase.Partidas", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarDisparos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from secondBase.Disparos", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }

        public bool EliminarPartida()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_EliminarPartidas", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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
        public bool EliminarDisparos()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_EliminarDisparos", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("secondBase.usp_EliminarLogin", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT Usuario,Contrasena FROM secondBase.Usuario WHERE Usuario='" + _Nombre + "' AND Contrasena='" + _Password + "'", cn);
                SqlDataReader dr = cmd.ExecuteReader();
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
