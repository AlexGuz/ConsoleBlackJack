using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Game
    {
        private Gambler _player = new Gambler(PlayerType.Player);
        private Gambler _diller = new Gambler(PlayerType.Diller);
        private static CardService _cardService = new CardService();
        private static GameService _gameService = new GameService();
        private static MoneyService moneyOp = new MoneyService();
        internal static EventMessage _eventMessage = new EventMessage();
        private static VictoryConditions _victoryConditions = new VictoryConditions();
        private ConsoleWriteHelper consoleWrite;
        internal static List<Card> deck;
        internal static string regularExpression = @"[a-zA-Z]";

        public Game()
        {
            deck = _cardService.CreateDeck();
            consoleWrite = new ConsoleWriteHelper(_eventMessage);
        }

        public static void FirstDistribution(Gambler player, Gambler diller)
        {
            if (player.Name == null)
            {
                Console.WriteLine("Enter your Name");
                string name = Console.ReadLine();
                if (Regex.Match(name, regularExpression).Success)
                {
                    player.Name = name;
                }
                else
                {
                    player.Name = "Player";
                }

                diller.Name = "Tommy Hyland";
            }

            moneyOp.EnterBet(player);

            player.playerCards = _cardService.AddTwoCard(ref deck);
            diller.playerCards = _cardService.AddTwoCard(ref deck);

            Console.Clear();

            _cardService.ShowCards(player);
            _cardService.ShowCards(diller);

            _victoryConditions.GotABlackJack(player, diller);
            _gameService.OneTurn(player, diller);
        }
    }
}