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

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar4);

            TextView yourClass = FindViewById<TextView>(Resource.Id.yourClass);

            yourClass.Text = "Race: " + c.RaceInstance.ToString().Substring(25) + "\nClass: " + c.ClassInstance.ToString().Substring(27) +"\nBackground: " + c.Background.ToString();

            switch(c.ClassInstance.ToString())
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