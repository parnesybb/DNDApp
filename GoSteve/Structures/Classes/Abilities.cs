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
    public class Abilities
    {
        // Core abilities
        private int _strength;
        private int _dex;
        private int _const;
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

        public Abilities(KnownValues.ClassType type, KnownValues.SkillType[] skills)
        {
            this._savingThrows = new SavingThrows(type);
            this._skills = new Skills();
            
            foreach (var skill in skills)
            {
                this._skills.SetProficient = skill;
            }

            this.InitToDefaults();
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

        public int Const
        {
            get
            {
                return _const;
            }

            set
            {
                _const = value;
                this.ConstMod = this._abilityToModifier[this.Const];
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
                    this._skills.Athletics = this.StrengthMod = this.Proficiency;
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
            this.Const = Abilities.STARTING_SCORE;
            this.Intel = Abilities.STARTING_SCORE;
            this.Wisdom = Abilities.STARTING_SCORE;
            this.Charisma = Abilities.STARTING_SCORE;      
        }
    }
}