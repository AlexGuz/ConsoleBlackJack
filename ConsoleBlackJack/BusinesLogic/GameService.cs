using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class GameService
    {
        private static CardService _cardService = new CardService();
        private static GamblerService _gamblerService = new GamblerService();

        internal void OneTurn(Gambler player, Gambler diller)
        {
            if (player.Type == PlayerType.Player)
            {
                _gamblerService.PlayerGame(diller, player);
            }
            else
            {
                _gamblerService.DillerGame(player, diller);
            }
        }

        internal void NextTurnGame(Gambler player, Gambler diller)
        {
            player.playerCards.Add(_cardService.AddCard(ref Game.deck));
            Console.Clear();
            _cardService.ShowCards(player);
            _cardService.ShowCards(diller);

            if (VictoryConditions.IsAceOnHend(player.playerCards) & player.Style == HandStyle.Soft & !player.EndTurn)
            {
                _gamblerService.ChangeHand(player);
                _cardService.ShowCards(player);
            }
            OneTurn(player, diller);
        }

        internal static void CardCounter(Gambler player)
        {
            int points = 0;

            foreach (var card in player.playerCards)
            {
                points += card.Point;
            }
            player.PlayerPoint = points;
        }

        internal void ExitGame(Gambler player, Gambler diller)
        {
            Game._eventMessage.HandleGameEvent(EventMessage.EndGameMessage);
            Console.ReadLine();
            Environment.Exit(0);
        }

        internal void NewGameSelector(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessage.YesKey, Game.FirstDistribution},
                {EventMessage.NoKey, ExitGame},
                {EventMessage.Default, NewGameSelector}
            };
            player.playerCards.Clear();
            diller.playerCards.Clear();
            Game._eventMessage.HandleGameEvent(EventMessage.StartGameMessage);
            Game._eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        private static List<Card> AceByHardStyle(List<Card> cardsOnHands)
        {
            foreach (var card in cardsOnHands)
            {
                if (card.Rank == Rank.Ace)
                {
                    card.Point = (int)Rank.AceByHardHand;
                }
            }
            return cardsOnHands;
        }        
    }
}