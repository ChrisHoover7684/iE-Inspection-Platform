namespace iE.Core.FlangeJointIntegrity.Models
{
    public class FlangeJointRecommendation
    {
        public string Category { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public RecommendationPriority Priority { get; set; } = RecommendationPriority.Info;
    }
}
