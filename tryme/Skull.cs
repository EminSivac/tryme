using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryme
{
    internal class Skull
    {
        string _open = "          _______\r\n       .-\"       \"-.\r\n      /             \\\r\n     |               |\r\n     |,  .-.   .-.  ,|\r\n     | )(__/   \\__)( |\r\n     |/     /\\      \\|\r\n     (_     ^^     _)\r\n      \\__|IIIIII|__/\r\n       |   ----   |\r\n       \\          /\r\n        `--------`\r\n";
        string _closed = "          _______\r\n       .-\"       \"-.\r\n      /             \\\r\n     |               |\r\n     |,  .-.   .-.  ,|\r\n     | )(__/   \\__)( |\r\n     |/     /\\      \\|\r\n     (_     ^^     _)\r\n      \\__|IIIIII|__/\r\n       |  \\____/  |\r\n       \\  /    \\  /\r\n        `--------`\r\n";

        public Skull(int delay,int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                Console.SetCursorPosition(0, 4);
                Console.Write(_open);
                Thread.Sleep(delay);

                Console.SetCursorPosition(0, 4);
                Console.Write(_closed);
                Thread.Sleep(delay);
            }
        }


    }
}
