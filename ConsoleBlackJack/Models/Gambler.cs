using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Gambler
    {
        public List<Card> playerCards = new List<Card>();
        public string Name { get; set; }
        public int PlayerPoint { get; set; }
        public HandStyle Style { get; set; }
        public PlayerType Type { get; set; }
        public bool EndTurn { get; set; }
        public readonly int maxPoint = 21;

        public Gambler(PlayerType gambler)
        {
            Type = gambler;
            Style = HandStyle.Soft;
        }
    }
}