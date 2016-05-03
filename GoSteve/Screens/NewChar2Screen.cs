using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Runtime.Serialization.Formatters.Binary;
using GoSteve.Screens;
using Server;
using Android.Util;
using System.Collections.Generic;

namespace GoSteve
{
    [Activity(Label = "GoSteve! Dungeons and Dragons")]
    public class NewChar2Screen : Activity
    {

        private class Skills
        {
            public const int Str = 0;
            public const int Dex = 1;
            public const int Con = 2;
            public const int Intel = 3;
            public const int Wis = 4;
            public const int Cha = 5;
        }

        private int[] _skillsNum;
        // key minus button ID to position in _skillsNum ary
        private Dictionary<int,int> _minusBtnAry;
        // key plus button ID to position in _skillsNum ary
        private Dictionary<int, int> _plusBtnAry;
        private TextView[] _skillTxtAry;
        private int _numPoints;
        private TextView _remPoints;

        protected override void OnCreate(Bundle bundle)
        {
            Button curBtn;
            _numPoints = 27;
            _skillsNum = new int[] { 8, 8, 8, 8, 8, 8 };

            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            var cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            CharacterSheet c = cs;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar2);

            TextView instructionAb1 = FindViewById<TextView>(Resource.Id.instructionAb1);
            TextView instructionAb2 = FindViewById<TextView>(Resource.Id.instructionAb2);
            TextView blank1 = FindViewById<TextView>(Resource.Id.blank1);
            TextView blank2 = FindViewById<TextView>(Resource.Id.blank2);
            TextView blank3 = FindViewById<TextView>(Resource.Id.blank3);
            TextView blank4 = FindViewById<TextView>(Resource.Id.blank4);
            TextView blank5 = FindViewById<TextView>(Resource.Id.blank5);
            TextView blank6 = FindViewById<TextView>(Resource.Id.blank6);
            TextView errMsg = FindViewById<TextView>(Resource.Id.errMsg);
            TextView strScore = FindViewById<TextView>(Resource.Id.strScore);
            TextView dexScore = FindViewById<TextView>(Resource.Id.dexScore);
            TextView conScore = FindViewById<TextView>(Resource.Id.conScore);
            TextView intScore = FindViewById<TextView>(Resource.Id.intScore);
            TextView wisScore = FindViewById<TextView>(Resource.Id.wisScore);
            TextView chaScore = FindViewById<TextView>(Resource.Id.chaScore);
            TextView strScoreNum = FindViewById<TextView>(Resource.Id.strScoreNum);
            TextView dexScoreNum = FindViewById<TextView>(Resource.Id.dexScoreNum);
            TextView conScoreNum = FindViewById<TextView>(Resource.Id.conScoreNum);
            TextView intScoreNum = FindViewById<TextView>(Resource.Id.intScoreNum);
            TextView wisScoreNum = FindViewById<TextView>(Resource.Id.wisScoreNum);
            TextView chaScoreNum = FindViewById<TextView>(Resource.Id.chaScoreNum);
            _remPoints = FindViewById<TextView>(Resource.Id.remPoints);
            TextView raceLabel = FindViewById<TextView>(Resource.Id.raceLabel);
            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);
            Button strMinus = FindViewById<Button>(Resource.Id.strMinus);
            Button strPlus = FindViewById<Button>(Resource.Id.strPlus);
            Button dexMinus = FindViewById<Button>(Resource.Id.dexMinus);
            Button dexPlus = FindViewById<Button>(Resource.Id.dexPlus);
            Button conMinus = FindViewById<Button>(Resource.Id.conMinus);
            Button conPlus = FindViewById<Button>(Resource.Id.conPlus);
            Button intMinus = FindViewById<Button>(Resource.Id.intMinus);
            Button intPlus = FindViewById<Button>(Resource.Id.intPlus);
            Button wisMinus = FindViewById<Button>(Resource.Id.wisMinus);
            Button wisPlus = FindViewById<Button>(Resource.Id.wisPlus);
            Button chaMinus = FindViewById<Button>(Resource.Id.chaMinus);
            Button chaPlus = FindViewById<Button>(Resource.Id.chaPlus);

            _minusBtnAry = new Dictionary<int, int>();
            _plusBtnAry = new Dictionary<int, int>();

            // Add Minus button ID as key to position in _skillsAry
            _minusBtnAry.Add(Resource.Id.strMinus, Skills.Str);
            _minusBtnAry.Add(Resource.Id.dexMinus, Skills.Dex);
            _minusBtnAry.Add(Resource.Id.conMinus, Skills.Con);
            _minusBtnAry.Add(Resource.Id.intMinus, Skills.Intel);
            _minusBtnAry.Add(Resource.Id.wisMinus, Skills.Wis);
            _minusBtnAry.Add(Resource.Id.chaMinus, Skills.Cha);

