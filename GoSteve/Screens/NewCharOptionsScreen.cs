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

namespace GoSteve.Screens
{
    [Activity(Theme = "@style/AppTheme", Label = "GoSteve! Dungeons and Dragons")]
    public class NewCharOptionsScreen : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NewCharOptions);

            base.OnCreate(bundle);

            Button createBtn = FindViewById<Button>(Resource.Id.createBtn);
            Button tutorialBtn = FindViewById<Button>(Resource.Id.tutorialBtn);

            createBtn.Click += (s, arg) =>
            {
                StartActivity(typeof(NewChar1Screen));
            };

            tutorialBtn.Click += (s, arg) =>
            {
                StartActivity(typeof(Tutorial1Screen));
            };
        }
    }
}