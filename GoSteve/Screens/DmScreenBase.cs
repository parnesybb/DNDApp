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
    [Activity(Label = "DmScreenBase")]
    public class DmScreenBase : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DmScreenBase);
            // Create your application here

            for (int i = 0; i < 5; i++)
            {
                Button b = new Button(this);
                b.Id = i;

                b.Click += (s, arg) =>
                {
                    new AlertDialog.Builder(this)
                    .SetMessage(b.Id)
                    .Show();
                };
            }

        }
    }
}