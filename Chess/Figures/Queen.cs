using System.Collections.Generic;

namespace Chess
{
	namespace Figures
	{

		public class Queen: Figure
		{
            public Queen(FigureColor color)
                : base(color)
			{
				diff = false;
				moved = false;
				LoadBitmap("queen");
			}

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
			{
				List<Position> available = new List<Position> ();
				//checking for vertical and horizontal move
				for (int i=0; i<8; i++) {
					if(currentPos.X!=i)
						available.Add( new Position(i, currentPos.Y) );
					if(currentPos.Y!= i)
						available.Add( new Position(currentPos.X, i) );
				}
				//end checking


				//for dioganaly move
				//checking available positions
				int x = currentPos.X+1;
				int y = currentPos.Y+1;

				while( IsInField( x, y) )
					available.Add ( new Position( x++, y++) );

				x = currentPos.X-1;
				y = currentPos.Y+1;

				while( IsInField( x, y) )
					available.Add ( new Position( x--, y++) );

				x = currentPos.X-1;
				y = currentPos.Y-1;

				while( IsInField( x, y) )
					available.Add ( new Position( x--, y--) );
		
				x = currentPos.X+1;
				y = currentPos.Y-1;

				while( IsInField( x, y) )
					available.Add ( new Position( x++, y--) );

				//end checking
				return available;
			}

		}
	}
}

