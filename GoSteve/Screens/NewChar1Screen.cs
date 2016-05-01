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

            CharacterSheet c = new CharacterSheet();

            c.Gender = "Male";
            c.Alignment = null;
            c.CharacterName = null;
            c.Background = KnownValues.Background.NONE;

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

                            if(c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.DRAGONBORN, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.DRAGONBORN, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            break;
                        case "Dwarf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            
                            if(c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.DWARF, true);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.DWARF, true);
                            }
                            break;
                        case "Elf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.ELF, true);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.ELF, true);
                            }
                            break;
                        case "Gnome":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.GNOME, true);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.GNOME, true);
                            }
                            break;
                        case "Half-Elf":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.HALF_ELF, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.HALF_ELF, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            break;
                        case "Halfling":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.HALFLING, true);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.HALFLING, true);
                            }
                            break;
                        case "Half-Orc":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.HALF_ORC, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.HALF_ORC, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            break;
                        case "Human":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.HUMAN, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.HUMAN, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            break;
                        case "Tiefling":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;

                            if (c.RaceInstance == null)
                            {
                                c.SetRace(KnownValues.Race.TIEFLING, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
                            else
                            {
                                c.ClearFeaturesAndTraits();
                                c.ClearProficienciesLanguages();
                                c.SetRace(KnownValues.Race.TIEFLING, true);
                                c.setSubRace(KnownValues.SubRace.NONE);
                            }
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
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Elf":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.elfSub);
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Gnome":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.gnomeSub);
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                    case "Halfling":
                        //pickSub.Text = "Pick a Sub Race";
                        m.Inflate(Resource.Xml.halflingSub);
                        c.setSubRace(KnownValues.SubRace.NONE);
                        break;
                }

                m.Show();

                m.MenuItemClick += (s1, arg1) =>
                {
                    pickSub.Text = arg1.Item.TitleFormatted.ToString();

                    switch (pickSub.Text)
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

            continueButton.Click += (s, arg) =>
            {
                if (String.IsNullOrEmpty(c.CharacterName) || c.RaceInstance == null || c.Alignment == null || c.Background == KnownValues.Background.NONE || (pickSub.Visibility == ViewStates.Visible && c.getSubRace() == KnownValues.SubRace.NONE))
                {
                    missing1.Visibility = ViewStates.Visible;
                }
                else
                {
                    var charScreen = new Intent(this, typeof(NewChar3Screen));
                    var gsMsg = new GSActivityMessage();
                    gsMsg.Message = CharacterSheet.GetBytes(c);
                    charScreen.PutExtra(gsMsg.CharacterMessage, gsMsg.Message);
                    StartActivity(charScreen);
                }
            };
        }
    }
}