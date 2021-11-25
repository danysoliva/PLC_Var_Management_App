using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Var_Management_App.Classes
{
    public class LecturaEnVivo
    {
        public LecturaEnVivo() { }

        public int id;
        public string descripcion;
        public int valor_actual_int;
        public decimal valor_actual_numeric;
        public string db;
        public bool recuperado;

        /// <summary>
        /// 1 = Lectura de id bin activo para alimentacion manual
        /// </summary>
        /// <param name="pIdLectura"></param>
        /// <returns></returns>
        public  bool    RecuperarRegistro(int pIdLectura)
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringAPMS);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_detalle_lectura_en_vivo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", pIdLectura);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                    descripcion = dr.GetString(1);
                    valor_actual_int = dr.GetInt32(2);
                    valor_actual_numeric = dr.GetDecimal(3);
                    db = dr.GetString(4);
                    recuperado = true;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ec)
            {
                //CajaDialogo.Error(ec.Message);
            }
            return recuperado;
        }
        //
        public bool UpdateValue(int pIdLectura, decimal pvalor, int pvalori)
        {
            bool val = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringAPMS);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_update_detalle_lectura_en_vivo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", pIdLectura);
                cmd.Parameters.AddWithValue("@valor", pvalori);//valor entero
                cmd.ExecuteNonQuery();
                con.Close();
                val = true;
            }
            catch (Exception ec)
            {
                //CajaDialogo.Error(ec.Message);
            }
            return val;
        }





    }
}
