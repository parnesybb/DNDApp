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

namespace GoSteve.Structures.Classes
{
    public abstract class AClass
    {
        protected KnownValues.ClassType _classType;
        protected List<string> _profs;
        protected List<string> _equip;
        protected List<string> _traits;
        protected HitDice _hitDice;

        public AClass()
        {
            this._profs = new List<string>();
            this._equip = new List<string>();
            this._traits = new List<string>();
            this._hitDice = new HitDice();
        }

        public KnownValues.ClassType Type
        {
            get
            {
                return this._classType;
            }
        }

        public string[] Proficiencies
        {
            get
            {
                return this._profs.ToArray();
            }
        }

        public string[] Equipment
        {
            get
            {
                return this._equip.ToArray();
            }
        }

        public string[] Traits
        {
            get
            {
                return this._traits.ToArray();
            }
        }

        public HitDice HitDice
        {
            get
            {
                return this._hitDice;
            }
        }
    }   
}