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
    public class Gnome : ARace
    {
        public Gnome()
        {
            this._race = KnownValues.Race.GNOME;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 25;
            this._size = ARace.SMALL_SIZE;
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if (value == KnownValues.SubRace.ROCK_GNOME || value == KnownValues.SubRace.FOREST_GNOME)
                {
                    this._subRace = value;
                }
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Gnome Cunning");
            ret.Add("Dark Vision - 60ft");

            if (this._subRace == KnownValues.SubRace.FOREST_GNOME)
            {
                ret.Add("Natural Illusionist");
                ret.Add("Speak with Small Beasts");
            }
            else if (this._subRace == KnownValues.SubRace.ROCK_GNOME)
            {
                ret.Add("Artificer's Lore");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Common");
            ret.Add("Gnomish");

            if (this._subRace == KnownValues.SubRace.ROCK_GNOME)
            {
                ret.Add("Tinker");
            }

            return ret.ToArray();
        }
    }
}