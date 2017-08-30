using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class MoneyService
    {
        internal void EnterBet(Gambler player, Gambler diller)
        {
            if (MoneyCounter(player))
            {
                Console.WriteLine($"You have {player.Cash}. Enter you bet");
                BetCounter(player, diller);
                return;
            }

            GoForTheMoney(player, diller);
            GameProvider.NewGameSelector(player, diller);
        }

        internal static void BlackJackWinnings(Gambler player)
        {
            Console.WriteLine($"BjackJack!!!!!!!!!!!!!{player.Name} Victory!!!");

            var winnings = player.Bet * MoneyConst.BlackJackWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.Bet} will be added to the winnings");

            player.Cash += player.Bet + (int)winnings;
        }

        internal static void ClasicWinnings(Gambler player)
        {
            Console.WriteLine($"{player.Name} Victory!");

            var winnings = player.Bet * MoneyConst.StandartWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.Bet} will be added to the winnings");

            player.Cash += player.Bet + (int)winnings;
        }

        private void BetCounter(Gambler player, Gambler diller)
        {
            int bet = 0;
            bool isBetDone = int.TryParse(Console.ReadLine(), out bet);
            if (isBetDone & bet <= player.Cash)
            {
                player.Bet = bet;
                player.Cash -= bet;
                return;
            }

            Game.eventMessage.HandleGameEvent(EventMessageConst.InvalidInputMessage);
            EnterBet(player, diller);
        }

        private bool MoneyCounter(Gambler player)
        {
            if (player.Cash > 0)
            {
                return true;
            }
            return false;
        }

        private void GoForTheMoney(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, AddMoney},
                {EventMessageConst.NoKey, GameProvider.ExitGame},
                {EventMessageConst.Default, GoForTheMoney}
            };
            Game.eventMessage.HandleGameEvent(EventMessageConst.NotEnoughMoney);
            Game.eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        private void AddMoney(Gambler player, Gambler diller)
        {
            player.Cash = 200;
        }
    }
}