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
    public class NewChar2Screen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            int numPoints = 27;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar2);

            TextView instructionAb1 = FindViewById<TextView>(Resource.Id.instructionAb1);
            TextView instructionAb2 = FindViewById<TextView>(Resource.Id.instructionAb2);
            TextView blank2 = FindViewById<TextView>(Resource.Id.blank2);
            TextView blank3 = FindViewById<TextView>(Resource.Id.blank3);
            TextView blank4 = FindViewById<TextView>(Resource.Id.blank4);
            TextView blank5 = FindViewById<TextView>(Resource.Id.blank5);
            TextView blank6 = FindViewById<TextView>(Resource.Id.blank6);
            TextView errMsg = FindViewById<TextView>(Resource.Id.errMsg);
            TextView strScore = FindViewById<TextView>(Resource.Id.strScore);
            TextView dexScore = FindViewById<TextView>(Resource.Id.dexScore);
            TextView conScore = FindViewById<TextView>(Resource.Id.conScore);
            TextView intScore = FindViewById<TextView>(Resource.Id.intScore);
            TextView wisScore = FindViewById<TextView>(Resource.Id.wisScore);
            TextView chaScore = FindViewById<TextView>(Resource.Id.chaScore);
            TextView remPoints = FindViewById<TextView>(Resource.Id.remPoints);
            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);

            continueBtn.Click += (s, arg) =>
            {
                if(errMsg.Visibility == ViewStates.Invisible)
                {
                    errMsg.Text = "You must spend all 27 points!";
                    errMsg.Visibility = ViewStates.Visible;
                }
                else
                {
                    StartActivity(typeof(NewChar3Screen));
                }
            };
        }
    }
}