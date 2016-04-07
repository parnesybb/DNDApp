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

namespace GoSteve
{
    public static class KnownValues
    {
        public static class Races
        {
            public const string DWARF = "Dwarf";
            public const string ELF = "Elf";
            public const string HALFLING = "Halfling";
            public const string HUMAN = "Human";
            public const string DRAGONBORN = "Dragonborn";
            public const string GNOME = "Gnome";
            public const string HALF_ELF = "Half-Elf";
            public const string HALF_ORC = "Half-Orc";
            public const string TIEFLING = "Tiefling";
        }

        public enum Background
        {
            ACOLYTE,
            CHARLATAN,
            CRIMINAL,
            ENTERTAINER,
            FOLK_HERO,
            GUILD_ARTISAN,
            HERMIT,
            NOBLE,
            OUTLANDER,
            SAGE,
            SAILOR,
            SOLDIER,
            URCHIN
        }

        public enum ClassType
        {
            BARBARIAN,
            BARD,
            CLERIC,
            DRUID,
            FIGHTER,
            MONK,
            PALADIN,
            RANGER,
            ROGUE,
            SORCERER,
            WARLOCK,
            WIZARD
        }

        public enum AbilityType
        {
            STRENGTH,
            DEXTERITY,
            CONSTITUTION,
            INTELLIGENCE,
            WISDOM,
            CHARISMA
        }

        public enum SkillType
        {
            Acrobatics,
            Animal_Handling,
            Arcana,
            Athletics,
            Deception,
            History,
            Insight,
            Intimidation,
            Investigation,
            Medicine,
            Nature,
            Perception,
            Performance,
            Persuasion,
            Religion,
            Sleight_Of_Hand,
            Stealth,
            Survival
        }
    }
}