using System;
using System.Collections.Generic;

namespace BusinessLogic
{
	public class PointsCalculator
	{
		private readonly IReadOnlyList<FramePoints> _game;
		private readonly PointsCalculationStrategy _calculationStrategy;

		public PointsCalculator(IReadOnlyList<FramePoints> game)
		{
			_game = game;
			_calculationStrategy = new PointsCalculationStrategy(game);
		}

		public int GetPointsUpTo(int frameNumber)
		{
			if (frameNumber <= 0 || frameNumber > _game.Count)
				throw new ArgumentOutOfRangeException($"Frame number should be from 1 to 10, but received {frameNumber}");

			if (frameNumber == 1)
				return _calculationStrategy.GetCurrentFramePoints(frameNumber);

			return _calculationStrategy.GetCurrentFramePoints(frameNumber) + GetPointsUpTo(frameNumber - 1);
		}
	}
}
