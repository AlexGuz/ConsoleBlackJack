using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class CardService
    {
        private static Random rand = new Random();
        private static int rankLenght = Enum.GetNames(typeof(Rank)).Length;
        private const int figurePoint = 10;
        private const int rankCorrectionFactor = 2;

        public void ShowCards<T>(T player) where T : Gambler
        {
            Console.WriteLine(player.Name);
            foreach (var card in player.playerCards)
            {
                Console.WriteLine("{0} {1} {2}", card.Rank, card.Suit, card.Point);
            }
            GameService.CardCounter(player);
            Console.WriteLine($"Total points: {player.PlayerPoint}");

            Console.WriteLine("---------------------");
        }

        internal List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < Enum.GetNames(typeof(Suit)).Length; i++)
            {
                for (int j = 0; j < rankLenght; j++)
                {
                    int point = j + rankCorrectionFactor;
                    if (point > (int)Rank.Ace && point < (int)Rank.King)
                    {
                        point = figurePoint;
                    }

                    deck.Add(new Card()
                    {
                        Rank = (Rank)j + rankCorrectionFactor,
                        Suit = (Suit)i,
                        Point = point,
                    });
                }
            }
            return deck;
        }

        internal List<Card> AddTwoCard(ref List<Card> deck)
        {
            List<Card> playerCards = new List<Card>();
            for (int i = 0; i < 2; i++)
            {
                playerCards.Add(AddCard(ref deck));
            }
            return playerCards;
        }

        internal Card AddCard(ref List<Card> deck)
        {
            int addCardId = rand.Next(0, deck.Count);
            Card playerCard = deck[addCardId];
            deck.Remove(playerCard);
            return playerCard;
        }
    }
}