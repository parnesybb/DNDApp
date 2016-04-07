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
    class Barbarian : AClass
    {
        
        public Barbarian(KnownValues.SkillType[] skills)
        {
            this._classType = KnownValues.ClassType.BARBARIAN;
            this._skillProf = skills;
        }
    }
}