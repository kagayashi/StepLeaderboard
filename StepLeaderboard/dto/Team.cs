namespace StepLeaderboard.Models
{
    public class Team
    {
        public required string TeamId { get; set; }
        public required string Name { get; set; }
        public List<Counter> Counters { get; set; } = new List<Counter>();

        public int GetTotalSteps()
        {
            int totalSteps = 0;
            foreach (var counter in Counters)
            {
                totalSteps += counter.Steps;
            }
            return totalSteps;
        }
    }
}
