using System;

namespace Chess
{

	public enum FigureColor { BLACK, WHITE };

	public class Figure
	{
		private Chess.FigureColor color;

		public FigureColor Color
		{
			get
			{
				return color;
			}
		}

		public Figure (FigureColor acolor)
		{
			this.color = acolor;
		}
	}
}

