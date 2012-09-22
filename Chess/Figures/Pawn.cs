using System.Collections.Generic;

namespace Chess
{
	namespace Figures
	{
		public class Pawn: Figure
		{
            public Pawn(FigureColor color)
                : base(color)
			{
				diff = true;
				moved = false;
				LoadBitmap ("pawn");
			}

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();
				int m;

				//white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
					m=-1;
				} else {  //black fugures upwardly
					m=1;
				}

				Position pos = new Position ();
				pos.X = currentPos.X;
				pos.Y = currentPos.Y + 1 * m;
				available.Add (pos);

				if (!IsMoved) {
					Position pos2 = new Position ();
					pos2.X = currentPos.X;
					pos2.Y = currentPos.Y + 2*m;
					available.Add (pos2);
				}

				return available;
			}

			public override List<Position> GetAvailableAtackPositons (Position currentPos)
			{
				List<Position> atack = GetAvailableMovePossitons (currentPos);

				int m;
				//white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
					m = -1;
				} else {  //black fugures upwardly
					m = 1;
				}

				int y = currentPos.Y + 1 * m;
				int x1 = currentPos.X + 1;
				int x2 = currentPos.X - 1;

				if (IsInField (x1, y)) {
					Position pos = new Position();
					pos.X  = x1;
					pos.Y = y;
					atack.Add( pos );
				}

				if (IsInField (x2, y)) {
					Position pos = new Position();
					pos.X  = x2;
					pos.Y = y;
					atack.Add( pos );
				}


				return atack;
			}

		}
	}
}
