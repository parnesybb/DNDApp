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

namespace GoSteve.Structures.Weapons
{
    [Serializable]
    public class Weapon
    {
        public Weapon()
        {
        }

        /// <summary>
        /// Weapon name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Weapon damage type.
        /// </summary>
        public string DamageType
        {
            get;
            set;
        }

        /// <summary>
        /// Weapon attack bonus.
        /// </summary>
        public int AtkBonus
        {
            get;
            set;
        }
    }
}