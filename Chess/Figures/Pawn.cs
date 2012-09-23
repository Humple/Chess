using System.Collections.Generic;

namespace Chess
{
    namespace Figures
    {
        public class Pawn : Figure
        {
            private bool firstStepFlag = true;
            protected bool FirstStepFlag
            {
                get
                {
                    bool tmp = firstStepFlag;
                    firstStepFlag = false;
                    return tmp;
                }
            }
            public Pawn(FigureColor color)
                : base(color)
            {
                diff = true;
                moved = false;
                LoadBitmap("pawn");
            }

            public override List<Position> GetAvailableMovePossitons(Position currentPos)
            {
                List<Position> available = new List<Position>();
                int m;

                //white figures below
                if (Color == Chess.Figures.FigureColor.WHITE)
                {
                    m = -1;
                }
                else
                {  //black fugures upwardly
                    m = 1;
                }

                Position pos = new Position();
                pos.X = currentPos.X;
                pos.Y = currentPos.Y + 1 * m;
                available.Add(pos);

                if (!IsMoved)
                {
                    Position pos2 = new Position();
                    pos2.X = currentPos.X;
                    pos2.Y = currentPos.Y + 2 * m;
                    available.Add(pos2);
                }

                return available;
            }

            public override List<Position> GetAvailableAtackPositons(Position currentPos, CoreMatrix matrix)
            {
                List<Position> available = new List<Position>();
                int i, j = 0;
                if (FirstStepFlag) i = 2;
                else i = 1;
                while (j++ != i)
                {
                    switch (this.Color)
                    {
                        case FigureColor.WHITE:
                            {
                                if (IsInField(currentPos.X, currentPos.Y - j) && matrix.FigureAt(currentPos.X, currentPos.Y - j) == null)
                                    available.Add(new Position(currentPos.X, currentPos.Y - j));
                            } break;
                        case FigureColor.BLACK:
                            {
                                if (IsInField(currentPos.X, currentPos.Y + j) && matrix.FigureAt(currentPos.X, currentPos.Y + j) == null)
                                    available.Add(new Position(currentPos.X, currentPos.Y + j));
                            } break;
                    }
                }
                switch (this.Color)
                {
                    case FigureColor.WHITE:
                        {
                            if (IsInField(currentPos.X - 1, currentPos.Y - 1) && matrix.FigureAt(currentPos.X - 1, currentPos.Y - 1) != null && matrix.FigureAt(currentPos.X - 1, currentPos.Y - 1).Color != this.Color)
                                available.Add(new Position(currentPos.X - 1, currentPos.Y - 1));
                            if (IsInField(currentPos.X + 1, currentPos.Y - 1) && matrix.FigureAt(currentPos.X + 1, currentPos.Y - 1) != null && matrix.FigureAt(currentPos.X + 1, currentPos.Y - 1).Color != this.Color)
                                available.Add(new Position(currentPos.X + 1, currentPos.Y - 1));
                        } break;
                    case FigureColor.BLACK:
                        {
                            if (IsInField(currentPos.X - 1, currentPos.Y + 1) && matrix.FigureAt(currentPos.X - 1, currentPos.Y + 1) != null && matrix.FigureAt(currentPos.X - 1, currentPos.Y + 1).Color != this.Color)
                                available.Add(new Position(currentPos.X - 1, currentPos.Y + 1));
                            if (IsInField(currentPos.X + 1, currentPos.Y + 1) && matrix.FigureAt(currentPos.X + 1, currentPos.Y + 1) != null && matrix.FigureAt(currentPos.X + 1, currentPos.Y + 1).Color != this.Color)
                                available.Add(new Position(currentPos.X + 1, currentPos.Y + 1));
                        } break;
                }
                return available;
            }

        }
    }
}
