using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    internal class ConsoleWriteHelper
    {
        private GameService gameService;

        public ConsoleWriteHelper(GameService gameService)
        {
            this.gameService = gameService;
            this.gameService.OngameEvent += OnWriteMessageOnConsole;
        }

        public void OnWriteMessageOnConsole(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}