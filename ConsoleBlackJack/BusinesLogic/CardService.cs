using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class CardService
    {
        private static Random _rand = new Random();
        private static int _rankLenght = Enum.GetNames(typeof(Rank)).Length;

        internal void ShowCards(Gambler player)
        {
            Console.WriteLine(player.Name);
            foreach (var card in player.playerCards)
            {
                Console.WriteLine("{0} {1} {2}", card.Rank, card.Suit, card.Point);
            }
            CardCounter(player);
            if (player.Type == PlayerType.Player)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine($"Total points: {player.PlayerPoint}");
                Console.WriteLine($"Player bet: {player.bet}");
                Console.WriteLine($"Player cash: {player.cash}");
                Console.WriteLine("---------------------");
                return;
            }

            Console.WriteLine($"Total points: {player.PlayerPoint}");
            Console.WriteLine("---------------------");
        }

        internal List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < Enum.GetNames(typeof(Suit)).Length; i++)
            {
                for (int j = 0; j < _rankLenght - 1; j++)
                {
                    int point = j + CardServiceConst.rankCorrectionFactor;
                    if (point > (int)Rank.Ace && point <= (int)Rank.King)
                    {
                        point = CardServiceConst.figurePoint;
                    }

                    deck.Add(new Card()
                    {
                        Rank = (Rank)j + CardServiceConst.rankCorrectionFactor,
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
            if (deck.Count < Card.MinDeckCapacity)
            {
                deck = CreateDeck();
            }

            int addCardId = _rand.Next(0, deck.Count);
            Card playerCard = deck[addCardId];
            deck.Remove(playerCard);
            return playerCard;
        }

        private void CardCounter(Gambler player)
        {
            int points = 0;

            foreach (var card in player.playerCards)
            {
                points += card.Point;
            }
            player.PlayerPoint = points;
        }
    }
}