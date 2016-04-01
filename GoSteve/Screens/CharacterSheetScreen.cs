using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GoSteve
{
    [Activity(Label = "GoSteve! Dungeons and Dragons", MainLauncher = true, Icon = "@drawable/icon")]
    public class CharacterSheetScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CharacterSheet);

            // Set fields.
            Button classSelectButton = FindViewById<Button>(Resource.Id.classSelect);
            Button backgroundSelectButton = FindViewById<Button>(Resource.Id.charBG);
            Button raceSelectButton = FindViewById<Button>(Resource.Id.race);
            Button subRaceSelectButton = FindViewById<Button>(Resource.Id.subRace);
            Button alignmentSelectButton = FindViewById<Button>(Resource.Id.alignment);

            // Class selection.
            classSelectButton.Click += (s, arg) => 
            {
                PopupMenu menu = new PopupMenu(this, classSelectButton);
                menu.Inflate(Resource.Xml.ClassList);
                menu.Show();

                menu.MenuItemClick += (s1, arg1) => 
                {
                    classSelectButton.Text = arg1.Item.TitleFormatted.ToString();
                };
            };

            // Background selection.          
            backgroundSelectButton.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, backgroundSelectButton);
                m.Inflate(Resource.Xml.BackgroundList);
                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    backgroundSelectButton.Text = arg1.Item.TitleFormatted.ToString();
                };
            };

            // Race selection.           
            raceSelectButton.Click += (s, arg) =>
            {               
                PopupMenu m = new PopupMenu(this, raceSelectButton);
                m.Inflate(Resource.Xml.RaceList);
                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    var prev = raceSelectButton.Text;
                    raceSelectButton.Text = arg1.Item.TitleFormatted.ToString();

                    if (prev != raceSelectButton.Text)
                    {
                        subRaceSelectButton.Text = "None";
                    }
                };
            };

            // Sub-race selection.      
            subRaceSelectButton.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, subRaceSelectButton);

                switch (raceSelectButton.Text)
                {
                    case KnownValues.Races.DWARF:
                        m.Inflate(Resource.Xml.dwarfSub);
                        break;

                    case KnownValues.Races.ELF:
                        m.Inflate(Resource.Xml.elfSub);
                        break;

                    case KnownValues.Races.GNOME:
                        m.Inflate(Resource.Xml.gnomeSub);
                        break;

                    case KnownValues.Races.HALFLING:
                        m.Inflate(Resource.Xml.halflingSub);
                        break;

                    default:
                        break;
                }

                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    subRaceSelectButton.Text = arg1.Item.TitleFormatted.ToString();
                };
            };

            // Alignment selection.
            alignmentSelectButton.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, alignmentSelectButton);
                m.Inflate(Resource.Xml.alignment);
                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    alignmentSelectButton.Text = arg1.Item.TitleFormatted.ToString();
                };
            };

        }
    }
}

