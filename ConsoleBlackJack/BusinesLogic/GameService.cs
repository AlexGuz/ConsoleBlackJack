using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class GameService
    {
        private CardService cardService = new CardService();

        private const string StartGameMessage = "Start new game? y - yes, n - no";
        private const string EndGameMessage = "See you!";
        private const string AddCardeMessage = "Add one card? y - yes, n - no";
        private const string ChangeHandMessage = "Change hand? y - yes, n - no";
        private const string InvalidInputMessage = "Invalid Input";
        private const string YesKey = "Y";
        private const string NoKey = "N";
        public event EventHandler<string> OngameEvent;

        protected virtual void HandleGameEvent(string e)
        {
            EventHandler<string> handler = OngameEvent;

            handler?.Invoke(this, e);
        }

        public void NewGameSelector<T>(T player, T diller) where T : Gambler
        {
            bool isNewGameStart = true;
            while (isNewGameStart)
            {
                HandleGameEvent(StartGameMessage);
                string choise = Console.ReadLine();

                switch (choise.ToUpper())
                {
                    case YesKey:
                        Game game = new Game();
                        game.FirstDistribution();
                        isNewGameStart = false;
                        break;
                    case NoKey:
                        HandleGameEvent(EndGameMessage);
                        Console.ReadLine();
                        Environment.Exit(0);
                        break;
                    default:
                        HandleGameEvent(InvalidInputMessage);
                        break;
                }
            }
        }

        internal static void CardCounter<T>(T player) where T : Gambler
        {
            int points = 0;

            foreach (var card in player.playerCards)
            {
                points += card.Point;
            }
            player.PlayerPoint = points;
        }

        internal void GotABlackJack<T>(T player, T diller) where T : Gambler
        {
            if (IsAceOnHend(player.playerCards) & IsFigureOnHend(player.playerCards))
            {
                Console.WriteLine($"BjackJack!!!!!!!!!!!!!{player.Name} Victory!!!");
                NewGameSelector(player, diller);
            }
        }

        internal void StartGame<T>(T player, T diller) where T : Gambler
        {
            Console.Clear();
            cardService.ShowCards(player);
            cardService.ShowCards(diller);

            if (IsAceOnHend(player.playerCards) & player.Style == HandStyle.Soft & !player.EndTurn)
            {
                ChangeHand<Gambler>(player);
                cardService.ShowCards(player);
            }
            OneTurn(player, diller);
        }

        private void ChangeHand<T>(T player) where T : Gambler
        {
            if (player.Type == PlayerType.Player)
            {
                PlayerHand(player);
            }
            else
            {
                DillerHand(player);
            }
        }

        private void PlayerHand<T>(T player) where T : Gambler
        {
            bool isChangeStyle = true;
            while (isChangeStyle)
            {
                string choise = string.Empty;
                HandleGameEvent(ChangeHandMessage);

                choise = Console.ReadLine();

                switch (choise.ToUpper())
                {
                    case YesKey:
                        player.Style = HandStyle.Hard;
                        for (int i = 0; i < player.playerCards.Count; i++)
                        {
                            if (player.playerCards[i].Rank == Rank.Ace)
                            {
                                player.playerCards[i].Point = 1;
                                break;
                            }
                        }
                        isChangeStyle = false;
                        break;
                    case NoKey:
                        isChangeStyle = false;
                        break;
                    default:
                        HandleGameEvent(InvalidInputMessage);
                        break;
                }
            }
        }

        private void DillerHand<T>(T player) where T : Gambler
        {
            Diller diller = (Diller)((Gambler)player);
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

        private void OneTurn<T>(T player, T diller) where T : Gambler
        {
            if (player.Type == PlayerType.Player)
            {
                PlayerGame(diller, player);
            }
            else
            {
                DillerGame(player, diller);
            }
        }

        private void DillerGame<T>(T player, T diller) where T : Gambler
        {
            if (player.PlayerPoint < ((Diller)((Gambler)player)).dillerMaxHandPoint)
            {
                player.playerCards.Add(cardService.AddCard(ref Game.deck));
                StartGame(player, diller);
            }
            else if (diller.EndTurn)
            {
                VictoryConditions(player, diller);
            }
        }

        private void PlayerGame<T>(T diller, T player) where T : Gambler
        {
            bool isPlay = true;
            while (isPlay)
            {
                HandleGameEvent(AddCardeMessage);
                string choise = Console.ReadLine();

                switch (choise.ToUpper())
                {
                    case YesKey:
                        player.playerCards.Add(cardService.AddCard(ref Game.deck));
                        StartGame(player, diller);
                        isPlay = false;
                        break;
                    case NoKey:
                        player.EndTurn = true;
                        if (diller.PlayerPoint < ((Diller)((Gambler)diller)).dillerMaxHandPoint)
                        {
                            diller.playerCards.Add(cardService.AddCard(ref Game.deck));
                            StartGame(diller, player);
                            isPlay = false;
                        }
                        else
                        {
                            VictoryConditions(player, diller);
                        }
                        break;
                    default:
                        HandleGameEvent(InvalidInputMessage);
                        break;
                }
            }
        }

        private void VictoryConditions<T>(T player, T diller) where T : Gambler
        {
            if (player.PlayerPoint > diller.PlayerPoint & !IsLosing(player))
            {
                Console.WriteLine($"{player.Name} Victory!!!");
                NewGameSelector(player, diller);
            }
            else if (IsLosing(player))
            {
                Console.WriteLine($"{player.Name} Bust! The game is over");
                NewGameSelector(player, diller);
            }
        }

        private static bool IsLosing<T>(T player) where T : Gambler
        {
            bool isLosing = player.PlayerPoint > player.maxPoint;

            return isLosing;
        }

        private static bool IsAceOnHend(List<Card> playerCards)
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

        private static bool IsFigureOnHend(List<Card> playerCards)
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