using BusinessLogic.CalculationCommands;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
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
			return _pointsStrategy.First(s => s.CanCalculate(currentFrame)).Calculate(frameNumber, _game);
		}

		private readonly IReadOnlyCollection<PointCalculationCommand> _pointsStrategy = new PointCalculationCommand[]
		{
			new StrikeStrategy(),
			new SpareStrategy(),
			new PlainStrategy()
		};
	}
}