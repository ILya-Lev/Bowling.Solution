using System.Collections.Generic;

namespace BusinessLogic.CalculationCommands
{
	internal abstract class PointCalculationCommand
	{
		public abstract bool CanCalculate(FramePoints frame);
		public abstract int Calculate(int frameNumber, IReadOnlyList<FramePoints> game);

		protected const int AllSkittles = 10;
		protected static bool IsLastFrameOfTheGame(int frameNumber, IReadOnlyCollection<FramePoints> game)
		{
			return frameNumber == game.Count;
		}
	}
}