namespace PLC_Var_Management_App.Classes
{
    /// <summary>
    /// Mapeo Trigger / Lote / Intake_Real de un BIN hacia direcciones S7.
    /// </summary>
    public class BinIntakePoint
    {
        public string CodigoBin { get; set; }

        public int Trigger { get; set; }
        public string AddrTrigger { get; set; }

        public int Lote { get; set; }
        public string AddrLote { get; set; }

        public double IntakeReal { get; set; }
        public string AddrIntakeReal { get; set; }

        public BinIntakePoint(string codigoBin, int baseOffset, int dbNumber)
        {
            CodigoBin = codigoBin;
            AddrTrigger = string.Format("DB{0}.DBW{1}", dbNumber, baseOffset);
            AddrLote = string.Format("DB{0}.DBW{1}", dbNumber, baseOffset + 2);
            AddrIntakeReal = string.Format("DB{0}.DBD{1}", dbNumber, baseOffset + 4);
        }
    }
}
