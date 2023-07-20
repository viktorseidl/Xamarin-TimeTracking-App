using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZFAAPP.Models;

namespace ZFAAPP.Services
{   /*
     * @Class Anmelden
     * <Summary>
     * <IF> LoggedIn == true
     * @Return Task<User>
     * <ELSE>
     * @Return Null
     * <END IF>
     * </Summary>
     */
    public class Anmelden
    { 
        /*
         * @Param LogMeIn(String)
         * @Return Task<User> || NULL
         */
        public async Task<User> LogMeIn(string gkey)
        {
            Fetch fetch = new Fetch();
            Login log = new Login
            {
                MID = gkey.ToString()
            };

            var b = await fetch.LogMeIn("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/login.php", log);
            if (b.ContainsKey("data"))
            {
                var data = b["data"].ToString();
                var uObj = JArray.Parse(data);
                string bid = (string)uObj[0]["id"];
                var bc = new User();
                bc.Id = (string)fetch.sha256(bid);
                bc.V_name = (string)uObj[0]["name2"];
                bc.N_name = (string)uObj[0]["name1"];
                bc.TimeTouchNr = (int)uObj[0]["timetouchnr"];
                bc.LogState = true;
                bc.SetKeysOnUser((string)uObj[0]["pin"], fetch.sha256((string)uObj[0]["timetouchnr"]));
                
                return bc;
            }
            else
            {
                return null;
            }
        }
    }
}
