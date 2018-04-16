using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public class PointsCalculator
	{
		private readonly IReadOnlyList<FramePoints> _game;

		public PointsCalculator(IReadOnlyList<FramePoints> game)
		{
			_game = game;
		}

		public int GetPointsUpTo(int frameNumber)
		{
			if (frameNumber <= 0 || frameNumber > _game.Count)
				throw new ArgumentOutOfRangeException($"Frame number should be from 1 to 10, but received {frameNumber}");

			if (frameNumber == 1)
				return GetCurrentFramePoints(frameNumber);

			return GetCurrentFramePoints(frameNumber) + GetPointsUpTo(frameNumber - 1);
		}

		public int GetCurrentFramePoints(int frameNumber)
		{
			var currentFrame = _game[frameNumber - 1];
			return _pointsStrategy.First(s => s.Key(currentFrame)).Value(frameNumber, _game);
		}

		private readonly IReadOnlyDictionary<Func<FramePoints, bool>, Func<int, IReadOnlyList<FramePoints>, int>> _pointsStrategy
			= new Dictionary<Func<FramePoints, bool>, Func<int, IReadOnlyList<FramePoints>, int>>
			{
				[fp => fp.FirstRun == SkittleAmount] = StrikePoints,
				[fp => fp.FirstRun + fp.SecondRun == SkittleAmount] = SparePoints,
				[fp => true] = StandardPoints
			};

		private const int SkittleAmount = 10;

		private static int StrikePoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (frameNumber == game.Count)
				return SkittleAmount + game.Last().ThirdRun + game.Last().FourthRun;

			return SkittleAmount + game[frameNumber].FirstRun + game[frameNumber].SecondRun;
		}

		private static int SparePoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (frameNumber == game.Count)
				return SkittleAmount + game.Last().ThirdRun;
			return SkittleAmount + game[frameNumber].FirstRun;
		}

		private static int StandardPoints(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			return game[frameNumber - 1].FirstRun + game[frameNumber - 1].SecondRun;
		}
	}
}
