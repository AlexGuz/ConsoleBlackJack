using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    internal class ConsoleWriteHelper
    {
        private EventMessage _eventMessage;

        public ConsoleWriteHelper(EventMessage eventMessage)
        {
            _eventMessage = eventMessage;
            _eventMessage.OngameEvent += OnWriteMessageOnConsole;
        }

        public void OnWriteMessageOnConsole(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}