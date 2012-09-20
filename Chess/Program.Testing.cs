using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Chess.Figure;



namespace Chess
{
    static class Program
    {
        static void Main()
        {
			Position somePostion = new Position(5, 5);
			Chess.Figure.King king = new Chess.Figure.King(Chess.Figure.Color.BLACK);
			king.ConsolePrintPosition(somePostion);
			System.Console.WriteLine ("Black\n\n");
			new Knight(Color.BLACK).ConsolePrintPosition(somePostion);
			new Queen(Color.BLACK).ConsolePrintPosition(somePostion);
			new Rock(Color.BLACK).ConsolePrintPosition(somePostion);
			new Bishop(Color.BLACK).ConsolePrintPosition(somePostion);
			new Pawn(Color.BLACK).ConsolePrintPosition(somePostion);
			System.Console.WriteLine ("White\n\n");
			new Knight(Color.WHITE).ConsolePrintPosition(somePostion);
			new Queen(Color.WHITE).ConsolePrintPosition(somePostion);
			new Rock(Color.WHITE).ConsolePrintPosition(somePostion);
			new Bishop(Color.WHITE).ConsolePrintPosition(somePostion);
			new Pawn(Color.WHITE).ConsolePrintPosition(somePostion);
		}
    }
}
