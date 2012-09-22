using System;

namespace Chess
{
	namespace Figures
	{
		public enum FigureColor { BLACK, WHITE };

		public class Position: Object
		{
			public Position (int x, int y)
			{
				this.x = x;
				this.y = y;
                errFlag = false;
			}

			public Position()
			{
                this.x = 0;
                this.y = 0;
                errFlag = false;
			}

            private bool errFlag;
            public bool errorFlag { 
                get 
                {
                    bool tmp = errFlag;
                    errFlag = false;
                    return tmp;
                }
            }
			private int x;
			public int X {
				get {
					return x;
				}
				set {
                    if (value < 8 && value >= 0)
                    {
                        x = value;
                        errFlag = false;
                    }
                    else errFlag = true;
				}
			}

			private int y;
			public int Y {
				get {
					return y;
				}
				set {
                    if (value < 8 && value >= 0)
                    {
                        y = value;
                        errFlag = false;
                    }
                    else errFlag = true;
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
            public override int GetHashCode()
            {
                return 0;
            }

		}
	}
}
