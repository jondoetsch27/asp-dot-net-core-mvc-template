using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace asp.net.core.mvc.demo.Models
{
    public class Player
    {
        public long Id { get; set; }

        public string _key;

        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(playerId.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }

        [Required]
        private string playerId { get; set; }

        [Required]
        private string playerFirstName { get; set; }

        [Required]
        private string playerLastName { get; set; }

        [Required]
        private int playerNumber { get; set; }

        public Player() { }

        public Player(string playerFirstName, string playerLastName, int playerNumber)
        {
            this.playerFirstName = playerFirstName;
            this.playerLastName = playerLastName;
            this.playerNumber = playerNumber;
            this.playerId = String.Format("{0}{1}{2}", playerFirstName, playerLastName, playerNumber);
        }
    }
}
