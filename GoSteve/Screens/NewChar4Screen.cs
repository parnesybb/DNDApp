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
    public class NewChar4Screen : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            var cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            CharacterSheet c = cs;

            var bg = "";
            var race = "";
            var numMaxSkills = 0;
            var numSkillsChosen = 0;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar4);

            TextView yourOptions = FindViewById<TextView>(Resource.Id.yourOptions);
            TextView numPoints = FindViewById<TextView>(Resource.Id.numPoints);
            TextView errMsg = FindViewById<TextView>(Resource.Id.errMsg);
            CheckBox acrobaticsChk = FindViewById<CheckBox>(Resource.Id.acrobaticsChk);
            CheckBox animalhandlingChk = FindViewById<CheckBox>(Resource.Id.animalhandlingChk);
            CheckBox arcanaChk = FindViewById<CheckBox>(Resource.Id.arcanaChk);
            CheckBox athleticsChk = FindViewById<CheckBox>(Resource.Id.athleticsChk);
            CheckBox deceptionChk = FindViewById<CheckBox>(Resource.Id.deceptionChk);
            CheckBox historyChk = FindViewById<CheckBox>(Resource.Id.historyChk);
            CheckBox insightChk = FindViewById<CheckBox>(Resource.Id.insightChk);
            CheckBox intimidationChk = FindViewById<CheckBox>(Resource.Id.intimidationChk);
            CheckBox investigationChk = FindViewById<CheckBox>(Resource.Id.investigationChk);
            CheckBox medicineChk = FindViewById<CheckBox>(Resource.Id.medicineChk);
            CheckBox natureChk = FindViewById<CheckBox>(Resource.Id.natureChk);
            CheckBox perceptionChk = FindViewById<CheckBox>(Resource.Id.perceptionChk);
            CheckBox performanceChk = FindViewById<CheckBox>(Resource.Id.performanceChk);
            CheckBox persuasionChk = FindViewById<CheckBox>(Resource.Id.persuasionChk);
            CheckBox religionChk = FindViewById<CheckBox>(Resource.Id.religionChk);
            CheckBox sleightChk = FindViewById<CheckBox>(Resource.Id.sleightChk);
            CheckBox stealthChk = FindViewById<CheckBox>(Resource.Id.stealthChk);
            CheckBox survivalChk = FindViewById<CheckBox>(Resource.Id.survivalChk);
            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);

            CheckBox[] chks = { acrobaticsChk, animalhandlingChk, arcanaChk, athleticsChk, deceptionChk, historyChk, insightChk, intimidationChk, investigationChk, medicineChk, natureChk, perceptionChk, performanceChk, persuasionChk, religionChk, sleightChk, stealthChk, survivalChk };

            switch(c.Background)
            {
                case KnownValues.Background.ACOLYTE:
                    bg = "Acolyte";
                    insightChk.Checked = true;
                    insightChk.Enabled = false;
                    insightChk.SetTextColor(Android.Graphics.Color.Cyan);
                    religionChk.Checked = true;
                    religionChk.Enabled = false;
                    religionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.CHARLATAN:
                    bg = "Charlatan";
                    deceptionChk.Checked = true;
                    deceptionChk.Enabled = false;
                    deceptionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    sleightChk.Checked = true;
                    sleightChk.Enabled = false;
                    sleightChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.CRIMINAL:
                    bg = "Criminal";
                    deceptionChk.Checked = true;
                    deceptionChk.Enabled = false;
                    deceptionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    stealthChk.Checked = true;
                    stealthChk.Enabled = false;
                    stealthChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.ENTERTAINER:
                    bg = "Entertainer";
                    acrobaticsChk.Checked = true;
                    acrobaticsChk.Enabled = false;
                    acrobaticsChk.SetTextColor(Android.Graphics.Color.Cyan);
                    performanceChk.Checked = true;
                    performanceChk.Enabled = false;
                    performanceChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.FOLK_HERO:
                    bg = "Folk Hero";
                    animalhandlingChk.Checked = true;
                    animalhandlingChk.Enabled = false;
                    animalhandlingChk.SetTextColor(Android.Graphics.Color.Cyan);
                    survivalChk.Checked = true;
                    survivalChk.Enabled = false;
                    survivalChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.GUILD_ARTISAN:
                    bg = "Guild Artisan";
                    insightChk.Checked = true;
                    insightChk.Enabled = false;
                    insightChk.SetTextColor(Android.Graphics.Color.Cyan);
                    persuasionChk.Checked = true;
                    persuasionChk.Enabled = false;
                    persuasionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.HERMIT:
                    bg = "Hermit";
                    medicineChk.Checked = true;
                    medicineChk.Enabled = false;
                    medicineChk.SetTextColor(Android.Graphics.Color.Cyan);
                    religionChk.Checked = true;
                    religionChk.Enabled = false;
                    religionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.NOBLE:
                    bg = "Noble";
                    historyChk.Checked = true;
                    historyChk.Enabled = false;
                    historyChk.SetTextColor(Android.Graphics.Color.Cyan);
                    persuasionChk.Checked = true;
                    persuasionChk.Enabled = false;
                    persuasionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.OUTLANDER:
                    bg = "Outlander";
                    athleticsChk.Checked = true;
                    athleticsChk.Enabled = false;
                    athleticsChk.SetTextColor(Android.Graphics.Color.Cyan);
                    survivalChk.Checked = true;
                    survivalChk.Enabled = false;
                    survivalChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.SAGE:
                    bg = "Sage";
                    arcanaChk.Checked = true;
                    arcanaChk.Enabled = false;
                    arcanaChk.SetTextColor(Android.Graphics.Color.Cyan);
                    historyChk.Checked = true;
                    historyChk.Enabled = false;
                    historyChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.SAILOR:
                    bg = "Sailor";
                    athleticsChk.Checked = true;
                    athleticsChk.Enabled = false;
                    athleticsChk.SetTextColor(Android.Graphics.Color.Cyan);
                    perceptionChk.Checked = true;
                    perceptionChk.Enabled = false;
                    perceptionChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.SOLDIER:
                    bg = "Soldier";
                    athleticsChk.Checked = true;
                    athleticsChk.Enabled = false;
                    athleticsChk.SetTextColor(Android.Graphics.Color.Cyan);
                    intimidationChk.Checked = true;
                    intimidationChk.Enabled = false;
                    intimidationChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                case KnownValues.Background.URCHIN:
                    bg = "Urchin";
                    sleightChk.Checked = true;
                    sleightChk.Enabled = false;
                    sleightChk.SetTextColor(Android.Graphics.Color.Cyan);
                    stealthChk.Checked = true;
                    stealthChk.Enabled = false;
                    stealthChk.SetTextColor(Android.Graphics.Color.Cyan);
                    break;
                default:
                    break;
            }

            switch(c.RaceInstance.SubRace)
            {
                case KnownValues.SubRace.DARK_ELF:
                    race = "Dark Elf";
                    break;
                case KnownValues.SubRace.FOREST_GNOME:
                    race = "Forest Gnome";
                    break;
                case KnownValues.SubRace.HIGH_ELF:
                    race = "High Elf";
                    break;
                case KnownValues.SubRace.HILL_DWARF:
                    race = "Hill Dwarf";
                    break;
                case KnownValues.SubRace.LIGHTFOOT_HALFLING:
                    race = "Lightfoot Halfling";
                    break;
                case KnownValues.SubRace.MOUNTAIN_DWARF:
                    race = "Mountain Dwarf";
                    break;
                case KnownValues.SubRace.NONE:
                    race = c.RaceInstance.ToString().Substring(25);
                    break;
                case KnownValues.SubRace.ROCK_GNOME:
                    race = "Rock Gnome";
                    break;
                case KnownValues.SubRace.STOUT_HALFLING:
                    race = "Stout Halfling";
                    break;
                case KnownValues.SubRace.WOOD_ELF:
                    race = "Wood Elf";
                    break;
                default:
                    race = "NOT FOUND";
                    break;
            }

            switch(c.ClassInstance.ToString().Substring(27))
            {
                case "Barbarian":
                    numMaxSkills = 2;
                    break;
                case "Bard":
                    numMaxSkills = 3;
                    break;
                case "Cleric":
                    numMaxSkills = 2;
                    break;
                case "Druid":
                    numMaxSkills = 2;
                    break;
                case "Fighter":
                    numMaxSkills = 2;
                    break;
                case "Monk":
                    numMaxSkills = 2;
                    break;
                case "Paladin":
                    numMaxSkills = 2;
                    break;
                case "Ranger":
                    numMaxSkills = 3;
                    break;
                case "Rogue":
                    numMaxSkills = 4;
                    break;
                case "Sorceror":
                    numMaxSkills = 2;
                    break;
                case "Warlock":
                    numMaxSkills = 2;
                    break;
                case "Wizard":
                    numMaxSkills = 2;
                    break;
                default:
                    break;
            }

            numPoints.Text = "Choose " + numMaxSkills + " Skills";

            foreach(CheckBox checkbox in chks)
            {
                switch (c.ClassInstance.ToString().Substring(27))
                {
                    case "Barbarian":
                        if(checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if(checkbox == animalhandlingChk || checkbox == athleticsChk || checkbox == intimidationChk || checkbox == natureChk || checkbox == perceptionChk || checkbox == survivalChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Bard":
                        checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                        break;
                    case "Cleric":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == historyChk || checkbox == insightChk || checkbox == religionChk || checkbox == medicineChk || checkbox == persuasionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Druid":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == animalhandlingChk || checkbox == arcanaChk || checkbox == insightChk || checkbox == natureChk || checkbox == perceptionChk || checkbox == survivalChk || checkbox == medicineChk || checkbox == religionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Fighter":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == animalhandlingChk || checkbox == athleticsChk || checkbox == acrobaticsChk || checkbox == historyChk || checkbox == perceptionChk || checkbox == survivalChk || checkbox == insightChk || checkbox == intimidationChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Monk":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == acrobaticsChk || checkbox == athleticsChk || checkbox == historyChk || checkbox == insightChk || checkbox == religionChk || checkbox == stealthChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Ranger":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == animalhandlingChk || checkbox == athleticsChk || checkbox == insightChk || checkbox == investigationChk || checkbox == perceptionChk || checkbox == survivalChk || checkbox == natureChk || checkbox == stealthChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Rogue":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == acrobaticsChk || checkbox == athleticsChk || checkbox == deceptionChk || checkbox == insightChk || checkbox == intimidationChk || checkbox == investigationChk || checkbox == perceptionChk || checkbox ==  performanceChk || checkbox == persuasionChk || checkbox == sleightChk || checkbox == stealthChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Paladin":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == insightChk || checkbox == athleticsChk || checkbox == intimidationChk || checkbox == medicineChk || checkbox == persuasionChk || checkbox == religionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Sorceror":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == arcanaChk || checkbox == deceptionChk || checkbox == insightChk || checkbox == intimidationChk || checkbox == persuasionChk || checkbox == religionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Warlock":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == arcanaChk || checkbox == deceptionChk || checkbox == intimidationChk || checkbox == historyChk || checkbox == investigationChk || checkbox == natureChk || checkbox == religionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                    case "Wizard":
                        if (checkbox.CurrentTextColor == Android.Graphics.Color.Cyan)
                        {
                            continue;
                        }
                        else if (checkbox == arcanaChk || checkbox == historyChk || checkbox == insightChk || checkbox == investigationChk || checkbox == medicineChk || checkbox == religionChk)
                        {
                            checkbox.SetTextColor(Android.Graphics.Color.Cyan);
                            checkbox.Enabled = true;
                        }
                        else
                        {
                            checkbox.Enabled = false;
                        }
                        break;
                }
            }

            yourOptions.Text = "Race: " + race + "\nClass: " + c.ClassInstance.ToString().Substring(27) + "\nBackground: " + bg;
            
            continueBtn.Click += (s, arg) =>
            {
                foreach(CheckBox chk in chks)
                {
                    if(chk.Checked == true && chk.Enabled == true)
                    {
                        numSkillsChosen++;
                    }
                }

                if(numSkillsChosen < numMaxSkills)
                {
                    errMsg.Text = "YOU DID NOT PICK ALL OF YOUR SKILLS";
                }
                else if(numSkillsChosen > numMaxSkills)
                {
                    errMsg.Text = "YOU HAVE CHOSEN TOO MANY SKILLS";
                }
                else
                {
                    var charScreen = new Intent(this, typeof(CharacterScreen));
                    var gsMsg1 = new GSActivityMessage();
                    gsMsg1.Message = CharacterSheet.GetBytes(c);
                    charScreen.PutExtra(gsMsg1.CharacterMessage, gsMsg1.Message);
                    StartActivity(charScreen);
                }
            };
        }
    }
}