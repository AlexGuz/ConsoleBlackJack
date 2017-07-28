using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class VictoryConditions
    {
        private static GameService _gameService = new GameService();
        private MoneyService _moneyService = new MoneyService();

        internal void CheckVictoryConditions(Gambler player, Gambler diller)
        {
            if (player.IsLoose & diller.IsLoose || player.PlayerPoint == diller.PlayerPoint)
            {
                Console.WriteLine($"{player.Name} and {diller.Name} dead heat !!!");
                _moneyService.ReturnBet(player);
            }
            else
            {
                CheckAllConditions(player, diller);
            }
        }
       
        internal bool IsLosing(Gambler player, Gambler diller)
        {
            bool isLosing = player.PlayerPoint > player.maxPoint;
            
            return isLosing;
        }

        internal void GotABlackJack(Gambler player, Gambler diller)
        {
            if (IsAceOnHend(player.playerCards) & IsFigureOnHend(player.playerCards))
            {
                _moneyService.BlackJackWinnings(player);
                _gameService.NewGameSelector(player, diller);
            }
        }
        
        internal static bool IsAceOnHend(List<Card> playerCards)
        {
            bool isAce = false;
            foreach (var card in playerCards)
            {
                if (card.Rank == Rank.Ace)
                {
                    isAce = true;
                    break;
                }
            }
            return isAce;
        }

        private void CheckAllConditions(Gambler player, Gambler diller)
        {
            if (player.PlayerPoint > diller.PlayerPoint & !player.IsLoose)
            {
                Console.WriteLine($"{player.Name} Victory!!!");
                _gameService.NewGameSelector(player, diller);
                _moneyService.ClasicWinnings(player);
            }
            else
            {
                Console.WriteLine($"{player.Name} Bust! The game is over");
                _gameService.NewGameSelector(player, diller);
            }
        }

        private bool IsFigureOnHend(List<Card> playerCards)
        {
            bool isFigure = false;
            foreach (var card in playerCards)
            {
                if ((int)card.Rank > (int)Rank.Ten & !IsAceOnHend(playerCards))
                {
                    isFigure = true;
                    break;
                }
            }
            return isFigure;
        }
    }
}