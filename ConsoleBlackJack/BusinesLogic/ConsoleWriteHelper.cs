using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    internal class ConsoleWriteHelper
    {
        private EventMessage eventMessage;

        public ConsoleWriteHelper(EventMessage eventMessage)
        {
            this.eventMessage = eventMessage;
            this.eventMessage.OngameEvent += OnWriteMessageOnConsole;
        }

        public void OnWriteMessageOnConsole(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}