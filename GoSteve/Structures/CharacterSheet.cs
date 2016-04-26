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
    [Serializable]
    public class CharacterSheet
    {
        private string _gender;
        private string _id;
        private string _charName;
        private AClass _class;
        private int _level;
        private ARace _race;
        //private KnownValues.SubRace _subrace;
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
        private HitPoints _hitPoints;
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
            this._id = String.Empty;
            this._gender = String.Empty;
            this._charName = String.Empty;
            this._level = 0;
            this._alignment = String.Empty;
            this._playerName = String.Empty;
            this._xp = 0;
            this._hasInspiration = false;
            this._passiveWisdom = 0;
            this._armorClass = 0;
            this._initiative = 0;
            this._personalityTraits = String.Empty;
            this._ideals = String.Empty;
            this._bonds = String.Empty;
            this._flaws = String.Empty;

            this._abilities = new Abilities();
            this._othrProfsLangs = new List<string>();
            this._hitPoints = new HitPoints();
            this._spells = new List<Spell>();
            this._weapons = new List<Weapon>();
            this._equipment = new List<string>();
            this._currency = new Currency();
            this._featuresTraits = new List<string>();
        }

        public String ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Gender
        {
            get
            {
                return this._gender;
            }
            set
            {
                this._gender = value;
            }
        }

        /// <summary>
        /// The Name of the character.
        /// </summary>
        public string CharacterName
        {
            get
            {
                return _charName;
            }

            set
            {
                this._charName = value;
            }
        }

        /// <summary>
        /// The characters abilities, saving throws and skills.
        /// </summary>
        public Abilities AbilitiesAndStats
        {
            get
            {
                return this._abilities;
            }
        }

        /// <summary>
        /// Sets the class for the sheet.
        /// </summary>
        /// <param name="value">The class type.</param>
        /// <param name="isAddDefaults">Whether defaults are added to the sheet for the class.</param>
        public void SetClass(KnownValues.ClassType value, bool isAddDefaults)
        {
            switch (value)
            {
                case KnownValues.ClassType.BARBARIAN:
                    this._class = new Barbarian();
                    break;
                case KnownValues.ClassType.BARD:
                    this._class = new Bard();
                    break;
                case KnownValues.ClassType.CLERIC:
                    this._class = new Cleric();
                    break;
                case KnownValues.ClassType.DRUID:
                    this._class = new Druid();
                    break;
                case KnownValues.ClassType.FIGHTER:
                    this._class = new Fighter();
                    break;
                case KnownValues.ClassType.MONK:
                    this._class = new Monk();
                    break;
                case KnownValues.ClassType.PALADIN:
                    this._class = new Paladin();
                    break;
                case KnownValues.ClassType.RANGER:
                    this._class = new Ranger();
                    break;
                case KnownValues.ClassType.ROGUE:
                    this._class = new Rogue();
                    break;
                case KnownValues.ClassType.SORCERER:
                    this._class = new Sorcerer();
                    break;
                case KnownValues.ClassType.WARLOCK:
                    this._class = new Warlock();
                    break;
                case KnownValues.ClassType.WIZARD:
                    this._class = new Wizard();
                    break;
                default:
                    break;
            }

            // Set the class
            this._abilities.SetClass(value);

            // Add class values to lists.
            if (isAddDefaults)
            {
                this._featuresTraits.AddRange(this._class.GetTraits());
                this._equipment.AddRange(this._class.GetEquipment());
                this._othrProfsLangs.AddRange(this._class.GetProficiencies());
                this._hitPoints.Max = this._class.GetLevelOneHitPoints(this._abilities.ConstMod);
                this._hitPoints.Current = this._hitPoints.Max;
            }         
        }

        /// <summary>
        /// Gets the instance of the sheets class.
        /// </summary>
        public AClass ClassInstance
        {
            get
            {
                return _class;
            }
        }

        public void setSubRace(KnownValues.SubRace sub)
        {
            try
            {
                this.RaceInstance.SubRace = sub;
            }
            catch (Exception)
            { }
        }

        public KnownValues.SubRace getSubRace()
        {
            return this.RaceInstance.SubRace;
        }

        /// <summary>
        /// The characters level.
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                this._level = value;
            }
        }

        /// <summary>
        /// Gets the instance of the sheets race.
        /// </summary>
        public ARace RaceInstance
        {
            get
            {
                return _race;
            }
        }

        /// <summary>
        /// Sets the sheets race.
        /// </summary>
        /// <param name="r">The race type.</param>
        /// <param name="isAddDefaults">Whether to add defaults to the sheet for the race type.</param>
        public void SetRace(KnownValues.Race r, bool isAddDefaults)
        {
            switch (r)
            {
                case KnownValues.Race.DWARF:
                    this._race = new Dwarf();
                    break;
                case KnownValues.Race.ELF:
                    this._race = new Elf();
                    break;
                case KnownValues.Race.HALFLING:
                    this._race = new Halfling();
                    break;
                case KnownValues.Race.HUMAN:
                    this._race = new Human();
                    break;
                case KnownValues.Race.DRAGONBORN:
                    this._race = new Dragonborn();
                    break;
                case KnownValues.Race.GNOME:
                    this._race = new Gnome();
                    break;
                case KnownValues.Race.HALF_ELF:
                    this._race = new HalfElf();
                    break;
                case KnownValues.Race.HALF_ORC:
                    this._race = new HalfOrc();
                    break;
                case KnownValues.Race.TIEFLING:
                    this._race = new Tiefling();
                    break;
                default:
                    break;
            }

            // Set the race.
            this._abilities.SetRace(_race);

            if (isAddDefaults)
            {
                // Add race values to lists.
                this._featuresTraits.AddRange(this._race.GetFeaturesTraits());
                this._othrProfsLangs.AddRange(this._race.GetProficienciesLanguages());
            }
        }

        /// <summary>
        /// The character sheets background.
        /// </summary>
        public KnownValues.Background Background
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
                this._abilities.SetBackground(value);
            }
        }

        /// <summary>
        /// The alignment of the character.
        /// </summary>
        public string Alignment
        {
            get
            {
                return _alignment;
            }

            set
            {
                _alignment = value;
            }
        }

        /// <summary>
        /// The players name.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return _playerName;
            }

            set
            {
                _playerName = value;
            }
        }

        /// <summary>
        /// Current XP of the character.
        /// </summary>
        public int Xp
        {
            get
            {
                return _xp;
            }

            set
            {
                _xp = value;
            }
        }

        /// <summary>
        /// True if the character has inspiration.
        /// </summary>
        public bool HasInspiration
        {
            get
            {
                return _hasInspiration;
            }

            set
            {
                _hasInspiration = value;
            }
        }

        /// <summary>
        /// The characters passive wisdom.
        /// </summary>
        public int PassiveWisdom
        {
            get
            {
                return _passiveWisdom;
            }

            set
            {
                _passiveWisdom = value;
            }
        }

        /// <summary>
        /// An array of the misc proficiencies and languages.
        /// </summary>
        public string[] OtherProficienciesLanguages
        {
            get
            {
                return this._othrProfsLangs.ToArray();
            }
        }

        /// <summary>
        /// Add a misc. proficiency or language.
        /// </summary>
        /// <param name="val">The value to add.</param>
        public void AddProficiencyORLanguage(string val)
        {
            this._othrProfsLangs.Add(val);
        }

        /// <summary>
        /// The characters armor class.
        /// </summary>
        public int ArmorClass
        {
            get
            {
                return _armorClass;
            }

            set
            {
                _armorClass = value;
            }
        }

        /// <summary>
        /// The characters initiative, if set.
        /// </summary>
        public int Initiative
        {
            get
            {
                return _initiative;
            }

            set
            {
                _initiative = value;
            }
        }

        /// <summary>
        /// The characters hit points.
        /// </summary>
        public HitPoints HitPoints
        {
            get
            {
                return _hitPoints;
            }

            set
            {
                _hitPoints = value;
            }
        }

        /// <summary>
        /// Gets an array of the characters spells.
        /// </summary>
        public Spell[] Spells
        {
            get
            {
                return this._spells.ToArray();
            }
        }

        /// <summary>
        /// Adds a spell to the character sheet.
        /// </summary>
        /// <param name="s">The spell to add.</param>
        public void AddSpell(Spell s)
        {
            this._spells.Add(s);
        }

        /// <summary>
        /// Gets an array of the chacters weapons.
        /// </summary>
        public Weapon[] Weapons
        {
            get
            {
                return this._weapons.ToArray();
            }
        }

        /// <summary>
        /// Adds a weapon to the character sheet.
        /// </summary>
        /// <param name="w"></param>
        public void AddWeapon(Weapon w)
        {
            this._weapons.Add(w);
        }

        /// <summary>
        /// Gets an array of the characters weapons.
        /// </summary>
        public string[] Equipment
        {
            get
            {
                return this._equipment.ToArray();
            }
        }

        /// <summary>
        /// Adds an equipment piece to the character sheet.
        /// </summary>
        /// <param name="e">The equipment to add.</param>
        public void AddEquipment(string e)
        {
            this._equipment.Add(e);
        }

        /// <summary>
        /// The characters currency.
        /// </summary>
        public Currency Currency
        {
            get
            {
                return _currency;
            }

            set
            {
                _currency = value;
            }
        }

        /// <summary>
        /// The characters personality traits.
        /// </summary>
        public string PersonalityTraits
        {
            get
            {
                return _personalityTraits;
            }

            set
            {
                _personalityTraits = value;
            }
        }

        /// <summary>
        /// The characters ideals.
        /// </summary>
        public string Ideals
        {
            get
            {
                return _ideals;
            }

            set
            {
                _ideals = value;
            }
        }

        /// <summary>
        /// The characters bonds.
        /// </summary>
        public string Bonds
        {
            get
            {
                return _bonds;
            }

            set
            {
                _bonds = value;
            }
        }

        /// <summary>
        /// The characters flaws.
        /// </summary>
        public string Flaws
        {
            get
            {
                return _flaws;
            }

            set
            {
                _flaws = value;
            }
        }

        /// <summary>
        /// Gets an array of the characters features and traits.
        /// </summary>
        public string[] FeaturesAndTraits
        {
            get
            {
                return this._featuresTraits.ToArray();
            }
        }

        /// <summary>
        /// Adds a feature or trait to the character sheet.
        /// </summary>
        /// <param name="val">The feature or trait to add.</param>
        public void AddFeatureOrTrait(string val)
        {
            this._featuresTraits.Add(val);
        }

        /// <summary>
        /// Converts a character sheet into a byte array.
        /// </summary>
        /// <param name="cs">The character sheet to convert.</param>
        /// <returns>The character sheet as a byte array.</returns>
        public static byte[] GetBytes(CharacterSheet cs)
        {
            throw new NotImplementedException();
        } 

        /// <summary>
        /// Converts a byte array into a character sheet.
        /// </summary>
        /// <param name="csBytes">The byte array to convert.</param>
        /// <returns>A new instance of a character sheet repesented by the byte array.</returns>
        public static CharacterSheet GetCharacterSheet(byte[] csBytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a character sheet in from file.
        /// </summary>
        /// <param name="loc">Location of the character sheet.</param>
        /// <returns>An instance of CharacterSheet.</returns>
        public static CharacterSheet ReadFromFile(string loc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a CharacterSheet out to a file in serialized form.
        /// </summary>
        /// <param name="loc">The name of the file.</param>
        /// <param name="cs">The character sheet to write out.</param>
        public static void WriteToFile(string loc, CharacterSheet cs)
        {
            throw new NotImplementedException();
        }
    }
}