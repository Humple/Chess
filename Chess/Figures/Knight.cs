using System.Collections.Generic;

namespace Chess
{
	namespace Figures
	{

		public class Knight: Figure
		{
            public Knight(FigureColor color)
                : base(color)
			{
				diff = false;
				moved = false;
				LoadBitmap("knight");
			}

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();

				int x;
				int y;

				//search algoritm
				x = currentPos.X + 1;
				y = currentPos.Y + 2;
				if (IsInField (x, y)) 
					available.Add (new Position(x, y));
			
				x=currentPos.X-1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-2;
				x=currentPos.X+1;
				if( IsInField(x, y) )
					available.Add ( new Position(x, y) );

				x=currentPos.X-1;
				if( IsInField(x, y) )
					available.Add ( new Position(x, y) );

				x=currentPos.X+2;
				y=currentPos.Y+1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
				if( IsInField(x, y) )
					available.Add ( new Position(x,y) );

				x=currentPos.X-2;
				y=currentPos.Y+1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );

				y=currentPos.Y-1;
				if(IsInField(x, y))
					available.Add ( new Position(x, y) );
				//end search algoritm

				return available;
			}
		}
	}
}

