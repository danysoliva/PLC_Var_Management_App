using System.Collections.Generic;

namespace PLC_Var_Management_App.Classes
{
    /// <summary>
    /// Catálogo según Mapeo_Variables_PLC_DB831.xlsx (hoja Direcciones PLC).
    /// Cada BIN: DBW+0 Trigger, DBW+2 Lote, DBD+4 Intake_Real (8 bytes).
    /// </summary>
    public static class BinIntakeCatalog
    {
        public const int DbNumber = 831;

        /// <summary>Orden exacto del Excel (54 bines).</summary>
        private static readonly string[] BinCodes = new string[]
        {
            "FD1","FD2","FD3","FD4","FD5","FD6","FD7","FD8","FD9","FD10",
            "FD11","FD12","FD13","FD14","FD15","FD16","FD17",
            "SD1","SD2","SD3",
            "WL1_M2","WL2_M2","WL3_M2","WL4_M2","WL5_M2","WL6_M2",
            "WL1_M1","WL2_M1","WL3_M1","WL4_M1","WL5_M1","WL6_M1",
            "FYLAX",
            "WL7_M2","WL8_M2","WL7_M1","WL8_M1",
            "ACEITE_1","ACEITE_2","ACEITE_3","ACEITE_4",
            "Colina","Agua_1","Agua_2","Fylax_Interno",
            "C1_Tanque_2","C1_Tanque_3","C1_Tanque_5","C1_Hacienda",
            "C2_Tanque_2","C2_Tanque_3","C2_Tanque_5","C2_Hacienda",
            "Lecitina"
        };

        private static List<BinIntakePoint> _points;

        public static IList<BinIntakePoint> All
        {
            get
            {
                if (_points == null)
                    _points = Build();
                return _points;
            }
        }

        private static List<BinIntakePoint> Build()
        {
            var list = new List<BinIntakePoint>(BinCodes.Length);
            for (int i = 0; i < BinCodes.Length; i++)
            {
                int offset = i * 8;
                list.Add(new BinIntakePoint(BinCodes[i], offset, DbNumber));
            }
            return list;
        }
    }
}
