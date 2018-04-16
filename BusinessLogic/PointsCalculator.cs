using System;
using System.Collections.Generic;
using System.Linq;

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

	internal sealed class PointsCalculationStrategy
	{
		private readonly IReadOnlyList<FramePoints> _game;

		public PointsCalculationStrategy(IReadOnlyList<FramePoints> game)
		{
			_game = game;
		}

		public int GetCurrentFramePoints(int frameNumber)
		{
			var currentFrame = _game[frameNumber - 1];
			return _pointsStrategy.First(s => s.Key(currentFrame)).Value(frameNumber, _game);
		}

		private const int AllSkittles = 10;
		private readonly IReadOnlyDictionary<Func<FramePoints, bool>, Func<int, IReadOnlyList<FramePoints>, int>>
			_pointsStrategy = new Dictionary<Func<FramePoints, bool>, Func<int, IReadOnlyList<FramePoints>, int>>
			{
				[fp => fp.FirstRun == AllSkittles] = StrikePoints,
				[fp => fp.FirstRun + fp.SecondRun == AllSkittles] = SparePoints,
				[fp => true] = StandardPoints
			};

		private static int StrikePoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (IsLastFrameOfTheGame(frameNumber, game))
			{
				var currentFrame = game.Last();
				return AllSkittles + currentFrame.ThirdRun + currentFrame.FourthRun;
			}

			var nextFrame = game[frameNumber];
			return AllSkittles + nextFrame.FirstRun + nextFrame.SecondRun;
		}

		private static int SparePoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (IsLastFrameOfTheGame(frameNumber, game))
			{
				var currentFrame = game.Last();
				return AllSkittles + currentFrame.ThirdRun;
			}

			var nextFrame = game[frameNumber];
			return AllSkittles + nextFrame.FirstRun;
		}

		private static int StandardPoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			var currentFrame = game[frameNumber - 1];
			return currentFrame.FirstRun + currentFrame.SecondRun;
		}

		private static bool IsLastFrameOfTheGame(int frameNumber, IReadOnlyCollection<FramePoints> game)
		{
			return frameNumber == game.Count;
		}
	}
}
