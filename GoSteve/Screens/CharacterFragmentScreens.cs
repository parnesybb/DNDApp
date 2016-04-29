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

            _view.FindViewById<TextView>(Resource.Id.StatsSkillsStrengthMod).Text = _cs.AbilitiesAndStats.StrengthMod.ToString();
            _view.FindViewById<TextView>(Resource.Id.StatsSkillsDexterityMod).Text = _cs.AbilitiesAndStats.DexMod.ToString();
            _view.FindViewById<TextView>(Resource.Id.StatsSkillsConMod).Text = _cs.AbilitiesAndStats.ConstMod.ToString();
            _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntMod).Text = _cs.AbilitiesAndStats.IntelMod.ToString();
            _view.FindViewById<TextView>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();
            _view.FindViewById<TextView>(Resource.Id.StatsSkillsCharMod).Text = _cs.AbilitiesAndStats.CharismaMod.ToString();

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
                        break;
                    case Resource.Id.StatsSkillsDexterity:
                        _cs.AbilitiesAndStats.Dex = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsDexterityMod).Text = _cs.AbilitiesAndStats.DexMod.ToString();
                        break;
                    case Resource.Id.StatsSkillsCon:
                        _cs.AbilitiesAndStats.Con = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsConMod).Text = _cs.AbilitiesAndStats.ConstMod.ToString();
                        break;
                    case Resource.Id.StatsSkillsInt:
                        _cs.AbilitiesAndStats.Intel = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsIntMod).Text = _cs.AbilitiesAndStats.IntelMod.ToString();
                        break;
                    case Resource.Id.StatsSkillsWis:
                        _cs.AbilitiesAndStats.Strength = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();
                        break;
                    case Resource.Id.StatsSkillsChar:
                        _cs.AbilitiesAndStats.Strength = Int32.Parse(s.Text);
                        _view.FindViewById<TextView>(Resource.Id.StatsSkillsCharMod).Text = _cs.AbilitiesAndStats.CharismaMod.ToString();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            { }   
        }
    }
}