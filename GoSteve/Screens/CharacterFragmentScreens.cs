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

namespace GoSteve.Screens
{
    public class StatsSkillsFragment : Fragment
    {
        private CharacterSheet _cs;
        private View _view;

        public StatsSkillsFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.CharacterStatsSkillsScreen, null);

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
                        _cs.AbilitiesAndStats.Strength = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkills_w_saving).Text = _cs.AbilitiesAndStats.WisdomSavingThrow.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsAnimalHdnl).Text = _cs.AbilitiesAndStats.AnimalHandling.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsInsight).Text = _cs.AbilitiesAndStats.Insight.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsMedicine).Text = _cs.AbilitiesAndStats.Medicine.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsPerception).Text = _cs.AbilitiesAndStats.Perception.ToString();
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsSurvival).Text = _cs.AbilitiesAndStats.Survival.ToString();
                        break;
                    case Resource.Id.StatsSkillsChar:
                        _cs.AbilitiesAndStats.Strength = Int32.Parse(s.Text);
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

        public AbilitiesFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            throw new NotImplementedException();
        }
    }

    public class ProfsLangsFragment : Fragment
    {
        private CharacterSheet _cs;

        public ProfsLangsFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            throw new NotImplementedException();
        }
    }

    public class EquipFragment : Fragment
    {
        private CharacterSheet _cs;

        public EquipFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            throw new NotImplementedException();
        }
    }

    public class InfoFragment : Fragment
    {
        private CharacterSheet _cs;

        public InfoFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            throw new NotImplementedException();
        }
    }
}