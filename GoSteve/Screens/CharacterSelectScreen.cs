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
using System.IO;
using System.Collections.Generic;

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

            // Open menu of saved character sheets.
            exCharButton.Click += (s, arg) =>
            {
                var locPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string[] files = Directory.GetFiles(locPath);
                var menu = new PopupMenu(this, exCharButton);

                foreach (var file in files)
                {
                    if (file.Contains(CharacterSheet.FILE_EXT))
                    {
                        menu.Menu.Add(file.Substring(file.LastIndexOf('/') + 1));
                    }
                }

                // Load the clicked character sheet.
                menu.MenuItemClick += (ss, ee) =>
                {
                    CharacterScreen.IsDM = false;
                    var csFile = CharacterSheet.ReadFromFile(ee.Item.TitleFormatted.ToString());
                    //csFile.ID = "";
                    var msg = new GSActivityMessage();
                    msg.Message = CharacterSheet.GetBytes(csFile);

                    var intent = new Intent(this, typeof(CharacterScreen));
                    intent.PutExtra(msg.CharacterMessage, msg.Message);
                    StartActivity(intent);
                };

                menu.Show();
            };
        }
    }
}

