using System;
using System.Collections.Generic;

namespace Chess
{
	namespace Figure
	{

		public class King: Figure {

			public King (Color color): base(color)	{
				diff = false;
				moved = false; 
				LoadBitmap("king");
			}
		

			public override List<Position> GetAvailableMovePossitons (Position currentPos)
			{
				List<Position> available = new List<Position> ();

				for (int i=-1; i<2; i++) {
					for (int j=-1; j<2; j++) {
						if(j!= 0 ||i != 0 ){
							if( IsInField( currentPos.X + i, currentPos.Y + j) )
								available.Add ( new Position(currentPos.X + i, currentPos.Y + j) );
						}
					}
				}
				return available;
			}

			public override List<Position> GetAvailableAtackPositons (Position currentPos) {
				return GetAvailableMovePossitons(currentPos);
			}
		}
	}
}
