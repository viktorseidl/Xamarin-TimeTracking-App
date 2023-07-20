using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZFAAPP.Models
{
    /*
     * @Class DateTimeFormat
     * <Summary>
     * @Return Task<TimetouchEntry> || @Return Null
     * </Summary>
     */
    public class DateTimeFormat
    {
        public async Task<TimetouchEntry> prepareTime(string pin, string uid, string vtype)
        {
            var b = DateTime.UtcNow;
            TimeZoneInfo timezone = TimeZoneInfo.Local;
            object value = b.ToLocalTime();  
            String jahr = b.ToLocalTime().ToString("yyyy");
            String jahrkurz = b.ToLocalTime().ToString("yy");
            String monat = b.ToLocalTime().ToString("MM");
            String tag = b.ToLocalTime().ToString("dd");
            String monateinstellig = b.ToLocalTime().ToString("%M");
            String stunden = b.ToLocalTime().ToString("HH");
            String minuten = b.ToLocalTime().ToString("mm");
            String s = b.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            var extDateTime = s + ":00.000";
            var extDate = jahr + '-' + monateinstellig + '-' + tag + ' ' + "00:00:00.000";
            var vorgang = vtype.ToString();
            var seter = "sc";
            var ImportDatum = s + ":00";
            var ctid = pin.ToString();
            var ctidlength = pin.Length;
            var finalTid = "";
            if (ctidlength < 4)
            {
                for (int i = 0; i < (4 - ctidlength); i++)
                {
                    finalTid += "0";
                }
                finalTid += ctid;
            }
            else
            {
                finalTid = ctid;
            }
            var Buchung = "@t01ZB" + jahrkurz + monat + tag + stunden + minuten + "00" + vorgang + finalTid;
            var Uhrzeit = stunden + ":" + minuten;
            var Datum = tag + "." + monat + "." + jahrkurz;
            TimetouchEntry entry = new TimetouchEntry
            {
                MID = uid.ToString(),
                Personalnr = finalTid,
                Monat = monateinstellig,
                Jahr = jahr,
                Datum = Datum,
                Uhrzeit = Uhrzeit,
                Buchung = Buchung,
                ImportDatum = ImportDatum,
                Benutzer = seter,
                Vorgang = vorgang,
                ExtDate = extDate,
                ExtDateTime = extDateTime
            };

            return entry;
        }
    }
}
