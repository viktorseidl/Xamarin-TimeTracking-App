using System;
using System.Collections.Generic;
using System.Text;

namespace ZFAAPP.Models
{
    /*
     * @Class Zeitbuchungen
     * <Summary>
     * QUERY MODEL  
     * </Summary>
     */
    public class Zeitbuchungen
    {
        public string Datum { get; set; }
        public string Vorgang { get; set; }
        public string Fall { get; set; }
        public string HexCode { get; set; }
    }
}
