using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Game
    {
        private Player player = new Player(PlayerType.Player);
        private Diller diller = new Diller(PlayerType.Diller);
        private CardService cardService = new CardService();
        private GameService gameService = new GameService();
        private ConsoleWriteHelper consoleWrite;
        internal static List<Card> deck;

        public Game()
        {
            deck = cardService.CreateDeck();
            consoleWrite = new ConsoleWriteHelper(gameService);
        }

        public void FirstDistribution()
        {
            Console.WriteLine("Enter your Name");
            player.Name = Console.ReadLine();
            diller.Name = "Tommy Hyland";

            player.playerCards = cardService.AddTwoCard(ref deck);
            diller.playerCards = cardService.AddTwoCard(ref deck);

            Console.Clear();
            Console.WriteLine("Game Start");

            cardService.ShowCards(player);
            cardService.ShowCards(diller);

            gameService.GotABlackJack<Gambler>(player, diller);
            gameService.StartGame<Gambler>(player, diller);
        }
    }
}