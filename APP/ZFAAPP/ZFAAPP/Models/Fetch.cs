using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Client.Result;

namespace ZFAAPP.Models
{
    /*
     * @Class Fetch
     * <Summary>
     * @Return Task<JObject> || @Return NULL
     * </Summary>
     */
    public class Fetch
    {
        /*
         * @Param QueryIt()
         * @Return Task<JObject> || NULL
         */
        private async Task<JObject> QueryIt(string uril, object klas)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uril);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(klas);
            //Console.WriteLine(json.ToString());
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uril, contentJson);
            Console.WriteLine(response.StatusCode.ToString());
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                string cjson = content.ToString();
                var jObj = JObject.Parse(cjson);
                Console.WriteLine(jObj.ToString());
                return jObj;
                
            }
            else
            {
                if(klas.GetType() == typeof(Login))
                {

                    return JObject.Parse($"{{'message':'Pin oder Passwort falsch'}}");
                }
                else if (klas.GetType() == typeof(TimetouchEntry))
                {
                    return JObject.Parse($"{{'message':'Zeit konnte nicht erfasst werden. Bitte stempeln Sie erneut'}}");
                }
                else if (klas.GetType() == typeof(LastTimeEntries))
                {
                    return JObject.Parse($"{{'message':'Verbindungsfehler! Stempelzeiten können im Moment nicht abgerufen werden. Bitte versuchen Sie es später nochmal.'}}");
                }
                else
                {
                    return JObject.Parse($"{{'message':'Ein Fehler ist aufgetreten!'}}");
                }

            }
        }
        /*
         * @Param sha256()
         * @Return String
         */
        public string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        /*
         * @Param LogMeIn()
         * @Return Task<JObject> || NULL
         */
        public async Task<JObject> LogMeIn(string urilink, Login logObj)
        {           
            var c= await QueryIt(urilink, logObj);
            return c;                   
        }
        /*
         * @Param SetMyStamps()
         * @Return Task<JObject> || NULL
         */
        public async Task<JObject> SetMyStamps(string urilink, TimetouchEntry entriesObj)
        {
            var c = await QueryIt(urilink, entriesObj);
            Console.WriteLine(c.ToString());
            return c;
        }
        /*
         * @Param GetMyStamps()
         * @Return Task<JObject> || NULL
         */
        public async Task<JObject> GetMyStamps(string urilink, LastTimeEntries lastEntriesObj)
        {
            var c = await QueryIt(urilink, lastEntriesObj);
            return c;
        }
    }
}
