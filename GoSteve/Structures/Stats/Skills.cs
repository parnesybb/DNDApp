namespace GoSteve.Structures.Classes
{
    public class Skills
    {

        private int _acrobatics;
        private int _anmlHdl;
        private int _arcana;
        private int _athletics;
        private int _deception;
        private int _history;
        private int _insight;
        private int _intimidation;
        private int _investigation;
        private int _medicine;
        private int _nature;
        private int _perception;
        private int _performance;
        private int _persuasion;
        private int _religion;
        private int _slghtHnd;
        private int _stealth;
        private int _survival;

        private bool _isAcrobatics;
        private bool _isAnmlHdl;
        private bool _isArcana;
        private bool _isAthletics;
        private bool _isDeception;
        private bool _isHistory;
        private bool _isInsight;
        private bool _isIntimidation;
        private bool _isInvestigation;
        private bool _isMedicine;
        private bool _isNature;
        private bool _isPerception;
        private bool _isPerformance;
        private bool _isPersuasion;
        private bool _isReligion;
        private bool _isSlghtHnd;
        private bool _isStealth;
        private bool _isSurvival;

        public Skills()
        {
            this.Acrobatics = 0;
            this.AnmlHdl = 0;
            this.Arcana = 0;
            this.Athletics = 0;
            this.Deception = 0;
            this.History = 0;
            this.Insight = 0;
            this.Intimidation = 0;
            this.Investigation = 0;
            this.Medicine = 0;
            this.Nature = 0;
            this.Perception = 0;
            this.Performance = 0;
            this.Persuasion = 0;
            this.Religion = 0;
            this.SlghtHnd = 0;
            this.Stealth = 0;
            this.Survival = 0;

            this.IsAcrobatics = false;
            this.IsAnmlHdl = false;
            this.IsArcana = false;
            this.IsAthletics = false;
            this.IsDeception = false;
            this.IsHistory = false;
            this.IsInsight = false;
            this.IsIntimidation = false;
            this.IsInvestigation = false;
            this.IsMedicine = false;
            this.IsNature = false;
            this.IsPerception = false;
            this.IsPerformance = false;
            this.IsPersuasion = false;
            this.IsReligion = false;
            this.IsSlghtHnd = false;
            this.IsStealth = false;
            this.IsSurvival = false;
        }

        public KnownValues.SkillType SetProficient
        {
            set
            {
                switch (value)
                {
                    case KnownValues.SkillType.Acrobatics:
                        this.IsAcrobatics = true;
                        break;
                    case KnownValues.SkillType.Animal_Handling:
                        this.IsAnmlHdl = true;
                        break;
                    case KnownValues.SkillType.Arcana:
                        this.IsArcana = true;
                        break;
                    case KnownValues.SkillType.Athletics:
                        this.IsAthletics = true;
                        break;
                    case KnownValues.SkillType.Deception:
                        this.IsDeception = true;
                        break;
                    case KnownValues.SkillType.History:
                        this.IsHistory = true;
                        break;
                    case KnownValues.SkillType.Insight:
                        this.IsInsight = true;
                        break;
                    case KnownValues.SkillType.Intimidation:
                        this.IsIntimidation = true;
                        break;
                    case KnownValues.SkillType.Investigation:
                        this.IsInvestigation = true;
                        break;
                    case KnownValues.SkillType.Medicine:
                        this.IsMedicine = true;
                        break;
                    case KnownValues.SkillType.Nature:
                        this.IsNature = true;
                        break;
                    case KnownValues.SkillType.Perception:
                        this.IsPerception = true;
                        break;
                    case KnownValues.SkillType.Performance:
                        this.IsPerformance = true;
                        break;
                    case KnownValues.SkillType.Persuasion:
                        this.IsPersuasion = true;
                        break;
                    case KnownValues.SkillType.Religion:
                        this.IsReligion = true;
                        break;
                    case KnownValues.SkillType.Sleight_Of_Hand:
                        this.IsSlghtHnd = true;
                        break;
                    case KnownValues.SkillType.Stealth:
                        this.IsStealth = true;
                        break;
                    case KnownValues.SkillType.Survival:
                        this.IsSurvival = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public int Acrobatics
        {
            get
            {
                return _acrobatics;
            }

            set
            {
                _acrobatics = value;
            }
        }

        public int AnmlHdl
        {
            get
            {
                return _anmlHdl;
            }

            set
            {
                _anmlHdl = value;
            }
        }

        public int Arcana
        {
            get
            {
                return _arcana;
            }

            set
            {
                _arcana = value;
            }
        }

        public int Athletics
        {
            get
            {
                return _athletics;
            }

            set
            {
                _athletics = value;
            }
        }

        public int Deception
        {
            get
            {
                return _deception;
            }

            set
            {
                _deception = value;
            }
        }

        public int History
        {
            get
            {
                return _history;
            }

            set
            {
                _history = value;
            }
        }

        public int Insight
        {
            get
            {
                return _insight;
            }

            set
            {
                _insight = value;
            }
        }

        public int Intimidation
        {
            get
            {
                return _intimidation;
            }

            set
            {
                _intimidation = value;
            }
        }

        public int Investigation
        {
            get
            {
                return _investigation;
            }

            set
            {
                _investigation = value;
            }
        }

        public int Medicine
        {
            get
            {
                return _medicine;
            }

            set
            {
                _medicine = value;
            }
        }

        public int Nature
        {
            get
            {
                return _nature;
            }

            set
            {
                _nature = value;
            }
        }

        public int Perception
        {
            get
            {
                return _perception;
            }

            set
            {
                _perception = value;
            }
        }

        public int Performance
        {
            get
            {
                return _performance;
            }

            set
            {
                _performance = value;
            }
        }

        public int Persuasion
        {
            get
            {
                return _persuasion;
            }

            set
            {
                _persuasion = value;
            }
        }

        public int Religion
        {
            get
            {
                return _religion;
            }

            set
            {
                _religion = value;
            }
        }

        public int SlghtHnd
        {
            get
            {
                return _slghtHnd;
            }

            set
            {
                _slghtHnd = value;
            }
        }

        public int Stealth
        {
            get
            {
                return _stealth;
            }

            set
            {
                _stealth = value;
            }
        }

        public int Survival
        {
            get
            {
                return _survival;
            }

            set
            {
                _survival = value;
            }
        }

        public bool IsAcrobatics
        {
            get
            {
                return _isAcrobatics;
            }

            set
            {
                _isAcrobatics = value;
            }
        }

        public bool IsAnmlHdl
        {
            get
            {
                return _isAnmlHdl;
            }

            set
            {
                _isAnmlHdl = value;
            }
        }

        public bool IsArcana
        {
            get
            {
                return _isArcana;
            }

            set
            {
                _isArcana = value;
            }
        }

        public bool IsAthletics
        {
            get
            {
                return _isAthletics;
            }

            set
            {
                _isAthletics = value;
            }
        }

        public bool IsDeception
        {
            get
            {
                return _isDeception;
            }

            set
            {
                _isDeception = value;
            }
        }

        public bool IsHistory
        {
            get
            {
                return _isHistory;
            }

            set
            {
                _isHistory = value;
            }
        }

        public bool IsInsight
        {
            get
            {
                return _isInsight;
            }

            set
            {
                _isInsight = value;
            }
        }

        public bool IsIntimidation
        {
            get
            {
                return _isIntimidation;
            }

            set
            {
                _isIntimidation = value;
            }
        }

        public bool IsInvestigation
        {
            get
            {
                return _isInvestigation;
            }

            set
            {
                _isInvestigation = value;
            }
        }

        public bool IsMedicine
        {
            get
            {
                return _isMedicine;
            }

            set
            {
                _isMedicine = value;
            }
        }

        public bool IsNature
        {
            get
            {
                return _isNature;
            }

            set
            {
                _isNature = value;
            }
        }

        public bool IsPerception
        {
            get
            {
                return _isPerception;
            }

            set
            {
                _isPerception = value;
            }
        }

        public bool IsPerformance
        {
            get
            {
                return _isPerformance;
            }

            set
            {
                _isPerformance = value;
            }
        }

        public bool IsPersuasion
        {
            get
            {
                return _isPersuasion;
            }

            set
            {
                _isPersuasion = value;
            }
        }

        public bool IsReligion
        {
            get
            {
                return _isReligion;
            }

            set
            {
                _isReligion = value;
            }
        }

        public bool IsSlghtHnd
        {
            get
            {
                return _isSlghtHnd;
            }

            set
            {
                _isSlghtHnd = value;
            }
        }

        public bool IsStealth
        {
            get
            {
                return _isStealth;
            }

            set
            {
                _isStealth = value;
            }
        }

        public bool IsSurvival
        {
            get
            {
                return _isSurvival;
            }

            set
            {
                _isSurvival = value;
            }
        }
    }
}