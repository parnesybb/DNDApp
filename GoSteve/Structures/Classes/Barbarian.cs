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
    class Barbarian : AClass
    {
        
        public Barbarian()
        {
            this._classType = KnownValues.ClassType.BARBARIAN;
        }

        public override string[] GetEquipment()
        {
            throw new NotImplementedException();
        }

        public override string[] GetProficiencies()
        {
            throw new NotImplementedException();
        }

        public override string[] GetTraits()
        {
            throw new NotImplementedException();
        }
    }
}