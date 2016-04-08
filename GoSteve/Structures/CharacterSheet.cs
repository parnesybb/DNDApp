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
using GoSteve.Structures.Weapons;
using GoSteve.Structures.Spells;
using GoSteve.Structures.Classes;
using GoSteve.Structures.Races;
using GoSteve.Structures;

namespace GoSteve
{
    public class CharacterSheet
    {
        private string _charName;
        private AClass _class;
        private int _level;
        private ARace _race;
        private KnownValues.Background _background;
        private string _alignment;     
        private string _playerName;
        private int _xp;
        private bool _hasInspiration;
        private Abilities _abilities;
        private int _passiveWisdom;
        private List<string> _othrProfsLangs;
        private int _armorClass;
        private int _initiative;
        private int _speed;
        private HitPoints _hitPoints;
        private HitDice _hitDice;
        private List<Spell> _spells;
        private List<Weapon> _weapons;
        private List<string> _equipment;
        private Currency _currency;
        private string _personalityTraits;
        private string _ideals;
        private string _bonds;
        private string _flaws;
        private List<String> _featuresTraits;
       

        public CharacterSheet()
        {

        }

        public CharacterSheet(string charName)
        {

        }
    }
}