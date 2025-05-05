using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC_Var_Management_App.Classes
{
    public class OrdenPostPellet
    {

        public OrdenPostPellet()
        {
            dp = new DataOperations();
        }

        public int id;
        public Int64 id_mix;
        int cant;
        int finish;
        DataOperations dp;
        public ErrorMsjCatch ErrorActual;

        public bool RecuperarUltimaOrden()
        {
            bool encontrado = false;
            int vid = 0;
            string error = "";
            try
            {
                SqlConnection conn = new SqlConnection(dp.ConnectionStringAPMS);
                conn.Open();
                string sql = @"SELECT case when count(*)>0 then 1 else 0 end as existe
                               FROM [APMS].[dbo].[oil_get_out]
                               where finish = 0";
                error = "Recuperar Ultima orden Clase PostPellet";
                SqlCommand cmd = new SqlCommand(sql, conn);
                encontrado = Convert.ToBoolean(cmd.ExecuteScalar());
                if (encontrado)
                {
                    //Existe orden
                    string sql1 = @"SELECT top 1 id
                                    FROM [APMS].[dbo].[oil_get_out]
                                    where finish = 0
                                    order by id asc";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);

                    vid = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    //No existe orden
                    //insert nuevo record
                    string sqlx = @"INSERT INTO [dbo].[oil_get_out]
                                                               ([id_mix]
                                                               ,[cant])
                                                         VALUES
                                                               (" + id_mix +
                                                           ",0); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmdx = new SqlCommand(sqlx, conn);
                    vid = Convert.ToInt32(cmdx.ExecuteScalar());
                }
                conn.Close();
            }
            catch (Exception ec)
            {
                //MessageBox.Show(error + ec.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.ErrorActual.Fecha = DateTime.Now;
                this.ErrorActual.IsEmpty = false;
                this.ErrorActual.Mensaje = ec.Message;
                this.ErrorActual.Tipo = "Operacion SQL Server, Function: poner la orden activa en pellet";
                //WriteErrorInGrid();

            }
            this.id = vid;
            return encontrado;
        }






    }
}
