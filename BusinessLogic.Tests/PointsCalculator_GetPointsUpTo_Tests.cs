using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessLogic.Tests
{
	public class PointsCalculator_GetPointsUpTo_Tests
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
		public void AFrame_MatchExpectations(int frameNumber, int expectedPoints)
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

		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(11)]
		[InlineData(100)]
		[Theory]
		public void InvalidFrameNumber_ThrowsOutOfRange(int frameNumber)
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1, 4),
				new FramePoints(4, 5),
				new FramePoints(6, 4),
				new FramePoints(5, 5),
				new FramePoints(10, 0),
				new FramePoints(0, 1),
				new FramePoints(7, 3),
				new FramePoints(6, 4),
				new FramePoints(10, 0),
				new FramePoints(2, 8, 6),
			};
			var calculator = new PointsCalculator(game);

			Action getPoints = () => calculator.GetPointsUpTo(frameNumber);

			getPoints.Should().Throw<ArgumentOutOfRangeException>($"frame number must be from 1 to 10 but is {frameNumber}");
		}

		[Fact]
		public void SpareWithNoContinuation_OnlyActualPoints()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(5,5),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(15);
		}

		[Fact]
		public void SpareAndNextFrame_FullPoints()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(5,5),
				new FramePoints(4,2),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(19);
		}

		[Fact]
		public void SpareAndAdditionalRuns_SumOnly3rdRun()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(5, 5, 2, 1),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(17);
		}

		[Fact]
		public void StrikeWithNoContinuation_OnlyActualPoints()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(10,0),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(15);
		}

		[Fact]
		public void StrikeAndNextFrame_FullPoints()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(10,0),
				new FramePoints(4,2),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(21);
		}

		[Fact]
		public void StrikeAndAdditionalRuns_SumAll()
		{
			var game = new List<FramePoints>
			{
				new FramePoints(1,4),
				new FramePoints(10, 0, 2, 1),
			};
			var calculator = new PointsCalculator(game);

			var points = calculator.GetPointsUpTo(2);

			points.Should().Be(18);
		}
	}
}
