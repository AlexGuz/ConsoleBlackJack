using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class VictoryConditions
    {
        internal void CheckVictoryConditions(Gambler player, Gambler diller)
        {
            if (player.IsLoose & diller.IsLoose || player.PlayerPoint == diller.PlayerPoint)
            {
                Console.WriteLine($"{player.Name} and {diller.Name} dead heat !!!");
                player.Cash += player.Bet;
                GameProvider.NewGameSelector(player, diller);
                return;
            }

            CheckAllConditions(player, diller);
        }

        internal static bool IsLosing(Gambler player)
        {
            bool isLosing = player.PlayerPoint > GamblerConst.MaxPoint;

            return isLosing;
        }

        internal void GotABlackJack(Gambler player, Gambler diller)
        {
            if (IsAceOnHend(player.playerCards) & IsFigureOnHend(player.playerCards))
            {
                MoneyService.BlackJackWinnings(player);
                GameProvider.NewGameSelector(player, diller);
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
                MoneyService.ClasicWinnings(player);
                GameProvider.NewGameSelector(player, diller);
                return;
            }

            DillerVictory(player, diller);
        }

        private void DillerVictory(Gambler player, Gambler diller)
        {
            if (!diller.IsLoose)
            {
                Console.WriteLine($"{player.Name} Bust! The game is over");
                GameProvider.NewGameSelector(player, diller);
                return;
            }

            Console.WriteLine($"{diller.Name} Bust! The game is over");
            MoneyService.ClasicWinnings(player);
            GameProvider.NewGameSelector(player, diller);
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