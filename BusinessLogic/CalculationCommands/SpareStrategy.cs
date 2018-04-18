using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.CalculationCommands
{
	internal class SpareStrategy : PointCalculationCommand
	{
		public override bool CanCalculate(FramePoints frame) => frame.FirstRun + frame.SecondRun == AllSkittles;

		public override int Calculate(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (IsLastFrameOfTheGame(frameNumber, game))
			{
				var currentFrame = game.Last();
				return AllSkittles + currentFrame.ThirdRun;
			}

			var nextFrame = game[frameNumber];
			return AllSkittles + nextFrame.FirstRun;
		}
	}
}