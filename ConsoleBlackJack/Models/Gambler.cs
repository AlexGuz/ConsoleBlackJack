using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    public class Gambler
    {
        public List<Card> playerCards = new List<Card>();
        public string Name { get; set; }
        public int PlayerPoint { get; set; }
        public HandStyle Style { get; set; }
        public PlayerType Type { get; set; }
        public bool EndTurn { get; set; }
        public readonly int maxPoint = 21;
        public int dillerMaxHandPoint = 17;
        public int cash;
        public int bet;
        public bool IsLoose;

        public Gambler(PlayerType gambler)
        {
            Type = gambler;
            Style = HandStyle.Soft;
            bet = 0;
            cash = 200;
        }
    }
}