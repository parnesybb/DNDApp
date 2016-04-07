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

namespace GoSteve
{
    public class CharacterSheet
    {
        private string _playerName;
        private string _charName;
        private ARace _race;
        private Abilities _abilities;
        private Spell[] _spells;
        private Weapon[] _weapons;
        private bool _hasInspiration;
        private int passiveWisdom;


        public CharacterSheet()
        {

        }

        public CharacterSheet(string charName)
        {

        }
    }
}