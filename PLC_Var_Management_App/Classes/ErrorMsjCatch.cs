using PLC_Var_Management_App.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_Var_Management_App.Classes
{
    public class ErrorMsjCatch
    {
        public ErrorMsjCatch() 
        {
            IsEmpty = true;
        }

        public DateTime Fecha;
        public string Mensaje;
        public string Tipo;
        public bool IsEmpty;

        
    }
}
