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
        public static readonly string SMALL_SIZE = "SMALL";
        public static readonly string MEDIUM_SIZE = "MEDIUM";
        public static readonly string LARGE_SIZE = "LARGE";

        protected KnownValues.Race _race;
        protected KnownValues.SubRace _subRace;
        protected int _speed;
        protected string _size;

        public ARace()
        {
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 0;
            this._size = ARace.MEDIUM_SIZE;
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

        /// <summary>
        /// Gets the races size.
        /// </summary>
        public string Size
        {
            get
            {
                return this._size;
            }
        }
    }
}