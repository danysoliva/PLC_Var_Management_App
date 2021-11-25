using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Var_Management_App.Classes
{
    public class Funciones
    {
        DataOperations dp;
        public Funciones()
        {
            dp = new DataOperations();
        }

        public int GetRM_ID_APMS_from_Bin_id(int pBinID)
        {
            int rm = 0;
            try
            {
                string sql = "EXEC [dbo].[sp_get_material_bin_get_rm] @id_bin ="+ pBinID;
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                rm = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                throw new Exception(ec.Message);
            }
            return rm;
        }//end function GetRM_ID_APMS_from_Bin_id
         



        public string GetRM_ShortName_APMS_from_Bin_id(int pBinID)
        {
            string rm = "";
            try
            {
                string sql = "EXEC [dbo].[sp_get_material_bin_get_rm_name] @id_bin =" + pBinID;
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                rm = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ec)
            {
                throw new Exception(ec.Message);
            }
            return rm;
        }//end function GetRM_ID_APMS_from_Bin_id


        public bool GetInclusionLecitinaEnFormula()
        {
            bool lleva = false;
            try
            {
                string sql = "EXEC [dbo].[sp_get_incluye_lecitina_formula]";
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                lleva = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ec)
            {
                throw new Exception(ec.Message);
            }
            return lleva;
        }//end function GetInclusionLecitinaEnFormula
        

    }
}
