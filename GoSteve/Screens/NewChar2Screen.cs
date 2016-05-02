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
            int str = 8;
            int dex = 8;
            int con = 8;
            int intel = 8;
            int wis = 8;
            int cha = 8;

            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            var cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            CharacterSheet c = cs;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar2);

            TextView instructionAb1 = FindViewById<TextView>(Resource.Id.instructionAb1);
            TextView instructionAb2 = FindViewById<TextView>(Resource.Id.instructionAb2);
            TextView blank1 = FindViewById<TextView>(Resource.Id.blank1);
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
            TextView strScoreNum = FindViewById<TextView>(Resource.Id.strScoreNum);
            TextView dexScoreNum = FindViewById<TextView>(Resource.Id.dexScoreNum);
            TextView conScoreNum = FindViewById<TextView>(Resource.Id.conScoreNum);
            TextView intScoreNum = FindViewById<TextView>(Resource.Id.intScoreNum);
            TextView wisScoreNum = FindViewById<TextView>(Resource.Id.wisScoreNum);
            TextView chaScoreNum = FindViewById<TextView>(Resource.Id.chaScoreNum);
            TextView remPoints = FindViewById<TextView>(Resource.Id.remPoints);
            TextView raceLabel = FindViewById<TextView>(Resource.Id.raceLabel);
            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            Button strMinus = FindViewById<Button>(Resource.Id.strMinus);
            Button strPlus = FindViewById<Button>(Resource.Id.strPlus);
            Button dexMinus = FindViewById<Button>(Resource.Id.dexMinus);
            Button dexPlus = FindViewById<Button>(Resource.Id.dexPlus);
            Button conMinus = FindViewById<Button>(Resource.Id.conMinus);
            Button conPlus = FindViewById<Button>(Resource.Id.conPlus);
            Button intMinus = FindViewById<Button>(Resource.Id.intMinus);
            Button intPlus = FindViewById<Button>(Resource.Id.intPlus);
            Button wisMinus = FindViewById<Button>(Resource.Id.wisMinus);
            Button wisPlus = FindViewById<Button>(Resource.Id.wisPlus);
            Button chaMinus = FindViewById<Button>(Resource.Id.chaMinus);
            Button chaPlus = FindViewById<Button>(Resource.Id.chaPlus);


            switch (c.RaceInstance.SubRace)
            {
                case KnownValues.SubRace.DARK_ELF:
                    raceLabel.Text = "Dark Elf";
                    break;
                case KnownValues.SubRace.FOREST_GNOME:
                    raceLabel.Text = "Forest Gnome";
                    break;
                case KnownValues.SubRace.HIGH_ELF:
                    raceLabel.Text = "High Elf";
                    break;
                case KnownValues.SubRace.HILL_DWARF:
                    raceLabel.Text = "Hill Dwarf";
                    break;
                case KnownValues.SubRace.LIGHTFOOT_HALFLING:
                    raceLabel.Text = "Lightfoot Halfling";
                    break;
                case KnownValues.SubRace.MOUNTAIN_DWARF:
                    raceLabel.Text = "Mountain Dwarf";
                    break;
                case KnownValues.SubRace.NONE:
                    raceLabel.Text = c.RaceInstance.ToString().Substring(25);
                    break;
                case KnownValues.SubRace.ROCK_GNOME:
                    raceLabel.Text = "Rock Gnome";
                    break;
                case KnownValues.SubRace.STOUT_HALFLING:
                    raceLabel.Text = "Stout Halfling";
                    break;
                case KnownValues.SubRace.WOOD_ELF:
                    raceLabel.Text = "Wood Elf";
                    break;
                default:
                    raceLabel.Text = "NOT FOUND";
                    break;
            }

            strMinus.Click += (s, arg) =>
            {
                switch (str)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            str = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            str = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            str = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            str = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            str = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            str = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            str = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                strScoreNum.Text = str.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            strPlus.Click += (s, arg) =>
            {
                switch (str)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            str = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            str = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            str = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            str = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            str = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            str = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            str = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                strScoreNum.Text = str.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            dexMinus.Click += (s, arg) =>
            {
                switch (dex)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            dex = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            dex = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            dex = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            dex = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            dex = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            dex = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            dex = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                dexScoreNum.Text = dex.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            dexPlus.Click += (s, arg) =>
            {
                switch (dex)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            dex = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            dex = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            dex = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            dex = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            dex = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            dex = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            dex = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                dexScoreNum.Text = dex.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            conMinus.Click += (s, arg) =>
            {
                switch (con)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            con = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            con = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            con = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            con = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            con = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            con = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            con = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                conScoreNum.Text = con.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            conPlus.Click += (s, arg) =>
            {
                switch (con)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            con = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            con = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            con = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            con = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            con = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            con = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            con = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                conScoreNum.Text = con.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            intMinus.Click += (s, arg) =>
            {
                switch (intel)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            intel = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            intel = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            intel = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            intel = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            intel = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            intel = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            intel = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                intScoreNum.Text = intel.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            intPlus.Click += (s, arg) =>
            {
                switch (intel)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            intel = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            intel = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            intel = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            intel = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            intel = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            intel = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            intel = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                intScoreNum.Text = intel.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            wisMinus.Click += (s, arg) =>
            {
                switch (wis)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            wis = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            wis = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            wis = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            wis = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            wis = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            wis = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            wis = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                wisScoreNum.Text = wis.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            wisPlus.Click += (s, arg) =>
            {
                switch (wis)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            wis = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            wis = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            wis = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            wis = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            wis = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            wis = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            wis = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                wisScoreNum.Text = wis.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            chaMinus.Click += (s, arg) =>
            {
                switch (cha)
                {
                    case 8:
                        break;
                    case 9:
                        if (numPoints < 27)
                        {
                            cha = 8;
                            numPoints++;
                        }
                        break;
                    case 10:
                        if (numPoints < 27)
                        {
                            cha = 9;
                            numPoints++;
                        }
                        break;
                    case 11:
                        if (numPoints < 27)
                        {
                            cha = 10;
                            numPoints++;
                        }
                        break;
                    case 12:
                        if (numPoints < 27)
                        {
                            cha = 11;
                            numPoints++;
                        }
                        break;
                    case 13:
                        if (numPoints < 27)
                        {
                            cha = 12;
                            numPoints++;
                        }
                        break;
                    case 14:
                        if (numPoints < 27)
                        {
                            cha = 13;
                            numPoints += 2;
                        }
                        break;
                    case 15:
                        if (numPoints < 27)
                        {
                            cha = 14;
                            numPoints += 2;
                        }
                        break;
                    default:
                        break;
                }

                chaScoreNum.Text = cha.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };

            chaPlus.Click += (s, arg) =>
            {
                switch (cha)
                {
                    case 8:
                        if (numPoints > 0)
                        {
                            cha = 9;
                            numPoints--;
                        }
                        break;
                    case 9:
                        if (numPoints > 0)
                        {
                            cha = 10;
                            numPoints--;
                        }
                        break;
                    case 10:
                        if (numPoints > 0)
                        {
                            cha = 11;
                            numPoints--;
                        }
                        break;
                    case 11:
                        if (numPoints > 0)
                        {
                            cha = 12;
                            numPoints--;
                        }
                        break;
                    case 12:
                        if (numPoints > 0)
                        {
                            cha = 13;
                            numPoints--;
                        }
                        break;
                    case 13:
                        if (numPoints > 0)
                        {
                            cha = 14;
                            numPoints -= 2;
                        }
                        break;
                    case 14:
                        if (numPoints > 0)
                        {
                            cha = 15;
                            numPoints -= 2;
                        }
                        break;
                    case 15:
                        break;
                    default:
                        break;
                }

                chaScoreNum.Text = cha.ToString();
                remPoints.Text = "Remaining Points: " + numPoints;
            };


            continueBtn.Click += (s, arg) =>
            {
                if (numPoints != 0)
                {
                    errMsg.Text = "You must spend all 27 points!";
                    errMsg.Visibility = ViewStates.Visible;
                }
                else
                {
                    c.AbilitiesAndStats.Strength = str;
                    c.AbilitiesAndStats.Dex = dex;
                    c.AbilitiesAndStats.Con = con;
                    c.AbilitiesAndStats.Wisdom = wis;
                    c.AbilitiesAndStats.Intel = intel;
                    c.AbilitiesAndStats.Charisma = cha;

                    var charScreen = new Intent(this, typeof(NewChar4Screen));
                    var gsMsg1 = new GSActivityMessage();
                    gsMsg1.Message = CharacterSheet.GetBytes(c);
                    charScreen.PutExtra(gsMsg1.CharacterMessage, gsMsg1.Message);
                    StartActivity(charScreen);
                }
            };
        }
    }
}