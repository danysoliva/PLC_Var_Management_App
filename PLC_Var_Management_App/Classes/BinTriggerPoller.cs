using System;
using System.Data;
using System.Data.SqlClient;
using S7.Net;

namespace PLC_Var_Management_App.Classes
{
    /// <summary>
    /// Lee triggers cada ciclo; si Trigger==1 guarda Intake_Real + Lote vía SP y resetea el trigger.
    /// </summary>
    public class BinTriggerPoller
    {
        /// <summary>Nombre del SP. Ajusta parámetros según tu procedimiento real.</summary>
        public string StoredProcedureName = "sp_insert_bin_intake_from_trigger";

        private readonly DataOperations _dp = new DataOperations();

        public event Action<string> OnLog;
        public event Action<Exception, string> OnError;

        public int ProcessAll(Plc plc)
        {
            if (plc == null || !plc.IsConnected)
                return 0;

            int saved = 0;

            foreach (BinIntakePoint bin in BinIntakeCatalog.All)
            {
                try
                {
                    bin.Trigger = ReadInt(plc, bin.AddrTrigger);
                    if (bin.Trigger != 1)
                        continue;

                    bin.Lote = ReadInt(plc, bin.AddrLote);
                    bin.IntakeReal = ReadReal(plc, bin.AddrIntakeReal);

                    if (SaveReading(bin))
                    {
                        // Reinicia la variable real para cumplir ciclo de lectura/escritura
                        plc.Write(bin.AddrIntakeReal, 0f);

                        // Reset del trigger para no re-guardar el mismo ciclo
                        plc.Write(bin.AddrTrigger, (ushort)0);
                        saved++;
                        Log(string.Format("Guardado {0}: Lote={1}, Real={2}", bin.CodigoBin, bin.Lote, bin.IntakeReal));
                    }
                }
                catch (Exception ex)
                {
                    if (OnError != null)
                        OnError(ex, "BinTriggerPoller / " + bin.CodigoBin);
                }
            }

            return saved;
        }

        private bool SaveReading(BinIntakePoint bin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_dp.ConnectionStringAPMS))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo_bin", bin.CodigoBin);
                        cmd.Parameters.AddWithValue("@lote", bin.Lote);
                        cmd.Parameters.AddWithValue("@intake_real", bin.IntakeReal);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(ex, "SaveReading / " + bin.CodigoBin);
                return false;
            }
        }

        private static int ReadInt(Plc plc, string address)
        {
            object raw = plc.Read(address);
            if (raw is ushort)
                return (ushort)raw;
            if (raw is short)
                return (short)raw;
            if (raw is uint)
                return (int)(uint)raw;
            return Convert.ToInt32(raw);
        }

        private static double ReadReal(Plc plc, string address)
        {
            object raw = plc.Read(address);
            if (raw is uint)
                return ((uint)raw).ConvertToFloat();
            if (raw is float)
                return (float)raw;
            if (raw is double)
                return (double)raw;
            return Convert.ToDouble(raw);
        }

        private void Log(string message)
        {
            if (OnLog != null)
                OnLog(message);
        }
    }
}
