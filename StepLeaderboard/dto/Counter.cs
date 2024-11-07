namespace StepLeaderboard.Models
{
    public class Counter
    {
        public required string CounterId { get; set; }
        public required string EmployeeName { get; set; }
        public int Steps { get; set; }

        public void IncrementSteps(int increment)
        {
            Steps += increment;
        }
    }
}
