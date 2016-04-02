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
    public class SavingThrows
    {
        private int _str;
        private int _dex;
        private int _con;
        private int _intl;
        private int _wis;
        private int _chrm;

        private bool _strProf;
        private bool _dexProf;
        private bool _conProf;
        private bool _intlProf;
        private bool _wisProf;
        private bool _chrmProf;

        public SavingThrows()
        {
            this.StrengthSavingThrow = 0;
            this.DexteritySavingThrow = 0;
            this.ConstitutionSavingThrow = 0;
            this.IntelligenceSavingThrow = 0;
            this.WisdomSavingThrow = 0;
            this.CharismaSavingThrow = 0;

            this.IsStrProf = false;
            this.IsDexProf = false;
            this.IsConProf = false;
            this.IsIntlProf = false;
            this.IsWisProf = false;
            this.IsChrmProf = false;
        }

        public SavingThrows(KnownValues.ClassType type)
        {
            this.StrengthSavingThrow = 0;
            this.DexteritySavingThrow = 0;
            this.ConstitutionSavingThrow = 0;
            this.IntelligenceSavingThrow = 0;
            this.WisdomSavingThrow = 0;
            this.CharismaSavingThrow = 0;

            this.IsStrProf = false;
            this.IsDexProf = false;
            this.IsConProf = false;
            this.IsIntlProf = false;
            this.IsWisProf = false;
            this.IsChrmProf = false;

            switch (type)
            {
                case KnownValues.ClassType.BARBARIAN:
                    this.IsStrProf = true;
                    this.IsConProf = true;
                    break;
                case KnownValues.ClassType.BARD:
                    this.IsDexProf = true;
                    this.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.CLERIC:
                    this.IsWisProf = true;
                    this.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.DRUID:
                    this.IsIntlProf = true;
                    this.IsWisProf = true;
                    break;
                case KnownValues.ClassType.FIGHTER:
                    this.IsStrProf = true;
                    this.IsConProf = true;
                    break;
                case KnownValues.ClassType.MONK:
                    this.IsStrProf = true;
                    this.IsDexProf = true;
                    break;
                case KnownValues.ClassType.PALADIN:
                    this.IsWisProf = true;
                    this.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.RANGER:
                    this.IsStrProf = true;
                    this.IsDexProf = true;
                    break;
                case KnownValues.ClassType.ROGUE:
                    this.IsDexProf = true;
                    this.IsIntlProf = true;
                    break;
                case KnownValues.ClassType.SORCERER:
                    this.IsConProf = true;
                    this.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.WARLOCK:
                    this.IsWisProf = true;
                    this.IsChrmProf = true;
                    break;
                case KnownValues.ClassType.WIZARD:
                    this.IsIntlProf = true;
                    this.IsWisProf = true;
                    break;
                default:
                    break;
            }
        }

        public int StrengthSavingThrow
        {
            get
            {
                return _str;
            }

            set
            {
                _str = value;
            }
        }

        public int DexteritySavingThrow
        {
            get
            {
                return _dex;
            }

            set
            {
                _dex = value;
            }
        }

        public int ConstitutionSavingThrow
        {
            get
            {
                return _con;
            }

            set
            {
                _con = value;
            }
        }

        public int IntelligenceSavingThrow
        {
            get
            {
                return _intl;
            }

            set
            {
                _intl = value;
            }
        }

        public int WisdomSavingThrow
        {
            get
            {
                return _wis;
            }

            set
            {
                _wis = value;
            }
        }

        public int CharismaSavingThrow
        {
            get
            {
                return _chrm;
            }

            set
            {
                _chrm = value;
            }
        }

        public bool IsStrProf
        {
            get
            {
                return _strProf;
            }

            set
            {
                _strProf = value;
            }
        }

        public bool IsDexProf
        {
            get
            {
                return _dexProf;
            }

            set
            {
                _dexProf = value;
            }
        }

        public bool IsConProf
        {
            get
            {
                return _conProf;
            }

            set
            {
                _conProf = value;
            }
        }

        public bool IsIntlProf
        {
            get
            {
                return _intlProf;
            }

            set
            {
                _intlProf = value;
            }
        }

        public bool IsWisProf
        {
            get
            {
                return _wisProf;
            }

            set
            {
                _wisProf = value;
            }
        }

        public bool IsChrmProf
        {
            get
            {
                return _chrmProf;
            }

            set
            {
                _chrmProf = value;
            }
        }
    }
}