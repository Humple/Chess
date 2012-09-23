using System;
using Chess.Figures;

namespace Chess
{
	public interface IGameControl
	{
		void FigureMoved(Position oldPos, Position newPos);
		void SpotSelected(Position spotPos);
        bool SpotFocused(Position spotPos);
		void StartButtonClicked();
		void StopButtonClicked();
	}
}

