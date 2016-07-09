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
            AARAKOCRA,
            AASIMAR,
            DWARF,
            ELF,
            HALFLING,
            HUMAN,
            DRAGONBORN,
            GENASI,
            GOLIATH,
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
            DUERGAR,
            HIGH_ELF,
            WOOD_ELF,
            DROW,
            ORIGINAL,
            WATER_ELF,
            LIGHTFOOT_HALFLING,
            STOUT_HALFLING,
            FOREST_GNOME,
            ROCK_GNOME,
            DEEP_GNOME,
            AIR_GENASI,
            EARTH_GENASI,
            FIRE_GENASI,
            WATER_GENASI,
            FERAL,
            ELADRIN
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
            URCHIN,
            CITY_WATCH,
            CLAN_CRAFTER,
            CLOISTERED_SCHOLAR,
            COURTIER,
            FACTION_AGENT,
            FAR_TRAVELER,
            INHERITOR,
            KNIGHT_OF_THE_ORDER,
            MERCENARY_VETERAN,
            URBAN_BOUNTY_HUNTER,
            UTHGARDT_TRIBE_MEMBER,
            WATERDHAVIAN_NOBLE

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