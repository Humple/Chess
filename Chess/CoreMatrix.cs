using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess;
using Chess.Figures;

namespace Chess
{
    public class CoreMatrix: Object
    {
        private Figure[,] sMatrix;

        public CoreMatrix()
        {
            //WHITE
            sMatrix = new Figure[8, 8];

            sMatrix[0, 7] = new Rock(FigureColor.WHITE);
            sMatrix[7, 7] = new Rock(FigureColor.WHITE);

            sMatrix[6, 7] = new Knight(FigureColor.WHITE);
            sMatrix[1, 7] = new Knight(FigureColor.WHITE);

            sMatrix[2, 7] = new Bishop(FigureColor.WHITE);
            sMatrix[5, 7] = new Bishop(FigureColor.WHITE);

            sMatrix[4, 7] = new King(FigureColor.WHITE);
            sMatrix[3, 7] = new Queen(FigureColor.WHITE);

            for (int i = 0; i < 8; i++)
            {
                sMatrix[i, 6] = new Pawn(FigureColor.WHITE);
            }

            //BLACK
            sMatrix[0, 0] = new Rock(FigureColor.BLACK);
            sMatrix[7, 0] = new Rock(FigureColor.BLACK);

            sMatrix[6, 0] = new Knight(FigureColor.BLACK);
            sMatrix[1, 0] = new Knight(FigureColor.BLACK);

            sMatrix[2, 0] = new Bishop(FigureColor.BLACK);
            sMatrix[5, 0] = new Bishop(FigureColor.BLACK);

            sMatrix[4, 0] = new King(FigureColor.BLACK);
            sMatrix[3, 0] = new Queen(FigureColor.BLACK);

            for (int i = 0; i < 8; i++)
            {
                sMatrix[i, 1] = new Pawn(FigureColor.BLACK);
            }
        }
        public Figure FigureAt(Position pos)
        {
            return sMatrix[pos.X, pos.Y];
        }

		public bool HasFigureAt (Position pos)
		{
			return (sMatrix[pos.X, pos.Y] !=null);
		}
    }
}
