using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoSteve.Screens
{
    [Activity(Label = "TestScreen")]
    public class TestScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);

            var cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            //byte[] bytes = (byte[])Intent.Extras.Get("charSheet");
            //var formatter = new BinaryFormatter();
            //var ms = new System.IO.MemoryStream(bytes);
            //var cs = (CharacterSheet)formatter.Deserialize(ms);
            //ms.Close();

            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;

            var name = new TextView(this);
            name.Text = cs.CharacterName;
            var classType = new TextView(this);
            classType.Text = cs.ClassInstance.Type.ToString();

            this.ActionBar.Title = cs.CharacterName + "'s Character Sheet";

            layout.AddView(name);
            layout.AddView(classType);
            SetContentView(layout);
        }
    }
}