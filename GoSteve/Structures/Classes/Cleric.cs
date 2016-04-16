using System;
using System.Collections.Generic;

namespace GoSteve.Structures.Classes
{
    [Serializable]
    public class Cleric : AClass
    {
        public Cleric()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a mace or (b)a warhammer");
            ret.Add("(a)scale mail, (b)leather armor, or(c) chain mail");
            ret.Add("(a)a light erossbow and 20 bolts or (b)any simple weapon");
            ret.Add("(a) a priest's paek or (b) an explorer's paek");
            ret.Add("A Shield");
            ret.Add("Holy Symbol");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 8 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light Armor");
            ret.Add("Medium Armor");
            ret.Add("Shields");
            ret.Add("Simple Weapons");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Spellcasting");
            ret.Add("Divine Domain");

            return ret.ToArray();
        }
    }
}