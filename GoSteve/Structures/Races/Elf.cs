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
    public class Elf : ARace
    {
        public Elf()
        {
            this._race = KnownValues.Race.ELF;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if (value == KnownValues.SubRace.DROW || value == KnownValues.SubRace.HIGH_ELF || value == KnownValues.SubRace.WOOD_ELF || value == KnownValues.SubRace.WATER_ELF || value == KnownValues.SubRace.ELADRIN)
                {
                    this._subRace = value;

                }

                if (value == KnownValues.SubRace.WOOD_ELF)
                {
                    this._speed = 35;
                }
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Fey Ancestry");
            ret.Add("Trance");

            if (this._subRace == KnownValues.SubRace.DROW)
            {
                ret.Add("Superior Dark Vision - 120ft");
                ret.Add("Sunlight Sensitivity");
                ret.Add("Drow Magic");
            }
            else if (this._subRace == KnownValues.SubRace.HIGH_ELF)
            {
                ret.Add("Dark Vision - 60ft");
            }
            else if (this._subRace == KnownValues.SubRace.WOOD_ELF)
            {
                ret.Add("Dark Vision - 60ft");
                ret.Add("Mask of the Wild");
            }
            else if (this._subRace == KnownValues.SubRace.ELADRIN)
            {
                ret.Add("Elf Weapon Training");
                ret.Add("Fey Step");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Common");
            ret.Add("Elvish");

            if (this._subRace == KnownValues.SubRace.DROW)
            {
                ret.Add("Drow Weapon Training");
            }
            else if (this._subRace == KnownValues.SubRace.HIGH_ELF)
            {
                ret.Add("Elf Weapon Training");
                ret.Add("CHOOSE ONE WIZARD CANTRIP");
                ret.Add("CHOOSE ONE EXTRA LANGUAGE");
            }
            else if (this._subRace == KnownValues.SubRace.WOOD_ELF)
            {
                ret.Add("Elf Weapon Training");
            }

            return ret.ToArray();
        }
    }
}