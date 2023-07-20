using System;
using System.Collections.Generic;
using System.Text;

namespace ZFAAPP.Models
{
    /*
     * @Class User
     * <Summary>
     * QUERY MODEL AND SESSION
     * </Summary>
     */
    public class User
    {
        public string Id { get; set; }
        public string V_name { get; set; }
        public string N_name { get; set; }
        public int TimeTouchNr { get; set; }
        private string PIN { get; set; }
        private string TID { get; set; }
        public string Buchungen { get; set; }
        public bool LogState = false;

        public void SetKeysOnUser(string Pin, string Tid)
        {
            PIN = Pin.Trim();
            TID = Tid.Trim();
        }
        public string GetHashPass()
        {
            return PIN;
        }
        public string GetHashTid()
        {
            return TID;
        }
    }
}
