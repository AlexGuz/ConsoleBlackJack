using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    static class EventMessageConst
    {
        internal const string StartGameMessage = "Start new game? y - yes, n - no";
        internal const string EndGameMessage = "See you!";
        internal const string AddCardMessage = "Add one card? y - yes, n - no";
        internal const string ChangeHandMessage = "Change hand? y - yes, n - no";
        internal const string InvalidInputMessage = "Invalid Input";
        internal const string NotEnoughMoney = "You've lost everything. Send home for money? y - yes, n - no";
        internal const string YesKey = "Y";
        internal const string NoKey = "N";
        internal const string Default = "Default";
    }
}