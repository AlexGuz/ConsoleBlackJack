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
        private CardService _cardService = new CardService();
        internal static EventMessage eventMessage = new EventMessage();
        private ConsoleWriteHelper _consoleWrite;
        internal static List<Card> deck;
        internal static string regularExpression = @"[a-zA-Z]";

        public Game()
        {
            deck = _cardService.CreateDeck();
            _consoleWrite = new ConsoleWriteHelper(eventMessage);
        }
    }
}