namespace JuanMartin.Models.Euler
{
    public class Result
    {
        public const string INCORRECT_RESULT = "INCORRECT";
        public int Id { get; }
        public double Duration { get; set; }
        public string Answer { get; set; }
        public string Message { get; }
        public bool IsCorrect { get; } = false;
        public Result(int id, string message)
        {
            Id = id;
            Duration = -1;
            Message = message;
			if (message.Contains(INCORRECT_RESULT))
				IsCorrect = true;
		}
		public Result() : this(0, string.Empty)
		{
			Duration = -1;
		}
		public Result(int id, double duration, string message):this(id,message)
        {
            Duration = duration;
        }
    }
}