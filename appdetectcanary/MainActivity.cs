using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;

//using System.IO;
using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Android.Graphics.Drawables;

namespace AppDetectCanary
{

    [Activity(Label = "Detector de Canarios", MainLauncher = true, Icon = "@drawable/ico")]
    public class MainActivity : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

        

            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Click += TakeAPicture;

        }
       
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            //llamar a vistamostrar
            Intent intent = new Intent(this, typeof(AMostrar));


            App.bandera = true;
            

            StartActivity(intent);

        }

    }
}