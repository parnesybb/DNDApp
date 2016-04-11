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

namespace GoSteve.Structures.Races
{
    [Serializable]
    public abstract class ARace
    {
        protected KnownValues.Race _race;
        protected KnownValues.SubRace _subRace;
        protected int _speed;

        public ARace()
        {
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 0;
        }

        /// <summary>
        /// Gets or sets the SubRace is applicable.
        /// </summary>
        public abstract KnownValues.SubRace SubRace
        {
            get; set;
        }

        /// <summary>
        /// Gets the race proficiencies.
        /// </summary>
        /// <returns>Array of proficiences.</returns>
        public abstract string[] GetProficienciesLanguages();

        /// <summary>
        /// Gets the racial traits.
        /// </summary>
        /// <returns>Array of traits.</returns>
        public abstract string[] GetFeaturesTraits();

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        public KnownValues.Race Race
        {
            get
            {
                return this._race;
            }
            set
            {
                this._race = value;
            }
        }

        /// <summary>
        /// Gets the or sets the speed.
        /// </summary>
        public int Speed
        {
            get
            {
                return this._speed;
            }
            set
            {
                this._speed = value;
            }
        }
    }
}