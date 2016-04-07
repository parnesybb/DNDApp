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
using GoSteve.Structures.Races;

namespace GoSteve.Structures.Classes
{
    public class Abilities
    {
        // Core abilities
        private int _strength;
        private int _dex;
        private int _con;
        private int _intel;
        private int _wisdom;
        private int _charisma;

        // Modifiers
        private int _strengthMod;
        private int _dexMod;
        private int _constMod;
        private int _intelMod;
        private int _wisdomMod;
        private int _charismaMod;

        // Proficiency bonus
        private int _proficiency;

        // Saving throws and skills.
        private SavingThrows _savingThrows;
        private Skills _skills;

        private Dictionary<int, int> _abilityToModifier;
        private static readonly int NUM_TIERS = 16;
        private static readonly int MAX_SCORE = 30;
        private static readonly int MOD_LOW = -5;
        private static readonly int STARTING_SCORE = 8;
        private static readonly int LEVEL_ONE_PROF = 2;

        public Abilities()
        {
            this._savingThrows = new SavingThrows();
            this._skills = new Skills();

            this.InitToDefaults();
        }

        public void SetRace(ARace r)
        {
            switch (r.Race)
            {
                case KnownValues.Race.DWARF:
                    this.Con += 2;

                    if (r.SubRace == KnownValues.SubRace.MOUNTAIN_DWARF)
                    {
                        this.Strength += 2;
                    }
                    else if (r.SubRace == KnownValues.SubRace.HILL_DWARF)
                    {
                        this.Wisdom += 1;
                    }
                    break;

                case KnownValues.Race.ELF:
                    this.Dex += 2;
                    this._skills.SetProficient = KnownValues.SkillType.Perception;

                    if (r.SubRace == KnownValues.SubRace.HIGH_ELF)
                    {
                        this.Intel += 1;
                    }
                    else if (r.SubRace == KnownValues.SubRace.WOOD_ELF)
                    {
                        this.Wisdom += 1;
                    }
                    else if (r.SubRace == KnownValues.SubRace.DARK_ELF)
                    {
                        this.Charisma += 1;
                    }
                    break;

                case KnownValues.Race.HALFLING:
                    this.Dex += 2;

                    if (r.SubRace == KnownValues.SubRace.LIGHTFOOT_HALFLING)
                    {
                        this.Charisma += 1;
                    }
                    else if (r.SubRace == KnownValues.SubRace.STOUT_HALFLING)
                    {
                        this.Con += 1;
                    }
                    break;

                case KnownValues.Race.HUMAN:
                    this.Strength += 1;
                    this.Dex += 1;
                    this.Con += 1;
                    this.Intel += 1;
                    this.Wisdom += 1;
                    this.Charisma += 1;
                    break;

                case KnownValues.Race.DRAGONBORN:
                    this.Strength += 2;
                    this.Charisma += 1;
                    break;

                case KnownValues.Race.GNOME:
                    this.Intel += 2;

                    if (r.SubRace == KnownValues.SubRace.FOREST_GNOME)
                    {
                        this.Dex += 1;
                    }
                    else if (r.SubRace == KnownValues.SubRace.ROCK_GNOME)
                    {
                        this.Con += 1;
                    }
                    break;

                case KnownValues.Race.HALF_ELF:
                    this.Charisma += 2;
                    break;

                case KnownValues.Race.HALF_ORC:
                    this.Strength += 2;
                    this.Con += 1;
                    break;

                case KnownValues.Race.TIEFLING:
                    this.Intel += 1;
                    this.Charisma += 2;
                    break;

                default:
                    break;
            }
        }

        public void SetClass(KnownValues.ClassType c)
        {
            this._savingThrows.IsStrProf = false;
            this._savingThrows.IsDexProf = false;
            this._savingThrows.IsConProf = false;
            this._savingThrows.IsIntlProf = false;
            this._savingThrows.IsWisProf = false;
            this._savingThrows.IsChrmProf = false;

            switch (c)
            {
                case KnownValues.ClassType.BARBARIAN:
                    this._savingThrows.IsStrProf = true;
                    this._savingThrows.IsConProf = true;
                    break;
                case KnownValues.ClassType.BARD:
                    this._savingThrows.IsDexProf = true;
                    this._savingThrows.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.CLERIC:
                    this._savingThrows.IsWisProf = true;
                    this._savingThrows.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.DRUID:
                    this._savingThrows.IsIntlProf = true;
                    this._savingThrows.IsWisProf = true;
                    break;
                case KnownValues.ClassType.FIGHTER:
                    this._savingThrows.IsStrProf = true;
                    this._savingThrows.IsConProf = true;
                    break;
                case KnownValues.ClassType.MONK:
                    this._savingThrows.IsStrProf = true;
                    this._savingThrows.IsDexProf = true;
                    break;
                case KnownValues.ClassType.PALADIN:
                    this._savingThrows.IsWisProf = true;
                    this._savingThrows.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.RANGER:
                    this._savingThrows.IsStrProf = true;
                    this._savingThrows.IsDexProf = true;
                    break;
                case KnownValues.ClassType.ROGUE:
                    this._savingThrows.IsDexProf = true;
                    this._savingThrows.IsIntlProf = true;
                    break;
                case KnownValues.ClassType.SORCERER:
                    this._savingThrows.IsConProf = true;
                    this._savingThrows.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.WARLOCK:
                    this._savingThrows.IsWisProf = true;
                    this._savingThrows.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.WIZARD:
                    this._savingThrows.IsIntlProf = true;
                    this._savingThrows.IsWisProf = true;
                    break;
                default:
                    break;
            }

            this.ProfciencyUpdate();
        }

