using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class MoneyService
    {
        private GameService _gameService = new GameService();

        internal void EnterBet(Gambler player)
        {
            if (MoneyCounter(player))
            {
                Console.WriteLine($"You have {player.cash}. Enter you bet");
                BetCounter(player);
            }
            else
            {
                GoForTheMoney(player);
                _gameService.ExitGame(player, null);
            }
        }

        internal void ReturnBet(Gambler player)
        {
            player.cash += player.bet;
        }

        internal void BlackJackWinnings(Gambler player)
        {
            Console.WriteLine($"BjackJack!!!!!!!!!!!!!{player.Name} Victory!!!");

            double winnings = player.bet * Money.blackJackWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.bet} will be added to the winnings");

            player.cash = player.bet + (int)winnings;            
        }

        internal void ClasicWinnings(Gambler player)
        {
            Console.WriteLine($"{player.Name} Victory!");

            double winnings = player.bet * Money.standartWinnings;

            Console.WriteLine($"Your winnings is {winnings}, your bet {player.bet} will be added to the winnings");

            player.cash = player.bet + (int)winnings;
        }

        private void BetCounter(Gambler player)
        {
            int bet = 0;
            bool isBetDone = int.TryParse(Console.ReadLine(), out bet);
            if (isBetDone & bet <= player.cash)
            {
                player.bet = bet;
                player.cash -= bet;
            }
            else
            {
                Game._eventMessage.HandleGameEvent(EventMessage.InvalidInputMessage);
                EnterBet(player);
            }
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
                {EventMessage.YesKey, AddMoney},
                {EventMessage.NoKey, null},
                {EventMessage.Default, GoForTheMoney}
            };
            Game._eventMessage.HandleGameEvent(EventMessage.NotEnoughMoney);
            Game._eventMessage.WorkWithMoneyDictionary(player, choiseOperations);
        }

        private void AddMoney(Gambler player)
        {
            player.cash = 200;
        }
    }
}