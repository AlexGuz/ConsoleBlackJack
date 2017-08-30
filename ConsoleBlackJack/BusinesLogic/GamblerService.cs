using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class GamblerService
    {
        internal void PlayerGame(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, GameProvider.NextTurnGame},
                {EventMessageConst.NoKey, DillerGame},
                {EventMessageConst.Default, PlayerGame}
            };

            Game.eventMessage.HandleGameEvent(EventMessageConst.AddCardMessage);
            Game.eventMessage.WorkWithGamblerDictionary(player, diller, choiseOperations);
        }

        internal void DillerGame(Gambler player, Gambler diller)
        {
            player.EndTurn = true;
            player.IsLoose = VictoryConditions.IsLosing(player);
            DillerTurn(diller, player);
        }

        internal void DillerTurn(Gambler diller, Gambler player)
        {
            if (diller.PlayerPoint < GamblerConst.DillerMaxHandPoint)
            {
                GameProvider.NextTurnGame(diller, player);
                return;
            }

            diller.IsLoose = VictoryConditions.IsLosing(diller);
            diller.EndTurn = true;
            GameProvider.ExitGame(player, diller);
        }

        internal void ChangeHand(Gambler player, Gambler diller)
        {
            if (player.Type == PlayerType.Player)
            {
                PlayerHand(player, diller);
                return;
            }
            DillerHand(player);
        }

        internal void ClearGamblerData(Gambler player)
        {
            player.Bet = 0;
            player.EndTurn = false;
            player.IsLoose = false;
            player.PlayerPoint = 0;
            player.Style = HandStyle.Soft;
            player.playerCards.Clear();
        }

        private void PlayerHand(Gambler player, Gambler diller)
        {
            Dictionary<string, Action<Gambler, Gambler>> choiseOperations = new Dictionary<string, Action<Gambler, Gambler>>
            {
                {EventMessageConst.YesKey, HardHandStyle},
                {EventMessageConst.NoKey, PlayerGame},
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
            if (diller.PlayerPoint > GamblerConst.DillerMaxHandPoint)
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