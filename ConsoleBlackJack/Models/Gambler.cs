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
        public int Cash { get; set; }
        public int Bet { get; set; }
        public bool IsLoose { get; set; }

        public Gambler(PlayerType gambler)
        {
            Type = gambler;
            Style = HandStyle.Soft;
            Bet = 0;
            Cash = 200;
        }
    }
}