using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class GamblerService
    {
        private static GameService _gameService = new GameService();
        private static VictoryConditions _victory = new VictoryConditions();

        internal void DillerGame(Gambler player, Gambler diller)
        {
            player.EndTurn = true;
            player.IsLoose = _victory.IsLosing(player, diller);
            if (diller.PlayerPoint < diller.dillerMaxHandPoint)
            {
                _gameService.NextTurnGame(diller, player);
            }
            else
            {
                diller.IsLoose = _victory.IsLosing(diller, player);
                _victory.CheckVictoryConditions(player, diller);
            }
        }

        internal void PlayerGame(Gambler diller, Gambler player)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessage.YesKey, _gameService.NextTurnGame},
                {EventMessage.NoKey, DillerGame},
                {EventMessage.Default, PlayerGame}
            };

            Game._eventMessage.HandleGameEvent(EventMessage.AddCardeMessage);
            Game._eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        internal void ChangeHand(Gambler player)
        {
            if (player.Type == PlayerType.Player)
            {
                PlayerHand(player, null);
            }
            else
            {
                DillerHand(player);
            }
        }

        private void PlayerHand(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessage.YesKey, HardHandStyle},
                {EventMessage.NoKey, null},
                {EventMessage.Default, PlayerHand}
            };

            Game._eventMessage.HandleGameEvent(EventMessage.ChangeHandMessage);
            Game._eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        private static void HardHandStyle(Gambler player, Gambler diller)
        {
            player.Style = HandStyle.Hard;
            for (int i = 0; i < player.playerCards.Count; i++)
            {
                if (player.playerCards[i].Rank == Rank.Ace)
                {
                    player.playerCards[i].Point = 1;
                    break;
                }
            }
        }

        private void DillerHand(Gambler diller)
        {
            if (diller.PlayerPoint > diller.dillerMaxHandPoint)
            {
                diller.Style = HandStyle.Hard;
                for (int i = 0; i < diller.playerCards.Count; i++)
                {
                    if (diller.playerCards[i].Rank == Rank.Ace)
                    {
                        diller.playerCards[i].Point = 1;
                        break;
                    }
                }
            }
        }
    }
}