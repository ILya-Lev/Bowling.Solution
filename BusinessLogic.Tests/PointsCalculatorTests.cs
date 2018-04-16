using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace BusinessLogic.Tests
{
	public class PointsCalculatorTests
	{
		[InlineData(1, 5)]
		[InlineData(2, 14)]
		[InlineData(3, 29)]
		[InlineData(4, 49)]
		[InlineData(5, 60)]
		[InlineData(6, 61)]
		[InlineData(7, 77)]
		[InlineData(8, 97)]
		[InlineData(9, 117)]
		[InlineData(10, 133)]
		[Theory]
		public void GetPointsFor_FirstFrame_5(int frameNumber, int expectedPoints)
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(4,5),
				new FramePoints(6,4),
				new FramePoints(5,5),
				new FramePoints(10,0),
				new FramePoints(0,1),
				new FramePoints(7,3),
				new FramePoints(6,4),
				new FramePoints(10,0),
				new FramePoints(2,8, 6),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(frameNumber);

			points.Should().Be(expectedPoints);
		}
	}
}
