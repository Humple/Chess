namespace Chess
{
	namespace Figure
	{
		public enum Color { BLACK, WHITE };

		public class Position
		{
			public Position (int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			public Position()
			{

			}

			private int x;
			public int X {
				get {
					return x;
				}
				set {
					if (value < 9 && value > 0)
						x = value;
				}
			}

			private int y;
			public int Y {
				get {
					return y;
				}
				set {
					if (value < 9 && value > 0)
						y = value;
				}
			}


			public static bool operator==(Position first, Position second)
			{
				return ( first.X == second.X && first.Y == second.Y);
			
			}

			public static bool operator!=(Position first, Position second)
			{
				return ( first.X != second.X || first.Y != second.Y);
			
			}

			public override bool Equals (object obj)
			{
				return ( X == ((Position) obj).X) && ( Y == ((Position) obj).Y);
			}

		}
	}
}
