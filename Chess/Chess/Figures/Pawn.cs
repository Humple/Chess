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

                if (firstStepFlag)

                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                return available;
            }

            public override List<Position> GetAvailableAtackPositons (Position currentPos, CoreMatrix matrix)
			{
				List<Position> available = new List<Position> ();
				int m;

				//white figures below
				if (Color == Chess.Figures.FigureColor.WHITE)
					m = -1;
				else  //black fugures upwardly
					m = 1;

				FigureColor enemyColor = ( Color == Chess.Figures.FigureColor.WHITE )?(FigureColor.BLACK):(FigureColor.WHITE);
				int enemyRow = ( enemyColor == FigureColor.BLACK ) ? ( 3 ): ( 4 );

				Position pos = new Position ();
				pos.X = currentPos.X;
				pos.Y = currentPos.Y + 1 * m;

				if (CanMove (pos.X, pos.Y, matrix))
					available.Add (pos);
				//simple dioganal atack //
				if (CanMove (currentPos.X + 1, currentPos.Y + 1 * m, matrix) &&
					matrix.HasFigureAt (currentPos.X + 1, currentPos.Y + 1 * m))
					available.Add (new Position (currentPos.X + 1, currentPos.Y + 1 * m));

				if (CanMove (currentPos.X - 1, currentPos.Y + 1 * m, matrix)) {
					if (matrix.HasFigureAt (currentPos.X - 1, currentPos.Y + 1 * m))
						available.Add (new Position (currentPos.X + 1, currentPos.Y + 1 * m));
				}
				// ************ //

				// pass on atack //
				if( currentPos.Y == enemyRow ) {
					if (IsInField (currentPos.X - 1, currentPos.Y + 1 * m) )
						if(matrix.HasFigureAt (currentPos.X - 1, currentPos.Y)) {
							Figure fig = matrix.FigureAt( currentPos.X - 1,  currentPos.Y );
							if( fig is Pawn ) {
							Pawn p = fig as Pawn;
								if( p.TwoStepState && p.Color == enemyColor )
									if (!matrix.HasFigureAt (currentPos.X - 1, currentPos.Y + 1 * m))
										available.Add (new Position (currentPos.X - 1, currentPos.Y + 1 * m));
							}
					}
			
					if (IsInField (currentPos.X + 1, currentPos.Y + 1 * m) )
						if(matrix.HasFigureAt (currentPos.X + 1, currentPos.Y)) {
							Figure fig = matrix.FigureAt( currentPos.X + 1,  currentPos.Y );
							if( fig is Pawn ) {
								if( (fig as Pawn).TwoStepState && fig.Color == enemyColor )
									if (!matrix.HasFigureAt (currentPos.X + 1, currentPos.Y + 1 * m))
										available.Add (new Position (currentPos.X + 1, currentPos.Y + 1 * m));
							}
					}
				}
			
				// ********* //
                if (firstStepFlag)
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;

                    if(CanMove( pos2.X, pos2.Y, matrix))
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
