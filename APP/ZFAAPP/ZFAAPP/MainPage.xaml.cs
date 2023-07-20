using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Security.Cryptography;
using ZFAAPP.Models;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using ZFAAPP.Services;
using System.Net.NetworkInformation;

namespace ZFAAPP
{
    /*
     * MAINPAGE 
     */
    public partial class MainPage : ContentPage
    {
        
        public User user { get; set; }
        public MainPage(User benutzer)
        {
            InitializeComponent();
            this.user = benutzer;        
        }
        /*
         * @OnAction BtnQrScanner()
         * ---> Redirect to QR-SCAN
         */
        private void BtnQrScanner(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QRScanner(user));
        }

        /*
         * @OnAction BtnLoginZFA()
         * ---> Redirect to HOME
         */
        private async void BtnLoginZFA(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PPass.Text)==false && String.IsNullOrEmpty(PPin.Text)==false) {
                var b = new Anmelden();
                Fetch fetch = new Fetch();
                var PassWord = fetch.sha256(PPass.Text).Replace("-", string.Empty).ToLower();
                var PinWord = fetch.sha256(PPin.Text).Replace("-", string.Empty).ToLower();
                var GKey = PinWord + PassWord;
                var u = b.LogMeIn(GKey);
                
                try
                {
                    this.user = await u;
                    await Navigation.PushAsync(new Home(this.user));                        
                }
                catch(NullReferenceException)
                {
                    await DisplayAlert("Anmeldung fehlerhaft", "Pin oder Passwort ist falsch!", "OK");
                }       
        
            }

        }
        
    }
}