        public void SetSkills(KnownValues.SkillType[] skills)
        {
            foreach (var skill in skills)
            {
                this._skills.SetProficient = skill;
            }

            this.ProfciencyUpdate();
        }

        public void SetBackground(KnownValues.Background bg)
        {
            switch (bg)
            {
                case KnownValues.Background.ACOLYTE:
                    this._skills.SetProficient = KnownValues.SkillType.Insight;
                    this._skills.SetProficient = KnownValues.SkillType.Religion;
                    break;
                case KnownValues.Background.CHARLATAN:
                    this._skills.SetProficient = KnownValues.SkillType.Deception;
                    this._skills.SetProficient = KnownValues.SkillType.Sleight_Of_Hand;
                    break;
                case KnownValues.Background.CRIMINAL:
                    this._skills.SetProficient = KnownValues.SkillType.Deception;
                    this._skills.SetProficient = KnownValues.SkillType.Stealth;
                    break;
                case KnownValues.Background.ENTERTAINER:
                    this._skills.SetProficient = KnownValues.SkillType.Acrobatics;
                    this._skills.SetProficient = KnownValues.SkillType.Performance;
                    break;
                case KnownValues.Background.FOLK_HERO:
                    this._skills.SetProficient = KnownValues.SkillType.Animal_Handling;
                    this._skills.SetProficient = KnownValues.SkillType.Survival;
                    break;
                case KnownValues.Background.GUILD_ARTISAN:
                    this._skills.SetProficient = KnownValues.SkillType.Insight;
                    this._skills.SetProficient = KnownValues.SkillType.Persuasion;
                    break;
                case KnownValues.Background.HERMIT:
                    this._skills.SetProficient = KnownValues.SkillType.Medicine;
                    this._skills.SetProficient = KnownValues.SkillType.Religion;
                    break;
                case KnownValues.Background.NOBLE:
                    this._skills.SetProficient = KnownValues.SkillType.History;
                    this._skills.SetProficient = KnownValues.SkillType.Persuasion;
                    break;
                case KnownValues.Background.OUTLANDER:
                    this._skills.SetProficient = KnownValues.SkillType.Athletics;
                    this._skills.SetProficient = KnownValues.SkillType.Survival;
                    break;
                case KnownValues.Background.SAGE:
                    this._skills.SetProficient = KnownValues.SkillType.Arcana;
                    this._skills.SetProficient = KnownValues.SkillType.History;
                    break;
                case KnownValues.Background.SAILOR:
                    this._skills.SetProficient = KnownValues.SkillType.Athletics;
                    this._skills.SetProficient = KnownValues.SkillType.Perception;
                    break;
                case KnownValues.Background.SOLDIER:
                    this._skills.SetProficient = KnownValues.SkillType.Athletics;
                    this._skills.SetProficient = KnownValues.SkillType.Intimidation;
                    break;
                case KnownValues.Background.URCHIN:
                    this._skills.SetProficient = KnownValues.SkillType.Sleight_Of_Hand;
                    this._skills.SetProficient = KnownValues.SkillType.Stealth;
                    break;
                default:
                    break;
            }

            this.ProfciencyUpdate();
        }

        public int Strength
        { 
            get
            {
                return _strength;
            }

            set
            {
                _strength = value;

                this.StrengthMod = this._abilityToModifier[this.Strength];
            }
        }

        public int Dex
        {
            get
            {
                return _dex;
            }

            set
            {
                _dex = value;
                this.DexMod = this._abilityToModifier[this.Dex];
            }
        }

        public int Con
        {
            get
            {
                return _con;
            }

            set
            {
                _con = value;
                this.ConstMod = this._abilityToModifier[this.Con];
            }
        }

        public int Intel
        {
            get
            {
                return _intel;
            }

            set
            {
                _intel = value;
                this.IntelMod = this._abilityToModifier[this.Intel];
            }
        }

