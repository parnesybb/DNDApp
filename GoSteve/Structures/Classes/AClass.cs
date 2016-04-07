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
        protected Abilities _abilities;
        protected KnownValues.SkillType[] _skillProf;

        public AClass()
        {
            this._abilities = new Abilities();
        }

        public KnownValues.ClassType Type
        {
            get
            {
                return this._classType;
            }
        }

        public KnownValues.SkillType[] Skills
        {
            get
            {
                return this._skillProf;
            }
        }
    }   
}