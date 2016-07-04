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
    public class Tutorial1Screen : Activity
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

            CharacterSheet c = new CharacterSheet();

            c.Gender = "Male";
            c.Alignment = null;
            c.CharacterName = null;
            c.Background = KnownValues.Background.NONE;

            KnownValues.Race theRace = KnownValues.Race.NONE;
            KnownValues.SubRace theSubRace = KnownValues.SubRace.NONE;

            newCharName.AfterTextChanged += (s, arg) =>
            {
                if (String.IsNullOrEmpty(arg.ToString()))
                { }
                else
                {
                    c.CharacterName = newCharName.Text;
                }
            };

            radioGender.Click += (s, arg) =>
            {
                if (radioGender.CheckedRadioButtonId == radioMale.Id)
                {
                    c.Gender = "Male";
                }
                else
                {
                    c.Gender = "Female";
                }
            };

            pickRace.Click += (s, arg) =>
            {
                PopupMenu m = new PopupMenu(this, pickRace);
                m.Inflate(Resource.Xml.RaceList);
                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    var race = pickRace.Text;
                    pickRace.Text = arg1.Item.TitleFormatted.ToString();

                    if (race != pickRace.Text)
                    {
                        pickSub.Text = "Pick A Sub Race";
                    }

                    switch (pickRace.Text)
                    {
                        case "Dragonborn":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.DRAGONBORN;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.DRAGONBORN, true);
                            //c.setSubRace(KnownValues.SubRace.NONE);
                            break;
                        case "Dwarf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            //c.SetRace(KnownValues.Race.DWARF, true);
                            theRace = KnownValues.Race.DWARF;
                            theSubRace = KnownValues.SubRace.NONE;
                            break;
                        case "Elf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            theRace = KnownValues.Race.ELF;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.ELF, true);
                            break;
                        case "Gnome":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            theRace = KnownValues.Race.GNOME;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.GNOME, true);
                            break;
                        case "Half-Elf":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.HALF_ELF;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.HALF_ELF, true);
                            //c.setSubRace(KnownValues.SubRace.NONE);
                            break;
                        case "Halfling":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            theRace = KnownValues.Race.HALFLING;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.HALFLING, true);
                            break;
                        case "Half-Orc":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.HALF_ORC;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.HALF_ORC, true);
                            //c.setSubRace(KnownValues.SubRace.NONE);
                            break;
                        case "Human":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.HUMAN;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.HUMAN, true);
                            //c.setSubRace(KnownValues.SubRace.NONE);
                            break;
                        case "Tiefling":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.TIEFLING;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.TIEFLING, true);
                            //c.setSubRace(KnownValues.SubRace.NONE);
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

                switch (pickRace.Text)
                {
                    case "Dwarf":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.dwarfSub);
                        //c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Elf":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.elfSub);
                        //c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Gnome":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.gnomeSub);
                        //c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Halfling":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.halflingSub);
                        //c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                }

                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    pickSub.Text = arg1.Item.TitleFormatted.ToString();

                    switch (pickSub.Text)
                    {
                        case "Hill":
                            theSubRace = KnownValues.SubRace.HILL_DWARF;
                            //c.setSubRace(KnownValues.SubRace.HILL_DWARF);
                            break;
                        case "Mountain":
                            theSubRace = KnownValues.SubRace.MOUNTAIN_DWARF;
                            //c.setSubRace(KnownValues.SubRace.MOUNTAIN_DWARF);
                            break;
                        case "Wood":
                            theSubRace = KnownValues.SubRace.WOOD_ELF;
                            //c.setSubRace(KnownValues.SubRace.WOOD_ELF);
                            break;
                        case "High":
                            theSubRace = KnownValues.SubRace.HIGH_ELF;
                            //c.setSubRace(KnownValues.SubRace.HIGH_ELF);
                            break;
                        case "Drow":
                            theSubRace = KnownValues.SubRace.DROW;
                            // c.setSubRace(KnownValues.SubRace.DARK_ELF);
                            break;
                        case "Air":
                            theSubRace = KnownValues.SubRace.AIR_GENASI;
                            break;
                        case "Earth":
                            theSubRace = KnownValues.SubRace.EARTH_GENASI;
                            break;
                        case "Fire":
                            theSubRace = KnownValues.SubRace.FIRE_GENASI;
                            break;
                        case "Water":
                            theSubRace = KnownValues.SubRace.WATER_GENASI;
                            break;
                        case "Rock":
                            theSubRace = KnownValues.SubRace.ROCK_GNOME;
                            // c.setSubRace(KnownValues.SubRace.ROCK_GNOME);
                            break;
                        case "Forest":
                            theSubRace = KnownValues.SubRace.FOREST_GNOME;
                            // c.setSubRace(KnownValues.SubRace.FOREST_GNOME);
                            break;
                        case "Deep":
                            theSubRace = KnownValues.SubRace.DEEP_GNOME;
                            break;
                        case "Lightfoot":
                            theSubRace = KnownValues.SubRace.LIGHTFOOT_HALFLING;
                            // c.setSubRace(KnownValues.SubRace.LIGHTFOOT_HALFLING);
                            break;
                        case "Stout":
                            theSubRace = KnownValues.SubRace.STOUT_HALFLING;
                            //c.setSubRace(KnownValues.SubRace.STOUT_HALFLING);
                            break;
                        default:
                            theSubRace = KnownValues.SubRace.NONE;
                            // c.setSubRace(KnownValues.SubRace.NONE);
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
                    var background = pickBack.Text;
                    pickBack.Text = arg2.Item.TitleFormatted.ToString();

                    switch (pickBack.Text)
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
                    pickAlign.Text = arg2.Item.TitleFormatted.ToString();

                    c.Alignment = pickAlign.Text;
                };
            };

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetMessage("Our first step in the character creation process is to choose a name for our new Hero!  Let's pick something we are all familiar with!");
            alert.SetCancelable(false);
            alert.SetPositiveButton("OK", delegate 
            {
                newCharName.Text = "Harry Potter";
                newCharName.SetTextColor(Android.Graphics.Color.Cyan);
                newCharName.Enabled = false;
                alert.SetMessage("The next step is to choose a gender for our Hero.  Harry is male so we will leave the button alone.");
                radioGender.Enabled = false;
                radioMale.SetTextColor(Android.Graphics.Color.Cyan);
                radioMale.Enabled = false;
                radioFemale.Enabled = false;
                alert.SetPositiveButton("OK", delegate
                {
                    alert.SetMessage("After this, we pick a race for our Hero.  Harry is a Human, so let's select Human from the Pick Race button");
                    pickRace.Text = "Human";
                    pickRace.SetTextColor(Android.Graphics.Color.DarkCyan);
                    pickRace.Enabled = false;
                    alert.SetPositiveButton("OK", delegate
                    {
                        alert.SetMessage("Once we have selected a race, it is time to choose an alignment.  Alignment is a base estimate of a character's personality.  Harry Potter is Chaotic Good, so we will pick that from the Pick Alignment button.");
                        pickAlign.Text = "Chaotic Good";
                        pickAlign.SetTextColor(Android.Graphics.Color.DarkCyan);
                        pickAlign.Enabled = false;
                        alert.SetPositiveButton("OK", delegate
                        {
                            alert.SetMessage("When the alignment has been chosen, the last step in filling this portion of the character creation process is to choose a background for our Hero.  Harry Potter closely resembles a Folk Hero, so we will select that from the Pick Background button.");
                            pickBack.Text = "Folk Hero";
                            pickBack.SetTextColor(Android.Graphics.Color.DarkCyan);
                            pickBack.Enabled = false;
                            alert.SetPositiveButton("OK", delegate
                            {
                                alert.SetMessage("We have now finished this portion of the character creation process.  Press the continue button to proceed!");
                                continueButton.SetTextColor(Android.Graphics.Color.DarkCyan);
                            });
                            alert.Show();
                        });
                        alert.Show();
                    });
                    alert.Show();
                });
                alert.Show();
            });
            alert.Show();

            c.CharacterName = "Harry Potter";
            c.SetRace(KnownValues.Race.HUMAN, true);
            c.setSubRace(KnownValues.SubRace.NONE);
            c.Alignment = "Chaotic Good";
            c.Background = KnownValues.Background.FOLK_HERO;

            
           
            
            pickAlign.Enabled = false;
            pickBack.Enabled = false;


            continueButton.Click += (s, arg) =>
            {
                c.Level = 1;

                var charScreen = new Intent(this, typeof(Tutorial2Screen));
                var gsMsg = new GSActivityMessage();
                gsMsg.Message = CharacterSheet.GetBytes(c);
                charScreen.PutExtra(gsMsg.CharacterMessage, gsMsg.Message);
                StartActivity(charScreen);
            };
        }
    }
}