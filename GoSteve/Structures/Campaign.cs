using System;
using System.Collections.Generic;

namespace GoSteve.Structures
{
    [Serializable]
    public class Campaign
    {
        private Dictionary<string, CharacterSheet> _players;

        /// <summary>
        /// M2P1 = Two monsters per player.
        /// M1P1 = One monster per player.
        /// M1P2 = One monster per two players.
        /// </summary>
        public enum DesiredDifficulty {M2P1, M1P1, M1P2}

        public Campaign()
        {
            this._players = new Dictionary<string, CharacterSheet>();
        }

        /// <summary>
        /// Generates number of enemies and CR level.
        /// </summary>
        public string GenerateEncounter(DesiredDifficulty dd)
        {
            // Sanity check.
            if (_players.Count <= 0)
            {
                return "There are no players.";
            }

            var result = string.Empty;
            var nPlayer = _players.Count;
            double ratio = 0.0;
            int avgLvl = 0;
            int cr = 0;
            int nMonster = 0;

            // Avg level of group.
            foreach (var player in _players)
            {
                avgLvl += player.Value.Level;
            }
            avgLvl /= nPlayer;

            // Calc ratio.
            switch (dd)
            {
                case DesiredDifficulty.M2P1:
                    nMonster = nPlayer * 2;
                    ratio = .25;
                    break;
                case DesiredDifficulty.M1P1:
                    nMonster = nPlayer;
                    ratio = .33;
                    break;
                case DesiredDifficulty.M1P2:
                    nMonster = nPlayer / 2;
                    ratio = .66;
                    break;
                default:
                    break;
            }

            // Results
            cr = (int)Math.Round(ratio * avgLvl);
            result = String.Format("An encounter with {0} enemies with a CR of {1} is recommended.", nMonster, cr);

            return result;
        }

        /// <summary>
        /// Check is a player is in the campaign.
        /// </summary>
        /// <param name="id">The player ID to check.</param>
        /// <returns>True if the player is in the campaign, false if not.</returns>
        public bool IsMember(string id)
        {
            return _players.ContainsKey(id);
        }

        /// <summary>
        /// Add a player to campaign.
        /// </summary>
        /// <param name="id">The ID of the player to add</param>
        /// <param name="player">The character sheet of the player</param>
        public void AddPlayer(string id, CharacterSheet player)
        {
            _players.Add(id, player);
        }

        /// <summary>
        /// Remove a player from campaign.
        /// </summary>
        /// <param name="id">The ID of the player to remove.</param>
        public void RemovePlayer(string id)
        {
            _players.Remove(id);
        }

        /// <summary>
        /// Gets or Sets a player.
        /// </summary>
        /// <param name="id">The players ID</param>
        /// <returns>The players character sheet</returns>
        public CharacterSheet this[string id]
        {
            get
            {
                return _players[id];
            }
            set
            {
                _players[id] = value;
            }
        }
    }
}