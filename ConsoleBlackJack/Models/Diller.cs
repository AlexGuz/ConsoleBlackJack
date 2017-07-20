using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Diller : Gambler
    {
        public int dillerMaxHandPoint = 17;
        public Diller(PlayerType diller) : base(diller)
        { }
    }
}