        public int Wisdom
        {
            get
            {
                return _wisdom;
            }

            set
            {
                _wisdom = value;
                this.WisdomMod = this._abilityToModifier[this.Wisdom];
            }
        }

        public int Charisma
        {
            get
            {
                return _charisma;
            }

            set
            {
                _charisma = value;
                this.CharismaMod = this._abilityToModifier[this.Charisma];
            }
        }

        public int StrengthMod
        {
            get
            {
                return _strengthMod;
            }

            set
            {
                _strengthMod = value;

                if (this._savingThrows.IsStrProf)
                {
                    this._savingThrows.StrengthSavingThrow = this.StrengthMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.StrengthSavingThrow = this.StrengthMod;
                }

                if (this._skills.IsAthletics)
                {
                    this._skills.Athletics = this.StrengthMod + this.Proficiency;
                }
                else
                {
                    this._skills.Athletics = this.StrengthMod;
                }
            }
        }

        public int DexMod
        {
            get
            {
                return _dexMod;
            }

            set
            {
                _dexMod = value;

                if (this._savingThrows.IsDexProf)
                {
                    this._savingThrows.DexteritySavingThrow = this.DexMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.DexteritySavingThrow = this.DexMod;
                }


                if (this._skills.IsAcrobatics)
                {
                    this._skills.Acrobatics = this.DexMod + this.Proficiency;
                }
                else
                {
                    this._skills.Acrobatics = this.DexMod;
                }

                if (this._skills.IsSlghtHnd)
                {
                    this._skills.SlghtHnd = this.DexMod + this.Proficiency;
                }
                else
                {
                    this._skills.SlghtHnd = this.DexMod;
                }

                if (this._skills.IsStealth)
                {
                    this._skills.Stealth = this.DexMod + this.Proficiency;
                }
                else
                {
                    this._skills.Stealth = this.DexMod;
                }
            }
        }

        public int ConstMod
        {
            get
            {
                return _constMod;
            }

            set
            {
                _constMod = value;

                if (this._savingThrows.IsConProf)
                {
                    this._savingThrows.ConstitutionSavingThrow = this.ConstMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.ConstitutionSavingThrow = this.ConstMod;
                }
            }
        }

        public int IntelMod
        {
            get
            {
                return _intelMod;
            }

            set
            {
                _intelMod = value;

                if (this._savingThrows.IsIntlProf)
                {
                    this._savingThrows.IntelligenceSavingThrow = this.IntelMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.IntelligenceSavingThrow = this.IntelMod;
                }


                if (this._skills.IsArcana)
                    this._skills.Arcana = this.IntelMod + this.Proficiency;
                else
                    this._skills.Arcana = this.IntelMod;

                if (this._skills.IsHistory)
                    this._skills.History = this.IntelMod + this.Proficiency;
                else
                    this._skills.History = this.IntelMod;

                if (this._skills.IsInvestigation)
                    this._skills.Investigation = this.IntelMod + this.Proficiency;
                else
                    this._skills.Investigation = this.IntelMod;

                if (this._skills.IsNature)
                    this._skills.Nature = this.IntelMod + this.Proficiency;
                else
                    this._skills.Nature = this.IntelMod;

                if (this._skills.IsReligion)
                    this._skills.Religion = this.IntelMod + this.Proficiency;
                else
                    this._skills.Religion = this.IntelMod;
            }
        }

        public int WisdomMod
        {
            get
            {
                return _wisdomMod;
            }

            set
            {
                _wisdomMod = value;

                if (this._savingThrows.IsWisProf)
                {
                    this._savingThrows.WisdomSavingThrow = this.WisdomMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.WisdomSavingThrow = this.WisdomMod;
                }

                if (this._skills.IsAnmlHdl)
                    this._skills.AnmlHdl = this.WisdomMod + this.Proficiency;
                else
                    this._skills.AnmlHdl = this.WisdomMod;

                if (this._skills.IsInsight)
                    this._skills.Insight = this.WisdomMod + this.Proficiency;
                else
                    this._skills.Insight = this.WisdomMod;

                if (this._skills.IsMedicine)
                    this._skills.Medicine = this.WisdomMod + this.Proficiency;
                else
                    this._skills.Medicine = this.WisdomMod;

                if (this._skills.IsPerception)
                    this._skills.Perception = this.WisdomMod + this.Proficiency;
                else
                    this._skills.Perception = this.WisdomMod;

                if (this._skills.IsSurvival)
                    this._skills.Survival = this.WisdomMod + this.Proficiency;
                else
                    this._skills.Survival = this.WisdomMod;
            }
        }

