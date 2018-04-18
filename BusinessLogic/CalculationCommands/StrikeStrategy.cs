using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.CalculationCommands
{
	internal class StrikeStrategy : PointCalculationCommand
	{
		public override bool CanCalculate(FramePoints frame) => frame.FirstRun == AllSkittles;

		public override int Calculate(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			if (IsLastFrameOfTheGame(frameNumber, game))
			{
				var currentFrame = game.Last();
				return AllSkittles + currentFrame.ThirdRun + currentFrame.FourthRun;
			}

			var nextFrame = game[frameNumber];
			if (IsStrike(nextFrame))
			{
				if (frameNumber < game.Count - 1)
					return AllSkittles + nextFrame.FirstRun + game[frameNumber + 1].FirstRun;
				return AllSkittles + nextFrame.FirstRun + nextFrame.ThirdRun;//here next frame is the last one
			}
			return AllSkittles + nextFrame.FirstRun + nextFrame.SecondRun;
		}
		private bool IsStrike(FramePoints nextFrame) => CanCalculate(nextFrame);
	}
}