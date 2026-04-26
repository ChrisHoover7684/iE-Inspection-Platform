using System.Collections.Generic;

namespace iE.Core.FlangeJointIntegrity.Models
{
    public class FlangeJointReviewResult
    {
        public FlangeJointOverallStatus OverallStatus { get; set; } = FlangeJointOverallStatus.Acceptable;
        public ReviewSeverity Severity { get; set; } = ReviewSeverity.Low;
        public List<string> Findings { get; set; } = new();
        public List<string> MissingDataWarnings { get; set; } = new();
        public List<FlangeJointRecommendation> Recommendations { get; set; } = new();
        public string Disclaimer { get; set; } =
            "Conservative screening only. Final acceptance shall be verified against ASME PCC-1 Appendix D and owner/user requirements.";
    }
}
