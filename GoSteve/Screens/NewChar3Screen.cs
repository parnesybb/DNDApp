using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Runtime.Serialization.Formatters.Binary;
using GoSteve.Screens;
using Server;

namespace GoSteve
{
    [Activity(Label = "GoSteve! Dungeons and Dragons")]
    public class NewChar3Screen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            int numPoints = 27;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar3);

            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
        }
    }
}