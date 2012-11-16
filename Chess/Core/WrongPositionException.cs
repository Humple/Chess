using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Figures;

namespace Chess.Core
{
    public class WrongPositionException: ApplicationException
    {
        private Position pos;
        public Position NewPosition
        {
            get
            {
                return pos;
            }
        }
        WrongPositionException()
        {
            pos = null;
        }
        WrongPositionException(Position position)
        {
            pos = position;
        }
    }
}