            // Add Plus button ID as key to position in _skillsAry
            _plusBtnAry.Add(Resource.Id.strPlus, Skills.Str);
            _plusBtnAry.Add(Resource.Id.dexPlus, Skills.Dex);
            _plusBtnAry.Add(Resource.Id.conPlus, Skills.Con);
            _plusBtnAry.Add(Resource.Id.intPlus, Skills.Intel);
            _plusBtnAry.Add(Resource.Id.wisPlus, Skills.Wis);
            _plusBtnAry.Add(Resource.Id.chaPlus, Skills.Cha);

            _skillTxtAry = new TextView[] { strScoreNum, dexScoreNum, conScoreNum, intScoreNum, wisScoreNum, chaScoreNum };

            foreach (KeyValuePair<int,int> elem in _plusBtnAry)
            {
                curBtn = FindViewById<Button>(elem.Key);
                Log.Debug("NewChar2Screen","PlusButton id:"+elem.Key+"");
                curBtn.Click += IncrementClicked;
            }

            foreach (KeyValuePair<int, int> elem in _minusBtnAry)
            {
                curBtn = FindViewById<Button>(elem.Key);
                Log.Debug("NewChar2Screen", "MinusButton id:" + elem.Key + "");
                curBtn.Click += DecrementClicked;
            }


            switch (c.RaceInstance.SubRace)
            {
                case KnownValues.SubRace.DARK_ELF:
                    raceLabel.Text = "Dark Elf";
                    break;
                case KnownValues.SubRace.FOREST_GNOME:
                    raceLabel.Text = "Forest Gnome";
                    break;
                case KnownValues.SubRace.HIGH_ELF:
                    raceLabel.Text = "High Elf";
                    break;
                case KnownValues.SubRace.HILL_DWARF:
                    raceLabel.Text = "Hill Dwarf";
                    break;
                case KnownValues.SubRace.LIGHTFOOT_HALFLING:
                    raceLabel.Text = "Lightfoot Halfling";
                    break;
                case KnownValues.SubRace.MOUNTAIN_DWARF:
                    raceLabel.Text = "Mountain Dwarf";
                    break;
                case KnownValues.SubRace.NONE:
                    raceLabel.Text = c.RaceInstance.ToString().Substring(25);
                    break;
                case KnownValues.SubRace.ROCK_GNOME:
                    raceLabel.Text = "Rock Gnome";
                    break;
                case KnownValues.SubRace.STOUT_HALFLING:
                    raceLabel.Text = "Stout Halfling";
                    break;
                case KnownValues.SubRace.WOOD_ELF:
                    raceLabel.Text = "Wood Elf";
                    break;
                default:
                    raceLabel.Text = "NOT FOUND";
                    break;
            }

            continueBtn.Click += (s, arg) =>
            {
                if (_numPoints != 0)
                {
                    errMsg.Text = "You must spend all 27 points!";
                    errMsg.Visibility = ViewStates.Visible;
                }
                else
                {
                    c.AbilitiesAndStats.Strength = _skillsNum[Skills.Str];
                    c.AbilitiesAndStats.Dex = _skillsNum[Skills.Dex];
                    c.AbilitiesAndStats.Con = _skillsNum[Skills.Con];
                    c.AbilitiesAndStats.Wisdom = _skillsNum[Skills.Wis];
                    c.AbilitiesAndStats.Intel = _skillsNum[Skills.Intel];
                    c.AbilitiesAndStats.Charisma = _skillsNum[Skills.Cha];

                    var charScreen = new Intent(this, typeof(NewChar4Screen));
                    var gsMsg1 = new GSActivityMessage();
                    gsMsg1.Message = CharacterSheet.GetBytes(c);
                    charScreen.PutExtra(gsMsg1.CharacterMessage, gsMsg1.Message);
                    StartActivity(charScreen);
                }
            };
        }

        private void IncrementClicked(Object sender, EventArgs arg)
        {
            Button btn;
            int pos;
            btn = sender as Button;
            pos=_plusBtnAry[btn.Id];
            Log.Debug("Char2screen","Array Index:"+pos);
            if(_skillsNum[pos] < 15 && _numPoints > 0)
            {
                _skillsNum[pos]++;
                _numPoints--;
                _skillTxtAry[pos].Text = _skillsNum[pos].ToString();
                UpdateSkillsNumText();
            }
        }

        private void DecrementClicked(Object sender, EventArgs arg)
        {
            Button btn;
            int pos;
            btn = sender as Button;
            pos = _minusBtnAry[btn.Id];
            Log.Debug("Char2screen", "Array Index:" + pos);
            if (_skillsNum[pos] > 8 && _numPoints < 27)
            {
                _skillsNum[pos]--;
                _numPoints++;
                _skillTxtAry[pos].Text = _skillsNum[pos].ToString();
                UpdateSkillsNumText();
            }
        }

        private void UpdateSkillsNumText()
        {
            _remPoints.Text = "Remaining Points: " + _numPoints;
        }
    }
}