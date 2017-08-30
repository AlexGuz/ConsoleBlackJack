using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleBlackJack
{
    static class GameProvider
    {
        private static CardService _cardService = new CardService();
        private static MoneyService _moneyOp = new MoneyService();
        private static GamblerService _gamblerService = new GamblerService();
        private static VictoryConditions _victoryConditions = new VictoryConditions();

        internal static void FirstDistribution(Gambler player, Gambler diller)
        {
            if (player.Name == null)
            {
                EnterName(player, diller);
            }

            _moneyOp.EnterBet(player, diller);

            player.playerCards = _cardService.AddTwoCard(ref Game.deck);
            diller.playerCards = _cardService.AddTwoCard(ref Game.deck);

            Console.Clear();

            _cardService.ShowCards(player);
            _cardService.ShowCards(diller);

            _victoryConditions.GotABlackJack(player, diller);
            OneTurn(player, diller);
        }

        private static void EnterName(Gambler player, Gambler diller)
        {
            diller.Name = GamblerConst.DillerName;
            player.Name = "Player";

            Console.WriteLine("Enter your Name");
            var name = Console.ReadLine();

            if (!string.IsNullOrEmpty(name) && Regex.Match(name, Game.regularExpression).Success)
            {
                player.Name = name;
            }
        }

        internal static void OneTurn(Gambler player, Gambler diller)
        {
            if (player.Type == PlayerType.Player)
            {
                _gamblerService.PlayerGame(player, diller);
                return;
            }

            _gamblerService.DillerTurn(player, diller);
        }

        internal static void NextTurnGame(Gambler player, Gambler diller)
        {
            player.playerCards.Add(_cardService.AddCard(ref Game.deck));
            Console.Clear();
            _cardService.ShowCards(player);
            _cardService.ShowCards(diller);

            if (VictoryConditions.IsAceOnHend(player.playerCards) & player.Style == HandStyle.Soft & !player.EndTurn)
            {
                _gamblerService.ChangeHand(player, diller);
                _cardService.ShowCards(player);
            }
            OneTurn(player, diller);
        }

        internal static void ExitGame(Gambler player, Gambler diller)
        {
            if (player.EndTurn & diller.EndTurn)
            {
                _victoryConditions.CheckVictoryConditions(player, diller);
                return;
            }
            Game.eventMessage.HandleGameEvent(EventMessageConst.EndGameMessage);
            Console.ReadLine();
            Environment.Exit(0);
        }

        internal static void NewGameSelector(Gambler player, Gambler diller)
        {
            _gamblerService.ClearGamblerData(player);
            _gamblerService.ClearGamblerData(diller);

            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, FirstDistribution},
                {EventMessageConst.NoKey, ExitGame},
                {EventMessageConst.Default, NewGameSelector}
            };

            Game.eventMessage.HandleGameEvent(EventMessageConst.StartGameMessage);
            Game.eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }
    }
}