        public int CharismaMod
        {
            get
            {
                return _charismaMod;
            }

            set
            {
                _charismaMod = value;

                if (this._savingThrows.IsChrmProf)
                {
                    this._savingThrows.CharismaSavingThrow = this.CharismaMod + this.Proficiency;
                }
                else
                {
                    this._savingThrows.CharismaSavingThrow = this.CharismaMod;
                }


                if (this._skills.IsDeception)
                    this._skills.Deception = this.CharismaMod + this.Proficiency;
                else
                    this._skills.Deception = this.CharismaMod;

                if (this._skills.IsIntimidation)
                    this._skills.Intimidation = this.CharismaMod + this.Proficiency;
                else
                    this._skills.Intimidation = this.CharismaMod;

                if (this._skills.IsPerformance)
                    this._skills.Performance = this.CharismaMod + this.Proficiency;
                else
                    this._skills.Performance = this.CharismaMod;

                if (this._skills.IsPersuasion)
                    this._skills.Persuasion = this.CharismaMod + this.Proficiency;
                else
                    this._skills.Persuasion = this.CharismaMod;
            }
        }

        public int Proficiency
        {
            get
            {
                return _proficiency;
            }

            set
            {
                _proficiency = value;
            }
        }

        public int StrengthSavingThrow
        {
            get
            {
                return this._savingThrows.StrengthSavingThrow;
            }
        }

        public int DexteritySavingThrow
        {
            get
            {
                return this._savingThrows.DexteritySavingThrow;
            }
        }

        public int ConstitutionSavingThrow
        {
            get
            {
                return this._savingThrows.ConstitutionSavingThrow;
            }
        }

        public int IntelligenceSavingThrow
        {
            get
            {
                return this._savingThrows.IntelligenceSavingThrow;
            }

        }

        public int WisdomSavingThrow
        {
            get
            {
                return this._savingThrows.WisdomSavingThrow;
            }
        }

        public int CharismaSavingThrow
        {
            get
            {
                return this._savingThrows.CharismaSavingThrow;
            }

        }

        public int Acrobatics
        {
            get
            {
                return this._skills.Acrobatics;
            }
        }

        public int AnimalHandling
        {
            get
            {
                return this._skills.AnmlHdl;
            }
        }

        public int Arcana
        {
            get
            {
                return this._skills.Arcana;
            }
        }

        public int Athletics
        {
            get
            {
                return this._skills.Athletics;
            }
        }

        public int Deception
        {
            get
            {
                return this._skills.Deception;
            }
        }

        public int History
        {
            get
            {
                return this._skills.History;
            }
        }

        public int Insight
        {
            get
            {
                return this._skills.Insight;
            }
        }

        public int Intimidation
        {
            get
            {
                return this._skills.Intimidation;
            }
        }

        public int Investigation
        {
            get
            {
                return this._skills.Investigation;
            }
        }

        public int Medicine
        {
            get
            {
                return this._skills.Medicine;
            }
        }

        public int Nature
        {
            get
            {
                return this._skills.Nature;
            }
        }

        public int Perception
        {
            get
            {
                return this._skills.Perception;
            }
        }

        public int Performance
        {
            get
            {
                return this._skills.Performance;
            }
        }

        public int Persuasion
        {
            get
            {
                return this._skills.Persuasion;
            }
        }

        public int Religion
        {
            get
            {
                return this._skills.Religion;
            }
        }

        public int SlightOfHand
        {
            get
            {
                return this._skills.SlghtHnd;
            }
        }

        public int Stealth
        {
            get
            {
                return this._skills.Stealth;
            }
        }

        public int Survival
        {
            get
            {
                return this._skills.Survival;
            }
        }

        private void ProfciencyUpdate()
        {
            this.Strength = this.Strength;
            this.Dex = this.Dex;
            this.Con = this.Con;
            this.Intel = this.Intel;
            this.Wisdom = this.Wisdom;
            this.Charisma = this.Charisma;
        }

        private void InitToDefaults()
        {
            // Generate our dictionary to convert ability to modifier.
            this._abilityToModifier = new Dictionary<int, int>();
            int score = Abilities.MOD_LOW;
            int count = 1;
            for (int i = 0; i < Abilities.NUM_TIERS; i++)
            {
                if (count == 1 || count == Abilities.MAX_SCORE)
                {
                    this._abilityToModifier.Add(count, score);
                    count++;
                }
                else
                {
                    this._abilityToModifier.Add(count, score);
                    this._abilityToModifier.Add(count + 1, score);
                    count += 2;
                }

                score++;
            }
            this.Proficiency = Abilities.LEVEL_ONE_PROF;
            this.Strength = Abilities.STARTING_SCORE;
            this.Dex = Abilities.STARTING_SCORE;
            this.Con = Abilities.STARTING_SCORE;
            this.Intel = Abilities.STARTING_SCORE;
            this.Wisdom = Abilities.STARTING_SCORE;
            this.Charisma = Abilities.STARTING_SCORE;      
        }
    }
}