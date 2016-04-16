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

namespace GoSteve.Structures.Spells
{
    [Serializable]
    public class Spell
    {
        public Spell()
        {
        }

        /// <summary>
        /// Spell name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}