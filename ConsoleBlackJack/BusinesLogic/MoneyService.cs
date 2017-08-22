using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class MoneyService
    {
        internal void EnterBet(Gambler player)
        {
            if (MoneyCounter(player))
            {
                Console.WriteLine($"You have {player.cash}. Enter you bet");
                BetCounter(player);
                return;
            }

            GoForTheMoney(player);
            GameProvider.ExitGame(player, null);
        }

        internal static void BlackJackWinnings(Gambler player)
        {
            Console.WriteLine($"BjackJack!!!!!!!!!!!!!{player.Name} Victory!!!");

            double winnings = player.bet * MoneyConst.BlackJackWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.bet} will be added to the winnings");

            player.cash += player.bet + (int)winnings;
        }

        internal static void ClasicWinnings(Gambler player)
        {
            Console.WriteLine($"{player.Name} Victory!");

            double winnings = player.bet * MoneyConst.StandartWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.bet} will be added to the winnings");

            player.cash += player.bet + (int)winnings;
        }

        private void BetCounter(Gambler player)
        {
            int bet = 0;
            bool isBetDone = int.TryParse(Console.ReadLine(), out bet);
            if (isBetDone & bet <= player.cash)
            {
                player.bet = bet;
                player.cash -= bet;
                return;
            }

            Game.eventMessage.HandleGameEvent(EventMessageConst.InvalidInputMessage);
            EnterBet(player);
        }

        private bool MoneyCounter(Gambler player)
        {
            if (player.cash > 0)
            {
                return true;
            }
            return false;
        }

        private void GoForTheMoney(Gambler player)
        {
            Dictionary<string, Action<Gambler>> choiseOperations = new Dictionary<string, Action<Gambler>>
            {
                {EventMessageConst.YesKey, AddMoney},
                {EventMessageConst.NoKey, null},
                {EventMessageConst.Default, GoForTheMoney}
            };
            Game.eventMessage.HandleGameEvent(EventMessageConst.NotEnoughMoney);
            Game.eventMessage.WorkWithMoneyDictionary(player, choiseOperations);
        }

        private void AddMoney(Gambler player)
        {
            player.cash = 200;
        }
    }
}