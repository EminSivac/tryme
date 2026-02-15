using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryme
{
    internal class Skull
    {
        string _open = File.ReadAllText("open.txt");
        string _closed = File.ReadAllText("closed.txt");

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
