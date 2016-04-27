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
        private CharacterSheet _cs;
        private System.Timers.Timer _timer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);

            _cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;

            var name = new TextView(this);
            name.Text = _cs.CharacterName;
            var classType = new TextView(this);
            classType.Text = _cs.ClassInstance.Type.ToString();

            this.ActionBar.Title = _cs.CharacterName + "'s Character Sheet";

            layout.AddView(name);
            layout.AddView(classType);
            SetContentView(layout);

            // Timed writer.
            _timer = new System.Timers.Timer();
            _timer.Interval = 30000; // 1 min = 60000
            _timer.AutoReset = true;
            _timer.Elapsed += TimedSave;
            _timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            
            if (_timer != null)
            {
                _timer.Enabled = false;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (_timer != null)
            {
                _timer.Enabled = true;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _timer.Stop();
            _timer.Dispose();
        }

        private void TimedSave(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_cs != null /*Check for updates bool. Check if it's a dm that opened this bool.*/)
            {
                CharacterSheet.WriteToFile(_cs);
            }
        }
    }
}