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
                return;
            }

            HandleGameEvent(EventMessageConst.InvalidInputMessage);
            choiseOperations[EventMessageConst.Default](player, diller);
        }

        internal void WorkWithMoneyDictionary(Gambler player, Dictionary<string, Action<Gambler>> choiseOperations)
        {
            string choise = Console.ReadLine().ToUpper();

            if (choiseOperations.ContainsKey(choise) & Regex.Match(choise, Game.regularExpression).Success)
            {
                choiseOperations[choise](player);
                return;
            }

            HandleGameEvent(EventMessageConst.InvalidInputMessage);
            choiseOperations[EventMessageConst.Default](player);
        }
    }
}