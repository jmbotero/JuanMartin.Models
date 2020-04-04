namespace JuanMartin.Models
{
    public class Result
    {
        public int Id { get; }
        public double Duration { get; set; }
        public string Answer { get; set; }
        public string Message { get; }

        public Result()
        {
            Id = 0;
            Duration = -1;
            Message = string.Empty;
        }
        public Result(int id, string message)
        {
            Id = id;
            Duration = -1;
            Message = message;
        }
        public Result(int id, double duration, string message)
        {
            Id = id;
            Duration = duration;
            Message = message;
        }
    }
}