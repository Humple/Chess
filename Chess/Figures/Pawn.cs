using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
    namespace Figures
    {
        public class Pawn : Figure
        {
			public bool TwoStepState = false;
          
            public Pawn(FigureColor color)
                : base(color)
            {
                diff = true;
                LoadBitmap("pawn");
            }

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
            {
                List<Position> available = new List<Position>();
                int m;

                //white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
                    m = -1;
                }
                else
                {  //black fugures upwardly
                    m = 1;
                }

                Position pos = new Position();
                pos.X = currentPos.X;
                pos.Y = currentPos.Y + 1 * m;
                available.Add(pos);

                if (!IsMoved)
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                return available;
            }

            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();
                int m;

                //white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
                    m = -1;
                }
                else
                {  //black fugures upwardly
                    m = 1;
                }

                Position pos = new Position();
                pos.X = currentPos.X;
                pos.Y = currentPos.Y + 1 * m;
                available.Add(pos);

                if (!IsMoved)
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                return available;
            }

            public override string ToString()
            {
                return "pawn";
            }
        }
    }
}
