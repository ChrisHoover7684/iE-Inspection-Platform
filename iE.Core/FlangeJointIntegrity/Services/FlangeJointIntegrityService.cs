using iE.Core.FlangeJointIntegrity.Models;

namespace iE.Core.FlangeJointIntegrity.Services
{
    public class FlangeJointIntegrityService
    {
        public FlangeJointReviewResult ReviewGasketDefect(GasketSeatingDefectInput input)
        {
            var result = CreateDefaultResult();

            if (!input.DefectDepthInches.HasValue)
                result.MissingDataWarnings.Add("Defect depth is missing.");

            if (!input.DefectWidthAcrossFaceInches.HasValue)
                result.MissingDataWarnings.Add("Defect width across face is missing.");

            bool crossesMostSeatingFace = input.SeatingWidthInches.HasValue
                && input.DefectWidthAcrossFaceInches.HasValue
                && input.SeatingWidthInches.Value > 0
                && (input.DefectWidthAcrossFaceInches.Value / input.SeatingWidthInches.Value) >= 0.75;

            if (crossesMostSeatingFace || input.DefectLocation == DefectLocation.AcrossSealingFace)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Defect appears to cross most of the seating face; repair is required before assembly.");
                AddRecommendation(result, "Gasket Seating", "Restore or machine the seating surface and re-inspect prior to service.", RecommendationPriority.Critical);
            }

            bool defectOnCriticalSealingSurface = input.DefectLocation is DefectLocation.AcrossSealingFace or DefectLocation.InnerEdge;
            if ((input.GasketType == GasketType.Hard || input.GasketType == GasketType.RTJ) && defectOnCriticalSealingSurface)
            {
                AddStatus(result, FlangeJointOverallStatus.EngineeringReviewRequired, ReviewSeverity.High,
                    "Hard-gasket/RTJ sealing surface defect requires engineering review.");
                AddRecommendation(result, "Engineering", "Obtain engineering disposition for hard-gasket/RTJ surface damage before reuse.", RecommendationPriority.Critical);
            }

