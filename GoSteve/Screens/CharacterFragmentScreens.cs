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

        public StatsSkillsFragment(CharacterSheet cs)
        {
            _cs = cs;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.CharacterStatsSkillsScreen, null);

            view.FindViewById<EditText>(Resource.Id.StatsSkillsStrength).Text = _cs.AbilitiesAndStats.Strength.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsStrengthMod).Text = _cs.AbilitiesAndStats.StrengthMod.ToString();

            view.FindViewById<EditText>(Resource.Id.StatsSkillsDexterity).Text = _cs.AbilitiesAndStats.Dex.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsDexterityMod).Text = _cs.AbilitiesAndStats.DexMod.ToString();

            view.FindViewById<EditText>(Resource.Id.StatsSkillsCon).Text = _cs.AbilitiesAndStats.Con.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsConMod).Text = _cs.AbilitiesAndStats.ConstMod.ToString();

            view.FindViewById<EditText>(Resource.Id.StatsSkillsInt).Text = _cs.AbilitiesAndStats.Intel.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsIntMod).Text = _cs.AbilitiesAndStats.IntelMod.ToString();

            view.FindViewById<EditText>(Resource.Id.StatsSkillsWis).Text = _cs.AbilitiesAndStats.Wisdom.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsWisMod).Text = _cs.AbilitiesAndStats.WisdomMod.ToString();

            view.FindViewById<EditText>(Resource.Id.StatsSkillsChar).Text = _cs.AbilitiesAndStats.Charisma.ToString();
            view.FindViewById<EditText>(Resource.Id.StatsSkillsCharMod).Text = _cs.AbilitiesAndStats.CharismaMod.ToString();

            return view;
        }
    }
}