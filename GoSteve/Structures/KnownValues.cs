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

        public enum Race
        {
            DWARF,
            ELF,
            HALFLING,
            HUMAN,
            DRAGONBORN,
            GNOME,
            HALF_ELF,
            HALF_ORC,
            TIEFLING,
            NONE
        }

        public enum SubRace
        {
            NONE,
            HILL_DWARF,
            MOUNTAIN_DWARF,
            HIGH_ELF,
            WOOD_ELF,
            DARK_ELF,
            LIGHTFOOT_HALFLING,
            STOUT_HALFLING,
            FOREST_GNOME,
            ROCK_GNOME
        }

        public enum Background
        {
            ACOLYTE,
            CHARLATAN,
            CRIMINAL,
            ENTERTAINER,
            FOLK_HERO,
            GUILD_ARTISAN,
            HAUNTED_ONE,
            HERMIT,
            NOBLE,
            NONE,
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
            BLOODHUNTER,
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