            if (input.DefectDepthInches.HasValue)
            {
                double depth = input.DefectDepthInches.Value;
                if (depth > 0.030)
                {
                    AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                        "Defect depth exceeds 0.030 in and should be repaired unless engineering accepts as-is.");
                    AddRecommendation(result, "Gasket Seating", "Repair seating defect or secure documented engineering acceptance.", RecommendationPriority.Critical);
                }
                else if (depth > 0.010)
                {
                    AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium,
                        "Defect depth exceeds 0.010 in; monitor and verify suitability before service.");
                }
                else if (input.GasketType == GasketType.Soft
                         && input.DefectLocation == DefectLocation.Isolated
                         && input.DefectWidthAcrossFaceInches.GetValueOrDefault() <= 0.10)
                {
                    AddStatus(result, FlangeJointOverallStatus.Acceptable, ReviewSeverity.Low,
                        "Minor isolated defect on soft-gasket seating area appears acceptable for screening.");
                }
            }

            if (!result.Findings.Any())
                result.Findings.Add("No significant gasket seating defects identified from provided data.");

            return result;
        }

        public FlangeJointReviewResult ReviewFlangeFace(FlangeFaceConditionInput input)
        {
            var result = CreateDefaultResult();

            if (input.SerrationsCondition is SerrationsCondition.HeavyDamage or SerrationsCondition.Missing)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Severe or missing serrations observed on flange face.");
                AddRecommendation(result, "Flange Face", "Refinish or replace flange face and verify final facing condition.", RecommendationPriority.Critical);
            }

            if (input.WarpageSuspected)
            {
                AddStatus(result, FlangeJointOverallStatus.EngineeringReviewRequired, ReviewSeverity.High,
                    "Warpage suspected; flatness verification and engineering review required.");
                AddRecommendation(result, "Engineering", "Perform dimensional verification and engineering assessment for warpage.", RecommendationPriority.Critical);
            }

            if (input.MechanicalDamageObserved)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Mechanical damage observed on sealing surface.");
                AddRecommendation(result, "Flange Face", "Repair local mechanical damage and confirm gasket seating profile.", RecommendationPriority.Action);
            }

            if (input.FacingType == FacingType.RTJ && (input.MechanicalDamageObserved || input.SerrationsCondition != SerrationsCondition.Good))
            {
                AddStatus(result, FlangeJointOverallStatus.EngineeringReviewRequired, ReviewSeverity.High,
                    "RTJ groove damage indicators present; engineering review required.");
                AddRecommendation(result, "RTJ Groove", "Evaluate RTJ groove geometry and damage limits prior to reuse.", RecommendationPriority.Critical);
            }

            if (input.CorrosionObserved || input.PittingObserved)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium,
                    "Corrosion/pitting observed on flange face.");
                AddRecommendation(result, "Flange Face", "Clean face, perform dimensional review, and evaluate gasket seating condition.", RecommendationPriority.Action);
            }

            if (!result.Findings.Any())
                result.Findings.Add("Flange face condition appears acceptable based on provided screening inputs.");

            return result;
        }

        public FlangeJointReviewResult ReviewBoltAssembly(BoltAssemblyConditionInput input)
        {
            var result = CreateDefaultResult();

            if (!input.BoltMaterialKnown)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium,
                    "Bolt material is unknown.");
                result.MissingDataWarnings.Add("Bolt material traceability is missing.");
                AddRecommendation(result, "Bolting", "Confirm bolt material grade/specification before tightening.", RecommendationPriority.Warning);
            }

            if (input.BoltCondition is AssemblyCondition.Corroded or AssemblyCondition.Damaged)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Bolt condition is unacceptable (corroded/damaged).");
                AddRecommendation(result, "Bolting", "Replace corroded or damaged bolts/studs.", RecommendationPriority.Critical);
            }

            if (input.NutCondition is AssemblyCondition.Corroded or AssemblyCondition.Damaged)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Nut condition is unacceptable (corroded/damaged).");
                AddRecommendation(result, "Bolting", "Replace corroded or damaged nuts.", RecommendationPriority.Critical);
            }

            if (!input.LubricantUsed || !input.LubricantKnown)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium,
                    "Lubrication practice is incomplete or unknown.");
                AddRecommendation(result, "Assembly", "Use known lubricant compatible with procedure and apply consistently.", RecommendationPriority.Warning);
            }

            if (input.ThreadDamageObserved)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Thread damage observed on bolting components.");
                AddRecommendation(result, "Bolting", "Replace threaded components with observed thread damage.", RecommendationPriority.Critical);
            }

            if (!input.StudsProtrudeBeyondNuts)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.Medium,
                    "Stud protrusion beyond nuts is insufficient.");
                AddRecommendation(result, "Assembly", "Correct stud engagement/protrusion before final torque.", RecommendationPriority.Action);
            }

            if (!input.WasherUsed)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Low,
                    "Washers not indicated; verify assembly procedure requirements.");
            }

            if (!result.Findings.Any())
                result.Findings.Add("Bolt/nut/washer condition appears acceptable for screening.");

            return result;
        }

        public FlangeJointReviewResult ReviewAssemblyChecklist(FlangeAssemblyChecklistInput input)
        {
            var result = CreateDefaultResult();

            EvaluateChecklistFlag(result, input.GasketCentered, "Gasket centering not verified.", "Verify gasket is properly centered before tightening.");
            EvaluateChecklistFlag(result, input.CrossPatternUsed, "Cross-pattern tightening not confirmed.", "Apply cross-pattern tightening sequence.");
            EvaluateChecklistFlag(result, input.MultiPassTighteningUsed, "Multi-pass tightening not confirmed.", "Use controlled multi-pass tightening.");
            EvaluateChecklistFlag(result, input.FinalTorqueCheckComplete, "Final torque check is incomplete.", "Complete final torque verification before service.");

            if (!input.FlangeFacesCleaned)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium, "Flange faces were not confirmed clean.");
                AddRecommendation(result, "Preparation", "Clean flange faces before gasket placement.", RecommendationPriority.Action);
            }

            if (!input.GasketNew)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium, "Gasket is not confirmed new.");
                AddRecommendation(result, "Gasket", "Install new gasket unless procedure explicitly permits reuse.", RecommendationPriority.Warning);
            }

            if (input.LeakTestRequired && !input.LeakTestComplete)
            {
                AddStatus(result, FlangeJointOverallStatus.RepairRequired, ReviewSeverity.High,
                    "Required leak test is not complete.");
                AddRecommendation(result, "Testing", "Complete required leak test prior to return to service.", RecommendationPriority.Critical);
            }

            if (!result.Findings.Any())
                result.Findings.Add("Assembly checklist inputs indicate acceptable completion status.");

            return result;
        }

        public FlangeJointReviewResult ReviewJoint(FlangeJointInput input)
        {
            var aggregate = CreateDefaultResult();

            if (input.GasketDefect is not null)
                Merge(aggregate, ReviewGasketDefect(input.GasketDefect));
            else
                aggregate.MissingDataWarnings.Add("Gasket defect screening input is missing.");

            if (input.FlangeFace is not null)
                Merge(aggregate, ReviewFlangeFace(input.FlangeFace));
            else
                aggregate.MissingDataWarnings.Add("Flange face condition input is missing.");

            if (input.BoltAssembly is not null)
                Merge(aggregate, ReviewBoltAssembly(input.BoltAssembly));
            else
                aggregate.MissingDataWarnings.Add("Bolt assembly condition input is missing.");

            if (input.Checklist is not null)
                Merge(aggregate, ReviewAssemblyChecklist(input.Checklist));
            else
                aggregate.MissingDataWarnings.Add("Assembly checklist input is missing.");

            AppendBasicBoltStressScreening(aggregate, input);

            if (!aggregate.Findings.Any())
                aggregate.Findings.Add("Joint review contains no screening findings from supplied inputs.");

            aggregate.Findings = aggregate.Findings.Distinct().ToList();
            aggregate.MissingDataWarnings = aggregate.MissingDataWarnings.Distinct().ToList();
            aggregate.Recommendations = aggregate.Recommendations
                .GroupBy(r => new { r.Category, r.Action, r.Priority })
                .Select(g => g.First())
                .ToList();

            return aggregate;
        }

        private void AppendBasicBoltStressScreening(FlangeJointReviewResult result, FlangeJointInput input)
        {
            if (!input.TargetTorqueFtLbs.HasValue && !input.NutFactorK.HasValue && !input.BoltNominalDiameterInches.HasValue && !input.TensileStressAreaIn2.HasValue)
                return;

            if (!input.TargetTorqueFtLbs.HasValue || !input.NutFactorK.HasValue || !input.BoltNominalDiameterInches.HasValue || !input.TensileStressAreaIn2.HasValue)
            {
                result.MissingDataWarnings.Add("Basic torque/stress screening was requested but one or more required fields are missing.");
                return;
            }

            if (input.NutFactorK <= 0 || input.BoltNominalDiameterInches <= 0 || input.TensileStressAreaIn2 <= 0)
            {
                result.MissingDataWarnings.Add("Basic torque/stress screening inputs must be greater than zero.");
                return;
            }

            // T = K * F * d => F = T / (K * d)
            var preload = (input.TargetTorqueFtLbs.Value * 12.0) / (input.NutFactorK.Value * input.BoltNominalDiameterInches.Value);
            var boltStressPsi = preload / input.TensileStressAreaIn2.Value;

            result.Findings.Add($"Estimated bolt preload from torque input: {preload:N0} lbf.");
            result.Findings.Add($"Estimated bolt stress from torque input: {boltStressPsi:N0} psi.");
            AddRecommendation(result, "Bolting", "Validate torque/preload assumptions against approved procedure and bolt material allowables.", RecommendationPriority.Warning);
        }

        private static FlangeJointReviewResult CreateDefaultResult()
        {
            return new FlangeJointReviewResult();
        }

        private static void Merge(FlangeJointReviewResult target, FlangeJointReviewResult source)
        {
            AddStatus(target, source.OverallStatus, source.Severity, null);
            target.Findings.AddRange(source.Findings);
            target.MissingDataWarnings.AddRange(source.MissingDataWarnings);
            target.Recommendations.AddRange(source.Recommendations);
        }

        private static void AddStatus(FlangeJointReviewResult result, FlangeJointOverallStatus status, ReviewSeverity severity, string? finding)
        {
            if (RankStatus(status) > RankStatus(result.OverallStatus))
                result.OverallStatus = status;

            if (RankSeverity(severity) > RankSeverity(result.Severity))
                result.Severity = severity;

            if (!string.IsNullOrWhiteSpace(finding))
                result.Findings.Add(finding);
        }

        private static void AddRecommendation(FlangeJointReviewResult result, string category, string action, RecommendationPriority priority)
        {
            result.Recommendations.Add(new FlangeJointRecommendation
            {
                Category = category,
                Action = action,
                Priority = priority
            });
        }

        private static void EvaluateChecklistFlag(FlangeJointReviewResult result, bool conditionMet, string finding, string action)
        {
            if (!conditionMet)
            {
                AddStatus(result, FlangeJointOverallStatus.Monitor, ReviewSeverity.Medium, finding);
                AddRecommendation(result, "Checklist", action, RecommendationPriority.Warning);
            }
        }

        private static int RankStatus(FlangeJointOverallStatus status)
        {
            return status switch
            {
                FlangeJointOverallStatus.Acceptable => 0,
                FlangeJointOverallStatus.Monitor => 1,
                FlangeJointOverallStatus.RepairRequired => 2,
                FlangeJointOverallStatus.EngineeringReviewRequired => 3,
                _ => 0
            };
        }

        private static int RankSeverity(ReviewSeverity severity)
        {
            return severity switch
            {
                ReviewSeverity.Low => 0,
                ReviewSeverity.Medium => 1,
                ReviewSeverity.High => 2,
                _ => 0
            };
        }
    }
}
