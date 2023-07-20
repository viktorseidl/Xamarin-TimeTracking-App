using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZFAAPP.Models;
using ZFAAPP.Services;

namespace ZFAAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class QRScanner : ContentPage
    {
        public User user { get; set; }
        public QRScanner(User benutzer)
        {
            InitializeComponent();
            this.user = benutzer;

         }
        //Result result
        private void Handle_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () => {
                //36d1a8545ee72e5c041408f37f927b3e
                if (String.IsNullOrEmpty(result.Text) == false)
                {
                    var b = new Anmelden();
                    Fetch fetch = new Fetch();
                    var GKey = result.Text;
                    var u = b.LogMeIn(GKey);

                    try
                    {
                        user = await u;
                        await Navigation.PushAsync(new Home(this.user));
                    }
                    catch (NullReferenceException)
                    {
                        await DisplayAlert("Anmeldung fehlerhaft", "Pin oder Passwort ist falsch!", "OK");
                    }

                };
                    

                
            });
        }

        
    }
}