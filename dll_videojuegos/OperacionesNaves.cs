using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll_videojuegos
{
    public class OperacionesNaves
    {
        #region Atributos
        private byte _Id;
        private string _Nombre;
        private string _Conexion;
        private string _Password;
        private int _Puntuacion;
        private byte _Vidas;
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
        public int Puntuacion
        {
            get { return _Puntuacion; }
            set { _Puntuacion = value; }
        }
        public byte Vidas
        {
            get { return _Vidas; }
            set { _Vidas = value; }
        }
        #endregion

        #region metodos
        public OperacionesNaves()
        {
            _Id = 0;
            _Nombre = "";
            _Password = "";
            _Puntuacion = 0;
            _Vidas = 0;
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
                SqlCommand cmd = new SqlCommand("primerBase.usp_AgregarLogin", cn);
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
        public bool AgregarGanador()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_AgregarDatosGanador", cn);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("@puntuacion", SqlDbType.SmallInt).Value = _Puntuacion;
                cmd.Parameters.Add("@vidasRestantes", SqlDbType.SmallInt).Value = _Vidas;
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

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_AgregarDatosPerdedor", cn);
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 30).Value = _Nombre;
                cmd.Parameters.Add("@puntuacion", SqlDbType.Int).Value = _Puntuacion;
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
                SqlCommand cmd = new SqlCommand("primerBase.usp_BuscarLogin", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_BuscarGanador", cn);
                cmd.Parameters.Add("@ide", SqlDbType.TinyInt).Value = _Id;
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
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_BuscarPerdedor", cn);
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
                SqlCommand cmd = new SqlCommand("select * from primerbase.Usuario", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarGanador()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from primerbase.ganador", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }
        public DataTable ConsultarPerdedor()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from primerbase.perdedor", cn);
                cn.Open();
                dt.Load(cmd.ExecuteReader());
                cn.Close();
            }
            return dt;
        }

        public bool EliminarGanador()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_EliminarGanador", cn);
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
        public bool EliminarPerdedor()
        {
            bool resultado = false;

            using (SqlConnection cn = new SqlConnection(_Conexion))
            {
                SqlCommand cmd = new SqlCommand("primerBase.usp_EliminarPerdedor", cn);
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
                SqlCommand cmd = new SqlCommand("primerBase.usp_EliminarLogin", cn);
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
                SqlCommand cmd = new SqlCommand("SELECT Usuario,Contrasena FROM primerBase.Usuario WHERE Usuario='" + _Nombre + "' AND Contrasena='" + _Password + "'", cn);
                SqlDataReader dr =cmd.ExecuteReader();
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
