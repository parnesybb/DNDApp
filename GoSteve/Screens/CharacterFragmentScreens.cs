using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

/*
* These classes hold the fragments to display for each tab
* in the CharacterScreen.cs file.
*/

namespace GoSteve.Screens
{
    public class StatsSkillsFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterStatsSkillsScreen, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                // BASE STATS
                var input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsStrength);
                input.Text = _cs.AbilitiesAndStats.Strength.ToString();
                input.TextChanged += Input_TextChanged;

                input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsDexterity);
                input.Text = _cs.AbilitiesAndStats.Dex.ToString();
                input.TextChanged += Input_TextChanged;

                input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsCon);
                input.Text = _cs.AbilitiesAndStats.Con.ToString();
                input.TextChanged += Input_TextChanged;

                input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsInt);
                input.Text = _cs.AbilitiesAndStats.Intel.ToString();
                input.TextChanged += Input_TextChanged;

                input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsWis);
                input.Text = _cs.AbilitiesAndStats.Wisdom.ToString();
                input.TextChanged += Input_TextChanged;

                input = _view.FindViewById<EditText>(Resource.Id.StatsSkillsChar);
                input.Text = _cs.AbilitiesAndStats.Charisma.ToString();
                input.TextChanged += Input_TextChanged;

                // MODS
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsStrengthMod).Text = _cs.AbilitiesAndStats.StrengthMod.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsDexterityMod).Text = _cs.AbilitiesAndStats.DexMod.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsConMod).Text = _cs.AbilitiesAndStats.ConstMod.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntMod).Text = _cs.AbilitiesAndStats.IntelMod.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsCharMod).Text = _cs.AbilitiesAndStats.CharismaMod.ToString();

                // SAVING THROWS
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_s_saving).Text = _cs.AbilitiesAndStats.StrengthSavingThrow.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_d_saving).Text = _cs.AbilitiesAndStats.DexteritySavingThrow.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_con_saving).Text = _cs.AbilitiesAndStats.ConstitutionSavingThrow.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_i_saving).Text = _cs.AbilitiesAndStats.IntelligenceSavingThrow.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_w_saving).Text = _cs.AbilitiesAndStats.WisdomSavingThrow.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkills_char_saving).Text = _cs.AbilitiesAndStats.CharismaSavingThrow.ToString();

                // SKILLS
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsAcrobatics).Text = _cs.AbilitiesAndStats.Acrobatics.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsAnimalHdnl).Text = _cs.AbilitiesAndStats.AnimalHandling.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsArcana).Text = _cs.AbilitiesAndStats.Arcana.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsAthletics).Text = _cs.AbilitiesAndStats.Athletics.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsDeception).Text = _cs.AbilitiesAndStats.Deception.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsHistory).Text = _cs.AbilitiesAndStats.History.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsInsight).Text = _cs.AbilitiesAndStats.Insight.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntimidation).Text = _cs.AbilitiesAndStats.Intimidation.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsInvestigation).Text = _cs.AbilitiesAndStats.Investigation.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsMedicine).Text = _cs.AbilitiesAndStats.Medicine.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsNature).Text = _cs.AbilitiesAndStats.Nature.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsPerception).Text = _cs.AbilitiesAndStats.Perception.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsPerformance).Text = _cs.AbilitiesAndStats.Performance.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsPersuasion).Text = _cs.AbilitiesAndStats.Persuasion.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsReligion).Text = _cs.AbilitiesAndStats.Religion.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsSlghtHnd).Text = _cs.AbilitiesAndStats.SlightOfHand.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsStealth).Text = _cs.AbilitiesAndStats.Stealth.ToString();
                _view.FindViewById<TextView>(Resource.Id.StatsSkillsSurvival).Text = _cs.AbilitiesAndStats.Survival.ToString();
            }

            return _view;
        }

        private void Input_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var s = sender as EditText;

            try
            {
                switch (s.Id)
                {
                    case Resource.Id.StatsSkillsStrength:
                        _cs.AbilitiesAndStats.Strength = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsStrengthMod).Text = _cs.AbilitiesAndStats.StrengthMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_s_saving).Text = _cs.AbilitiesAndStats.StrengthSavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsAthletics).Text = _cs.AbilitiesAndStats.Athletics.ToString();
                        break;
                    case Resource.Id.StatsSkillsDexterity:
                        _cs.AbilitiesAndStats.Dex = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsDexterityMod).Text = _cs.AbilitiesAndStats.DexMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_d_saving).Text = _cs.AbilitiesAndStats.DexteritySavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsAcrobatics).Text = _cs.AbilitiesAndStats.Acrobatics.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsSlghtHnd).Text = _cs.AbilitiesAndStats.SlightOfHand.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsStealth).Text = _cs.AbilitiesAndStats.Stealth.ToString();
                        break;
                    case Resource.Id.StatsSkillsCon:
                        _cs.AbilitiesAndStats.Con = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsConMod).Text = _cs.AbilitiesAndStats.ConstMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_con_saving).Text = _cs.AbilitiesAndStats.ConstitutionSavingThrow.ToString();
                        break;
                    case Resource.Id.StatsSkillsInt:
                        _cs.AbilitiesAndStats.Intel = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntMod).Text = _cs.AbilitiesAndStats.IntelMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_i_saving).Text = _cs.AbilitiesAndStats.IntelligenceSavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsArcana).Text = _cs.AbilitiesAndStats.Arcana.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsHistory).Text = _cs.AbilitiesAndStats.History.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsInvestigation).Text = _cs.AbilitiesAndStats.Investigation.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsNature).Text = _cs.AbilitiesAndStats.Nature.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsReligion).Text = _cs.AbilitiesAndStats.Religion.ToString();
                        break;
                    case Resource.Id.StatsSkillsWis:
                        _cs.AbilitiesAndStats.Wisdom = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_w_saving).Text = _cs.AbilitiesAndStats.WisdomSavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsAnimalHdnl).Text = _cs.AbilitiesAndStats.AnimalHandling.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsInsight).Text = _cs.AbilitiesAndStats.Insight.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsMedicine).Text = _cs.AbilitiesAndStats.Medicine.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsPerception).Text = _cs.AbilitiesAndStats.Perception.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsSurvival).Text = _cs.AbilitiesAndStats.Survival.ToString();
                        break;
                    case Resource.Id.StatsSkillsChar:
                        _cs.AbilitiesAndStats.Charisma = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsCharMod).Text = _cs.AbilitiesAndStats.CharismaMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_char_saving).Text = _cs.AbilitiesAndStats.CharismaSavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsDeception).Text = _cs.AbilitiesAndStats.Deception.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntimidation).Text = _cs.AbilitiesAndStats.Intimidation.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsPerformance).Text = _cs.AbilitiesAndStats.Performance.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsPersuasion).Text = _cs.AbilitiesAndStats.Persuasion.ToString();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            { }   
        }
    }

    public class AbilitiesFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //throw new NotImplementedException();
            _view = inflater.Inflate(Resource.Layout.CharacterAbilitiesScreen, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                // AC INIT SPEED
                var input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesArmorClass);
                input.Text = _cs.ArmorClass.ToString();
                input.TextChanged += Input_Changed;
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesInitiative);
                input.Text = _cs.Initiative.ToString();
                input.TextChanged += Input_Changed;
                _view.FindViewById<TextView>(Resource.Id.CharacterAbilitiesSpeed).Text = _cs.RaceInstance.Speed.ToString();

                // HIT POINTS
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesHitPntMax);
                input.Text = _cs.HitPoints.Max.ToString();
                input.TextChanged += Input_Changed;
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesHitPntCrnt);
                input.Text = _cs.HitPoints.Current.ToString();
                input.TextChanged += Input_Changed;
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesHitPntTmp);
                input.Text = _cs.HitPoints.Temp.ToString();
                input.TextChanged += Input_Changed;

                // HIT DICE
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesHitDiceMax);
                input.Text = _cs.ClassInstance.HitDice.TotalAmount.ToString();
                input.TextChanged += Input_Changed;
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesHitDiceCurrent);
                input.Text = _cs.ClassInstance.HitDice.AvailableAmount.ToString();
                input.TextChanged += Input_Changed;
                _view.FindViewById<TextView>(Resource.Id.CharacterAbilitiesHitDiceType).Text = "d" + _cs.ClassInstance.HitDice.NumberOfSides.ToString();

                // ATTACKS & SPELLS
                input = _view.FindViewById<EditText>(Resource.Id.CharacterAbilitiesAttkSpell);
                foreach (var s in _cs.Weapons)
                {
                    input.Text += s + "\n";
                }

                input.FocusChange += (e, s) =>
                {
                    var sender = e as EditText;

                    if (!s.HasFocus)
                    {
                        var text = sender.Text;
                        string[] lines = text.Split('\n');
                        // remove previous weapons.
                        _cs.ClearWeapons();

                        // add new weapons and prev weapons.
                        foreach (var l in lines)
                        {
                            _cs.AddWeapon(l);
                        }
                    }
                };
            }

            return _view;
        }

        private void Input_Changed(object sender, Android.Text.TextChangedEventArgs e)
        {
            var s = sender as EditText;

            switch (s.Id)
            {
                case Resource.Id.CharacterAbilitiesArmorClass:
                    _cs.ArmorClass = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesAttkSpell:
                    break;
                case Resource.Id.CharacterAbilitiesHitDiceCurrent:
                    _cs.ClassInstance.HitDice.AvailableAmount = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesHitDiceMax:
                    _cs.ClassInstance.HitDice.TotalAmount = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesHitPntCrnt:
                    _cs.HitPoints.Current = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesHitPntMax:
                    _cs.HitPoints.Max = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesHitPntTmp:
                    _cs.HitPoints.Temp = Int32.Parse(s.Text);
                    break;
                case Resource.Id.CharacterAbilitiesInitiative:
                    _cs.Initiative = Int32.Parse(s.Text);
                    break;

                default:
                    break;
            }
        }
    }

    public class ProfsLangsFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterScreenProfsLangs, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                var input = _view.FindViewById<EditText>(Resource.Id.characterScreenProfsLangsseditText);

                foreach (var s in _cs.OtherProficienciesLanguages)
                {
                    input.Text += s + "\n";
                }

                input.FocusChange += (s, e) =>
                {
                    if (!e.HasFocus)
                    {
                        var sender = s as EditText;
                        var text = sender.Text;
                        string[] lines = text.Split('\n');

                        // clear prev values.
                        _cs.ClearProficienciesLanguages();

                        // readd old values and new values.
                        foreach (var l in lines)
                        {
                            _cs.AddProficiencyORLanguage(l);
                        }
                    }
                };  
            }

            return _view;
        }
    }

    public class FeaturesTraitsFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterScreenFeaturesTraits, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                var input = _view.FindViewById<EditText>(Resource.Id.characterScreenFeaturesTraitsEditText);

                foreach (var s in _cs.FeaturesAndTraits)
                {
                    input.Text += s + "\n";
                }

                input.FocusChange += (s, e) =>
                {
                    if (!e.HasFocus)
                    {
                        var sender = s as EditText;
                        var text = sender.Text;
                        string[] lines = text.Split('\n');

                        // clear prev values.
                        _cs.ClearFeaturesAndTraits();

                        // readd old values and new values.
                        foreach (var l in lines)
                        {
                            _cs.AddFeatureOrTrait(l);
                        }
                    }
                };
            }

            return _view;
        }
    }

    public class EquipFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterScreenEquipment, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                // BUY
                var btn = _view.FindViewById<Button>(Resource.Id.equipmentBuyBtn);
                btn.Click += (s, e) =>
                {
                    var money = _view.FindViewById<EditText>(Resource.Id.equipmentCP);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var cost = Int32.Parse(money.Text);
                        _cs.Currency.Buy(cost, Structures.Currency.CurrencyType.cp);
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentSP);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var cost = Int32.Parse(money.Text);
                        _cs.Currency.Buy(cost, Structures.Currency.CurrencyType.sp);
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentGP);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var cost = Int32.Parse(money.Text);
                        _cs.Currency.Buy(cost, Structures.Currency.CurrencyType.gp);
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentPP);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var cost = Int32.Parse(money.Text);
                        _cs.Currency.Buy(cost, Structures.Currency.CurrencyType.pp);
                    }

                    UpdateMoney();
                };

                // ADD 
                btn = _view.FindViewById<Button>(Resource.Id.equipmentMoneyADD);
                btn.Click += (s, e) =>
                {
                    var money = _view.FindViewById<EditText>(Resource.Id.equipmentCPADD);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var amt = Int32.Parse(money.Text);
                        _cs.Currency.Cp += amt;
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentSPADD);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var amt = Int32.Parse(money.Text);
                        _cs.Currency.Sp += amt;
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentGPADD);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var amt = Int32.Parse(money.Text);
                        _cs.Currency.Gp += amt;
                    }

                    money = _view.FindViewById<EditText>(Resource.Id.equipmentPPADD);
                    if (!String.IsNullOrWhiteSpace(money.Text))
                    {
                        var amt = Int32.Parse(money.Text);
                        _cs.Currency.Pp += amt;
                    }

                    UpdateMoney();
                };

                // EQUIPMENT
                var input = _view.FindViewById<EditText>(Resource.Id.characterScreenEquipmentEditText);

                foreach (var s in _cs.Equipment)
                {
                    input.Text += s + "\n";
                }

                input.FocusChange += (s, e) =>
                {
                    if (!e.HasFocus)
                    {
                        var sender = s as EditText;
                        var text = sender.Text;
                        string[] lines = text.Split('\n');

                        // clear prev values.
                        _cs.ClearEquipment();

                        // readd old values and new values.
                        foreach (var l in lines)
                        {
                            _cs.AddEquipment(l);
                        }
                    }
                };  
            }

            return _view;
        }

        private void UpdateMoney()
        {
            var totalMoney = _view.FindViewById<TextView>(Resource.Id.equipmentMoneyBal);
            totalMoney.Text = _cs.Currency.Pp + "PP " + _cs.Currency.Gp + "GP " + _cs.Currency.Sp + "SP " + _cs.Currency.Cp + "CP";
        }
    }

    public class InfoFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterScreenInfo, null);
            _cs = CharacterScreen.CharacterSheet;

            if (_cs != null)
            {
                // traits
                var input = _view.FindViewById<EditText>(Resource.Id.characterScreenInfoTraits);
                input.Text = _cs.PersonalityTraits;
                input.TextChanged += Input_TextChanged;

                // ideals
                input = _view.FindViewById<EditText>(Resource.Id.characterScreenInfoIeals);
                input.Text = _cs.Ideals;
                input.TextChanged += Input_TextChanged;

                // bonds
                input = _view.FindViewById<EditText>(Resource.Id.characterScreenInfoBonds);
                input.Text = _cs.Bonds;
                input.TextChanged += Input_TextChanged;

                // flaws
                input = _view.FindViewById<EditText>(Resource.Id.characterScreenInfoFlaws);
                input.Text = _cs.Flaws;
                input.TextChanged += Input_TextChanged;
            }

            return _view;
        }

        private void Input_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var s = sender as EditText;

            switch (s.Id)
            {
                case Resource.Id.characterScreenInfoTraits:
                    _cs.PersonalityTraits = s.Text;
                    break;

                case Resource.Id.characterScreenInfoIeals:
                    _cs.Ideals = s.Text;
                    break;

                case Resource.Id.characterScreenInfoBonds:
                    _cs.Bonds = s.Text;
                    break;

                case Resource.Id.characterScreenInfoFlaws:
                    _cs.Flaws = s.Text;
                    break;

                default:
                    break;
            }
        }
    }
}