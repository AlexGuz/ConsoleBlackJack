using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class EventMessage
    {
        internal const string StartGameMessage = "Start new game? y - yes, n - no";
        internal const string EndGameMessage = "See you!";
        internal const string AddCardeMessage = "Add one card? y - yes, n - no";
        internal const string ChangeHandMessage = "Change hand? y - yes, n - no";
        internal const string InvalidInputMessage = "Invalid Input";
        internal const string NotEnoughMoney = "You've lost everything. Send home for money? y - yes, n - no";
        internal const string YesKey = "Y";
        internal const string NoKey = "N";
        internal const string Default = "Default";



        public event EventHandler<string> OngameEvent;

        public virtual void HandleGameEvent(string e)
        {
            EventHandler<string> handler = OngameEvent;

            handler?.Invoke(this, e);
        }

        internal void WorkWithGamblerDictionary(Gambler player, Gambler diller, Dictionary<string, Action<Gambler, Gambler>> choiseOperations)
        {
            string choise = Console.ReadLine().ToUpper();

            if (choiseOperations.ContainsKey(choise) & Regex.Match(choise, Game.regularExpression).Success)
            {
                choiseOperations[choise](player, diller);
            }
            else
            {
                HandleGameEvent(InvalidInputMessage);
                choiseOperations[Default](player, diller);
            }
        }

        internal void WorkWithMoneyDictionary(Gambler player, Dictionary<string, Action<Gambler>> choiseOperations)
        {
            string choise = Console.ReadLine().ToUpper();

            if (choiseOperations.ContainsKey(choise) & Regex.Match(choise, Game.regularExpression).Success)
            {
                choiseOperations[choise](player);
            }
            else
            {
                HandleGameEvent(InvalidInputMessage);
                choiseOperations[Default](player);
            }
        }
    }
}