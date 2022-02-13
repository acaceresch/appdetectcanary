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
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;

        public static ImageView _imageView;
        public static Boolean bandera = true;
    }

    [Activity(Label = "AMostrar")]
    public class AMostrar : Activity
    {
      

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.
            int height = Resources.DisplayMetrics.HeightPixels;
            //int height = 10;
            //int width = App._imageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
        }
        private void TakeAPicture()//object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new File(App._dir, String.Format("detectcanary_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));

            StartActivityForResult(intent, 0);

        }
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void CreateDirectoryForPictures()
        {
            App._dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "AppDetectCanary");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MostarDatos);



            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();


                App._imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                if (App.bitmap != null)
                {
                   
                    App._imageView.SetImageBitmap(App.bitmap);

                   
                }
                if (App.bandera)
                {
                    
                    TakeAPicture();

                    App.bandera = false;
                }
            }
            Button buttont = FindViewById<Button>(Resource.Id.myButtonp);
            buttont.Click += Buttont_Click;

            

        }
        void Buttontp_Click(object sender, EventArgs e)
        {
            App.bitmap = null;
            App.bandera = true;
            
        }

        void Buttont_Click(object sender, EventArgs e)
        {
            
            App._imageView.BuildDrawingCache();
            Bitmap bmp = App._imageView.GetDrawingCache(false);

            
            System.IO.MemoryStream str = new System.IO.MemoryStream();
            

            App.bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, str);
            
            byte[] byteArray = str.ToArray();
            //////// localhostdetagavos.detaservicio objserviciom = new localhostdetagavos.detaservicio();
            WebReference4.detaservicio objservicio = new WebReference4.detaservicio();
            
            TextView tt = FindViewById<TextView>(Resource.Id.textView1);
            //////// tt.Text = objserviciom.algoritmodecomparacion(byteArray, cultivo);
            tt.Text = objservicio.Comparar("ajfcaceres",byteArray);
           
        }



    }
}