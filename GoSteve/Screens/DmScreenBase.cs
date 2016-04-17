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
using GoSteve.Structures;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoSteve.Screens
{
    [Activity(Label = "DmScreenBase")]
    public class DmScreenBase : Activity
    {
        private Dictionary<string, CharacterSheet> _charSheets;
        private int _buttonCount;
        private LinearLayout _layout;

        public DmScreenBase()
        {
            this._charSheets = new Dictionary<string, CharacterSheet>();
            this._buttonCount = 0;
        }

        public void Update(CharacterSheet cs)
        {
            if (String.IsNullOrEmpty(cs.ID))
            {
                // New player.
                cs.ID = System.Guid.NewGuid().ToString();
                this._charSheets.Add(cs.ID, cs);

                var b = new CharacterButton(this);
                b.Id = ++this._buttonCount;
                b.CharacterID = cs.ID;
                b.Text = cs.CharacterName;

                b.Click += (sender, args) => 
                {
                    var charScreen = new Intent(this, typeof(TestScreen));
                    var ms = new System.IO.MemoryStream();
                    var formatter = new BinaryFormatter();

                    formatter.Serialize(ms, _charSheets[b.CharacterID]);
                    var arr = ms.ToArray();
                    ms.Close();

                    charScreen.PutExtra("charSheet", arr);
                    StartActivity(charScreen);
                };

                this._layout.AddView(b);
            }
            else
            {
                // Update the dictionary for existing player.
                this._charSheets[cs.ID] = cs;
            }
        }

        private void CreateFakeRequest()
        {
            var cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            cs.SetRace(KnownValues.Race.DWARF, true);
            cs.Background = KnownValues.Background.SOLDIER;
            cs.CharacterName = "Flaf";

            this.Update(cs);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this._layout = new LinearLayout(this);
            this._layout.Orientation = Orientation.Vertical;
            SetContentView(this._layout);

            // TEST
            this.CreateFakeRequest();
        }
    }
}