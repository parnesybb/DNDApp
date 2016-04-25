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
    public class NewChar1Screen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar1);

            // Set fields.
            TextView newCharName = FindViewById<TextView>(Resource.Id.newCharName);
            TextView instructionSubRace = FindViewById<TextView>(Resource.Id.instructionSubRace);
            TextView missing1 = FindViewById<TextView>(Resource.Id.missing1);
            RadioGroup radioGender = FindViewById<RadioGroup>(Resource.Id.radioGender);
            RadioButton radioMale = FindViewById<RadioButton>(Resource.Id.radioMale);
            RadioButton radioFemale = FindViewById<RadioButton>(Resource.Id.radioFemale);
            Button pickRace = FindViewById<Button>(Resource.Id.pickRace);
            Button pickSub = FindViewById<Button>(Resource.Id.pickSub);
            Button pickBack = FindViewById<Button>(Resource.Id.pickBack);
            Button pickAlign = FindViewById<Button>(Resource.Id.pickAlign);
            Button continueButton = FindViewById<Button>(Resource.Id.continueButton);

            String race = pickRace.Text;
            String sub;
            String background;
            String align;
            String gender;
            //String name;

            CharacterSheet c = new CharacterSheet();

            radioGender.Click += (s, arg) =>
            {
                if(radioGender.CheckedRadioButtonId == radioMale.Id)
                {
                    gender = "male";
                }
                else
                {
                    gender = "female";
                }

                c.Gender = gender;
            };

            pickRace.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, pickRace);
                m.Inflate(Resource.Xml.RaceList);
                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    race = pickRace.Text;
                    pickRace.Text = arg1.Item.TitleFormatted.ToString();

                    switch (race)
                    {
                        case "Dragonborn":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            c.SetRace(KnownValues.Race.DRAGONBORN, true);
                            c.setSubRace(KnownValues.SubRace.NONE);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Dwarf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            c.SetRace(KnownValues.Race.DWARF, true);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Elf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            c.SetRace(KnownValues.Race.ELF, true);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Gnome":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            c.SetRace(KnownValues.Race.GNOME, true);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Half-Elf":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            c.SetRace(KnownValues.Race.HALF_ELF, true);
                            c.setSubRace(KnownValues.SubRace.NONE);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Halfling":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            c.SetRace(KnownValues.Race.HALFLING, true);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "HalfOrc":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            c.SetRace(KnownValues.Race.HALF_ORC, true);
                            c.setSubRace(KnownValues.SubRace.NONE);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Human":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            c.SetRace(KnownValues.Race.HUMAN, true);
                            c.setSubRace(KnownValues.SubRace.NONE);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        case "Tiefling":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            c.SetRace(KnownValues.Race.TIEFLING, true);
                            c.setSubRace(KnownValues.SubRace.NONE);
                            pickRace.Text = arg1.Item.TitleFormatted.ToString();
                            break;
                        default:
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            break;
                    }
                };
            };

            pickSub.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, pickSub);

                switch(race)
                {
                    case "Dwarf":
                        m.Inflate(Resource.Xml.dwarfSub);
                        pickSub.Text = "Pick a Sub Race";
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Elf":
                        m.Inflate(Resource.Xml.elfSub);
                        pickSub.Text = "Pick a Sub Race";
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Gnome":
                        m.Inflate(Resource.Xml.gnomeSub);
                        pickSub.Text = "Pick a Sub Race";
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Halfling":
                        m.Inflate(Resource.Xml.halflingSub);
                        pickSub.Text = "Pick a Sub Race";
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                }

                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    sub = pickSub.Text;
                    pickSub.Text = arg1.Item.TitleFormatted.ToString();

                    switch(sub)
                    {
                        case "Hill":
                            c.setSubRace(KnownValues.SubRace.HILL_DWARF);
                            break;
                        case "Mountain":
                            c.setSubRace(KnownValues.SubRace.MOUNTAIN_DWARF);
                            break;
                        case "Wood":
                            c.setSubRace(KnownValues.SubRace.WOOD_ELF);
                            break;
                        case "High":
                            c.setSubRace(KnownValues.SubRace.HIGH_ELF);
                            break;
                        case "Dark":
                            c.setSubRace(KnownValues.SubRace.DARK_ELF);
                            break;
                        case "Rock":
                            c.setSubRace(KnownValues.SubRace.ROCK_GNOME);
                            break;
                        case "Forest":
                            c.setSubRace(KnownValues.SubRace.FOREST_GNOME);
                            break;
                        case "Lightfoot":
                            c.setSubRace(KnownValues.SubRace.LIGHTFOOT_HALFLING);
                            break;
                        case "Stout":
                            c.setSubRace(KnownValues.SubRace.STOUT_HALFLING);
                            break;
                        default:
                            c.setSubRace(KnownValues.SubRace.NONE);
                            break;
                    }

                };
            };

            pickBack.Click += (s1, arg1) =>
            {
                PopupMenu m = new PopupMenu(this, pickBack);
                m.Inflate(Resource.Xml.BackgroundList);
                m.Show();

                m.MenuItemClick += (s2, arg2) =>
                {
                    background = pickBack.Text;
                    pickBack.Text = arg2.Item.TitleFormatted.ToString();

                    switch(background)
                    {
                        case "Acolyte":
                            c.Background = KnownValues.Background.ACOLYTE;
                            break;
                        case "Charlatan":
                            c.Background = KnownValues.Background.CHARLATAN;
                            break;
                        case "Criminal":
                            c.Background = KnownValues.Background.CRIMINAL;
                            break;
                        case "Entertainer":
                            c.Background = KnownValues.Background.ENTERTAINER;
                            break;
                        case "Folk Hero":
                            c.Background = KnownValues.Background.FOLK_HERO;
                            break;
                        case "Guild Artisan":
                            c.Background = KnownValues.Background.GUILD_ARTISAN;
                            break;
                        case "Hermit":
                            c.Background = KnownValues.Background.HERMIT;
                            break;
                        case "Noble":
                            c.Background = KnownValues.Background.NOBLE;
                            break;
                        case "Outlander":
                            c.Background = KnownValues.Background.OUTLANDER;
                            break;
                        case "Sage":
                            c.Background = KnownValues.Background.SAGE;
                            break;
                        case "Sailor":
                            c.Background = KnownValues.Background.SAILOR;
                            break;
                        case "Soldier":
                            c.Background = KnownValues.Background.SOLDIER;
                            break;
                        case "Urchin":
                            c.Background = KnownValues.Background.URCHIN;
                            break;
                    }
                };
            };

            pickAlign.Click += (s1, arg1) =>
            {
                PopupMenu m = new PopupMenu(this, pickAlign);
                m.Inflate(Resource.Xml.alignment);
                m.Show();

                m.MenuItemClick += (s2, arg2) =>
                {
                    align = pickAlign.Text;
                    pickAlign.Text = arg2.Item.TitleFormatted.ToString();

                    c.Alignment = align;
                };
            };

            continueButton.Click += (s, arg) =>
            {
                c.CharacterName = newCharName.Text;

                if (c.CharacterName == "" ||  pickRace.Text == "Pick a race" || c.Alignment == "Pick an alignment" || pickSub.Text == "Pick a sub race")
                {
                    missing1.Visibility = ViewStates.Visible;
                }
                else
                {
                    StartActivity(typeof(NewChar2Screen));
                }
            };
        }
    }
}