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

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar4);

            TextView yourClass = FindViewById<TextView>(Resource.Id.yourClass);

            switch (c.Background.ToString())
            {
                case "ACOLYTE":
                    bg = "Acolyte";
                    break;
                case "CHARLATAN":
                    bg = "Charlatan";
                    break;
                case "CRIMINAL":
                    bg = "Criminal";
                    break;
                case "ENTERTAINER":
                    bg = "Entertainer";
                    break;
                case "FOLK_HERO":
                    bg = "Folk Hero";
                    break;
                case "GUILD_ARTISAN":
                    bg = "Guild Artisan";
                    break;
                case "HERMIT":
                    bg = "Hermit";
                    break;
                case "NOBLE":
                    bg = "Noble";
                    break;
                case "OUTLANDER":
                    bg = "Outlander";
                    break;
                case "SAGE":
                    bg = "Sage";
                    break;
                case "SAILOR":
                    bg = "Sailor";
                    break;
                case "SOLDIER":
                    bg = "Soldier";
                    break;
                case "URCHIN":
                    bg = "Urchin";
                    break;
                default:
                    break;
            }



            yourClass.Text = "Race: " + c.RaceInstance.ToString().Substring(25) + "\nClass: " + c.ClassInstance.ToString().Substring(27) + "\nBackground: " + bg;

            switch (c.ClassInstance.ToString())
            {
                case "BARBARIAN":
                    break;
                case "BARD":
                    break;
                case "CLERIC":
                    break;
                case "DRUID":
                    break;
                case "FIGHTER":
                    break;
                case "MONK":
                    break;
                case "PALADIN":
                    break;
                case "RANGER":
                    break;
                case "ROGUE":
                    break;
                case "SORCEROR":
                    break;
                case "WARLOCK":
                    break;
                case "WIZARD":
                    break;
                default:
                    break;
            }
        }
    }
}