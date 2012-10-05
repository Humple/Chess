
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chess
{
    static class Program
    {
        static void Main(string[] arguments)
        {	
			GameCore game = new GameCore();
			if(arguments.Count () < 0)
				game.Initialize();

			else if(arguments[0] == "client")
				game.Initialize(arguments[1]);
			else if(arguments[0]=="server")
				game.Initialize("0.0.0.0");
		}
    }
}
