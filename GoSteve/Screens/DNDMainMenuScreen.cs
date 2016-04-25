using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GoSteve.Screens;

namespace GoSteve
{
    [Activity(Label = "GoSteve! Dungeons and Dragons", MainLauncher = true, Icon = "@drawable/icon")]
    public class DNDMainMenuScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DNDMain);

            Button dmModeBtn = FindViewById<Button>(Resource.Id.dmModeBtn);
            Button playerModeBtn = FindViewById<Button>(Resource.Id.playerModeBtn);

            dmModeBtn.Click += (s, arg) =>
            {
                StartActivity(typeof(DmScreenBase));
            };

            playerModeBtn.Click += (s, arg) =>
            {
                StartActivity(typeof(CharacterSelectScreen));
            };
        }
    }
}