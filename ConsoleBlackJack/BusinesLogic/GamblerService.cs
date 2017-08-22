using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class GamblerService
    {
        internal void PlayerGame(Gambler diller, Gambler player)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, GameProvider.NextTurnGame},
                {EventMessageConst.NoKey, DillerGame},
                {EventMessageConst.Default, PlayerGame}
            };

            Game.eventMessage.HandleGameEvent(EventMessageConst.AddCardeMessage);
            Game.eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        internal void DillerGame(Gambler player, Gambler diller)
        {
            player.EndTurn = true;
            player.IsLoose = VictoryConditions.IsLosing(player, diller);
            if (diller.PlayerPoint < diller.dillerMaxHandPoint)
            {
                GameProvider.NextTurnGame(diller, player);
                return;
            }

            diller.IsLoose = VictoryConditions.IsLosing(diller, player);
            diller.EndTurn = true;
            GameProvider.ExitGame(diller, player);
        }

        internal void ChangeHand(Gambler player)
        {
            if (player.Type == PlayerType.Player)
            {
                PlayerHand(player, null);
                return;
            }
            DillerHand(player);
        }

        private void PlayerHand(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, HardHandStyle},
                {EventMessageConst.NoKey, null},
                {EventMessageConst.Default, PlayerHand}
            };

            Game.eventMessage.HandleGameEvent(EventMessageConst.ChangeHandMessage);
            Game.eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        private static void HardHandStyle(Gambler player, Gambler diller)
        {
            player.Style = HandStyle.Hard;
            for (int i = 0; i < player.playerCards.Count; i++)
            {
                if (player.playerCards[i].Rank == Rank.Ace)
                {
                    player.playerCards[i].Point = (int)Rank.AceByHardHand;
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