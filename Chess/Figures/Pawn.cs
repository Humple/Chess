using System.Collections.Generic;
using Chess.Core;

namespace Chess
{
    namespace Figures
    {
        public class Pawn : Figure
        {
			public bool TwoStepState = false;
            public Figure[] NeighborsFigures;

            public Pawn(FigureColor color)
                : base(color)
            {
                diff = true;

                NeighborsFigures = new Figure[2];
                NeighborsFigures[0] = null;
                NeighborsFigures[1] = null;

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

                if (IsMoved)
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

                if (!IsMoved && !matrix.HasFigureAt(pos))
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                //pawn pass implementation
                foreach( int k in new int[]{1, -1}  )
                    if ( matrix.HasFigureAt(pos.X + k, pos.Y-1)) {
                        Figure neighFigure = matrix.FigureAt(pos.X + k, pos.Y-1);
                        //in last move state neighbors figure is not Pawn
                        if (!(NeighborsFigures[(k==-1)?0:1] is Pawn))
                        {
                            //now neighbors figure is Pawn
                            if (neighFigure.Color != Color)
                            {
                                //It's enemy Pawn
                                if (neighFigure is Pawn)
                                {
                                    //Enemy pawn moved two step
                                    if (((Pawn)neighFigure).TwoStepState)
                                    {
                                        available.Add(new Position(pos.X + k, pos.Y));
                                    }
                                }
                            }
                        }
                    }

                if (matrix.HasFigureAt(pos.X + 1, pos.Y)) {
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
