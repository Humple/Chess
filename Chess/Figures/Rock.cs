using System.Collections.Generic;

namespace Chess
{
	namespace Figure
	{

		public class Rock: Figure
		{
			public Rock (Color color): base(color)
			{
				diff = false;
				moved = false;
				LoadBitmap("rock");
			}

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();
				for (int i=0; i<8; i++) {
					if(currentPos.X!=i)
						available.Add( new Position(i, currentPos.Y) );
					if(currentPos.Y!= i)
						available.Add( new Position(currentPos.X, i) );
				}
				return available;
			}
		}
	}
}
