using System.Collections.Generic;

namespace BusinessLogic.CalculationCommands
{
	internal class PlainStrategy : PointCalculationCommand
	{
		public override bool CanCalculate(FramePoints frame) => true;

		public override int Calculate(int frameNumber, IReadOnlyList<FramePoints> game)
		{
			var currentFrame = game[frameNumber - 1];
			return currentFrame.FirstRun + currentFrame.SecondRun;
		}
	}
}