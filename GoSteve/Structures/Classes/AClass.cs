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
    [Serializable]
    public abstract class AClass
    {
        protected KnownValues.ClassType _classType;
        protected HitDice _hitDice;

        public AClass()
        {
            this._hitDice = new HitDice();
        }

        /// <summary>
        /// The type of class.
        /// </summary>
        public KnownValues.ClassType Type
        {
            get
            {
                return this._classType;
            }
        }

        /// <summary>
        /// Gets the classes proficiencies.
        /// </summary>
        /// <returns>Array of proficiences.</returns>
        public abstract string[] GetProficiencies();

        /// <summary>
        /// Gets the classes equipment.
        /// </summary>
        /// <returns>Array of equipment.</returns>
        public abstract string[] GetEquipment();

        /// <summary>
        /// Gets the classes traits.
        /// </summary>
        /// <returns>Array of traits.</returns>
        public abstract string[] GetTraits();

        /// <summary>
        /// Gets the number of hit points for this class at level 1.
        /// </summary>
        /// <param name="modifier">A modifier value. 0 if no modifer.</param>
        /// <returns></returns>
        public abstract int GetLevelOneHitPoints(int modifier);

        /// <summary>
        /// Gets the classes default hit dice.
        /// </summary>
        public HitDice HitDice
        {
            get
            {
                return this._hitDice;
            }
            protected set
            {
                this._hitDice = value;
            }
        }
    }   
}