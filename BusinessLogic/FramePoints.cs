namespace BusinessLogic
{
	public class FramePoints
	{
		public FramePoints()
		{

		}
		public FramePoints(int firstRun, int secondRun = 0, int thirdRun = 0, int fourthRun = 0)
		{
			FirstRun = firstRun;
			SecondRun = secondRun;
			ThirdRun = thirdRun;
			FourthRun = fourthRun;
		}

		public int FirstRun { get; set; }
		public int SecondRun { get; set; }
		public int ThirdRun { get; }
		public int FourthRun { get; }
	}
}