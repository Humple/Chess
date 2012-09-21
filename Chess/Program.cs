
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chess
{
    static class Program
    {
        static void Main()
        {	
			GameCore game = new GameCore();
			game.InitGame();
		}
    }
}
