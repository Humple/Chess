using System.Collections.Generic;

namespace Chess
{
    namespace Figures
    {

        public class Rock : Figure
        {
            public Rock(FigureColor color)
                : base(color)
            {
                diff = false;
                moved = false;
                LoadBitmap("rock");
            }

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
            {
                List<Position> available = new List<Position>();
                for (int i = 0; i < 8; i++)
                {
                    if (currentPos.X != i)
                        available.Add(new Position(i, currentPos.Y));
                    if (currentPos.Y != i)
                        available.Add(new Position(currentPos.X, i));
                }
                return available;
            }
            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();
                for (int i = currentPos.X + 1; i < 8; i++)
                {
                    if (matrix.FigureAt(i, currentPos.Y) == null) available.Add(new Position(i, currentPos.Y));
                    else if (matrix.FigureAt(i, currentPos.Y).Color != this.Color)
                    {
                        available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                }
                for (int i = currentPos.X - 1; i >= 0; i--)
                {
                    if (matrix.FigureAt(i, currentPos.Y) == null) available.Add(new Position(i, currentPos.Y));
                    else if (matrix.FigureAt(i, currentPos.Y).Color != this.Color)
                    {
                        available.Add(new Position(i, currentPos.Y));
                        break;
                    }
                }
                for (int j = currentPos.Y + 1; j < 8; j++)
                {
                    if (matrix.FigureAt(currentPos.X, j) == null) available.Add(new Position(currentPos.X, j));
                    else if (matrix.FigureAt(currentPos.X, j).Color != this.Color)
                    {
                        available.Add(new Position(currentPos.X, j));
                        break;
                    }
                }
                for (int j = currentPos.Y - 1; j >= 0; j--)
                {
                    if (matrix.FigureAt(currentPos.X, j) == null) available.Add(new Position(currentPos.X, j));
                    else if (matrix.FigureAt(currentPos.X, j).Color != this.Color)
                    {
                        available.Add(new Position(currentPos.X, j));
                        break;
                    }
                    else break;
                }
                return available;
            }
        }
    }
}
