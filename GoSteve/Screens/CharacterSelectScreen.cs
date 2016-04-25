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
    public class CharacterSelectScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CharacterSelect);

            // Set fields.
            Button newCharButton = FindViewById<Button>(Resource.Id.newCharButton);
            Button exCharButton = FindViewById<Button>(Resource.Id.exCharButton);

            newCharButton.Click += (s, arg) =>
            {
                StartActivity(typeof(NewChar1Screen));
            };

            exCharButton.Click += (s, arg) =>
            {
                StartActivity(typeof(GSClient));
            };
        }
    }
}

