using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Game
    {
        internal Gambler player = new Gambler(PlayerType.Player);
        internal Gambler diller = new Gambler(PlayerType.Diller);
        internal static EventMessage eventMessage = new EventMessage();
        internal static List<Card> deck;
        internal static string regularExpression = @"[a-zA-Z]";
        private CardService _cardService = new CardService();
        private ConsoleWriteHelper _consoleWrite;

        public Game()
        {
            deck = _cardService.CreateDeck();
            _consoleWrite = new ConsoleWriteHelper(eventMessage);
        }
    }
}