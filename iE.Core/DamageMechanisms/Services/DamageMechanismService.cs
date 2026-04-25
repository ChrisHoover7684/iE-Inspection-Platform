using System.Linq;
using iE.Core.DamageMechanisms.Models;
using iE.Core.DamageMechanisms.Repositories;

namespace iE.Core.DamageMechanisms.Services
{
    public class DamageMechanismService
    {
        public List<DamageMechanism> Search(string? query)
        {
            return DamageMechanismRepository.SearchMechanisms(query ?? "").ToList();
        }

        public DamageMechanism? GetByName(string name)
        {
            return DamageMechanismRepository.GetMechanism(name);
        }

        public object GetStats()
        {
            var all = DamageMechanismRepository.SearchMechanisms("");

            return new
            {
                totalMechanisms = all.Count,
                riskLevels = all
                    .GroupBy(x => x.RiskLevel ?? "Unknown")
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }
    }
}