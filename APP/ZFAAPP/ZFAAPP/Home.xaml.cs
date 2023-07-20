using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZFAAPP.Models;

namespace ZFAAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /*
     * HOME 
     */
    public partial class Home : ContentPage
    {
        public User user { get; set; }
        
        public IList<Zeitbuchungen> Zeitbuchungens { get; private set; }

        public Home(User benutzer)
        {
            
            this.user = benutzer;
            if(user== null || (((bool)user.LogState) == false))
            {
                Navigation.PushAsync(new MainPage(user));
            }
            else
            {
                InitializeComponent();
                LastBuchungen();
            }
            Welcome.Text = user.N_name; 
        }
        /*
         * @OnAction BtnUserMenu()
         * ---> Redirect to Show QR-Code
         */
        private void BtnUserMenu(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowMyQRCode(user));
        }
        /*
         * @OnAction Logout()
         * ---> Destroy User Object and Redirect to LoginPage
         */
        private void Logout(object sender, EventArgs e)
        {
            user = null;
            User benutzer = new User();
            benutzer.LogState = false;
            Navigation.PopAsync();
            App.Current.MainPage = new NavigationPage(new MainPage(benutzer));
        }
        /*
         * @Param LastBuchungen()
         * @Return List<Zeitbuchungen>
         */
        private async void LastBuchungen()
        {

            Zeitbuchungens = new List<Zeitbuchungen>();
            Fetch fetch = new Fetch();


            LastTimeEntries lastTimeEntries = new LastTimeEntries
            {
                MID=user.Id
            };
            var b = await fetch.GetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/timetouches.php", lastTimeEntries);
            Console.WriteLine(b.ToString());
            
                if (b.ContainsKey("data"))
                {
                    //User can be logged in
                    var data = b["data"].ToString();
                    var uObj = JArray.Parse(data);
                    
                    for(int i = 0; i < uObj.Count; i++)
                    {
                        var sbs = "";
                        var hex = "";
                        if (uObj[i]["vorgang"].ToString() == "2")
                        {
                            sbs = "Kommen";
                            hex = "#00ff08";
                        }
                        else
                        {
                            sbs = "Gehen";
                            hex = "#f80a0a";
                        }
                        Zeitbuchungens.Add(new Zeitbuchungen
                        {
                            Datum = uObj[i]["datum"].ToString()+" - "+ uObj[i]["uhrzeit"].ToString(),
                            Vorgang = uObj[i]["vorgang"].ToString(),
                            Fall = sbs.ToString(),
                            HexCode = hex
                        });
                        
                        
                    }
                    
                }                

            
            
            BindingContext = this;
        }
        /*
         * @OnAction MyGoing()
         * ---> Save Going Timestamp
         */
        private async void MyGoing(object sender, EventArgs e)
        {
            Fetch fetch = new Fetch();
            DateTimeFormat dt = new DateTimeFormat();
            TimetouchEntry timetouchEntry = await dt.prepareTime(user.TimeTouchNr.ToString(), user.Id.ToString(), "3");
            var fetc = await fetch.SetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/createtimetouch.php", timetouchEntry);
            if (fetc.ContainsKey("message"))
            {
                var data = fetc["message"].ToString();

                await DisplayAlert("Information", data.ToString(), "OK");
                App.Current.MainPage = new NavigationPage(new Home(user));


            }
            else
            {
                await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte versuchen Sie es erneut.", "OK");
            }


        }
        /*
         * @OnAction MyGoing()
         * ---> Save Coming Timestamp
         */
        private async void MyComing(object sender, EventArgs e)
        {
            Fetch fetch = new Fetch();
            DateTimeFormat dt = new DateTimeFormat();
            TimetouchEntry timetouchEntry = await dt.prepareTime(user.TimeTouchNr.ToString(), user.Id.ToString(), "2");
            var fetc = await fetch.SetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/createtimetouch.php", timetouchEntry);
            if (fetc.ContainsKey("message"))
                {
                    var data = fetc["message"].ToString();
                    
                    await DisplayAlert("Information", data.ToString(), "OK");
                    App.Current.MainPage=new NavigationPage(new Home(user));


                }
                else
                {
                    await DisplayAlert("Fehler", "Ein Fehler ist aufgetreten. Bitte versuchen Sie es erneut.", "OK");
                }

            
        }
    }
}