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
                        case "Aarakocra":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.AARAKOCRA;
                            theSubRace = KnownValues.SubRace.NONE;
                            break;
                        case "Aasimar":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.AASIMAR;
                            theSubRace = KnownValues.SubRace.NONE;
                            break;
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
                        case "Genasi":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            theRace = KnownValues.Race.GENASI;
                            theSubRace = KnownValues.SubRace.NONE;
                            break;
                        case "Goliath":
                            instructionSubRace.Visibility = ViewStates.Invisible;
                            pickSub.Visibility = ViewStates.Invisible;
                            theRace = KnownValues.Race.GOLIATH;
                            theSubRace = KnownValues.SubRace.NONE;
                            break;
                        case "Gnome":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
                            theRace = KnownValues.Race.GNOME;
                            theSubRace = KnownValues.SubRace.NONE;
                            //c.SetRace(KnownValues.Race.GNOME, true);
                            break;
                        case "Half-Elf":
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
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
                            instructionSubRace.Visibility = ViewStates.Visible;
                            pickSub.Visibility = ViewStates.Visible;
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
                    case "Half-Elf":
                        m.Inflate(Resource.Xml.halfelfSub);
                        break;
                    case "Genasi":
                        m.Inflate(Resource.Xml.genasiSub);
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
                    case "Tiefling":
                        m.Inflate(Resource.Xml.tieflingSub);
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
                        case "Wood Elf":
                            theSubRace = KnownValues.SubRace.WOOD_ELF;
                            break;
                        case "Wood":
                            theSubRace = KnownValues.SubRace.WOOD_ELF;
                            //c.setSubRace(KnownValues.SubRace.WOOD_ELF);
                            break;
                        case "High Elf":
                            theSubRace = KnownValues.SubRace.HIGH_ELF;
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
                        case "Water Elf":
                            theSubRace = KnownValues.SubRace.WATER_ELF;
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
                        case "Original":
                            theSubRace = KnownValues.SubRace.ORIGINAL;
                            break;
                        case "Feral":
                            theSubRace = KnownValues.SubRace.FERAL;
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
                        case "Haunted One":
                            c.Background = KnownValues.Background.HAUNTED_ONE;
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
                        case "City Watch":
                            c.Background = KnownValues.Background.CITY_WATCH;
                            break;
                        case "Clan Crafter":
                            c.Background = KnownValues.Background.CLAN_CRAFTER;
                            break;
                        case "Cloistered Scholar":
                            c.Background = KnownValues.Background.CLOISTERED_SCHOLAR;
                            break;
                        case "Courtier":
                            c.Background = KnownValues.Background.COURTIER;
                            break;
                        case "Faction Agent":
                            c.Background = KnownValues.Background.FACTION_AGENT;
                            break;
                        case "Far Traveler":
                            c.Background = KnownValues.Background.FAR_TRAVELER;
                            break;
                        case "Inheritor":
                            c.Background = KnownValues.Background.INHERITOR;
                            break;
                        case "Knight of the Order":
                            c.Background = KnownValues.Background.KNIGHT_OF_THE_ORDER;
                            break;
                        case "Mercenary Veteran":
                            c.Background = KnownValues.Background.MERCENARY_VETERAN;
                            break;
                        case "Urban Bounty Hunter":
                            c.Background = KnownValues.Background.URBAN_BOUNTY_HUNTER;
                            break;
                        case "Uthgardt Tribe Member":
                            c.Background = KnownValues.Background.UTHGARDT_TRIBE_MEMBER;
                            break;
                        case "Waterdhavian Noble":
                            c.Background = KnownValues.Background.WATERDHAVIAN_NOBLE;
                            break;
                        default:
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
                if (String.IsNullOrEmpty(c.CharacterName) || theRace == KnownValues.Race.NONE || c.Alignment == null || c.Background == KnownValues.Background.NONE || (pickSub.Visibility == ViewStates.Visible && theSubRace == KnownValues.SubRace.NONE))
                {
                    missing1.Visibility = ViewStates.Visible;
                }
                else
                {
                    c.SetRace(theRace, true);
                    c.setSubRace(theSubRace);
                    c.Level = 1;

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