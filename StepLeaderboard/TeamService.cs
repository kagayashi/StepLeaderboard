using StepLeaderboard.Models;

namespace StepLeaderboard.Services
{
    public static class TeamService
    {
        private static readonly Dictionary<string, Team> teams = new Dictionary<string, Team>();

        public static Team CreateTeam(string name)
        {
            var teamId = Guid.NewGuid().ToString();
            var team = new Team { TeamId = teamId, Name = name };
            teams[teamId] = team;
            return team;
        }

        public static List<Team> GetAllTeams()
        {
            return teams.Values.ToList();
        }

        public static Team GetTeamById(string teamId)
        {
            return teams.ContainsKey(teamId) ? teams[teamId] : null;
        }

        public static bool DeleteTeam(string teamId)
        {
            return teams.Remove(teamId);
        }

        public static Counter AddCounterToTeam(string teamId, string employeeName)
        {
            var team = GetTeamById(teamId);
            if (team == null) return null;

            var counterId = Guid.NewGuid().ToString();
            var counter = new Counter { CounterId = counterId, EmployeeName = employeeName };
            team.Counters.Add(counter);
            return counter;
        }

        public static bool DeleteCounterFromTeam(string teamId, string counterId)
        {
            var team = GetTeamById(teamId);
            if (team == null) return false;

            var counter = team.Counters.FirstOrDefault(c => c.CounterId == counterId);
            if (counter != null)
            {
                team.Counters.Remove(counter);
                return true;
            }
            return false;
        }

        public static bool IncrementCounterSteps(string teamId, string counterId, int increment)
        {
            var team = GetTeamById(teamId);
            if (team == null) return false;

            var counter = team.Counters.FirstOrDefault(c => c.CounterId == counterId);
            if (counter != null)
            {
                counter.IncrementSteps(increment);
                return true;
            }
            return false;
        }
    }
}
