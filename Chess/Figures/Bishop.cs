using System.Collections.Generic;

namespace Chess
{
	namespace Figures
	{

		public class Bishop: Figure
		{
            public Bishop(FigureColor color)
                : base(color)
			{
				diff = false;
				moved = false;
				LoadBitmap("bishop");
			}

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();


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

				return available;
			}

		}
	}
}
